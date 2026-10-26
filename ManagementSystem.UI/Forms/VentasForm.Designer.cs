namespace ManagementSystem.UI.Forms
{
    partial class VentasForm
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
            lblTotalSale = new Label();
            dtpSaleDate = new DateTimePicker();
            lblDate = new Label();
            cmbClient = new ComboBox();
            lblClient = new Label();
            pnlDetail = new Panel();
            btnNewSale = new Button();
            btnConfirmSale = new Button();
            btnRemoveDetail = new Button();
            btnAddDetail = new Button();
            lblLineSubTotal = new Label();
            nudQuantity = new NumericUpDown();
            lblQuantity = new Label();
            nudUnitPrice = new NumericUpDown();
            lblUnitPrice = new Label();
            lblStockInfo = new Label();
            cmbProduct = new ComboBox();
            lblProduct = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvSaleDetails = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colProducto = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colPrecioUnitario = new DataGridViewTextBoxColumn();
            colSubTotal = new DataGridViewTextBoxColumn();
            pnlTop.SuspendLayout();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitPrice).BeginInit();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSaleDetails).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(lblTotalSale);
            pnlTop.Controls.Add(dtpSaleDate);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Controls.Add(cmbClient);
            pnlTop.Controls.Add(lblClient);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(10, 10);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15);
            pnlTop.Size = new Size(1000, 64);
            pnlTop.TabIndex = 0;
            // 
            // lblTotalSale
            // 
            lblTotalSale.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalSale.AutoSize = false;
            lblTotalSale.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalSale.ForeColor = Color.FromArgb(30, 58, 138);
            lblTotalSale.Location = new Point(595, 14);
            lblTotalSale.Name = "lblTotalSale";
            lblTotalSale.Size = new Size(388, 36);
            lblTotalSale.TabIndex = 4;
            lblTotalSale.Text = "Total Venta: $0.00";
            lblTotalSale.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpSaleDate
            // 
            dtpSaleDate.Font = new Font("Segoe UI", 9F);
            dtpSaleDate.Format = DateTimePickerFormat.Short;
            dtpSaleDate.Location = new Point(450, 18);
            dtpSaleDate.Name = "dtpSaleDate";
            dtpSaleDate.Size = new Size(135, 27);
            dtpSaleDate.TabIndex = 3;
            // 
            // lblDate
            // 
            lblDate.AutoSize = false;
            lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(75, 85, 99);
            lblDate.Location = new Point(390, 18);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(56, 28);
            lblDate.TabIndex = 2;
            lblDate.Text = "Fecha:";
            lblDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbClient
            // 
            cmbClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClient.Font = new Font("Segoe UI", 9F);
            cmbClient.FormattingEnabled = true;
            cmbClient.Location = new Point(108, 18);
            cmbClient.Name = "cmbClient";
            cmbClient.Size = new Size(268, 28);
            cmbClient.TabIndex = 1;
            // 
            // lblClient
            // 
            lblClient.AutoSize = false;
            lblClient.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblClient.ForeColor = Color.FromArgb(30, 58, 138);
            lblClient.Location = new Point(15, 18);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(90, 28);
            lblClient.TabIndex = 0;
            lblClient.Text = "Cliente (*):";
            lblClient.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(btnNewSale);
            pnlDetail.Controls.Add(btnConfirmSale);
            pnlDetail.Controls.Add(btnRemoveDetail);
            pnlDetail.Controls.Add(btnAddDetail);
            pnlDetail.Controls.Add(lblLineSubTotal);
            pnlDetail.Controls.Add(nudQuantity);
            pnlDetail.Controls.Add(lblQuantity);
            pnlDetail.Controls.Add(nudUnitPrice);
            pnlDetail.Controls.Add(lblUnitPrice);
            pnlDetail.Controls.Add(lblStockInfo);
            pnlDetail.Controls.Add(cmbProduct);
            pnlDetail.Controls.Add(lblProduct);
            pnlDetail.Controls.Add(lblDetailHeader);
            pnlDetail.Dock = DockStyle.Right;
            pnlDetail.Location = new Point(655, 74);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(16);
            pnlDetail.Size = new Size(355, 596);
            pnlDetail.TabIndex = 1;
            // 
            // btnNewSale
            // 
            btnNewSale.BackColor = Color.FromArgb(243, 244, 246);
            btnNewSale.Cursor = Cursors.Hand;
            btnNewSale.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnNewSale.FlatStyle = FlatStyle.Flat;
            btnNewSale.Font = new Font("Segoe UI", 9F);
            btnNewSale.ForeColor = Color.FromArgb(55, 65, 81);
            btnNewSale.Location = new Point(18, 376);
            btnNewSale.Name = "btnNewSale";
            btnNewSale.Size = new Size(318, 38);
            btnNewSale.TabIndex = 12;
            btnNewSale.Text = "Limpiar / Nueva Venta";
            btnNewSale.UseVisualStyleBackColor = false;
            btnNewSale.Click += btnNewSale_Click;
            // 
            // btnConfirmSale
            // 
            btnConfirmSale.BackColor = Color.FromArgb(16, 185, 129);
            btnConfirmSale.Cursor = Cursors.Hand;
            btnConfirmSale.FlatAppearance.BorderSize = 0;
            btnConfirmSale.FlatStyle = FlatStyle.Flat;
            btnConfirmSale.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConfirmSale.ForeColor = Color.White;
            btnConfirmSale.Location = new Point(18, 324);
            btnConfirmSale.Name = "btnConfirmSale";
            btnConfirmSale.Size = new Size(318, 42);
            btnConfirmSale.TabIndex = 11;
            btnConfirmSale.Text = "Confirmar Venta";
            btnConfirmSale.UseVisualStyleBackColor = false;
            btnConfirmSale.Click += btnConfirmSale_Click;
            // 
            // btnRemoveDetail
            // 
            btnRemoveDetail.BackColor = Color.FromArgb(220, 38, 38);
            btnRemoveDetail.Cursor = Cursors.Hand;
            btnRemoveDetail.FlatAppearance.BorderSize = 0;
            btnRemoveDetail.FlatStyle = FlatStyle.Flat;
            btnRemoveDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemoveDetail.ForeColor = Color.White;
            btnRemoveDetail.Location = new Point(184, 266);
            btnRemoveDetail.Name = "btnRemoveDetail";
            btnRemoveDetail.Size = new Size(152, 38);
            btnRemoveDetail.TabIndex = 10;
            btnRemoveDetail.Text = "Quitar Línea";
            btnRemoveDetail.UseVisualStyleBackColor = false;
            btnRemoveDetail.Click += btnRemoveDetail_Click;
            // 
            // btnAddDetail
            // 
            btnAddDetail.BackColor = Color.FromArgb(30, 58, 138);
            btnAddDetail.Cursor = Cursors.Hand;
            btnAddDetail.FlatAppearance.BorderSize = 0;
            btnAddDetail.FlatStyle = FlatStyle.Flat;
            btnAddDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddDetail.ForeColor = Color.White;
            btnAddDetail.Location = new Point(18, 266);
            btnAddDetail.Name = "btnAddDetail";
            btnAddDetail.Size = new Size(152, 38);
            btnAddDetail.TabIndex = 9;
            btnAddDetail.Text = "Agregar Línea";
            btnAddDetail.UseVisualStyleBackColor = false;
            btnAddDetail.Click += btnAddDetail_Click;
            // 
            // lblLineSubTotal
            // 
            lblLineSubTotal.AutoSize = false;
            lblLineSubTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLineSubTotal.ForeColor = Color.FromArgb(30, 58, 138);
            lblLineSubTotal.Location = new Point(18, 222);
            lblLineSubTotal.Name = "lblLineSubTotal";
            lblLineSubTotal.Size = new Size(318, 28);
            lblLineSubTotal.TabIndex = 8;
            lblLineSubTotal.Text = "Subtotal línea: $0.00";
            lblLineSubTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Segoe UI", 9F);
            nudQuantity.Location = new Point(184, 182);
            nudQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(152, 27);
            nudQuantity.TabIndex = 7;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.ValueChanged += nudQuantity_ValueChanged;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = false;
            lblQuantity.Font = new Font("Segoe UI", 8.75F);
            lblQuantity.ForeColor = Color.FromArgb(75, 85, 99);
            lblQuantity.Location = new Point(184, 158);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(152, 22);
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Cantidad (*):";
            lblQuantity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudUnitPrice
            // 
            nudUnitPrice.DecimalPlaces = 2;
            nudUnitPrice.Enabled = false;
            nudUnitPrice.Font = new Font("Segoe UI", 9F);
            nudUnitPrice.Location = new Point(18, 182);
            nudUnitPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudUnitPrice.Name = "nudUnitPrice";
            nudUnitPrice.Size = new Size(152, 27);
            nudUnitPrice.TabIndex = 5;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = false;
            lblUnitPrice.Font = new Font("Segoe UI", 8.75F);
            lblUnitPrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblUnitPrice.Location = new Point(18, 158);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(152, 22);
            lblUnitPrice.TabIndex = 4;
            lblUnitPrice.Text = "Precio Unitario:";
            lblUnitPrice.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStockInfo
            // 
            lblStockInfo.AutoSize = false;
            lblStockInfo.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            lblStockInfo.ForeColor = Color.FromArgb(16, 185, 129);
            lblStockInfo.Location = new Point(18, 118);
            lblStockInfo.Name = "lblStockInfo";
            lblStockInfo.Size = new Size(318, 26);
            lblStockInfo.TabIndex = 3;
            lblStockInfo.Text = "Stock disponible: 0";
            lblStockInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbProduct
            // 
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.Font = new Font("Segoe UI", 9F);
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(18, 80);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(318, 28);
            cmbProduct.TabIndex = 2;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = false;
            lblProduct.Font = new Font("Segoe UI", 8.75F);
            lblProduct.ForeColor = Color.FromArgb(75, 85, 99);
            lblProduct.Location = new Point(18, 56);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(318, 22);
            lblProduct.TabIndex = 1;
            lblProduct.Text = "Producto (*):";
            lblProduct.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = false;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(16, 14);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(320, 28);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Agregar al Detalle";
            lblDetailHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvSaleDetails);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(10, 74);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 10, 12, 0);
            pnlGrid.Size = new Size(645, 596);
            pnlGrid.TabIndex = 2;
            // 
            // dgvSaleDetails
            // 
            dgvSaleDetails.AllowUserToAddRows = false;
            dgvSaleDetails.AllowUserToDeleteRows = false;
            dgvSaleDetails.AllowUserToResizeRows = false;
            dgvSaleDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSaleDetails.BackgroundColor = Color.White;
            dgvSaleDetails.BorderStyle = BorderStyle.None;
            dgvSaleDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSaleDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSaleDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSaleDetails.ColumnHeadersHeight = 40;
            dgvSaleDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSaleDetails.Columns.AddRange(new DataGridViewColumn[] { colId, colProducto, colCantidad, colPrecioUnitario, colSubTotal });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvSaleDetails.DefaultCellStyle = dataGridViewCellStyle2;
            dgvSaleDetails.Dock = DockStyle.Fill;
            dgvSaleDetails.EnableHeadersVisualStyles = false;
            dgvSaleDetails.GridColor = Color.FromArgb(229, 231, 235);
            dgvSaleDetails.Location = new Point(0, 10);
            dgvSaleDetails.MultiSelect = false;
            dgvSaleDetails.Name = "dgvSaleDetails";
            dgvSaleDetails.ReadOnly = true;
            dgvSaleDetails.RowHeadersVisible = false;
            dgvSaleDetails.RowHeadersWidth = 51;
            dgvSaleDetails.RowTemplate.Height = 32;
            dgvSaleDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSaleDetails.Size = new Size(633, 586);
            dgvSaleDetails.TabIndex = 0;
            // 
            // colId
            // 
            colId.FillWeight = 50F;
            colId.HeaderText = "ID Prod.";
            colId.MinimumWidth = 50;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colProducto
            // 
            colProducto.FillWeight = 185F;
            colProducto.HeaderText = "Producto";
            colProducto.MinimumWidth = 130;
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.FillWeight = 70F;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.MinimumWidth = 65;
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // colPrecioUnitario
            // 
            colPrecioUnitario.FillWeight = 90F;
            colPrecioUnitario.HeaderText = "Precio Unit.";
            colPrecioUnitario.MinimumWidth = 80;
            colPrecioUnitario.Name = "colPrecioUnitario";
            colPrecioUnitario.ReadOnly = true;
            // 
            // colSubTotal
            // 
            colSubTotal.FillWeight = 95F;
            colSubTotal.HeaderText = "Subtotal";
            colSubTotal.MinimumWidth = 80;
            colSubTotal.Name = "colSubTotal";
            colSubTotal.ReadOnly = true;
            // 
            // VentasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1020, 680);
            Controls.Add(pnlGrid);
            Controls.Add(pnlDetail);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "VentasForm";
            Padding = new Padding(10);
            Text = "Registrar Venta";
            Load += VentasForm_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitPrice).EndInit();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSaleDetails).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblClient;
        private ComboBox cmbClient;
        private Label lblDate;
        private DateTimePicker dtpSaleDate;
        private Label lblTotalSale;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblProduct;
        private ComboBox cmbProduct;
        private Label lblStockInfo;
        private Label lblUnitPrice;
        private NumericUpDown nudUnitPrice;
        private Label lblQuantity;
        private NumericUpDown nudQuantity;
        private Label lblLineSubTotal;
        private Button btnAddDetail;
        private Button btnRemoveDetail;
        private Button btnConfirmSale;
        private Button btnNewSale;
        private Panel pnlGrid;
        private DataGridView dgvSaleDetails;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colPrecioUnitario;
        private DataGridViewTextBoxColumn colSubTotal;
    }
}
