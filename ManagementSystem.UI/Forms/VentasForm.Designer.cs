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
            pnlTop.Location = new Point(15, 15);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15);
            pnlTop.Size = new Size(1070, 65);
            pnlTop.TabIndex = 0;
            // 
            // lblTotalSale
            // 
            lblTotalSale.AutoSize = true;
            lblTotalSale.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalSale.ForeColor = Color.FromArgb(30, 58, 138);
            lblTotalSale.Location = new Point(740, 18);
            lblTotalSale.Name = "lblTotalSale";
            lblTotalSale.Size = new Size(165, 28);
            lblTotalSale.TabIndex = 4;
            lblTotalSale.Text = "Total Venta: $0.00";
            // 
            // dtpSaleDate
            // 
            dtpSaleDate.Font = new Font("Segoe UI", 9F);
            dtpSaleDate.Format = DateTimePickerFormat.Short;
            dtpSaleDate.Location = new Point(515, 18);
            dtpSaleDate.Name = "dtpSaleDate";
            dtpSaleDate.Size = new Size(145, 27);
            dtpSaleDate.TabIndex = 3;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(75, 85, 99);
            lblDate.Location = new Point(450, 21);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(55, 20);
            lblDate.TabIndex = 2;
            lblDate.Text = "Fecha:";
            // 
            // cmbClient
            // 
            cmbClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClient.Font = new Font("Segoe UI", 9F);
            cmbClient.FormattingEnabled = true;
            cmbClient.Location = new Point(115, 18);
            cmbClient.Name = "cmbClient";
            cmbClient.Size = new Size(300, 28);
            cmbClient.TabIndex = 1;
            // 
            // lblClient
            // 
            lblClient.AutoSize = true;
            lblClient.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblClient.ForeColor = Color.FromArgb(30, 58, 138);
            lblClient.Location = new Point(15, 21);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(92, 21);
            lblClient.TabIndex = 0;
            lblClient.Text = "Cliente (*):";
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
            pnlDetail.Location = new Point(720, 80);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(15);
            pnlDetail.Size = new Size(365, 605);
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
            btnNewSale.Location = new Point(20, 415);
            btnNewSale.Name = "btnNewSale";
            btnNewSale.Size = new Size(315, 38);
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
            btnConfirmSale.Location = new Point(20, 365);
            btnConfirmSale.Name = "btnConfirmSale";
            btnConfirmSale.Size = new Size(315, 42);
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
            btnRemoveDetail.Location = new Point(185, 300);
            btnRemoveDetail.Name = "btnRemoveDetail";
            btnRemoveDetail.Size = new Size(150, 38);
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
            btnAddDetail.Location = new Point(20, 300);
            btnAddDetail.Name = "btnAddDetail";
            btnAddDetail.Size = new Size(150, 38);
            btnAddDetail.TabIndex = 9;
            btnAddDetail.Text = "Agregar";
            btnAddDetail.UseVisualStyleBackColor = false;
            btnAddDetail.Click += btnAddDetail_Click;
            // 
            // lblLineSubTotal
            // 
            lblLineSubTotal.AutoSize = true;
            lblLineSubTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLineSubTotal.ForeColor = Color.FromArgb(30, 58, 138);
            lblLineSubTotal.Location = new Point(20, 255);
            lblLineSubTotal.Name = "lblLineSubTotal";
            lblLineSubTotal.Size = new Size(175, 23);
            lblLineSubTotal.TabIndex = 8;
            lblLineSubTotal.Text = "Subtotal línea: $0.00";
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Segoe UI", 9F);
            nudQuantity.Location = new Point(185, 200);
            nudQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(150, 27);
            nudQuantity.TabIndex = 7;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.ValueChanged += nudQuantity_ValueChanged;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 8.5F);
            lblQuantity.ForeColor = Color.FromArgb(75, 85, 99);
            lblQuantity.Location = new Point(185, 177);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(92, 20);
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Cantidad (*):";
            // 
            // nudUnitPrice
            // 
            nudUnitPrice.DecimalPlaces = 2;
            nudUnitPrice.Enabled = false;
            nudUnitPrice.Font = new Font("Segoe UI", 9F);
            nudUnitPrice.Location = new Point(20, 200);
            nudUnitPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudUnitPrice.Name = "nudUnitPrice";
            nudUnitPrice.Size = new Size(150, 27);
            nudUnitPrice.TabIndex = 5;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Font = new Font("Segoe UI", 8.5F);
            lblUnitPrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblUnitPrice.Location = new Point(20, 177);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(106, 20);
            lblUnitPrice.TabIndex = 4;
            lblUnitPrice.Text = "Precio Unitario:";
            // 
            // lblStockInfo
            // 
            lblStockInfo.AutoSize = true;
            lblStockInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStockInfo.ForeColor = Color.FromArgb(16, 185, 129);
            lblStockInfo.Location = new Point(20, 130);
            lblStockInfo.Name = "lblStockInfo";
            lblStockInfo.Size = new Size(152, 20);
            lblStockInfo.TabIndex = 3;
            lblStockInfo.Text = "Stock disponible: 0";
            // 
            // cmbProduct
            // 
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.Font = new Font("Segoe UI", 9F);
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(20, 90);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(315, 28);
            cmbProduct.TabIndex = 2;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Font = new Font("Segoe UI", 8.5F);
            lblProduct.ForeColor = Color.FromArgb(75, 85, 99);
            lblProduct.Location = new Point(20, 67);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(92, 20);
            lblProduct.TabIndex = 1;
            lblProduct.Text = "Producto (*):";
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = true;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(15, 15);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(175, 25);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Agregar al Detalle";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvSaleDetails);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(15, 80);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 0, 15, 0);
            pnlGrid.Size = new Size(705, 605);
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
            dgvSaleDetails.ColumnHeadersHeight = 35;
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
            dgvSaleDetails.Location = new Point(0, 0);
            dgvSaleDetails.MultiSelect = false;
            dgvSaleDetails.Name = "dgvSaleDetails";
            dgvSaleDetails.ReadOnly = true;
            dgvSaleDetails.RowHeadersVisible = false;
            dgvSaleDetails.RowHeadersWidth = 51;
            dgvSaleDetails.RowTemplate.Height = 30;
            dgvSaleDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSaleDetails.Size = new Size(690, 605);
            dgvSaleDetails.TabIndex = 0;
            // 
            // colId
            // 
            colId.FillWeight = 45F;
            colId.HeaderText = "ID Prod.";
            colId.MinimumWidth = 45;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colProducto
            // 
            colProducto.FillWeight = 180F;
            colProducto.HeaderText = "Producto";
            colProducto.MinimumWidth = 140;
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.FillWeight = 70F;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.MinimumWidth = 60;
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
            colSubTotal.FillWeight = 90F;
            colSubTotal.HeaderText = "Subtotal";
            colSubTotal.MinimumWidth = 80;
            colSubTotal.Name = "colSubTotal";
            colSubTotal.ReadOnly = true;
            // 
            // VentasForm
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
            Name = "VentasForm";
            Padding = new Padding(15);
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
