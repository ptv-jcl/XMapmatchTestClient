using System;
using System.Collections.Generic;
using System.Windows.Forms;

using XServer;

namespace XMapmatchTestClient
{
    public partial class TollForm : Form
    {
        private CountryInfo[] countryInfos;

        private TollForm()
        {
        }

        public TollForm(CountryInfo[] countryInfos)
        {
            InitializeComponent();
            this.countryInfos = countryInfos;
            foreach (CountryInfo countryInfo in countryInfos)
            {
                countryInfoLstBx.Items.Add(countryInfo);
            }
        }

        public void PopulatePerTypeTollListBoxes(CountryInfo countryInfo)
        {
            perTypeTollDistanceLstBx.Items.Clear();
            perTypeTollPriceLstBx.Items.Clear();
            if (countryInfo == null)
                return;
            string[] tollTypeNames = Enum.GetNames(typeof(TollType));
            List<string> perTypeTollStringList = new List<string>();
            for (int i = 0; i < tollTypeNames.Length - 1; i++)
            {
                perTypeTollDistanceLstBx.Items.Add(tollTypeNames[i] + ": " + countryInfo.wrappedPerTypeTollDistance[i]);
                perTypeTollPriceLstBx.Items.Add(tollTypeNames[i] + ": " + countryInfo.wrappedPerTypeTollPrice[i]);
            }
        }

        private void countryInfoLstBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountryInfo countryInfo = (CountryInfo)countryInfoLstBx.SelectedItem;
            PopulatePerTypeTollListBoxes(countryInfo);
            tollCostInfoDataGridView.DataSource = countryInfo == null ? null : countryInfo.wrappedTollCostInfos;
        }
    }
}