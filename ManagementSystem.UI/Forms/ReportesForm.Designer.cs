namespace ManagementSystem.UI.Forms
{
    partial class ReportesForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlTop = new Panel();
            txtSearch = new TextBox();
            lblSearch = new Label();
            cmbReportType = new ComboBox();
            lblReportType = new Label();
            pnlDetail = new Panel();
            lblStatMin = new Label();
            lblStatMax = new Label();
            lblStatAvg = new Label();
            lblStatSum = new Label();
            lblStatCount = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvReports = new DataGridView();
            colCol1 = new DataGridViewTextBoxColumn();
            colCol2 = new DataGridViewTextBoxColumn();
            colCol3 = new DataGridViewTextBoxColumn();
            colCol4 = new DataGridViewTextBoxColumn();
            colCol5 = new DataGridViewTextBoxColumn();
            pnlTop.SuspendLayout();
            pnlDetail.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Controls.Add(cmbReportType);
            pnlTop.Controls.Add(lblReportType);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(15, 15);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15);
            pnlTop.Size = new Size(1070, 65);
            pnlTop.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(625, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Filtrar por coincidencia...";
            txtSearch.Size = new Size(250, 29);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(75, 85, 99);
            lblSearch.Location = new Point(505, 21);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(112, 20);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Buscar Texto:";
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.Font = new Font("Segoe UI", 9F);
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Items.AddRange(new object[]
            {
                "Productos Ordenados por Precio y Nombre",
                "Productos con Stock Bajo",
                "Total de Ventas por Cliente"
            });
            cmbReportType.Location = new Point(165, 18);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(315, 28);
            cmbReportType.TabIndex = 1;
            cmbReportType.SelectedIndexChanged += cmbReportType_SelectedIndexChanged;
            // 
            // lblReportType
            // 
            lblReportType.AutoSize = true;
            lblReportType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblReportType.ForeColor = Color.FromArgb(30, 58, 138);
            lblReportType.Location = new Point(15, 21);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(142, 21);
            lblReportType.TabIndex = 0;
            lblReportType.Text = "Tipo de Reporte:";
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(lblStatMin);
            pnlDetail.Controls.Add(lblStatMax);
            pnlDetail.Controls.Add(lblStatAvg);
            pnlDetail.Controls.Add(lblStatSum);
            pnlDetail.Controls.Add(lblStatCount);
            pnlDetail.Controls.Add(lblDetailHeader);
            pnlDetail.Dock = DockStyle.Right;
            pnlDetail.Location = new Point(720, 80);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(15);
            pnlDetail.Size = new Size(365, 605);
            pnlDetail.TabIndex = 1;
            // 
            // lblStatMin
            // 
            lblStatMin.AutoSize = true;
            lblStatMin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatMin.ForeColor = Color.FromArgb(55, 65, 81);
            lblStatMin.Location = new Point(20, 250);
            lblStatMin.Name = "lblStatMin";
            lblStatMin.Size = new Size(165, 23);
            lblStatMin.TabIndex = 5;
            lblStatMin.Text = "Valor mínimo: $0.00";
            // 
            // lblStatMax
            // 
            lblStatMax.AutoSize = true;
            lblStatMax.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatMax.ForeColor = Color.FromArgb(55, 65, 81);
            lblStatMax.Location = new Point(20, 205);
            lblStatMax.Name = "lblStatMax";
            lblStatMax.Size = new Size(170, 23);
            lblStatMax.TabIndex = 4;
            lblStatMax.Text = "Valor máximo: $0.00";
            // 
            // lblStatAvg
            // 
            lblStatAvg.AutoSize = true;
            lblStatAvg.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatAvg.ForeColor = Color.FromArgb(16, 185, 129);
            lblStatAvg.Location = new Point(20, 160);
            lblStatAvg.Name = "lblStatAvg";
            lblStatAvg.Size = new Size(185, 23);
            lblStatAvg.TabIndex = 3;
            lblStatAvg.Text = "Promedio: $0.00";
            // 
            // lblStatSum
            // 
            lblStatSum.AutoSize = true;
            lblStatSum.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblStatSum.ForeColor = Color.FromArgb(30, 58, 138);
            lblStatSum.Location = new Point(20, 115);
            lblStatSum.Name = "lblStatSum";
            lblStatSum.Size = new Size(175, 24);
            lblStatSum.TabIndex = 2;
            lblStatSum.Text = "Sumatoria total: $0.00";
            // 
            // lblStatCount
            // 
            lblStatCount.AutoSize = true;
            lblStatCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatCount.ForeColor = Color.FromArgb(55, 65, 81);
            lblStatCount.Location = new Point(20, 70);
            lblStatCount.Name = "lblStatCount";
            lblStatCount.Size = new Size(160, 23);
            lblStatCount.TabIndex = 1;
            lblStatCount.Text = "Cantidad de registros: 0";
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = true;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(15, 15);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(218, 25);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Resumen Estadístico";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvReports);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(15, 80);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 0, 15, 0);
            pnlGrid.Size = new Size(705, 605);
            pnlGrid.TabIndex = 2;
            // 
            // dgvReports
            // 
            dgvReports.AllowUserToAddRows = false;
            dgvReports.AllowUserToDeleteRows = false;
            dgvReports.AllowUserToResizeRows = false;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.BackgroundColor = Color.White;
            dgvReports.BorderStyle = BorderStyle.None;
            dgvReports.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReports.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvReports.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvReports.ColumnHeadersHeight = 35;
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvReports.Columns.AddRange(new DataGridViewColumn[] { colCol1, colCol2, colCol3, colCol4, colCol5 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvReports.DefaultCellStyle = dataGridViewCellStyle2;
            dgvReports.Dock = DockStyle.Fill;
            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.GridColor = Color.FromArgb(229, 231, 235);
            dgvReports.Location = new Point(0, 0);
            dgvReports.MultiSelect = false;
            dgvReports.Name = "dgvReports";
            dgvReports.ReadOnly = true;
            dgvReports.RowHeadersVisible = false;
            dgvReports.RowHeadersWidth = 51;
            dgvReports.RowTemplate.Height = 30;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.Size = new Size(690, 605);
            dgvReports.TabIndex = 0;
            // 
            // colCol1
            // 
            colCol1.FillWeight = 50F;
            colCol1.HeaderText = "ID";
            colCol1.Name = "colCol1";
            colCol1.ReadOnly = true;
            // 
            // colCol2
            // 
            colCol2.FillWeight = 160F;
            colCol2.HeaderText = "Descripción";
            colCol2.Name = "colCol2";
            colCol2.ReadOnly = true;
            // 
            // colCol3
            // 
            colCol3.FillWeight = 110F;
            colCol3.HeaderText = "Categoría / Detalle";
            colCol3.Name = "colCol3";
            colCol3.ReadOnly = true;
            // 
            // colCol4
            // 
            colCol4.FillWeight = 80F;
            colCol4.HeaderText = "Cantidad / Stock";
            colCol4.Name = "colCol4";
            colCol4.ReadOnly = true;
            // 
            // colCol5
            // 
            colCol5.FillWeight = 90F;
            colCol5.HeaderText = "Monto / Precio";
            colCol5.Name = "colCol5";
            colCol5.ReadOnly = true;
            // 
            // ReportesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1100, 700);
            Controls.Add(pnlGrid);
            Controls.Add(pnlDetail);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ReportesForm";
            Padding = new Padding(15);
            Text = "Reportes y Consultas";
            Load += ReportesForm_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblReportType;
        private ComboBox cmbReportType;
        private Label lblSearch;
        private TextBox txtSearch;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblStatCount;
        private Label lblStatSum;
        private Label lblStatAvg;
        private Label lblStatMax;
        private Label lblStatMin;
        private Panel pnlGrid;
        private DataGridView dgvReports;
        private DataGridViewTextBoxColumn colCol1;
        private DataGridViewTextBoxColumn colCol2;
        private DataGridViewTextBoxColumn colCol3;
        private DataGridViewTextBoxColumn colCol4;
        private DataGridViewTextBoxColumn colCol5;
    }
}
