namespace ManagementSystem.UI.Forms
{
    partial class ProductosForm
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
            pnlSearch = new Panel();
            btnResetFilter = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlDetail = new Panel();
            chkActive = new CheckBox();
            btnDelete = new Button();
            btnSave = new Button();
            btnNew = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            nudMinimumStock = new NumericUpDown();
            lblMinimumStock = new Label();
            nudCurrentStock = new NumericUpDown();
            lblCurrentStock = new Label();
            nudSalePrice = new NumericUpDown();
            lblSalePrice = new Label();
            nudPurchasePrice = new NumericUpDown();
            lblPurchasePrice = new Label();
            cmbCategory = new ComboBox();
            lblCategory = new Label();
            txtProductName = new TextBox();
            lblProductName = new Label();
            txtProductId = new TextBox();
            lblProductId = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvProducts = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colPrecioCompra = new DataGridViewTextBoxColumn();
            colPrecioVenta = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colStockMin = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlSearch.SuspendLayout();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinimumStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCurrentStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPurchasePrice).BeginInit();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(btnResetFilter);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(15, 15);
            pnlSearch.Margin = new Padding(3, 4, 3, 4);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(15);
            pnlSearch.Size = new Size(1070, 65);
            pnlSearch.TabIndex = 0;
            // 
            // btnResetFilter
            // 
            btnResetFilter.BackColor = Color.FromArgb(243, 244, 246);
            btnResetFilter.Cursor = Cursors.Hand;
            btnResetFilter.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnResetFilter.FlatStyle = FlatStyle.Flat;
            btnResetFilter.Font = new Font("Segoe UI", 9F);
            btnResetFilter.ForeColor = Color.FromArgb(55, 65, 81);
            btnResetFilter.Location = new Point(560, 16);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(110, 32);
            btnResetFilter.TabIndex = 3;
            btnResetFilter.Text = "🔄 Todos";
            btnResetFilter.UseVisualStyleBackColor = false;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(30, 58, 138);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(440, 16);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 32);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔍 Buscar";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(160, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nombre o descripción del producto...";
            txtSearch.Size = new Size(270, 29);
            txtSearch.TabIndex = 1;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(30, 58, 138);
            lblSearch.Location = new Point(15, 21);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(137, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Buscar Producto:";
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(chkActive);
            pnlDetail.Controls.Add(btnDelete);
            pnlDetail.Controls.Add(btnSave);
            pnlDetail.Controls.Add(btnNew);
            pnlDetail.Controls.Add(txtDescription);
            pnlDetail.Controls.Add(lblDescription);
            pnlDetail.Controls.Add(nudMinimumStock);
            pnlDetail.Controls.Add(lblMinimumStock);
            pnlDetail.Controls.Add(nudCurrentStock);
            pnlDetail.Controls.Add(lblCurrentStock);
            pnlDetail.Controls.Add(nudSalePrice);
            pnlDetail.Controls.Add(lblSalePrice);
            pnlDetail.Controls.Add(nudPurchasePrice);
            pnlDetail.Controls.Add(lblPurchasePrice);
            pnlDetail.Controls.Add(cmbCategory);
            pnlDetail.Controls.Add(lblCategory);
            pnlDetail.Controls.Add(txtProductName);
            pnlDetail.Controls.Add(lblProductName);
            pnlDetail.Controls.Add(txtProductId);
            pnlDetail.Controls.Add(lblProductId);
            pnlDetail.Controls.Add(lblDetailHeader);
            pnlDetail.Dock = DockStyle.Right;
            pnlDetail.Location = new Point(720, 80);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(15);
            pnlDetail.Size = new Size(365, 605);
            pnlDetail.TabIndex = 1;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = CheckState.Checked;
            chkActive.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkActive.ForeColor = Color.FromArgb(30, 58, 138);
            chkActive.Location = new Point(20, 475);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(157, 25);
            chkActive.TabIndex = 17;
            chkActive.Text = "Producto Activo";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(235, 520);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 38);
            btnDelete.TabIndex = 20;
            btnDelete.Text = "🗑️ Inactivar";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(30, 58, 138);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(125, 520);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 38);
            btnSave.TabIndex = 19;
            btnSave.Text = "💾 Guardar";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.FromArgb(243, 244, 246);
            btnNew.Cursor = Cursors.Hand;
            btnNew.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.Font = new Font("Segoe UI", 9F);
            btnNew.ForeColor = Color.FromArgb(55, 65, 81);
            btnNew.Location = new Point(20, 520);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(95, 38);
            btnNew.TabIndex = 18;
            btnNew.Text = "📄 Nuevo";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.Location = new Point(20, 405);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(315, 55);
            txtDescription.TabIndex = 16;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 8.5F);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Location = new Point(20, 382);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(90, 20);
            lblDescription.TabIndex = 15;
            lblDescription.Text = "Descripción:";
            // 
            // nudMinimumStock
            // 
            nudMinimumStock.Font = new Font("Segoe UI", 9F);
            nudMinimumStock.Location = new Point(185, 335);
            nudMinimumStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMinimumStock.Name = "nudMinimumStock";
            nudMinimumStock.Size = new Size(150, 27);
            nudMinimumStock.TabIndex = 14;
            // 
            // lblMinimumStock
            // 
            lblMinimumStock.AutoSize = true;
            lblMinimumStock.Font = new Font("Segoe UI", 8.5F);
            lblMinimumStock.ForeColor = Color.FromArgb(75, 85, 99);
            lblMinimumStock.Location = new Point(185, 312);
            lblMinimumStock.Name = "lblMinimumStock";
            lblMinimumStock.Size = new Size(103, 20);
            lblMinimumStock.TabIndex = 13;
            lblMinimumStock.Text = "Stock Mínimo:";
            // 
            // nudCurrentStock
            // 
            nudCurrentStock.Font = new Font("Segoe UI", 9F);
            nudCurrentStock.Location = new Point(20, 335);
            nudCurrentStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCurrentStock.Name = "nudCurrentStock";
            nudCurrentStock.Size = new Size(150, 27);
            nudCurrentStock.TabIndex = 12;
            // 
            // lblCurrentStock
            // 
            lblCurrentStock.AutoSize = true;
            lblCurrentStock.Font = new Font("Segoe UI", 8.5F);
            lblCurrentStock.ForeColor = Color.FromArgb(75, 85, 99);
            lblCurrentStock.Location = new Point(20, 312);
            lblCurrentStock.Name = "lblCurrentStock";
            lblCurrentStock.Size = new Size(94, 20);
            lblCurrentStock.TabIndex = 11;
            lblCurrentStock.Text = "Stock Actual:";
            // 
            // nudSalePrice
            // 
            nudSalePrice.DecimalPlaces = 2;
            nudSalePrice.Font = new Font("Segoe UI", 9F);
            nudSalePrice.Location = new Point(185, 265);
            nudSalePrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSalePrice.Name = "nudSalePrice";
            nudSalePrice.Size = new Size(150, 27);
            nudSalePrice.TabIndex = 10;
            // 
            // lblSalePrice
            // 
            lblSalePrice.AutoSize = true;
            lblSalePrice.Font = new Font("Segoe UI", 8.5F);
            lblSalePrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblSalePrice.Location = new Point(185, 242);
            lblSalePrice.Name = "lblSalePrice";
            lblSalePrice.Size = new Size(111, 20);
            lblSalePrice.TabIndex = 9;
            lblSalePrice.Text = "Precio Venta (*):";
            // 
            // nudPurchasePrice
            // 
            nudPurchasePrice.DecimalPlaces = 2;
            nudPurchasePrice.Font = new Font("Segoe UI", 9F);
            nudPurchasePrice.Location = new Point(20, 265);
            nudPurchasePrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudPurchasePrice.Name = "nudPurchasePrice";
            nudPurchasePrice.Size = new Size(150, 27);
            nudPurchasePrice.TabIndex = 8;
            // 
            // lblPurchasePrice
            // 
            lblPurchasePrice.AutoSize = true;
            lblPurchasePrice.Font = new Font("Segoe UI", 8.5F);
            lblPurchasePrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblPurchasePrice.Location = new Point(20, 242);
            lblPurchasePrice.Name = "lblPurchasePrice";
            lblPurchasePrice.Size = new Size(109, 20);
            lblPurchasePrice.TabIndex = 7;
            lblPurchasePrice.Text = "Precio Compra:";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Segoe UI", 9F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(20, 195);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(315, 28);
            cmbCategory.TabIndex = 6;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 8.5F);
            lblCategory.ForeColor = Color.FromArgb(75, 85, 99);
            lblCategory.Location = new Point(20, 172);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(97, 20);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Categoría (*):";
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Segoe UI", 9F);
            txtProductName.Location = new Point(20, 125);
            txtProductName.Name = "txtProductName";
            txtProductName.PlaceholderText = "Ej. Laptop Dell Inspiron";
            txtProductName.Size = new Size(315, 27);
            txtProductName.TabIndex = 4;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 8.5F);
            lblProductName.ForeColor = Color.FromArgb(75, 85, 99);
            lblProductName.Location = new Point(20, 102);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(151, 20);
            lblProductName.TabIndex = 3;
            lblProductName.Text = "Nombre Producto (*):";
            // 
            // txtProductId
            // 
            txtProductId.BackColor = Color.FromArgb(243, 244, 246);
            txtProductId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtProductId.Location = new Point(20, 60);
            txtProductId.Name = "txtProductId";
            txtProductId.ReadOnly = true;
            txtProductId.Size = new Size(100, 27);
            txtProductId.TabIndex = 2;
            txtProductId.Text = "0";
            // 
            // lblProductId
            // 
            lblProductId.AutoSize = true;
            lblProductId.Font = new Font("Segoe UI", 8.5F);
            lblProductId.ForeColor = Color.FromArgb(75, 85, 99);
            lblProductId.Location = new Point(20, 37);
            lblProductId.Name = "lblProductId";
            lblProductId.Size = new Size(27, 20);
            lblProductId.TabIndex = 1;
            lblProductId.Text = "ID:";
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = true;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(15, 5);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(181, 25);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Datos del Producto";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvProducts);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(15, 80);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 0, 15, 0);
            pnlGrid.Size = new Size(705, 605);
            pnlGrid.TabIndex = 2;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvProducts.ColumnHeadersHeight = 35;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colCategoria, colPrecioCompra, colPrecioVenta, colStock, colStockMin, colEstado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvProducts.DefaultCellStyle = dataGridViewCellStyle2;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.GridColor = Color.FromArgb(229, 231, 235);
            dgvProducts.Location = new Point(0, 0);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 30;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(690, 605);
            dgvProducts.TabIndex = 0;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            // 
            // colId
            // 
            colId.FillWeight = 40F;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 40;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 140F;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 120;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colCategoria
            // 
            colCategoria.FillWeight = 90F;
            colCategoria.HeaderText = "Categoría";
            colCategoria.MinimumWidth = 80;
            colCategoria.Name = "colCategoria";
            colCategoria.ReadOnly = true;
            // 
            // colPrecioCompra
            // 
            colPrecioCompra.FillWeight = 70F;
            colPrecioCompra.HeaderText = "P. Compra";
            colPrecioCompra.MinimumWidth = 60;
            colPrecioCompra.Name = "colPrecioCompra";
            colPrecioCompra.ReadOnly = true;
            // 
            // colPrecioVenta
            // 
            colPrecioVenta.FillWeight = 70F;
            colPrecioVenta.HeaderText = "P. Venta";
            colPrecioVenta.MinimumWidth = 60;
            colPrecioVenta.Name = "colPrecioVenta";
            colPrecioVenta.ReadOnly = true;
            // 
            // colStock
            // 
            colStock.FillWeight = 50F;
            colStock.HeaderText = "Stock";
            colStock.MinimumWidth = 50;
            colStock.Name = "colStock";
            colStock.ReadOnly = true;
            // 
            // colStockMin
            // 
            colStockMin.FillWeight = 55F;
            colStockMin.HeaderText = "Stock Mín.";
            colStockMin.MinimumWidth = 50;
            colStockMin.Name = "colStockMin";
            colStockMin.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.FillWeight = 60F;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 50;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // ProductosForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1100, 700);
            Controls.Add(pnlGrid);
            Controls.Add(pnlDetail);
            Controls.Add(pnlSearch);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ProductosForm";
            Padding = new Padding(15);
            Text = "Mantenimiento de Productos";
            Load += ProductosForm_Load;
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinimumStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCurrentStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPurchasePrice).EndInit();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnResetFilter;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblProductId;
        private TextBox txtProductId;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblPurchasePrice;
        private NumericUpDown nudPurchasePrice;
        private Label lblSalePrice;
        private NumericUpDown nudSalePrice;
        private Label lblCurrentStock;
        private NumericUpDown nudCurrentStock;
        private Label lblMinimumStock;
        private NumericUpDown nudMinimumStock;
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkActive;
        private Button btnNew;
        private Button btnSave;
        private Button btnDelete;
        private Panel pnlGrid;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colCategoria;
        private DataGridViewTextBoxColumn colPrecioCompra;
        private DataGridViewTextBoxColumn colPrecioVenta;
        private DataGridViewTextBoxColumn colStock;
        private DataGridViewTextBoxColumn colStockMin;
        private DataGridViewTextBoxColumn colEstado;
    }
}
