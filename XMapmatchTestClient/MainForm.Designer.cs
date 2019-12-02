namespace XMapmatchTestClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.xMapmatchProfileTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.xMapProfileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xMapURLTextBox = new System.Windows.Forms.TextBox();
            this.xMapmatchURLTextBox = new System.Windows.Forms.TextBox();
            this.xMapmatchLabel = new System.Windows.Forms.Label();
            this.dataPointGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latInputDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lonInputDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speedInMpsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speedInKmphDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latOutputDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lonOutputDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speedLimitForwardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speedLimitBackwardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.networkClassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localRatingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.probabilityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transitionRatingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkingDistanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.angleDifferenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoveredDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrivenDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LocalMatching = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loadDataBtn = new System.Windows.Forms.Button();
            this.matchDataBtn = new System.Windows.Forms.Button();
            this.mapView = new Ptv.XServer.Controls.Map.FormsMap();
            this.loadRequestBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.xLocateCheckBox = new System.Windows.Forms.CheckBox();
            this.xLocateURLTextBox = new System.Windows.Forms.TextBox();
            this.xLocateProfileTextBox = new System.Windows.Forms.TextBox();
            this.xRouteProfileTextBox = new System.Windows.Forms.TextBox();
            this.xRouteURLTextBox = new System.Windows.Forms.TextBox();
            this.xRouteCheckBox = new System.Windows.Forms.CheckBox();
            this.explanationLbl = new System.Windows.Forms.Label();
            this.csvHeaderChckBx = new System.Windows.Forms.CheckBox();
            this.idPresentChckBx = new System.Windows.Forms.CheckBox();
            this.commaSeparatorChckBx = new System.Windows.Forms.CheckBox();
            this.decimalPointChckBx = new System.Windows.Forms.CheckBox();
            this.tollChckBx = new System.Windows.Forms.CheckBox();
            this.emissionsChkBx = new System.Windows.Forms.CheckBox();
            this.makeOwnTimestampChkBx = new System.Windows.Forms.CheckBox();
            this.recalculateHeadingChkBx = new System.Windows.Forms.CheckBox();
            this.mainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.filterStandStillCheckBox = new System.Windows.Forms.CheckBox();
            this.routeSanityCheckCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.xRouteDistanceTxtBx = new System.Windows.Forms.TextBox();
            this.xMapmatchDistanceTxtBx = new System.Windows.Forms.TextBox();
            this.zoomOnSelctChkbx = new System.Windows.Forms.CheckBox();
            this.nmeaBtn = new System.Windows.Forms.Button();
            this.pollingIntervalTxtBx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.exportRouteBtn = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correctTimestampCheckbox = new System.Windows.Forms.CheckBox();
            this.exportResultsButton = new System.Windows.Forms.Button();
            this.xRouteTimeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.filterDoubleCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // xMapmatchProfileTextBox
            // 
            this.xMapmatchProfileTextBox.Location = new System.Drawing.Point(373, 38);
            this.xMapmatchProfileTextBox.Name = "xMapmatchProfileTextBox";
            this.xMapmatchProfileTextBox.Size = new System.Drawing.Size(100, 20);
            this.xMapmatchProfileTextBox.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(370, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "xServer Profiles";
            // 
            // xMapProfileTextBox
            // 
            this.xMapProfileTextBox.Location = new System.Drawing.Point(373, 64);
            this.xMapProfileTextBox.Name = "xMapProfileTextBox";
            this.xMapProfileTextBox.Size = new System.Drawing.Size(100, 20);
            this.xMapProfileTextBox.TabIndex = 27;
            this.xMapProfileTextBox.Leave += new System.EventHandler(this.xMapProfileTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "xServer URLs";
            // 
            // xMapURLTextBox
            // 
            this.xMapURLTextBox.Location = new System.Drawing.Point(108, 64);
            this.xMapURLTextBox.Name = "xMapURLTextBox";
            this.xMapURLTextBox.Size = new System.Drawing.Size(259, 20);
            this.xMapURLTextBox.TabIndex = 23;
            this.xMapURLTextBox.Text = "http://localhost:50010/xmap/ws/XMap";
            this.xMapURLTextBox.Leave += new System.EventHandler(this.xMapURLTextBox_Leave);
            // 
            // xMapmatchURLTextBox
            // 
            this.xMapmatchURLTextBox.Location = new System.Drawing.Point(108, 38);
            this.xMapmatchURLTextBox.Name = "xMapmatchURLTextBox";
            this.xMapmatchURLTextBox.Size = new System.Drawing.Size(259, 20);
            this.xMapmatchURLTextBox.TabIndex = 22;
            this.xMapmatchURLTextBox.Text = "http://localhost:50040/xmapmatch/ws/XMapmatch";
            // 
            // xMapmatchLabel
            // 
            this.xMapmatchLabel.AutoSize = true;
            this.xMapmatchLabel.Location = new System.Drawing.Point(29, 41);
            this.xMapmatchLabel.Name = "xMapmatchLabel";
            this.xMapmatchLabel.Size = new System.Drawing.Size(62, 13);
            this.xMapmatchLabel.TabIndex = 21;
            this.xMapmatchLabel.Text = "xMapmatch";
            // 
            // dataPointGridView
            // 
            this.dataPointGridView.AllowUserToAddRows = false;
            this.dataPointGridView.AllowUserToDeleteRows = false;
            this.dataPointGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPointGridView.AutoGenerateColumns = false;
            this.dataPointGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataPointGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPointGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.latInputDataGridViewTextBoxColumn,
            this.lonInputDataGridViewTextBoxColumn,
            this.speedInMpsDataGridViewTextBoxColumn,
            this.headingDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn,
            this.speedInKmphDataGridViewTextBoxColumn,
            this.latOutputDataGridViewTextBoxColumn,
            this.lonOutputDataGridViewTextBoxColumn,
            this.speedLimitForwardDataGridViewTextBoxColumn,
            this.speedLimitBackwardDataGridViewTextBoxColumn,
            this.ResultAddress,
            this.countryCodeDataGridViewTextBoxColumn,
            this.networkClassDataGridViewTextBoxColumn,
            this.localRatingDataGridViewTextBoxColumn,
            this.probabilityDataGridViewTextBoxColumn,
            this.transitionRatingDataGridViewTextBoxColumn,
            this.linkingDistanceDataGridViewTextBoxColumn,
            this.angleDifferenceDataGridViewTextBoxColumn,
            this.CoveredDistance,
            this.DrivenDistance,
            this.VendorId,
            this.Stable,
            this.LocalMatching});
            this.dataPointGridView.DataSource = this.dataPointBindingSource;
            this.dataPointGridView.Location = new System.Drawing.Point(12, 142);
            this.dataPointGridView.MultiSelect = false;
            this.dataPointGridView.Name = "dataPointGridView";
            this.dataPointGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataPointGridView.Size = new System.Drawing.Size(1447, 225);
            this.dataPointGridView.TabIndex = 32;
            this.dataPointGridView.SelectionChanged += new System.EventHandler(this.dataPointGridView_SelectionChanged);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Width = 41;
            // 
            // latInputDataGridViewTextBoxColumn
            // 
            this.latInputDataGridViewTextBoxColumn.DataPropertyName = "LatInput";
            this.latInputDataGridViewTextBoxColumn.HeaderText = "LatInput";
            this.latInputDataGridViewTextBoxColumn.Name = "latInputDataGridViewTextBoxColumn";
            this.latInputDataGridViewTextBoxColumn.ReadOnly = true;
            this.latInputDataGridViewTextBoxColumn.Width = 71;
            // 
            // lonInputDataGridViewTextBoxColumn
            // 
            this.lonInputDataGridViewTextBoxColumn.DataPropertyName = "LonInput";
            this.lonInputDataGridViewTextBoxColumn.HeaderText = "LonInput";
            this.lonInputDataGridViewTextBoxColumn.Name = "lonInputDataGridViewTextBoxColumn";
            this.lonInputDataGridViewTextBoxColumn.ReadOnly = true;
            this.lonInputDataGridViewTextBoxColumn.Width = 74;
            // 
            // speedInMpsDataGridViewTextBoxColumn
            // 
            this.speedInMpsDataGridViewTextBoxColumn.DataPropertyName = "SpeedInMps";
            this.speedInMpsDataGridViewTextBoxColumn.HeaderText = "SpeedInMps";
            this.speedInMpsDataGridViewTextBoxColumn.Name = "speedInMpsDataGridViewTextBoxColumn";
            this.speedInMpsDataGridViewTextBoxColumn.ReadOnly = true;
            this.speedInMpsDataGridViewTextBoxColumn.Width = 92;
            // 
            // headingDataGridViewTextBoxColumn
            // 
            this.headingDataGridViewTextBoxColumn.DataPropertyName = "Heading";
            this.headingDataGridViewTextBoxColumn.HeaderText = "Heading";
            this.headingDataGridViewTextBoxColumn.Name = "headingDataGridViewTextBoxColumn";
            this.headingDataGridViewTextBoxColumn.ReadOnly = true;
            this.headingDataGridViewTextBoxColumn.Width = 72;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            this.timestampDataGridViewTextBoxColumn.ReadOnly = true;
            this.timestampDataGridViewTextBoxColumn.Width = 83;
            // 
            // speedInKmphDataGridViewTextBoxColumn
            // 
            this.speedInKmphDataGridViewTextBoxColumn.DataPropertyName = "SpeedInKmph";
            this.speedInKmphDataGridViewTextBoxColumn.HeaderText = "SpeedInKmph";
            this.speedInKmphDataGridViewTextBoxColumn.Name = "speedInKmphDataGridViewTextBoxColumn";
            this.speedInKmphDataGridViewTextBoxColumn.ReadOnly = true;
            this.speedInKmphDataGridViewTextBoxColumn.Width = 99;
            // 
            // latOutputDataGridViewTextBoxColumn
            // 
            this.latOutputDataGridViewTextBoxColumn.DataPropertyName = "LatOutput";
            this.latOutputDataGridViewTextBoxColumn.HeaderText = "LatOutput";
            this.latOutputDataGridViewTextBoxColumn.Name = "latOutputDataGridViewTextBoxColumn";
            this.latOutputDataGridViewTextBoxColumn.ReadOnly = true;
            this.latOutputDataGridViewTextBoxColumn.Width = 79;
            // 
            // lonOutputDataGridViewTextBoxColumn
            // 
            this.lonOutputDataGridViewTextBoxColumn.DataPropertyName = "LonOutput";
            this.lonOutputDataGridViewTextBoxColumn.HeaderText = "LonOutput";
            this.lonOutputDataGridViewTextBoxColumn.Name = "lonOutputDataGridViewTextBoxColumn";
            this.lonOutputDataGridViewTextBoxColumn.ReadOnly = true;
            this.lonOutputDataGridViewTextBoxColumn.Width = 82;
            // 
            // speedLimitForwardDataGridViewTextBoxColumn
            // 
            this.speedLimitForwardDataGridViewTextBoxColumn.DataPropertyName = "SpeedLimitForward";
            this.speedLimitForwardDataGridViewTextBoxColumn.HeaderText = "SpeedLimitForward";
            this.speedLimitForwardDataGridViewTextBoxColumn.Name = "speedLimitForwardDataGridViewTextBoxColumn";
            this.speedLimitForwardDataGridViewTextBoxColumn.ReadOnly = true;
            this.speedLimitForwardDataGridViewTextBoxColumn.Width = 122;
            // 
            // speedLimitBackwardDataGridViewTextBoxColumn
            // 
            this.speedLimitBackwardDataGridViewTextBoxColumn.DataPropertyName = "SpeedLimitBackward";
            this.speedLimitBackwardDataGridViewTextBoxColumn.HeaderText = "SpeedLimitBackward";
            this.speedLimitBackwardDataGridViewTextBoxColumn.Name = "speedLimitBackwardDataGridViewTextBoxColumn";
            this.speedLimitBackwardDataGridViewTextBoxColumn.ReadOnly = true;
            this.speedLimitBackwardDataGridViewTextBoxColumn.Width = 132;
            // 
            // ResultAddress
            // 
            this.ResultAddress.DataPropertyName = "ResultAddress";
            this.ResultAddress.HeaderText = "ResultAddress";
            this.ResultAddress.Name = "ResultAddress";
            // 
            // countryCodeDataGridViewTextBoxColumn
            // 
            this.countryCodeDataGridViewTextBoxColumn.DataPropertyName = "CountryCode";
            this.countryCodeDataGridViewTextBoxColumn.HeaderText = "CountryCode";
            this.countryCodeDataGridViewTextBoxColumn.Name = "countryCodeDataGridViewTextBoxColumn";
            this.countryCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.countryCodeDataGridViewTextBoxColumn.Width = 93;
            // 
            // networkClassDataGridViewTextBoxColumn
            // 
            this.networkClassDataGridViewTextBoxColumn.DataPropertyName = "NetworkClass";
            this.networkClassDataGridViewTextBoxColumn.HeaderText = "NetworkClass";
            this.networkClassDataGridViewTextBoxColumn.Name = "networkClassDataGridViewTextBoxColumn";
            this.networkClassDataGridViewTextBoxColumn.ReadOnly = true;
            this.networkClassDataGridViewTextBoxColumn.Width = 97;
            // 
            // localRatingDataGridViewTextBoxColumn
            // 
            this.localRatingDataGridViewTextBoxColumn.DataPropertyName = "LocalRating";
            this.localRatingDataGridViewTextBoxColumn.HeaderText = "LocalRating";
            this.localRatingDataGridViewTextBoxColumn.Name = "localRatingDataGridViewTextBoxColumn";
            this.localRatingDataGridViewTextBoxColumn.ReadOnly = true;
            this.localRatingDataGridViewTextBoxColumn.Width = 89;
            // 
            // probabilityDataGridViewTextBoxColumn
            // 
            this.probabilityDataGridViewTextBoxColumn.DataPropertyName = "Probability";
            this.probabilityDataGridViewTextBoxColumn.HeaderText = "Probability";
            this.probabilityDataGridViewTextBoxColumn.Name = "probabilityDataGridViewTextBoxColumn";
            this.probabilityDataGridViewTextBoxColumn.ReadOnly = true;
            this.probabilityDataGridViewTextBoxColumn.Width = 80;
            // 
            // transitionRatingDataGridViewTextBoxColumn
            // 
            this.transitionRatingDataGridViewTextBoxColumn.DataPropertyName = "TransitionRating";
            this.transitionRatingDataGridViewTextBoxColumn.HeaderText = "TransitionRating";
            this.transitionRatingDataGridViewTextBoxColumn.Name = "transitionRatingDataGridViewTextBoxColumn";
            this.transitionRatingDataGridViewTextBoxColumn.ReadOnly = true;
            this.transitionRatingDataGridViewTextBoxColumn.Width = 109;
            // 
            // linkingDistanceDataGridViewTextBoxColumn
            // 
            this.linkingDistanceDataGridViewTextBoxColumn.DataPropertyName = "LinkingDistance";
            this.linkingDistanceDataGridViewTextBoxColumn.HeaderText = "LinkingDistance";
            this.linkingDistanceDataGridViewTextBoxColumn.Name = "linkingDistanceDataGridViewTextBoxColumn";
            this.linkingDistanceDataGridViewTextBoxColumn.ReadOnly = true;
            this.linkingDistanceDataGridViewTextBoxColumn.Width = 108;
            // 
            // angleDifferenceDataGridViewTextBoxColumn
            // 
            this.angleDifferenceDataGridViewTextBoxColumn.DataPropertyName = "AngleDifference";
            this.angleDifferenceDataGridViewTextBoxColumn.HeaderText = "AngleDifference";
            this.angleDifferenceDataGridViewTextBoxColumn.Name = "angleDifferenceDataGridViewTextBoxColumn";
            this.angleDifferenceDataGridViewTextBoxColumn.ReadOnly = true;
            this.angleDifferenceDataGridViewTextBoxColumn.Width = 108;
            // 
            // CoveredDistance
            // 
            this.CoveredDistance.DataPropertyName = "CoveredDistance";
            this.CoveredDistance.HeaderText = "CoveredDistance";
            this.CoveredDistance.Name = "CoveredDistance";
            this.CoveredDistance.ReadOnly = true;
            this.CoveredDistance.Width = 114;
            // 
            // DrivenDistance
            // 
            this.DrivenDistance.DataPropertyName = "DrivenDistance";
            this.DrivenDistance.HeaderText = "DrivenDistance";
            this.DrivenDistance.Name = "DrivenDistance";
            this.DrivenDistance.ReadOnly = true;
            this.DrivenDistance.Width = 105;
            // 
            // VendorId
            // 
            this.VendorId.DataPropertyName = "VendorId";
            this.VendorId.HeaderText = "VendorId";
            this.VendorId.Name = "VendorId";
            this.VendorId.ReadOnly = true;
            this.VendorId.Width = 75;
            // 
            // Stable
            // 
            this.Stable.DataPropertyName = "Stable";
            this.Stable.HeaderText = "Stable";
            this.Stable.Name = "Stable";
            this.Stable.ReadOnly = true;
            this.Stable.Width = 43;
            // 
            // LocalMatching
            // 
            this.LocalMatching.DataPropertyName = "LocalMatching";
            this.LocalMatching.HeaderText = "LocalMatching";
            this.LocalMatching.Name = "LocalMatching";
            this.LocalMatching.ReadOnly = true;
            this.LocalMatching.Width = 83;
            // 
            // dataPointBindingSource
            // 
            this.dataPointBindingSource.AllowNew = false;
            this.dataPointBindingSource.DataSource = typeof(XMapmatchTestClient.DataPoint);
            // 
            // loadDataBtn
            // 
            this.loadDataBtn.Location = new System.Drawing.Point(480, 7);
            this.loadDataBtn.Name = "loadDataBtn";
            this.loadDataBtn.Size = new System.Drawing.Size(163, 29);
            this.loadDataBtn.TabIndex = 33;
            this.loadDataBtn.Text = "Load data from CSV";
            this.loadDataBtn.UseVisualStyleBackColor = true;
            this.loadDataBtn.Click += new System.EventHandler(this.loadDataBtn_Click);
            // 
            // matchDataBtn
            // 
            this.matchDataBtn.Location = new System.Drawing.Point(480, 77);
            this.matchDataBtn.Name = "matchDataBtn";
            this.matchDataBtn.Size = new System.Drawing.Size(163, 29);
            this.matchDataBtn.TabIndex = 34;
            this.matchDataBtn.Text = "Match Data";
            this.matchDataBtn.UseVisualStyleBackColor = true;
            this.matchDataBtn.Click += new System.EventHandler(this.matchDataBtn_Click);
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mapView.Center = ((System.Windows.Point)(resources.GetObject("mapView.Center")));
            this.mapView.CoordinateDiplayFormat = Ptv.XServer.Controls.Map.CoordinateDiplayFormat.Degree;
            this.mapView.FitInWindow = false;
            this.mapView.InvertMouseWheel = false;
            this.mapView.Location = new System.Drawing.Point(12, 402);
            this.mapView.MaxZoom = 19;
            this.mapView.MinZoom = 0;
            this.mapView.MouseDoubleClickZoom = true;
            this.mapView.MouseDragMode = Ptv.XServer.Controls.Map.Gadgets.DragMode.SelectOnShift;
            this.mapView.MouseWheelSpeed = 0.5D;
            this.mapView.Name = "mapView";
            this.mapView.ShowCoordinates = true;
            this.mapView.ShowLayers = true;
            this.mapView.ShowMagnifier = true;
            this.mapView.ShowNavigation = true;
            this.mapView.ShowOverview = true;
            this.mapView.ShowScale = true;
            this.mapView.ShowZoomSlider = true;
            this.mapView.Size = new System.Drawing.Size(1447, 328);
            this.mapView.TabIndex = 35;
            this.mapView.UseAnimation = false;
            this.mapView.UseDefaultTheme = true;
            this.mapView.UseMiles = false;
            this.mapView.XMapCopyright = "Please configure a valid copyright text!";
            this.mapView.XMapCredentials = "";
            this.mapView.XMapStyle = "";
            this.mapView.XMapUrl = "";
            this.mapView.ZoomLevel = 16D;
            // 
            // loadRequestBtn
            // 
            this.loadRequestBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadRequestBtn.Location = new System.Drawing.Point(480, 42);
            this.loadRequestBtn.Name = "loadRequestBtn";
            this.loadRequestBtn.Size = new System.Drawing.Size(163, 29);
            this.loadRequestBtn.TabIndex = 36;
            this.loadRequestBtn.Text = "Load data from Request";
            this.loadRequestBtn.UseVisualStyleBackColor = true;
            this.loadRequestBtn.Click += new System.EventHandler(this.loadRequestBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "xMap";
            // 
            // xLocateCheckBox
            // 
            this.xLocateCheckBox.AutoSize = true;
            this.xLocateCheckBox.Location = new System.Drawing.Point(12, 92);
            this.xLocateCheckBox.Name = "xLocateCheckBox";
            this.xLocateCheckBox.Size = new System.Drawing.Size(64, 17);
            this.xLocateCheckBox.TabIndex = 20;
            this.xLocateCheckBox.Text = "xLocate";
            this.xLocateCheckBox.UseVisualStyleBackColor = true;
            // 
            // xLocateURLTextBox
            // 
            this.xLocateURLTextBox.Location = new System.Drawing.Point(108, 90);
            this.xLocateURLTextBox.Name = "xLocateURLTextBox";
            this.xLocateURLTextBox.Size = new System.Drawing.Size(259, 20);
            this.xLocateURLTextBox.TabIndex = 25;
            this.xLocateURLTextBox.Text = "http://localhost:50020/xlocate/ws/XLocate";
            // 
            // xLocateProfileTextBox
            // 
            this.xLocateProfileTextBox.Location = new System.Drawing.Point(373, 90);
            this.xLocateProfileTextBox.Name = "xLocateProfileTextBox";
            this.xLocateProfileTextBox.Size = new System.Drawing.Size(100, 20);
            this.xLocateProfileTextBox.TabIndex = 29;
            // 
            // xRouteProfileTextBox
            // 
            this.xRouteProfileTextBox.Location = new System.Drawing.Point(373, 116);
            this.xRouteProfileTextBox.Name = "xRouteProfileTextBox";
            this.xRouteProfileTextBox.Size = new System.Drawing.Size(100, 20);
            this.xRouteProfileTextBox.TabIndex = 40;
            this.mainFormToolTip.SetToolTip(this.xRouteProfileTextBox, "Defines the xRoute profile to be used in the calculation. If the profile starts w" +
        "ith a $, a local profile will be loaded and send ans XML snippet.");
            // 
            // xRouteURLTextBox
            // 
            this.xRouteURLTextBox.Location = new System.Drawing.Point(108, 116);
            this.xRouteURLTextBox.Name = "xRouteURLTextBox";
            this.xRouteURLTextBox.Size = new System.Drawing.Size(259, 20);
            this.xRouteURLTextBox.TabIndex = 39;
            this.xRouteURLTextBox.Text = "http://localhost:50030/xroute/ws/XRoute";
            // 
            // xRouteCheckBox
            // 
            this.xRouteCheckBox.AutoSize = true;
            this.xRouteCheckBox.Location = new System.Drawing.Point(12, 118);
            this.xRouteCheckBox.Name = "xRouteCheckBox";
            this.xRouteCheckBox.Size = new System.Drawing.Size(60, 17);
            this.xRouteCheckBox.TabIndex = 38;
            this.xRouteCheckBox.Text = "xRoute";
            this.xRouteCheckBox.UseVisualStyleBackColor = true;
            // 
            // explanationLbl
            // 
            this.explanationLbl.AutoSize = true;
            this.explanationLbl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.explanationLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.explanationLbl.Location = new System.Drawing.Point(649, 9);
            this.explanationLbl.MaximumSize = new System.Drawing.Size(435, 100);
            this.explanationLbl.Name = "explanationLbl";
            this.explanationLbl.Size = new System.Drawing.Size(426, 93);
            this.explanationLbl.TabIndex = 41;
            this.explanationLbl.Text = resources.GetString("explanationLbl.Text");
            this.explanationLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // csvHeaderChckBx
            // 
            this.csvHeaderChckBx.AutoSize = true;
            this.csvHeaderChckBx.Checked = true;
            this.csvHeaderChckBx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.csvHeaderChckBx.Location = new System.Drawing.Point(1096, 7);
            this.csvHeaderChckBx.Name = "csvHeaderChckBx";
            this.csvHeaderChckBx.Size = new System.Drawing.Size(85, 17);
            this.csvHeaderChckBx.TabIndex = 42;
            this.csvHeaderChckBx.Text = "CSV Header";
            this.csvHeaderChckBx.UseVisualStyleBackColor = true;
            // 
            // idPresentChckBx
            // 
            this.idPresentChckBx.AutoSize = true;
            this.idPresentChckBx.Checked = true;
            this.idPresentChckBx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.idPresentChckBx.Location = new System.Drawing.Point(1096, 27);
            this.idPresentChckBx.Name = "idPresentChckBx";
            this.idPresentChckBx.Size = new System.Drawing.Size(74, 17);
            this.idPresentChckBx.TabIndex = 43;
            this.idPresentChckBx.Text = "Id Present";
            this.idPresentChckBx.UseVisualStyleBackColor = true;
            // 
            // commaSeparatorChckBx
            // 
            this.commaSeparatorChckBx.AutoSize = true;
            this.commaSeparatorChckBx.Location = new System.Drawing.Point(1096, 50);
            this.commaSeparatorChckBx.Name = "commaSeparatorChckBx";
            this.commaSeparatorChckBx.Size = new System.Drawing.Size(110, 17);
            this.commaSeparatorChckBx.TabIndex = 44;
            this.commaSeparatorChckBx.Text = "Comma Separator";
            this.commaSeparatorChckBx.UseVisualStyleBackColor = true;
            // 
            // decimalPointChckBx
            // 
            this.decimalPointChckBx.AutoSize = true;
            this.decimalPointChckBx.Location = new System.Drawing.Point(1096, 73);
            this.decimalPointChckBx.Name = "decimalPointChckBx";
            this.decimalPointChckBx.Size = new System.Drawing.Size(91, 17);
            this.decimalPointChckBx.TabIndex = 45;
            this.decimalPointChckBx.Text = "Decimal Point";
            this.decimalPointChckBx.UseVisualStyleBackColor = true;
            // 
            // tollChckBx
            // 
            this.tollChckBx.AutoSize = true;
            this.tollChckBx.Location = new System.Drawing.Point(495, 118);
            this.tollChckBx.Name = "tollChckBx";
            this.tollChckBx.Size = new System.Drawing.Size(69, 17);
            this.tollChckBx.TabIndex = 46;
            this.tollChckBx.Text = "Show toll";
            this.tollChckBx.UseVisualStyleBackColor = true;
            // 
            // emissionsChkBx
            // 
            this.emissionsChkBx.AutoSize = true;
            this.emissionsChkBx.Location = new System.Drawing.Point(570, 118);
            this.emissionsChkBx.Name = "emissionsChkBx";
            this.emissionsChkBx.Size = new System.Drawing.Size(104, 17);
            this.emissionsChkBx.TabIndex = 47;
            this.emissionsChkBx.Text = "Show emmisions";
            this.emissionsChkBx.UseVisualStyleBackColor = true;
            // 
            // makeOwnTimestampChkBx
            // 
            this.makeOwnTimestampChkBx.AutoSize = true;
            this.makeOwnTimestampChkBx.Location = new System.Drawing.Point(1228, 7);
            this.makeOwnTimestampChkBx.Name = "makeOwnTimestampChkBx";
            this.makeOwnTimestampChkBx.Size = new System.Drawing.Size(126, 17);
            this.makeOwnTimestampChkBx.TabIndex = 48;
            this.makeOwnTimestampChkBx.Text = "Make own timestamp";
            this.makeOwnTimestampChkBx.UseVisualStyleBackColor = true;
            // 
            // recalculateHeadingChkBx
            // 
            this.recalculateHeadingChkBx.AutoSize = true;
            this.recalculateHeadingChkBx.Location = new System.Drawing.Point(1096, 96);
            this.recalculateHeadingChkBx.Name = "recalculateHeadingChkBx";
            this.recalculateHeadingChkBx.Size = new System.Drawing.Size(126, 17);
            this.recalculateHeadingChkBx.TabIndex = 52;
            this.recalculateHeadingChkBx.Text = "Recalculate Heading";
            this.recalculateHeadingChkBx.UseVisualStyleBackColor = true;
            // 
            // filterStandStillCheckBox
            // 
            this.filterStandStillCheckBox.AutoSize = true;
            this.filterStandStillCheckBox.Checked = true;
            this.filterStandStillCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterStandStillCheckBox.Location = new System.Drawing.Point(1228, 99);
            this.filterStandStillCheckBox.Name = "filterStandStillCheckBox";
            this.filterStandStillCheckBox.Size = new System.Drawing.Size(94, 17);
            this.filterStandStillCheckBox.TabIndex = 66;
            this.filterStandStillCheckBox.Text = "Filter stand still";
            this.mainFormToolTip.SetToolTip(this.filterStandStillCheckBox, "If checked filters out standstill positions when reading data from CSV file. It w" +
        "ill keep the first standstill point, but ingore consecutive standstill points.");
            this.filterStandStillCheckBox.UseVisualStyleBackColor = true;
            // 
            // routeSanityCheckCheckBox
            // 
            this.routeSanityCheckCheckBox.AutoSize = true;
            this.routeSanityCheckCheckBox.Location = new System.Drawing.Point(1228, 118);
            this.routeSanityCheckCheckBox.Name = "routeSanityCheckCheckBox";
            this.routeSanityCheckCheckBox.Size = new System.Drawing.Size(121, 17);
            this.routeSanityCheckCheckBox.TabIndex = 68;
            this.routeSanityCheckCheckBox.Text = "Route Sanity Check";
            this.mainFormToolTip.SetToolTip(this.routeSanityCheckCheckBox, resources.GetString("routeSanityCheckCheckBox.ToolTip"));
            this.routeSanityCheckCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 374);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "xRoute distance";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "xMapmatch distance";
            // 
            // xRouteDistanceTxtBx
            // 
            this.xRouteDistanceTxtBx.Enabled = false;
            this.xRouteDistanceTxtBx.Location = new System.Drawing.Point(410, 371);
            this.xRouteDistanceTxtBx.Name = "xRouteDistanceTxtBx";
            this.xRouteDistanceTxtBx.Size = new System.Drawing.Size(77, 20);
            this.xRouteDistanceTxtBx.TabIndex = 57;
            // 
            // xMapmatchDistanceTxtBx
            // 
            this.xMapmatchDistanceTxtBx.Enabled = false;
            this.xMapmatchDistanceTxtBx.Location = new System.Drawing.Point(237, 371);
            this.xMapmatchDistanceTxtBx.Name = "xMapmatchDistanceTxtBx";
            this.xMapmatchDistanceTxtBx.Size = new System.Drawing.Size(77, 20);
            this.xMapmatchDistanceTxtBx.TabIndex = 58;
            // 
            // zoomOnSelctChkbx
            // 
            this.zoomOnSelctChkbx.AutoSize = true;
            this.zoomOnSelctChkbx.Location = new System.Drawing.Point(12, 373);
            this.zoomOnSelctChkbx.Name = "zoomOnSelctChkbx";
            this.zoomOnSelctChkbx.Size = new System.Drawing.Size(99, 17);
            this.zoomOnSelctChkbx.TabIndex = 60;
            this.zoomOnSelctChkbx.Text = "Zoom on select";
            this.zoomOnSelctChkbx.UseVisualStyleBackColor = true;
            // 
            // nmeaBtn
            // 
            this.nmeaBtn.Location = new System.Drawing.Point(1361, 7);
            this.nmeaBtn.Name = "nmeaBtn";
            this.nmeaBtn.Size = new System.Drawing.Size(103, 46);
            this.nmeaBtn.TabIndex = 61;
            this.nmeaBtn.Text = "Load data from NMEA file";
            this.nmeaBtn.UseVisualStyleBackColor = true;
            this.nmeaBtn.Click += new System.EventHandler(this.nmeaBtn_Click);
            // 
            // pollingIntervalTxtBx
            // 
            this.pollingIntervalTxtBx.Location = new System.Drawing.Point(1361, 81);
            this.pollingIntervalTxtBx.Name = "pollingIntervalTxtBx";
            this.pollingIntervalTxtBx.Size = new System.Drawing.Size(100, 20);
            this.pollingIntervalTxtBx.TabIndex = 62;
            this.pollingIntervalTxtBx.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1358, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Polling interval:";
            // 
            // exportRouteBtn
            // 
            this.exportRouteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportRouteBtn.Location = new System.Drawing.Point(1359, 373);
            this.exportRouteBtn.Name = "exportRouteBtn";
            this.exportRouteBtn.Size = new System.Drawing.Size(100, 23);
            this.exportRouteBtn.TabIndex = 65;
            this.exportRouteBtn.Text = "Export Route";
            this.exportRouteBtn.UseVisualStyleBackColor = true;
            this.exportRouteBtn.Click += new System.EventHandler(this.exportRouteBtn_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ResultAddress";
            this.dataGridViewTextBoxColumn1.HeaderText = "ResultAddress";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // correctTimestampCheckbox
            // 
            this.correctTimestampCheckbox.AutoSize = true;
            this.correctTimestampCheckbox.Location = new System.Drawing.Point(1096, 119);
            this.correctTimestampCheckbox.Name = "correctTimestampCheckbox";
            this.correctTimestampCheckbox.Size = new System.Drawing.Size(110, 17);
            this.correctTimestampCheckbox.TabIndex = 67;
            this.correctTimestampCheckbox.Text = "Correct timestamp";
            this.correctTimestampCheckbox.UseVisualStyleBackColor = true;
            // 
            // exportResultsButton
            // 
            this.exportResultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportResultsButton.Location = new System.Drawing.Point(1254, 373);
            this.exportResultsButton.Name = "exportResultsButton";
            this.exportResultsButton.Size = new System.Drawing.Size(100, 23);
            this.exportResultsButton.TabIndex = 69;
            this.exportResultsButton.Text = "Export Results";
            this.exportResultsButton.UseVisualStyleBackColor = true;
            this.exportResultsButton.Click += new System.EventHandler(this.exportResultsButton_Click);
            // 
            // xRouteTimeTextBox
            // 
            this.xRouteTimeTextBox.Enabled = false;
            this.xRouteTimeTextBox.Location = new System.Drawing.Point(562, 371);
            this.xRouteTimeTextBox.Name = "xRouteTimeTextBox";
            this.xRouteTimeTextBox.Size = new System.Drawing.Size(112, 20);
            this.xRouteTimeTextBox.TabIndex = 71;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(493, 374);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "xRoute time";
            // 
            // filterDoubleCheckBox
            // 
            this.filterDoubleCheckBox.AutoSize = true;
            this.filterDoubleCheckBox.Location = new System.Drawing.Point(1228, 27);
            this.filterDoubleCheckBox.Name = "filterDoubleCheckBox";
            this.filterDoubleCheckBox.Size = new System.Drawing.Size(121, 17);
            this.filterDoubleCheckBox.TabIndex = 72;
            this.filterDoubleCheckBox.Text = "Filter double records";
            this.filterDoubleCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 742);
            this.Controls.Add(this.filterDoubleCheckBox);
            this.Controls.Add(this.xRouteTimeTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.exportResultsButton);
            this.Controls.Add(this.routeSanityCheckCheckBox);
            this.Controls.Add(this.correctTimestampCheckbox);
            this.Controls.Add(this.filterStandStillCheckBox);
            this.Controls.Add(this.exportRouteBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pollingIntervalTxtBx);
            this.Controls.Add(this.nmeaBtn);
            this.Controls.Add(this.zoomOnSelctChkbx);
            this.Controls.Add(this.xMapmatchDistanceTxtBx);
            this.Controls.Add(this.xRouteDistanceTxtBx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.recalculateHeadingChkBx);
            this.Controls.Add(this.makeOwnTimestampChkBx);
            this.Controls.Add(this.emissionsChkBx);
            this.Controls.Add(this.tollChckBx);
            this.Controls.Add(this.decimalPointChckBx);
            this.Controls.Add(this.commaSeparatorChckBx);
            this.Controls.Add(this.idPresentChckBx);
            this.Controls.Add(this.csvHeaderChckBx);
            this.Controls.Add(this.explanationLbl);
            this.Controls.Add(this.xRouteProfileTextBox);
            this.Controls.Add(this.xRouteURLTextBox);
            this.Controls.Add(this.xRouteCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadRequestBtn);
            this.Controls.Add(this.mapView);
            this.Controls.Add(this.matchDataBtn);
            this.Controls.Add(this.loadDataBtn);
            this.Controls.Add(this.dataPointGridView);
            this.Controls.Add(this.xMapmatchProfileTextBox);
            this.Controls.Add(this.xLocateProfileTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xMapProfileTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xLocateURLTextBox);
            this.Controls.Add(this.xMapURLTextBox);
            this.Controls.Add(this.xMapmatchURLTextBox);
            this.Controls.Add(this.xMapmatchLabel);
            this.Controls.Add(this.xLocateCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "XMapmatch test client";
            ((System.ComponentModel.ISupportInitialize)(this.dataPointGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xMapmatchProfileTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox xMapProfileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xMapURLTextBox;
        private System.Windows.Forms.TextBox xMapmatchURLTextBox;
        private System.Windows.Forms.Label xMapmatchLabel;
        private System.Windows.Forms.DataGridView dataPointGridView;
        private System.Windows.Forms.Button loadDataBtn;
        private System.Windows.Forms.Button matchDataBtn;
        private Ptv.XServer.Controls.Map.FormsMap mapView;
        private System.Windows.Forms.Button loadRequestBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox xLocateCheckBox;
        private System.Windows.Forms.TextBox xLocateURLTextBox;
        private System.Windows.Forms.TextBox xLocateProfileTextBox;
        private System.Windows.Forms.TextBox xRouteProfileTextBox;
        private System.Windows.Forms.TextBox xRouteURLTextBox;
        private System.Windows.Forms.CheckBox xRouteCheckBox;
        private System.Windows.Forms.Label explanationLbl;
        private System.Windows.Forms.CheckBox csvHeaderChckBx;
        private System.Windows.Forms.CheckBox idPresentChckBx;
        private System.Windows.Forms.CheckBox commaSeparatorChckBx;
        private System.Windows.Forms.CheckBox decimalPointChckBx;
        private System.Windows.Forms.CheckBox tollChckBx;
        private System.Windows.Forms.CheckBox emissionsChkBx;
        private System.Windows.Forms.CheckBox makeOwnTimestampChkBx;
        private System.Windows.Forms.CheckBox recalculateHeadingChkBx;
        private System.Windows.Forms.ToolTip mainFormToolTip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox xRouteDistanceTxtBx;
        private System.Windows.Forms.TextBox xMapmatchDistanceTxtBx;
        private System.Windows.Forms.CheckBox zoomOnSelctChkbx;
        private System.Windows.Forms.BindingSource dataPointBindingSource;
        private System.Windows.Forms.Button nmeaBtn;
        private System.Windows.Forms.TextBox pollingIntervalTxtBx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button exportRouteBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.CheckBox filterStandStillCheckBox;
        private System.Windows.Forms.CheckBox correctTimestampCheckbox;
        private System.Windows.Forms.CheckBox routeSanityCheckCheckBox;
        private System.Windows.Forms.Button exportResultsButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latInputDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lonInputDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn speedInMpsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn headingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn speedInKmphDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latOutputDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lonOutputDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn speedLimitForwardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn speedLimitBackwardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn countryCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn networkClassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn localRatingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn probabilityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transitionRatingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkingDistanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn angleDifferenceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoveredDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrivenDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Stable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LocalMatching;
        private System.Windows.Forms.TextBox xRouteTimeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox filterDoubleCheckBox;
    }
}

