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
            pnlSearch.Location = new Point(10, 10);
            pnlSearch.Margin = new Padding(3, 4, 3, 4);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(15);
            pnlSearch.Size = new Size(1000, 64);
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
            btnResetFilter.Location = new Point(575, 16);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(115, 32);
            btnResetFilter.TabIndex = 3;
            btnResetFilter.Text = "Ver Todos";
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
            btnSearch.Location = new Point(455, 16);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 32);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(155, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nombre o descripción del producto...";
            txtSearch.Size = new Size(288, 29);
            txtSearch.TabIndex = 1;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = false;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(30, 58, 138);
            lblSearch.Location = new Point(15, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(136, 28);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Buscar Producto:";
            lblSearch.TextAlign = ContentAlignment.MiddleLeft;
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
            pnlDetail.Location = new Point(655, 74);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(16);
            pnlDetail.Size = new Size(355, 596);
            pnlDetail.TabIndex = 1;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = CheckState.Checked;
            chkActive.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            chkActive.ForeColor = Color.FromArgb(30, 58, 138);
            chkActive.Location = new Point(150, 65);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(155, 25);
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
            btnDelete.Location = new Point(236, 422);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 38);
            btnDelete.TabIndex = 20;
            btnDelete.Text = "Inactivar";
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
            btnSave.Location = new Point(126, 422);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(102, 38);
            btnSave.TabIndex = 19;
            btnSave.Text = "Guardar";
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
            btnNew.Location = new Point(18, 422);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(100, 38);
            btnNew.TabIndex = 18;
            btnNew.Text = "Nuevo";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.Location = new Point(18, 350);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(318, 56);
            txtDescription.TabIndex = 16;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = false;
            lblDescription.Font = new Font("Segoe UI", 8.75F);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Location = new Point(18, 326);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(318, 22);
            lblDescription.TabIndex = 15;
            lblDescription.Text = "Descripción:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudMinimumStock
            // 
            nudMinimumStock.Font = new Font("Segoe UI", 9F);
            nudMinimumStock.Location = new Point(184, 292);
            nudMinimumStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMinimumStock.Name = "nudMinimumStock";
            nudMinimumStock.Size = new Size(152, 27);
            nudMinimumStock.TabIndex = 14;
            // 
            // lblMinimumStock
            // 
            lblMinimumStock.AutoSize = false;
            lblMinimumStock.Font = new Font("Segoe UI", 8.75F);
            lblMinimumStock.ForeColor = Color.FromArgb(75, 85, 99);
            lblMinimumStock.Location = new Point(184, 268);
            lblMinimumStock.Name = "lblMinimumStock";
            lblMinimumStock.Size = new Size(152, 22);
            lblMinimumStock.TabIndex = 13;
            lblMinimumStock.Text = "Stock Mínimo:";
            lblMinimumStock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudCurrentStock
            // 
            nudCurrentStock.Font = new Font("Segoe UI", 9F);
            nudCurrentStock.Location = new Point(18, 292);
            nudCurrentStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCurrentStock.Name = "nudCurrentStock";
            nudCurrentStock.Size = new Size(152, 27);
            nudCurrentStock.TabIndex = 12;
            // 
            // lblCurrentStock
            // 
            lblCurrentStock.AutoSize = false;
            lblCurrentStock.Font = new Font("Segoe UI", 8.75F);
            lblCurrentStock.ForeColor = Color.FromArgb(75, 85, 99);
            lblCurrentStock.Location = new Point(18, 268);
            lblCurrentStock.Name = "lblCurrentStock";
            lblCurrentStock.Size = new Size(152, 22);
            lblCurrentStock.TabIndex = 11;
            lblCurrentStock.Text = "Stock Actual:";
            lblCurrentStock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudSalePrice
            // 
            nudSalePrice.DecimalPlaces = 2;
            nudSalePrice.Font = new Font("Segoe UI", 9F);
            nudSalePrice.Location = new Point(184, 234);
            nudSalePrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSalePrice.Name = "nudSalePrice";
            nudSalePrice.Size = new Size(152, 27);
            nudSalePrice.TabIndex = 10;
            // 
            // lblSalePrice
            // 
            lblSalePrice.AutoSize = false;
            lblSalePrice.Font = new Font("Segoe UI", 8.75F);
            lblSalePrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblSalePrice.Location = new Point(184, 210);
            lblSalePrice.Name = "lblSalePrice";
            lblSalePrice.Size = new Size(152, 22);
            lblSalePrice.TabIndex = 9;
            lblSalePrice.Text = "Precio Venta (*):";
            lblSalePrice.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudPurchasePrice
            // 
            nudPurchasePrice.DecimalPlaces = 2;
            nudPurchasePrice.Font = new Font("Segoe UI", 9F);
            nudPurchasePrice.Location = new Point(18, 234);
            nudPurchasePrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudPurchasePrice.Name = "nudPurchasePrice";
            nudPurchasePrice.Size = new Size(152, 27);
            nudPurchasePrice.TabIndex = 8;
            // 
            // lblPurchasePrice
            // 
            lblPurchasePrice.AutoSize = false;
            lblPurchasePrice.Font = new Font("Segoe UI", 8.75F);
            lblPurchasePrice.ForeColor = Color.FromArgb(75, 85, 99);
            lblPurchasePrice.Location = new Point(18, 210);
            lblPurchasePrice.Name = "lblPurchasePrice";
            lblPurchasePrice.Size = new Size(152, 22);
            lblPurchasePrice.TabIndex = 7;
            lblPurchasePrice.Text = "Precio Compra:";
            lblPurchasePrice.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Segoe UI", 9F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(18, 176);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(318, 28);
            cmbCategory.TabIndex = 6;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = false;
            lblCategory.Font = new Font("Segoe UI", 8.75F);
            lblCategory.ForeColor = Color.FromArgb(75, 85, 99);
            lblCategory.Location = new Point(18, 152);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(318, 22);
            lblCategory.TabIndex = 5;
            lblCategory.Text = "Categoría (*):";
            lblCategory.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Segoe UI", 9F);
            txtProductName.Location = new Point(18, 120);
            txtProductName.Name = "txtProductName";
            txtProductName.PlaceholderText = "Ej. Laptop Dell Inspiron";
            txtProductName.Size = new Size(318, 27);
            txtProductName.TabIndex = 4;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = false;
            lblProductName.Font = new Font("Segoe UI", 8.75F);
            lblProductName.ForeColor = Color.FromArgb(75, 85, 99);
            lblProductName.Location = new Point(18, 96);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(318, 22);
            lblProductName.TabIndex = 3;
            lblProductName.Text = "Nombre del Producto (*):";
            lblProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtProductId
            // 
            txtProductId.BackColor = Color.FromArgb(243, 244, 246);
            txtProductId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtProductId.Location = new Point(18, 64);
            txtProductId.Name = "txtProductId";
            txtProductId.ReadOnly = true;
            txtProductId.Size = new Size(110, 27);
            txtProductId.TabIndex = 2;
            txtProductId.Text = "0";
            // 
            // lblProductId
            // 
            lblProductId.AutoSize = false;
            lblProductId.Font = new Font("Segoe UI", 8.75F);
            lblProductId.ForeColor = Color.FromArgb(75, 85, 99);
            lblProductId.Location = new Point(18, 40);
            lblProductId.Name = "lblProductId";
            lblProductId.Size = new Size(120, 22);
            lblProductId.TabIndex = 1;
            lblProductId.Text = "ID Producto:";
            lblProductId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = false;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(16, 10);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(320, 26);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Datos del Producto";
            lblDetailHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvProducts);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(10, 74);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 10, 12, 0);
            pnlGrid.Size = new Size(645, 596);
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
            dgvProducts.ColumnHeadersHeight = 42;
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
            dgvProducts.Location = new Point(0, 10);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 32;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(633, 586);
            dgvProducts.TabIndex = 0;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            // 
            // colId
            // 
            colId.FillWeight = 38F;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 36;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 145F;
            colNombre.HeaderText = "Producto";
            colNombre.MinimumWidth = 110;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colCategoria
            // 
            colCategoria.FillWeight = 100F;
            colCategoria.HeaderText = "Categoría";
            colCategoria.MinimumWidth = 80;
            colCategoria.Name = "colCategoria";
            colCategoria.ReadOnly = true;
            // 
            // colPrecioCompra
            // 
            colPrecioCompra.FillWeight = 72F;
            colPrecioCompra.HeaderText = "P. Compra";
            colPrecioCompra.MinimumWidth = 65;
            colPrecioCompra.Name = "colPrecioCompra";
            colPrecioCompra.ReadOnly = true;
            // 
            // colPrecioVenta
            // 
            colPrecioVenta.FillWeight = 72F;
            colPrecioVenta.HeaderText = "P. Venta";
            colPrecioVenta.MinimumWidth = 65;
            colPrecioVenta.Name = "colPrecioVenta";
            colPrecioVenta.ReadOnly = true;
            // 
            // colStock
            // 
            colStock.FillWeight = 52F;
            colStock.HeaderText = "Stock";
            colStock.MinimumWidth = 48;
            colStock.Name = "colStock";
            colStock.ReadOnly = true;
            // 
            // colStockMin
            // 
            colStockMin.FillWeight = 58F;
            colStockMin.HeaderText = "Mínimo";
            colStockMin.MinimumWidth = 52;
            colStockMin.Name = "colStockMin";
            colStockMin.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.FillWeight = 62F;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 56;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // ProductosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1020, 680);
            Controls.Add(pnlGrid);
            Controls.Add(pnlDetail);
            Controls.Add(pnlSearch);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ProductosForm";
            Padding = new Padding(10);
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
