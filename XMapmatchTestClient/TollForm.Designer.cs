namespace XMapmatchTestClient
{
    partial class TollForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TollForm));
            this.countryInfoLstBx = new System.Windows.Forms.ListBox();
            this.perTypeTollPriceLstBx = new System.Windows.Forms.ListBox();
            this.perTypeTollPriceLbl = new System.Windows.Forms.Label();
            this.perTypeTollDistanceLbl = new System.Windows.Forms.Label();
            this.perTypeTollDistanceLstBx = new System.Windows.Forms.ListBox();
            this.tollCostInfoDataGridView = new System.Windows.Forms.DataGridView();
            this.tollStationFromDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollStationToDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.streetNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollDistanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollProviderIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollProviderNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollSectionIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollSectionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tollTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicleTarifIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.waypointIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waypointIndexSpecifiedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tollCostInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.countryInfosLbl = new System.Windows.Forms.Label();
            this.tollCostInfosLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tollCostInfoDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tollCostInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // countryInfoLstBx
            // 
            this.countryInfoLstBx.FormattingEnabled = true;
            this.countryInfoLstBx.Location = new System.Drawing.Point(12, 38);
            this.countryInfoLstBx.Name = "countryInfoLstBx";
            this.countryInfoLstBx.Size = new System.Drawing.Size(183, 160);
            this.countryInfoLstBx.TabIndex = 0;
            this.countryInfoLstBx.SelectedIndexChanged += new System.EventHandler(this.countryInfoLstBx_SelectedIndexChanged);
            // 
            // perTypeTollPriceLstBx
            // 
            this.perTypeTollPriceLstBx.FormattingEnabled = true;
            this.perTypeTollPriceLstBx.Location = new System.Drawing.Point(201, 38);
            this.perTypeTollPriceLstBx.Name = "perTypeTollPriceLstBx";
            this.perTypeTollPriceLstBx.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.perTypeTollPriceLstBx.Size = new System.Drawing.Size(301, 160);
            this.perTypeTollPriceLstBx.TabIndex = 1;
            // 
            // perTypeTollPriceLbl
            // 
            this.perTypeTollPriceLbl.AutoSize = true;
            this.perTypeTollPriceLbl.Location = new System.Drawing.Point(201, 22);
            this.perTypeTollPriceLbl.Name = "perTypeTollPriceLbl";
            this.perTypeTollPriceLbl.Size = new System.Drawing.Size(87, 13);
            this.perTypeTollPriceLbl.TabIndex = 2;
            this.perTypeTollPriceLbl.Text = "perTypeTollPrice";
            // 
            // perTypeTollDistanceLbl
            // 
            this.perTypeTollDistanceLbl.AutoSize = true;
            this.perTypeTollDistanceLbl.Location = new System.Drawing.Point(505, 22);
            this.perTypeTollDistanceLbl.Name = "perTypeTollDistanceLbl";
            this.perTypeTollDistanceLbl.Size = new System.Drawing.Size(105, 13);
            this.perTypeTollDistanceLbl.TabIndex = 4;
            this.perTypeTollDistanceLbl.Text = "perTypeTollDistance";
            // 
            // perTypeTollDistanceLstBx
            // 
            this.perTypeTollDistanceLstBx.FormattingEnabled = true;
            this.perTypeTollDistanceLstBx.Location = new System.Drawing.Point(508, 38);
            this.perTypeTollDistanceLstBx.Name = "perTypeTollDistanceLstBx";
            this.perTypeTollDistanceLstBx.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.perTypeTollDistanceLstBx.Size = new System.Drawing.Size(301, 160);
            this.perTypeTollDistanceLstBx.TabIndex = 3;
            // 
            // tollCostInfoDataGridView
            // 
            this.tollCostInfoDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tollCostInfoDataGridView.AutoGenerateColumns = false;
            this.tollCostInfoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tollCostInfoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tollStationFromDataGridViewTextBoxColumn,
            this.tollStationToDataGridViewTextBoxColumn,
            this.currencyDataGridViewTextBoxColumn,
            this.currencyNameDataGridViewTextBoxColumn,
            this.streetNameDataGridViewTextBoxColumn,
            this.tollDistanceDataGridViewTextBoxColumn,
            this.tollPriceDataGridViewTextBoxColumn,
            this.tollProviderIDDataGridViewTextBoxColumn,
            this.tollProviderNameDataGridViewTextBoxColumn,
            this.tollSectionIDDataGridViewTextBoxColumn,
            this.tollSectionNameDataGridViewTextBoxColumn,
            this.tollTypeDataGridViewTextBoxColumn,
            this.vehicleTarifIDDataGridViewTextBoxColumn,
            this.vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn,
            this.waypointIndexDataGridViewTextBoxColumn,
            this.waypointIndexSpecifiedDataGridViewCheckBoxColumn});
            this.tollCostInfoDataGridView.DataSource = this.tollCostInfoBindingSource;
            this.tollCostInfoDataGridView.Location = new System.Drawing.Point(12, 217);
            this.tollCostInfoDataGridView.Name = "tollCostInfoDataGridView";
            this.tollCostInfoDataGridView.Size = new System.Drawing.Size(797, 348);
            this.tollCostInfoDataGridView.TabIndex = 5;
            // 
            // tollStationFromDataGridViewTextBoxColumn
            // 
            this.tollStationFromDataGridViewTextBoxColumn.DataPropertyName = "tollStationFrom";
            this.tollStationFromDataGridViewTextBoxColumn.HeaderText = "tollStationFrom";
            this.tollStationFromDataGridViewTextBoxColumn.Name = "tollStationFromDataGridViewTextBoxColumn";
            // 
            // tollStationToDataGridViewTextBoxColumn
            // 
            this.tollStationToDataGridViewTextBoxColumn.DataPropertyName = "tollStationTo";
            this.tollStationToDataGridViewTextBoxColumn.HeaderText = "tollStationTo";
            this.tollStationToDataGridViewTextBoxColumn.Name = "tollStationToDataGridViewTextBoxColumn";
            // 
            // currencyDataGridViewTextBoxColumn
            // 
            this.currencyDataGridViewTextBoxColumn.DataPropertyName = "currency";
            this.currencyDataGridViewTextBoxColumn.HeaderText = "currency";
            this.currencyDataGridViewTextBoxColumn.Name = "currencyDataGridViewTextBoxColumn";
            // 
            // currencyNameDataGridViewTextBoxColumn
            // 
            this.currencyNameDataGridViewTextBoxColumn.DataPropertyName = "currencyName";
            this.currencyNameDataGridViewTextBoxColumn.HeaderText = "currencyName";
            this.currencyNameDataGridViewTextBoxColumn.Name = "currencyNameDataGridViewTextBoxColumn";
            // 
            // streetNameDataGridViewTextBoxColumn
            // 
            this.streetNameDataGridViewTextBoxColumn.DataPropertyName = "streetName";
            this.streetNameDataGridViewTextBoxColumn.HeaderText = "streetName";
            this.streetNameDataGridViewTextBoxColumn.Name = "streetNameDataGridViewTextBoxColumn";
            // 
            // tollDistanceDataGridViewTextBoxColumn
            // 
            this.tollDistanceDataGridViewTextBoxColumn.DataPropertyName = "tollDistance";
            this.tollDistanceDataGridViewTextBoxColumn.HeaderText = "tollDistance";
            this.tollDistanceDataGridViewTextBoxColumn.Name = "tollDistanceDataGridViewTextBoxColumn";
            // 
            // tollPriceDataGridViewTextBoxColumn
            // 
            this.tollPriceDataGridViewTextBoxColumn.DataPropertyName = "tollPrice";
            this.tollPriceDataGridViewTextBoxColumn.HeaderText = "tollPrice";
            this.tollPriceDataGridViewTextBoxColumn.Name = "tollPriceDataGridViewTextBoxColumn";
            // 
            // tollProviderIDDataGridViewTextBoxColumn
            // 
            this.tollProviderIDDataGridViewTextBoxColumn.DataPropertyName = "tollProviderID";
            this.tollProviderIDDataGridViewTextBoxColumn.HeaderText = "tollProviderID";
            this.tollProviderIDDataGridViewTextBoxColumn.Name = "tollProviderIDDataGridViewTextBoxColumn";
            // 
            // tollProviderNameDataGridViewTextBoxColumn
            // 
            this.tollProviderNameDataGridViewTextBoxColumn.DataPropertyName = "tollProviderName";
            this.tollProviderNameDataGridViewTextBoxColumn.HeaderText = "tollProviderName";
            this.tollProviderNameDataGridViewTextBoxColumn.Name = "tollProviderNameDataGridViewTextBoxColumn";
            // 
            // tollSectionIDDataGridViewTextBoxColumn
            // 
            this.tollSectionIDDataGridViewTextBoxColumn.DataPropertyName = "tollSectionID";
            this.tollSectionIDDataGridViewTextBoxColumn.HeaderText = "tollSectionID";
            this.tollSectionIDDataGridViewTextBoxColumn.Name = "tollSectionIDDataGridViewTextBoxColumn";
            // 
            // tollSectionNameDataGridViewTextBoxColumn
            // 
            this.tollSectionNameDataGridViewTextBoxColumn.DataPropertyName = "tollSectionName";
            this.tollSectionNameDataGridViewTextBoxColumn.HeaderText = "tollSectionName";
            this.tollSectionNameDataGridViewTextBoxColumn.Name = "tollSectionNameDataGridViewTextBoxColumn";
            // 
            // tollTypeDataGridViewTextBoxColumn
            // 
            this.tollTypeDataGridViewTextBoxColumn.DataPropertyName = "tollType";
            this.tollTypeDataGridViewTextBoxColumn.HeaderText = "tollType";
            this.tollTypeDataGridViewTextBoxColumn.Name = "tollTypeDataGridViewTextBoxColumn";
            // 
            // vehicleTarifIDDataGridViewTextBoxColumn
            // 
            this.vehicleTarifIDDataGridViewTextBoxColumn.DataPropertyName = "vehicleTarifID";
            this.vehicleTarifIDDataGridViewTextBoxColumn.HeaderText = "vehicleTarifID";
            this.vehicleTarifIDDataGridViewTextBoxColumn.Name = "vehicleTarifIDDataGridViewTextBoxColumn";
            // 
            // vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn
            // 
            this.vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn.DataPropertyName = "vehicleTarifIDSpecified";
            this.vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn.HeaderText = "vehicleTarifIDSpecified";
            this.vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn.Name = "vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn";
            // 
            // waypointIndexDataGridViewTextBoxColumn
            // 
            this.waypointIndexDataGridViewTextBoxColumn.DataPropertyName = "waypointIndex";
            this.waypointIndexDataGridViewTextBoxColumn.HeaderText = "waypointIndex";
            this.waypointIndexDataGridViewTextBoxColumn.Name = "waypointIndexDataGridViewTextBoxColumn";
            // 
            // waypointIndexSpecifiedDataGridViewCheckBoxColumn
            // 
            this.waypointIndexSpecifiedDataGridViewCheckBoxColumn.DataPropertyName = "waypointIndexSpecified";
            this.waypointIndexSpecifiedDataGridViewCheckBoxColumn.HeaderText = "waypointIndexSpecified";
            this.waypointIndexSpecifiedDataGridViewCheckBoxColumn.Name = "waypointIndexSpecifiedDataGridViewCheckBoxColumn";
            // 
            // tollCostInfoBindingSource
            // 
            this.tollCostInfoBindingSource.DataSource = typeof(XServer.TollCostInfo);
            // 
            // countryInfosLbl
            // 
            this.countryInfosLbl.AutoSize = true;
            this.countryInfosLbl.Location = new System.Drawing.Point(12, 22);
            this.countryInfosLbl.Name = "countryInfosLbl";
            this.countryInfosLbl.Size = new System.Drawing.Size(65, 13);
            this.countryInfosLbl.TabIndex = 6;
            this.countryInfosLbl.Text = "countryInfos";
            // 
            // tollCostInfosLbl
            // 
            this.tollCostInfosLbl.AutoSize = true;
            this.tollCostInfosLbl.Location = new System.Drawing.Point(12, 201);
            this.tollCostInfosLbl.Name = "tollCostInfosLbl";
            this.tollCostInfosLbl.Size = new System.Drawing.Size(64, 13);
            this.tollCostInfosLbl.TabIndex = 7;
            this.tollCostInfosLbl.Text = "tollCostInfos";
            // 
            // TollForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 577);
            this.Controls.Add(this.tollCostInfosLbl);
            this.Controls.Add(this.countryInfosLbl);
            this.Controls.Add(this.tollCostInfoDataGridView);
            this.Controls.Add(this.perTypeTollDistanceLbl);
            this.Controls.Add(this.perTypeTollDistanceLstBx);
            this.Controls.Add(this.perTypeTollPriceLbl);
            this.Controls.Add(this.perTypeTollPriceLstBx);
            this.Controls.Add(this.countryInfoLstBx);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TollForm";
            this.Text = "TollForm";
            ((System.ComponentModel.ISupportInitialize)(this.tollCostInfoDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tollCostInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox countryInfoLstBx;
        private System.Windows.Forms.ListBox perTypeTollPriceLstBx;
        private System.Windows.Forms.Label perTypeTollPriceLbl;
        private System.Windows.Forms.Label perTypeTollDistanceLbl;
        private System.Windows.Forms.ListBox perTypeTollDistanceLstBx;
        private System.Windows.Forms.DataGridView tollCostInfoDataGridView;
        private System.Windows.Forms.Label countryInfosLbl;
        private System.Windows.Forms.Label tollCostInfosLbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollStationFromDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollStationToDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn streetNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollDistanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollProviderIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollProviderNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollSectionIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollSectionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicleTarifIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn vehicleTarifIDSpecifiedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn waypointIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn waypointIndexSpecifiedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource tollCostInfoBindingSource;
    }
}