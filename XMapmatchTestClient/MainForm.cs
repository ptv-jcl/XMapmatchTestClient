using Ptv.Components.Projections;
using Ptv.XServer.Controls.Map;
using Ptv.XServer.Controls.Map.Layers.Shapes;
using Ptv.XServer.Controls.Map.Layers.Tiled;
using Ptv.XServer.Controls.Map.Symbols;
using Ptv.XServer.Controls.Map.TileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.XPath;
using XServer;

namespace XMapmatchTestClient
{
    public partial class MainForm : Form
    {
        private const string appName = "XMapmatch test client";
        private string inputFileName = "";

        private const string hereStatelliteUrl = "http://{0}.aerial.maps.api.here.com/maptile/2.1/maptile/newest/satellite.day/{3}/{1}/{2}/256/png8?app_id={app_id}&app_code={app_code}";

        private BindingList<DataPoint> dataPointList;
        private ShapeLayer inputPointLayer, outputPointLayer, routeLayer, pathLayer;
        private ExtendedRoute extendedRoute;

        // line for visualizing segment a matched location can be found on
        private MapPolyline selectedSegmentLine = new MapPolyline()
        {
            Stroke = new SolidColorBrush(Colors.Red),
            MapStrokeThickness = 10,
            //Opacity = .65,
            ScaleFactor = 0.2,
            Points = new PointCollection(),
        };

        private DateTime loadTime;

        public MainForm()
        {
            InitializeComponent();

            // dirty hack to load in authors personal credentials from his local machine for debug testing
            if (File.Exists(@"d:\xservers source\private.txt"))
            {
                using (var reader = new StreamReader(@"d:\xservers source\private.txt"))
                {
                    Properties.Settings.Default.Token = reader.ReadLine();
                    Properties.Settings.Default.HereAppId = reader.ReadLine();
                    Properties.Settings.Default.HereAppCode = reader.ReadLine();
                }
            }

            xMapmatchProfileTextBox.Text = Properties.Settings.Default.XMapmatchProfile;
            xMapmatchURLTextBox.Text = Properties.Settings.Default.XMapmatchUrl;
            xMapProfileTextBox.Text = Properties.Settings.Default.XMapProfile;
            xMapURLTextBox.Text = Properties.Settings.Default.XMapUrl;
            xRouteProfileTextBox.Text = Properties.Settings.Default.XRouteProfile;
            xRouteURLTextBox.Text = Properties.Settings.Default.XRouteUrl;
            xLocateProfileTextBox.Text = Properties.Settings.Default.XLocateProfile;
            xLocateURLTextBox.Text = Properties.Settings.Default.XLocateUrl;

            loadTime = DateTime.Now;
            dataPointList = new BindingList<DataPoint>();
            dataPointGridView.DataSource = dataPointList;

            mapView.XMapUrl = xMapURLTextBox.Text;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Token))
            {
                mapView.XMapCredentials = "xtok:" + Properties.Settings.Default.Token;
            }
            if (!(string.IsNullOrEmpty(Properties.Settings.Default.HereAppCode) || string.IsNullOrEmpty(Properties.Settings.Default.HereAppId)))
            {
                var idx = mapView.Layers.IndexOf(mapView.Layers["Background"]);

                var hereUrl = hereStatelliteUrl.Replace("{app_id}", Properties.Settings.Default.HereAppId).Replace("{app_code}", Properties.Settings.Default.HereAppCode);

                var hereSatelliteLayer = new TiledLayer("HERE_Satellite")
                {
                    TiledProvider = new RemoteTiledProvider()
                    {
                        MinZoom = 0,
                        MaxZoom = 20,
                        RequestBuilderDelegate = (x, y, z) => String.Format(hereUrl, (x + y) % 4 + 1, x, y, z)
                    },
                    IsBaseMapLayer = true,
                    Opacity = .8,
                    Copyright = "© HERE",
                    Caption = "Here Satellite",
                };
                mapView.Layers.Insert(idx, hereSatelliteLayer);

                mapView.Layers["Background"].Opacity = 0.5;
                //((XMapTiledProvider)((TiledLayer)mapView.Layers["Background"]).TiledProvider).CustomXMapLayers = new xserver.Layer[]
                //{
                //    new xserver.StaticLayer() { category = -1, name="background", visible= false, },
                //};
            }

            pathLayer = new ShapeLayer("PathLayer") { SpatialReferenceId = CoordinateReferenceSystem.XServer.OG_GEODECIMAL.Id };
            mapView.Layers.Add(pathLayer);
            routeLayer = new ShapeLayer("RouteLayer") { SpatialReferenceId = CoordinateReferenceSystem.XServer.OG_GEODECIMAL.Id };
            mapView.Layers.Add(routeLayer);
            inputPointLayer = new ShapeLayer("InputPointLayer") { SpatialReferenceId = CoordinateReferenceSystem.XServer.OG_GEODECIMAL.Id };
            mapView.Layers.Add(inputPointLayer);
            outputPointLayer = new ShapeLayer("OutputPointLayer") { SpatialReferenceId = CoordinateReferenceSystem.XServer.OG_GEODECIMAL.Id };
            mapView.Layers.Add(outputPointLayer);

            mapView.CoordinateDiplayFormat = CoordinateDiplayFormat.Decimal;
            mapView.ShowCoordinates = true;

            // var drawControl = new Ptv.XServer.Controls.Custom.DrawControl(Ptv.XServer.Controls.Map.Tools.MapElementExtensions.FindChild<Ptv.XServer.Controls.Map.MapView>(mapView.WrappedMap));
            //Ptv.XServer.Controls.Map.Layers.XMapLayerFactory.UpdateXMapProfiles(mapView.Layers, xMapProfileTextBox.Text, xMapProfileTextBox.Text);

            mainFormToolTip.IsBalloon = true;
            mainFormToolTip.AutoPopDelay = int.MaxValue;
            mainFormToolTip.SetToolTip(csvHeaderChckBx, "Check if the first line are column discriptions, uncheck if the first line is a data.");
            mainFormToolTip.SetToolTip(idPresentChckBx, "Check if the first column contains record ID's, uncheck if it doesn't. Note that the program will assume the CSV file is build up like <lat>;<lon>;<speed>;<heading>;<timestamp> .");
            mainFormToolTip.SetToolTip(commaSeparatorChckBx, "Check if you are using a ',' as cell separator, uncheck if you are using ';' .");
            mainFormToolTip.SetToolTip(decimalPointChckBx, "Check if you are using '.' as decimal separator, uncheck if you are using ',' .");
            mainFormToolTip.SetToolTip(recalculateHeadingChkBx, "Check if you don't trust you own heading. The program will calculate the angle from current record to next record and use this a heading. Note that heading still has to be present in the CSV file. Uncheck if your heading is fine.");
            mainFormToolTip.SetToolTip(makeOwnTimestampChkBx, "Check if you have problems getting your timestamps parsed or do not have realistic timestamps. The programm will cacluate new timestamps based on the airline distance and the average speed between the points.");
            mainFormToolTip.SetToolTip(emissionsChkBx, "Check if you want to view the emmisions of the entire route. HBEFA 2.1 calculation is used. Note that emissions has to be enable in the xRoute configuration and license file.");
            mainFormToolTip.SetToolTip(tollChckBx, "Check if you want to view the toll costs associated with the route xRoute calculates");
            mainFormToolTip.SetToolTip(correctTimestampCheckbox, "Check if you want to make sure that timestamps are increasing. In case of a double timestamp a single milisecond will be added. If unchecked double timestamps are ignored.");

            xMapProfileTextBox_Leave(null, null);

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ProxyUri))
                WebRequest.DefaultWebProxy = new WebProxy(new Uri(Properties.Settings.Default.ProxyUri));
        }

        private void loadDataBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    //MessageBox.Show("Loading data aborted.");
                    return;
                }

                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                dataPointList.Clear();
                outputPointLayer.Shapes.Clear();
                routeLayer.Shapes.Clear();
                pathLayer.Shapes.Clear();

                var tempDataPointList = new BindingList<DataPoint>();

                int lineCount = 0;
                string line;
                string[] split;
                long id;
                bool cancel = false;
                DateTime timeStamp = DateTime.Now;
                int counter = 1;
                if (csvHeaderChckBx.Checked)
                    streamReader.ReadLine();

                while (true)
                {
                    if (cancel)
                    {
                        break;
                    }
                    line = streamReader.ReadLine();
                    if (line == null) break;
                    lineCount++;
                    if (commaSeparatorChckBx.Checked)
                        line = line.Replace(',', ';');
                    if (decimalPointChckBx.Checked)
                        line = line.Replace('.', ',');
                    split = line.Split(';');
                    int i = 0;
                    if (idPresentChckBx.Checked)
                    {
                        if (!long.TryParse(split[i++], out id))
                        {
                            if (MessageBox.Show("Error on line " + lineCount.ToString() + " with parsing Id", "Import error", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                                cancel = true;
                            continue;
                        }
                    }
                    else
                        id = counter++;
                    if (!double.TryParse(split[i++], out double lat))
                    {
                        if (MessageBox.Show("Error on line " + lineCount.ToString() + " with parsing Lat", "Import error", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                            cancel = true;
                        continue;
                    }
                    if (!double.TryParse(split[i++], out double lon))
                    {
                        if (MessageBox.Show("Error on line " + lineCount.ToString() + " with parsing Lon", "Import error", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                            cancel = true;
                        continue;
                    }
                    if (!float.TryParse(split[i++], out float speed))
                    {
                        if (split[i - 1] == "")
                            speed = 0;
                        else
                        {
                            if (MessageBox.Show("Error on line " + lineCount.ToString() + " with parsing Speed", "Import error", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                                cancel = true;
                            continue;
                        }
                    }
                    if (!float.TryParse(split[i++], out float heading))
                    {
                        if (split[i - 1] == "")
                            heading = 0;
                        else
                        {
                            if (MessageBox.Show("Error on line " + lineCount.ToString() + " with parsing Heading", "Import error", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                                cancel = true;
                            continue;
                        }
                    }
                    if (makeOwnTimestampChkBx.Checked)
                    {
                        timeStamp = loadTime.AddSeconds(10 * lineCount);
                    }
                    else
                    {
                        if (!DateTime.TryParse(split[i++], out timeStamp))
                        {
                            if (long.TryParse(split[i - 1], out long possibleUtcTime))
                                timeStamp = new DateTime(possibleUtcTime);
                            else
                            {
                                if (MessageBox.Show("Error on line " + lineCount.ToString() + " with parsing TimeStamp", "Import error", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                                    cancel = true;
                                continue;
                            }
                        }
                    }
                    DataPoint dataPoint = new DataPoint(id, lat, lon, speed, heading, timeStamp);
                    tempDataPointList.Add(dataPoint);
                }
                if (cancel)
                    return;

                if (recalculateHeadingChkBx.Checked)
                {
                    for (int i = 0; i < tempDataPointList.Count - 1; i++)
                    {
                        tempDataPointList[i].SetHeading(CalculateHeading(tempDataPointList[i], tempDataPointList[i + 1]));
                    }
                    tempDataPointList.Last().SetHeading(tempDataPointList[tempDataPointList.Count - 2].Heading);
                }

                if (filterStandStillCheckBox.Checked)
                {
                    var oldDataPointList = tempDataPointList.ToList();

                    for (int i = 1; i < oldDataPointList.Count; i++)
                    {
                        if (oldDataPointList[i].SpeedInMps == 0 && oldDataPointList[i - 1].SpeedInMps == 0)
                            tempDataPointList.Remove(oldDataPointList[i]);
                    }
                }

                for (int i = 1; i < tempDataPointList.Count;)
                {
                    if (tempDataPointList[i].Timestamp == tempDataPointList[i - 1].Timestamp)
                        if (correctTimestampCheckbox.Checked)
                        {
                            tempDataPointList[i].TrackPosition.timestamp = tempDataPointList[i].Timestamp.AddMilliseconds(1);
                            i++;
                        }
                        else
                            tempDataPointList.RemoveAt(i);
                    else
                        i++;
                }

                dataPointList = tempDataPointList;
                if (makeOwnTimestampChkBx.Checked) RedoTimeStamps();

                dataPointGridView.DataSource = dataPointBindingSource;
                dataPointBindingSource.DataSource = dataPointList;
                dataPointGridView.Invalidate();
                streamReader.Close();
                inputFileName = openFileDialog.FileName;
                UpdateFormTitle();
                populateInputPointLayer();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString());
            }
        }

        private void UpdateFormTitle()
        {
            this.Text = appName + " - " + inputFileName + " - " + dataPointList.Count().ToString() + " - " + dataPointList.Where(d => d.MatchedLocation != null).Count().ToString();
        }

        private void loadRequestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                dataPointList.Clear();
                outputPointLayer.Shapes.Clear();
                routeLayer.Shapes.Clear();
                pathLayer.Shapes.Clear();

                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.Load(openFileDialog.FileName);
                XPathNavigator navigator = xmlDocument.CreateNavigator();

                navigator.MoveToChild("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                navigator.MoveToChild("Body", "http://schemas.xmlsoap.org/soap/envelope/");
                navigator.MoveToChild("matchTrack", "http://types.xmapmatch.xserver.ptvag.com");
                navigator.MoveToChild("matchTrackExtended", "http://types.xmapmatch.xserver.ptvag.com"); // dirty hack so I don't have to bother figuring out which method is bing called
                navigator.MoveToChild("matchPositions", "http://types.xmapmatch.xserver.ptvag.com"); // dirty hack so I don't have to bother figuring out which method is bing called
                navigator.MoveToChild("ArrayOfTrackPosition_1", "http://types.xmapmatch.xserver.ptvag.com");
                // should on the trackposition array
                navigator.MoveToFirstChild();

                dataPointList.Clear();
                var dataPoints = new List<DataPoint>();
                do
                {
                    if (navigator.NodeType == XPathNodeType.Comment) continue;
                    double lat = 0, lon = 0;
                    navigator.MoveToAttribute("id", "");
                    long id = navigator.ValueAsLong;
                    navigator.MoveToParent();
                    if (string.IsNullOrEmpty(navigator.GetAttribute("lat", "")))
                    {
                        navigator.MoveToChild("coordinate", "http://xmapmatch.xserver.ptvag.com");
                        navigator.MoveToChild("point", "http://common.xserver.ptvag.com");
                        navigator.MoveToAttribute("y", "");
                        lat = navigator.ValueAsDouble;
                        navigator.MoveToParent();
                        navigator.MoveToAttribute("x", "");
                        lon = navigator.ValueAsDouble;
                        navigator.MoveToParent();
                        navigator.MoveToParent();
                        navigator.MoveToParent();
                    }
                    else
                    {
                        navigator.MoveToAttribute("lat", "");
                        lat = navigator.ValueAsDouble;
                        navigator.MoveToParent();
                        navigator.MoveToAttribute("lon", "");
                        lon = navigator.ValueAsDouble;
                        navigator.MoveToParent();
                    }
                    navigator.MoveToAttribute("speedInMps", "");
                    float speed = (float)navigator.ValueAsDouble;
                    navigator.MoveToParent();
                    navigator.MoveToAttribute("heading", "");
                    float heading = (float)navigator.ValueAsDouble;
                    navigator.MoveToParent();
                    navigator.MoveToAttribute("timestamp", "");
                    DateTime timestamp = navigator.ValueAsDateTime;
                    navigator.MoveToParent();

                    DataPoint dataPoint = new DataPoint(id, lat, lon, speed, heading, timestamp);

                    dataPoints.Add(dataPoint);
                }
                while (navigator.MoveToNext());
                navigator.MoveToParent();
                navigator.MoveToParent();

                if (navigator.MoveToChild("CallerContext_3", "http://types.xmapmatch.xserver.ptvag.com"))
                {
                    if (navigator.MoveToChild("wrappedProperties", "http://baseservices.service.jabba.ptvag.com"))
                    {
                        navigator.MoveToFirstChild();
                        do
                        {
                            if (navigator.GetAttribute("key", "").ToLower() == "profile")
                                xMapmatchProfileTextBox.Text = navigator.GetAttribute("value", "");
                            if (navigator.GetAttribute("key", "").ToLower() == "coordformat")
                            {
                                switch (navigator.GetAttribute("value", ""))
                                {
                                    case "OG_GEODECIMAL":
                                        break;

                                    case "PTV_GEODECIMAL":
                                        foreach (var dataPoint in dataPoints)
                                        {
                                            dataPoint.TrackPosition.lat = dataPoint.TrackPosition.lat / 100000;
                                            dataPoint.TrackPosition.lon = dataPoint.TrackPosition.lon / 100000;
                                        }
                                        break;
                                    default:
                                        break;
                                        //throw new NotSupportedException("Only OG_GEODEICMAL and PTV_GEODECIMAL are currently support by this tool.");
                                }
                            }
                        }
                        while (navigator.MoveToNext());
                    }
                }

                if (filterDoubleCheckBox.Checked)
                {
                    var unfilteredDataPoints = dataPoints;
                    dataPoints = new List<DataPoint>();
                    if (unfilteredDataPoints.Count > 0)
                    {
                        dataPoints.Add(unfilteredDataPoints[0]);
                        for (int i = 1; i < unfilteredDataPoints.Count; i++)
                        {
                            if (!unfilteredDataPoints[i].HasEqualValuesAs(unfilteredDataPoints[i - 1]))
                            {
                                dataPoints.Add(unfilteredDataPoints[i]);
                            }
                        }
                    }
                }

                foreach (var dataPoint in dataPoints)
                {
                    dataPointList.Add(dataPoint);
                }

                if (makeOwnTimestampChkBx.Checked) RedoTimeStamps();
                populateInputPointLayer();
                UpdateFormTitle();
                inputFileName = openFileDialog.FileName;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString());
            }
        }

        private void populateInputPointLayer()
        {
            inputPointLayer.Shapes.Clear();

            foreach (DataPoint dataPoint in dataPointList)
            {
                var triangle = new TriangleUp()
                {
                    Color = dataPoint.SpeedInMps > 4 ? Colors.Blue : Colors.HotPink,
                    Stroke = Colors.Black,
                    ToolTip = dataPoint.Id.ToString(),
                    RenderTransform = new RotateTransform(dataPoint.Heading),
                    RenderTransformOrigin = new System.Windows.Point(0.5, 0.5),
                    Width = 10,
                    Height = 15,
                };

                var triangleContainer = new Grid();
                triangleContainer.Children.Add(triangle);

                ShapeCanvas.SetLocation(triangleContainer, new System.Windows.Point(dataPoint.LonInput, dataPoint.LatInput));
                ShapeCanvas.SetAnchor(triangleContainer, LocationAnchor.Center);
                inputPointLayer.Shapes.Add(triangleContainer);
            }
        }

        private void matchDataBtn_Click(object sender, EventArgs e)
        {
            var previourCursor = Cursor;
            Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                outputPointLayer.Shapes.Clear();
                routeLayer.Shapes.Clear();
                pathLayer.Shapes.Clear();
                dataPointGridView.DataSource = null;

                extendedRoute = null; //always clear the route for the fix local function

                foreach (var dataPoint in dataPointList)
                {
                    dataPoint.MatchedLocation = null;
                }

                #region xmapmatch_action

                MatchResult matchResult = null;
                using (var xMapmatchClient = new XServer.XMapmatchWSService())
                {
                    xMapmatchClient.Url = xMapmatchURLTextBox.Text;
                    xMapmatchClient.Timeout = 600000000 + 10000; // xmapmatxch timeout + 10 seconds overhead for communication
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.Token))
                    {
                        xMapmatchClient.Credentials = new System.Net.NetworkCredential("xtok", Properties.Settings.Default.Token);
                    }

                    var trackPositions = dataPointList.Select(dp => dp.TrackPosition).ToArray();
                    //trackPositions = trackPositions.Where(tp => tp.speedInMps > 0.2).ToArray();

                    var resultListOptions = new ResultListOptions1() { polyline = true, polylineSpecified = true, vendorId = true, vendorIdSpecified = true };

                    CallerContext cc = new CallerContext()
                    {
                        wrappedProperties = new CallerContextProperty[]
                        {
                            new CallerContextProperty() { key = "Profile", value = xMapmatchProfileTextBox.Text, },
                            new CallerContextProperty() { key = "CoordFormat", value = CoordFormat.OG_GEODECIMAL.ToString(), },
                        },
                        log1 = Path.GetFileName(inputFileName),
                        log2 = trackPositions.Length.ToString(),
                    };

                    matchResult = xMapmatchClient.matchPositions(trackPositions, resultListOptions, cc);
                }

                xMapmatchDistanceTxtBx.Text = ((int)(matchResult.overallLength / 1000)).ToString() + " km";

                if (matchResult.wrappedMatchedLocations.Length == 0)
                {
                    return;
                }

                #region paint the result

                foreach (MatchedLocation matchedLocation in matchResult.wrappedMatchedLocations)
                {
                    var dataPoint = dataPointList.First(dp => dp.Id == matchedLocation.inputId);
                    dataPoint.MatchedLocation = matchedLocation;

                    var limit = Math.Max(matchedLocation.wrappedPath.Last().speedLimitForward, matchedLocation.wrappedPath.Last().speedLimitBackward);

                    var ball = new TriangleUp()
                    {
                        ToolTip = dataPoint.Id.ToString() + "\n" + dataPoint.SpeedInKmph.ToString() + " - " + Math.Max(dataPoint.SpeedLimitBackward, dataPoint.SpeedLimitForward).ToString(),
                        RenderTransform = new RotateTransform(dataPoint.MatchedLocation.heading),
                        RenderTransformOrigin = new System.Windows.Point(0.5, 0.5),
                        Width = 10,
                        Height = 15,
                        Stroke = Colors.Black,
                    };
                    if (limit == 0)
                        ball.Color = Colors.WhiteSmoke;
                    else
                    {
                        if (dataPoint.SpeedInKmph <= limit)
                            ball.Color = Colors.SpringGreen;
                        if (dataPoint.SpeedInKmph > limit && dataPoint.SpeedInKmph <= limit + 5)
                            ball.Color = Colors.Orange;
                        if (dataPoint.SpeedInKmph > limit + 5)
                            ball.Color = Colors.Crimson;
                    }
                    var ballContainer = new Grid();
                    ballContainer.Children.Add(ball);

                    ShapeCanvas.SetAnchor(ballContainer, LocationAnchor.Center);
                    ShapeCanvas.SetLocation(ballContainer, new System.Windows.Point(dataPoint.LonOutput, dataPoint.LatOutput));
                    outputPointLayer.Shapes.Add(ballContainer);

                    foreach (var matchedSegment in matchedLocation.wrappedPath)
                    {
                        MapPolyline mapPolyLine = new MapPolyline()
                        {
                            Stroke = new SolidColorBrush(Colors.DodgerBlue),
                            MapStrokeThickness = 16,
                            Opacity = .8,
                            ScaleFactor = 0.2
                        };
                        mapPolyLine.Points = new PointCollection();
                        foreach (XServer.Point xserverPoint in matchedSegment.wrappedPolyline)
                        {
                            System.Windows.Point windowsPoint = new System.Windows.Point()
                            {
                                X = xserverPoint.point.x,
                                Y = xserverPoint.point.y,
                            };
                            mapPolyLine.Points.Add(windowsPoint);
                        }
                        pathLayer.Shapes.Add(mapPolyLine);
                    }
                }

                pathLayer.Shapes.Add(selectedSegmentLine);

                #endregion paint the result

                #endregion xmapmatch_action

                #region xLocate_action

                if (xLocateCheckBox.Checked)
                {
                    XLocateWSService xLocateClient = new XLocateWSService()
                    {
                        Url = xLocateURLTextBox.Text,
                    };
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.Token))
                    {
                        xLocateClient.Credentials = new System.Net.NetworkCredential("xtok", Properties.Settings.Default.Token);
                    }

                    XServer.Location location = new XServer.Location()
                    {
                        coordinate = new XServer.Point()
                        {
                            point = new PlainPoint(),
                        },
                    };

                    SearchOptionBase[] searchOptions = new SearchOptionBase[]
                    {
                        new ReverseSearchOption() { param = ReverseSearchParameter.ENGINE_FILTERMODE, value = "0"},
                        new ReverseSearchOption() { param = ReverseSearchParameter.ENGINE_TARGETSIZE, value = "10"},
                        new ReverseSearchOption() { param = ReverseSearchParameter.ENGINE_TOLERANCE, value = "100"},
                        new ReverseSearchOption() { param = ReverseSearchParameter.ENGINE_SEARCHRANGE, value = "50"},
                    };

                    ResultField[] resultFields = new ResultField[]
                    {
                        ResultField.XYN,
                        ResultField.SEGMENT_ID,
                    };

                    CallerContext xLocateCallerContext = new CallerContext()
                    {
                        wrappedProperties = new CallerContextProperty[]
                        {
                            new CallerContextProperty() { key = "CoordFormat", value = CoordFormat.OG_GEODECIMAL.ToString(),},
                            new CallerContextProperty() { key = "Profile", value = xLocateProfileTextBox.Text,},
                        },
                    };
                    AddressResponse response = null;

                    foreach (DataPoint dataPoint in dataPointList)
                    {
                        if (dataPoint.LatOutput == 0 && dataPoint.LonOutput == 0) continue;

                        location.coordinate.point.x = dataPoint.LonOutput;
                        location.coordinate.point.y = dataPoint.LatOutput;

                        try
                        {
                            response = xLocateClient.findLocation(location, searchOptions, null, resultFields, xLocateCallerContext);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        foreach (ResultAddress resultAddress in response.wrappedResultList)
                        {
                            // comparing XYN of xMapmatch and xLocate to see if we have the correct segment
                            // unfortunatly coordinates at tile borders have 2 possibilities for representation and
                            // xLocate and xMapmatch are not consitent, converting to world coordinates before comparing
                            var segment = dataPoint.MatchedLocation.wrappedPath.Last();

                            string[] xynStrings = resultAddress.wrappedAdditionalFields[0].value.Split('(')[1].Split(')')[0].Replace(",", "").Split(' ');
                            long[] xyn = xynStrings.Select(x => long.Parse(x)).ToArray();

                            var mapmatchPlainPoints = new PlainPoint[]
                            {
                            new PlainPoint()
                            {
                                x = ((segment.tileId >> 16) & 0xffff) * 254 + segment.wrappedStartXYN[0],
                                y = ((segment.tileId ) & 0xffff) * 254 + segment.wrappedStartXYN[1],
                                z = segment.wrappedStartXYN[2],
                                zSpecified = true,
                            },
                            new PlainPoint()
                            {
                                x = ((segment.tileId >> 16) & 0xffff) * 254 + segment.wrappedEndXYN[0],
                                y = ((segment.tileId ) & 0xffff) * 254 + segment.wrappedEndXYN[1],
                                z = segment.wrappedEndXYN[2],
                                zSpecified = true,
                            },
                            };
                            mapmatchPlainPoints = mapmatchPlainPoints.OrderBy(pp => pp.x).ThenBy(pp => pp.y).ThenBy(pp => pp.z).ToArray();

                            var locatePlainPoints = new PlainPoint[]
                            {
                            new PlainPoint()
                            {
                                x = ((xyn[1] >> 16) & 0xffff) * 254 + xyn[2],
                                y = (xyn[1] & 0xffff) * 254 + xyn[3],
                                z = xyn[4],
                                zSpecified = true,
                            },
                            new PlainPoint()
                            {
                                x = ((xyn[6] >> 16) & 0xffff) * 254 + xyn[7],
                                y = (xyn[6] & 0xffff) * 254 + xyn[8],
                                z = xyn[9],
                                zSpecified = true,
                            },
                            };
                            locatePlainPoints = locatePlainPoints.OrderBy(pp => pp.x).ThenBy(pp => pp.y).ThenBy(pp => pp.z).ToArray();
                            if (mapmatchPlainPoints[0].x == locatePlainPoints[0].x)
                                if (mapmatchPlainPoints[0].y == locatePlainPoints[0].y)
                                    if (mapmatchPlainPoints[0].z == locatePlainPoints[0].z)
                                        if (mapmatchPlainPoints[1].x == locatePlainPoints[1].x)
                                            if (mapmatchPlainPoints[1].y == locatePlainPoints[1].y)
                                                if (mapmatchPlainPoints[1].z == locatePlainPoints[1].z)
                                                {
                                                    dataPoint.ResultAddress = resultAddress;
                                                    break;
                                                }
                        }
                    }
                }

                #endregion xLocate_action

                #region xRoute_action

                if (xRouteCheckBox.Checked)
                {
                    XRouteWSService xRouteClient = new XRouteWSService()
                    {
                        Url = xRouteURLTextBox.Text,
                    };
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.Token))
                    {
                        xRouteClient.Credentials = new System.Net.NetworkCredential("xtok", Properties.Settings.Default.Token);
                    }

                    RoutingOption[] routingOptions = new RoutingOption[]
                    {
                    new RoutingOption() {parameter = RoutingParameter.COUNTRY_ENCODING, value="ISO2",},
                    new RoutingOption() {parameter = RoutingParameter.REQUEST_VERSION, value="1.20",},
                    };
                    ResultListOptions routeResultListOptions = new ResultListOptions()
                    {
                        polygon = true,
                        segments = true,
                        segmentAttributes = true,
                    };
                    if (emissionsChkBx.Checked)
                    {
                        routeResultListOptions.emissions = new EmissionType() { emissionLevel = EmissionLevel.ALL };
                        routeResultListOptions.hbefaType = new HBEFAType() { version = HBEFAVersion.HBEFA_2_1 };
                    }

                    CountryInfoOptions countryInfoOptions = new CountryInfoOptions()
                    {
                        allEuro = true,
                        detailedTollCosts = true,
                        namedToll = true,
                        namedTollSpecified = true,
                        tollTotals = true,
                        tollTotalsSpecified = true,
                    };
                    CallerContext xRouteCallerContext;
                    if (!string.IsNullOrEmpty(xRouteProfileTextBox.Text))
                    {
                        if (xRouteProfileTextBox.Text[0] == '$')
                            xRouteCallerContext = new CallerContext()
                            {
                                wrappedProperties = new CallerContextProperty[]
                                {
                                new CallerContextProperty() { key = "CoordFormat", value=CoordFormat.OG_GEODECIMAL.ToString(),},
                                new CallerContextProperty() { key = "ProfileXMLSnippet", value=(new StreamReader(xRouteProfileTextBox.Text.Substring(1)+".xml").ReadToEnd()),},
                                },
                            };
                        else
                            xRouteCallerContext = new CallerContext()
                            {
                                wrappedProperties = new CallerContextProperty[]
                                {
                                new CallerContextProperty() { key = "CoordFormat", value=CoordFormat.OG_GEODECIMAL.ToString(),},
                                new CallerContextProperty() { key = "Profile", value=xRouteProfileTextBox.Text,},
                                },
                            };
                    }
                    else
                    {
                        xRouteCallerContext = new CallerContext()
                        {
                            wrappedProperties = new CallerContextProperty[]
                            {
                            new CallerContextProperty() { key = "CoordFormat", value=CoordFormat.OG_GEODECIMAL.ToString(),},
                            },
                        };
                    }

                    List<WaypointDesc> waypointDescList = new List<WaypointDesc>();

                    var dataPointWithMatchList = dataPointList.Where(d => d.MatchedLocation != null).ToList();

                    if (routeSanityCheckCheckBox.Checked)
                    {
                        // for the sanity check we are going to filter out al transitions to and from main road network
                        // this should prevent misrouting arround intersections and the occasional GPS drift match
                        // we are also filtering out low rated matches and low probalility matches since these means
                        // that usually there are mulitple segments nearby that match the details and we can't be certain we have the correct one
                        // we do always keep the first and last points
                        var tempDataPointList = new List<DataPoint>();
                        tempDataPointList.Add(dataPointWithMatchList.First());
                        for (int i = 1; i < dataPointWithMatchList.Count - 1; i++)
                        {
                            //if (dataPointWithMatchList[i].NetworkClass < 2 && dataPointWithMatchList[i + 1].NetworkClass >= 2) { i++; continue; }
                            //if (dataPointWithMatchList[i].NetworkClass >= 2 && dataPointWithMatchList[i + 1].NetworkClass < 2) { i++; continue; }
                            if (dataPointWithMatchList[i].LocalRating < 0.8) continue;
                            if (dataPointWithMatchList[i].Probability < 0.55) continue;
                            tempDataPointList.Add(dataPointWithMatchList[i]);
                        }
                        tempDataPointList.Add(dataPointWithMatchList.Last());
                        dataPointWithMatchList = tempDataPointList;
                    }

                    //// it could be an idea to include the first positions always to get complete xRoute tracks in case it takes a while for the
                    //// mapmatch engine to find it's first match, however it can lead to unnecessary long detour if you are starting from a private
                    //// area whitch isn't mapped.
                    //
                    //if (dataPointList.First().MatchedLocation == null)
                    //{
                    //    WaypointDesc start = new WaypointDesc()
                    //    {
                    //        linkType = LinkType.NEXT_SEGMENT,
                    //        wrappedCoords = new XServer.Point[]
                    //        {
                    //            new XServer.Point()
                    //            {
                    //                point = new PlainPoint() { x= dataPointList.First().LonInput,y=dataPointList.First().LatInput,},
                    //            }
                    //        },
                    //        heading = (int)dataPointList.First().Heading,
                    //        headingSpecified = true,
                    //    };
                    //    waypointDescList.Add(start);
                    //}
                    foreach (DataPoint dataPoint in dataPointWithMatchList)
                    {
                        WaypointDesc waypoint = new WaypointDesc()
                        {
                            linkType = LinkType.NEXT_SEGMENT,
                            wrappedSegmentID = new UniqueGeoID[]
                            {
                            new UniqueGeoID()
                            {
                                iuCode = dataPoint.MatchedLocation.wrappedPath.Last().countryCode,
                                tID = dataPoint.MatchedLocation.wrappedPath.Last().tileId,
                                xOff  = dataPoint.MatchedLocation.wrappedPath.Last().wrappedStartXYN[0],
                                yOff = dataPoint.MatchedLocation.wrappedPath.Last().wrappedStartXYN[1],
                                n = dataPoint.MatchedLocation.wrappedPath.Last().wrappedStartXYN[2],
                            },
                            new UniqueGeoID()
                            {
                                iuCode = dataPoint.MatchedLocation.wrappedPath.Last().countryCode,
                                tID = dataPoint.MatchedLocation.wrappedPath.Last().tileId,
                                xOff  = dataPoint.MatchedLocation.wrappedPath.Last().wrappedEndXYN[0],
                                yOff = dataPoint.MatchedLocation.wrappedPath.Last().wrappedEndXYN[1],
                                n = dataPoint.MatchedLocation.wrappedPath.Last().wrappedEndXYN[2],
                            },
                            },
                        };
                        waypointDescList.Add(waypoint);
                    }
                    //// see commment above

                    //if (dataPointList.Last().MatchedLocation == null)
                    //{
                    //    WaypointDesc stop = new WaypointDesc()
                    //    {
                    //        linkType = LinkType.NEXT_SEGMENT,
                    //        wrappedCoords = new XServer.Point[]
                    //        {
                    //            new XServer.Point()
                    //            {
                    //                point = new PlainPoint() { x= dataPointList.Last().LonInput,y=dataPointList.Last().LatInput,},
                    //            }
                    //        },
                    //    };
                    //    waypointDescList.Add(stop);
                    //}

                    // reducing the waypoint list to only remove 2 waypoints after eachother that are identical
                    // this lowers xRoute overhead on dense matching cases.
                    var reducedWaypointDescList = new List<WaypointDesc>();
                    reducedWaypointDescList.Add(waypointDescList.First());
                    for (int i = 1; i < waypointDescList.Count; i++)
                    {
                        var previous = waypointDescList[i - 1].wrappedSegmentID;
                        var current = waypointDescList[i].wrappedSegmentID;
                        if (previous[0].iuCode == current[0].iuCode
                            && previous[0].n == current[0].n
                            && previous[0].tID == current[0].tID
                            && previous[0].xOff == current[0].xOff
                            && previous[0].yOff == current[0].yOff
                            && previous[1].iuCode == current[1].iuCode
                            && previous[1].n == current[1].n
                            && previous[1].tID == current[1].tID
                            && previous[1].xOff == current[1].xOff
                            && previous[1].yOff == current[1].yOff
                            )
                            continue;
                        else
                            reducedWaypointDescList.Add(waypointDescList[i]);
                    }

                    xRouteCallerContext.log1 = inputFileName;
                    if (tollChckBx.Checked || emissionsChkBx.Checked)
                        extendedRoute = xRouteClient.calculateExtendedRoute(reducedWaypointDescList.ToArray(), routingOptions, null, routeResultListOptions, countryInfoOptions, xRouteCallerContext);
                    else
                    {
                        extendedRoute = new ExtendedRoute();
                        extendedRoute.route = xRouteClient.calculateRoute(reducedWaypointDescList.ToArray(), routingOptions, null, routeResultListOptions, xRouteCallerContext);
                    }
                    xRouteDistanceTxtBx.Text = (extendedRoute.route.info.distance / 1000).ToString() + " km";
                    xRouteTimeTextBox.Text = TimeSpan.FromSeconds(extendedRoute.route.info.time).ToString();
                    if (tollChckBx.Checked)
                    {
                        new TollForm(extendedRoute.wrappedCountryInfos).Show();
                    }
                    if (emissionsChkBx.Checked)
                    {
                        new EmmissionsForm(extendedRoute).Show();
                    }

                    MapPolyline mapPolyLine = new MapPolyline()
                    {
                        Stroke = new SolidColorBrush(Colors.DarkViolet),
                        MapStrokeThickness = 5,
                        ScaleFactor = 0.2
                    };
                    mapPolyLine.Points = new PointCollection();
                    foreach (PlainPoint plainPoint in extendedRoute.route.polygon.lineString.wrappedPoints)
                    {
                        mapPolyLine.Points.Add(new System.Windows.Point(plainPoint.x, plainPoint.y));
                    }
                    routeLayer.Shapes.Add(mapPolyLine);
                }

                #endregion xRoute_action


            }
            catch (Exception ex)
            {
                MessageBox.Show("Excpetion of the type: " + ex.GetType().ToString() + "\nMessage:\n" + ex.Message);
                return;
            }
            finally
            {
                Cursor = previourCursor;
                dataPointGridView.DataSource = dataPointBindingSource;
                dataPointBindingSource.DataSource = dataPointList;
                dataPointGridView.Invalidate();

                dataPointGridView.Refresh();
                UpdateFormTitle();
            }
        }

        private void dataPointGridView_SelectionChanged(object sender, EventArgs e)
        {
            double zoom = zoomOnSelctChkbx.Checked ? 18 : mapView.ZoomLevel;

            pathLayer.Shapes.Remove(selectedSegmentLine);
            if (dataPointGridView.SelectedRows.Count > 0)
            {

                var dataPoint = (DataPoint)dataPointGridView.SelectedRows[0].DataBoundItem;
                double x = dataPoint.LonInput, y = dataPoint.LatInput;
                mapView.SetMapLocation(new System.Windows.Point(x, y), zoom);

                var matchedLocation = ((DataPoint)dataPointGridView.SelectedRows[0].DataBoundItem).MatchedLocation;
                if (matchedLocation != null)
                {
                    selectedSegmentLine.Points.Clear();
                    foreach (var point in matchedLocation.wrappedPath.Last().wrappedPolyline)
                    {
                        selectedSegmentLine.Points.Add(new System.Windows.Point(point.point.x, point.point.y));
                    }
                    pathLayer.Shapes.Insert(0, selectedSegmentLine);
                    //pathLayer.Shapes.Add(selectedSegmentLine);
                }
            }
        }

        private float CalculateHeading(DataPoint start, DataPoint end)
        {
            //officaly i should convert to lineair coordinate projection like mercator, but for now we stick to geodecimal because the error is small on small coordinate differences

            float angle = (float)((180 / Math.PI) * Math.Atan2(end.LonInput - start.LonInput, end.LatInput - start.LatInput));
            if (angle < 0) angle += 360;
            if (angle == 360) angle = 0;
            return angle;
        }

        private void xMapURLTextBox_Leave(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(xMapURLTextBox.Text, UriKind.Absolute))
            {
                mapView.XMapUrl = xMapURLTextBox.Text;
                xMapProfileTextBox_Leave(null, null);
                mapView.Layers["Background"].Opacity = 0.5;
                mapView.Refresh();
            }
            else
            {
                MessageBox.Show("XMap url is not a well formed URL");
                xMapmatchURLTextBox.Focus();
            }
        }

        private void xMapProfileTextBox_Leave(object sender, EventArgs e)
        {
            Ptv.XServer.Controls.Map.Layers.XMapLayerFactory.UpdateXMapProfiles(mapView.Layers, xMapProfileTextBox.Text + "-bg", xMapProfileTextBox.Text + "-fg");
        }

        private void nmeaBtn_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(pollingIntervalTxtBx.Text, out int interval))
            {
                MessageBox.Show("Polling interval is not a valid interger.");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,
            };
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            dataPointList.Clear();
            outputPointLayer.Shapes.Clear();
            routeLayer.Shapes.Clear();
            pathLayer.Shapes.Clear();

            dataPointList = NMEAParsers.ParseFile(dialog.FileName, interval);

            populateInputPointLayer();

            dataPointGridView.DataSource = dataPointBindingSource;
            dataPointBindingSource.DataSource = dataPointList;
            dataPointGridView.Invalidate();
        }

        private void exportResultsButton_Click(object sender, EventArgs e)
        {
            string outputFile = "";
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV files (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                    outputFile = dialog.FileName;
                else
                    return;
            }
            string sepa = ";";
            if (commaSeparatorChckBx.Checked) sepa = ",";


            string decimalSepa = ",";
            if (decimalPointChckBx.Checked) decimalSepa = ".";
            var decimalSepaOrg = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            using (var writer = new StreamWriter(outputFile))
            {
                if (csvHeaderChckBx.Checked)
                {
                    var columnNames = new List<string>();
                    foreach (DataGridViewColumn column in dataPointGridView.Columns)
                        columnNames.Add(column.HeaderText);
                    writer.WriteLine(string.Join(sepa, columnNames));
                }
                foreach (var dataPoint in dataPointList)
                {
                    writer.WriteLine(
                        dataPoint.Id.ToString() + sepa +
                        dataPoint.LatInput.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.LonInput.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.SpeedInMps.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.SpeedInKmph.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.Timestamp.ToString() + sepa +
                        dataPoint.SpeedInKmph.ToString() + sepa +
                        dataPoint.LatOutput.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.LonOutput.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.SpeedLimitForward.ToString() + sepa +
                        dataPoint.SpeedLimitBackward.ToString() + sepa +
                        (dataPoint.ResultAddress ?? new ResultAddress()).ToString() + sepa +
                        dataPoint.CountryCode.ToString() + sepa +
                        dataPoint.NetworkClass.ToString() + sepa +
                        dataPoint.LocalRating.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.Probability.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.TransitionRating.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.LinkingDistance.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.AngleDifference.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.CoveredDistance.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        dataPoint.DrivenDistance.ToString().Replace(decimalSepaOrg, decimalSepa) + sepa +
                        (dataPoint.VendorId ?? "") + sepa +
                        dataPoint.Stable.ToString() + sepa +
                        dataPoint.LocalMatching.ToString()
                        );
                }
            }
        }

        private void exportRouteBtn_Click(object sender, EventArgs e)
        {
            if (extendedRoute == null)
            {
                MessageBox.Show("You need to activate xRoute and match some data first.");
                return;
            }
            if (extendedRoute.route.polygon == null || extendedRoute.route.polygon.lineString.wrappedPoints.Count() < 2)
            {
                MessageBox.Show("The calculated route is to small to make a proper export.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            try
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    char separator = commaSeparatorChckBx.Checked ? ',' : ';';
                    foreach (var point in extendedRoute.route.polygon.lineString.wrappedPoints)
                    {
                        writer.WriteLine(point.x.ToString() + separator + point.y.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception while exporting route.\n\nType: " + exception.GetType().ToString() + "\n" + "Message: " + exception.Message);
            }
        }

        private void RedoTimeStamps()
        {
            for (int i = 0; i < dataPointList.Count - 1; i++)
            {
                var begin = new System.Windows.Point(dataPointList[i].LonInput, dataPointList[i].LatInput);
                var end = new System.Windows.Point(dataPointList[i + 1].LonInput, dataPointList[i + 1].LatInput);
                // calculate the airline distance and mutiply it with 1.34 to take into account that routes usually are not following the airline
                // note the factor 1.34 is determined by measurement donw by PTV in the past on this topic and is a good average value factor
                var dist = Geotools.AirLineDistanceCalculator.CalculateUsingWGS84(begin, end) * 1.34;
                var speed = (dataPointList[i].SpeedInMps + dataPointList[i + 1].SpeedInMps) / 2;
                // make sure to add always at least a single second because xMapmatch demand the timestamps are increasing
                dataPointList[i + 1].TrackPosition.timestamp =
                    dataPointList[i].TrackPosition.timestamp.AddSeconds(Math.Max(1, (dist * 1.34) / speed));
            }
        }

        private void RedoTimeStamps(TrackPosition[] arrayOfTrackPostions)
        {
            for (int i = 0; i < arrayOfTrackPostions.Length - 1; i++)
            {
                var begin = new System.Windows.Point(arrayOfTrackPostions[i].lon, arrayOfTrackPostions[i].lat);
                var end = new System.Windows.Point(arrayOfTrackPostions[i + 1].lon, arrayOfTrackPostions[i + 1].lat);
                // calculate the airline distance and mutiply it with 1.34 to take into account that routes usually are not following the airline
                // note the factor 1.34 is determined by measurement donw by PTV in the past on this topic and is a good average value factor
                var dist = Geotools.AirLineDistanceCalculator.CalculateUsingWGS84(begin, end) * 1.34;
                var speed = (arrayOfTrackPostions[i].speedInMps + arrayOfTrackPostions[i + 1].speedInMps) / 2;
                // make sure to add always at least a single second because xMapmatch demand the timestamps are increasing
                arrayOfTrackPostions[i + 1].timestamp =
                    arrayOfTrackPostions[i].timestamp.AddSeconds(Math.Max(1, (dist * 1.34) / speed));
            }
        }
    }
}