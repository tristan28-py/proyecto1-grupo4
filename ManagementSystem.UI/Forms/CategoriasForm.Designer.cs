namespace ManagementSystem.UI.Forms
{
    partial class CategoriasForm
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
            cmbStatusFilter = new ComboBox();
            lblStatusFilter = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlDetail = new Panel();
            chkActive = new CheckBox();
            btnDelete = new Button();
            btnSave = new Button();
            btnNew = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtCategoryName = new TextBox();
            lblCategoryName = new Label();
            txtCategoryId = new TextBox();
            lblCategoryId = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvCategories = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlTop.SuspendLayout();
            pnlDetail.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(cmbStatusFilter);
            pnlTop.Controls.Add(lblStatusFilter);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(15, 15);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15);
            pnlTop.Size = new Size(1070, 65);
            pnlTop.TabIndex = 0;
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Font = new Font("Segoe UI", 9F);
            cmbStatusFilter.FormattingEnabled = true;
            cmbStatusFilter.Items.AddRange(new object[] { "Todos los estados", "Solo Activos", "Solo Inactivos" });
            cmbStatusFilter.Location = new Point(620, 18);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(160, 28);
            cmbStatusFilter.TabIndex = 3;
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.AutoSize = true;
            lblStatusFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusFilter.ForeColor = Color.FromArgb(75, 85, 99);
            lblStatusFilter.Location = new Point(480, 21);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(134, 20);
            lblStatusFilter.TabIndex = 2;
            lblStatusFilter.Text = "Filtrar por Estado:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(145, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Escriba para filtrar...";
            txtSearch.Size = new Size(290, 29);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(30, 58, 138);
            lblSearch.Location = new Point(15, 21);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(124, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Buscar Categ.:";
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
            pnlDetail.Controls.Add(txtCategoryName);
            pnlDetail.Controls.Add(lblCategoryName);
            pnlDetail.Controls.Add(txtCategoryId);
            pnlDetail.Controls.Add(lblCategoryId);
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
            chkActive.Location = new Point(20, 310);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(160, 25);
            chkActive.TabIndex = 7;
            chkActive.Text = "Categoría Activa";
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
            btnDelete.Location = new Point(235, 360);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 38);
            btnDelete.TabIndex = 10;
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
            btnSave.Location = new Point(125, 360);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 38);
            btnSave.TabIndex = 9;
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
            btnNew.Location = new Point(20, 360);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(95, 38);
            btnNew.TabIndex = 8;
            btnNew.Text = "📄 Nuevo";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.Location = new Point(20, 195);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(315, 90);
            txtDescription.TabIndex = 6;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 8.5F);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Location = new Point(20, 172);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(90, 20);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Descripción:";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Font = new Font("Segoe UI", 9F);
            txtCategoryName.Location = new Point(20, 125);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.PlaceholderText = "Ej. Bebidas y Alimentos";
            txtCategoryName.Size = new Size(315, 27);
            txtCategoryName.TabIndex = 4;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Font = new Font("Segoe UI", 8.5F);
            lblCategoryName.ForeColor = Color.FromArgb(75, 85, 99);
            lblCategoryName.Location = new Point(20, 102);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(153, 20);
            lblCategoryName.TabIndex = 3;
            lblCategoryName.Text = "Nombre Categoría (*):";
            // 
            // txtCategoryId
            // 
            txtCategoryId.BackColor = Color.FromArgb(243, 244, 246);
            txtCategoryId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtCategoryId.Location = new Point(20, 60);
            txtCategoryId.Name = "txtCategoryId";
            txtCategoryId.ReadOnly = true;
            txtCategoryId.Size = new Size(100, 27);
            txtCategoryId.TabIndex = 2;
            txtCategoryId.Text = "0";
            // 
            // lblCategoryId
            // 
            lblCategoryId.AutoSize = true;
            lblCategoryId.Font = new Font("Segoe UI", 8.5F);
            lblCategoryId.ForeColor = Color.FromArgb(75, 85, 99);
            lblCategoryId.Location = new Point(20, 37);
            lblCategoryId.Name = "lblCategoryId";
            lblCategoryId.Size = new Size(27, 20);
            lblCategoryId.TabIndex = 1;
            lblCategoryId.Text = "ID:";
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = true;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(15, 5);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(183, 25);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Datos de Categoría";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvCategories);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(15, 80);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 0, 15, 0);
            pnlGrid.Size = new Size(705, 605);
            pnlGrid.TabIndex = 2;
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCategories.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCategories.ColumnHeadersHeight = 35;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colFecha, colEstado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvCategories.DefaultCellStyle = dataGridViewCellStyle2;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.GridColor = Color.FromArgb(229, 231, 235);
            dgvCategories.Location = new Point(0, 0);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.RowTemplate.Height = 30;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(690, 605);
            dgvCategories.TabIndex = 0;
            dgvCategories.SelectionChanged += dgvCategories_SelectionChanged;
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
            colNombre.FillWeight = 130F;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 120;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colDescripcion
            // 
            colDescripcion.FillWeight = 180F;
            colDescripcion.HeaderText = "Descripción";
            colDescripcion.MinimumWidth = 150;
            colDescripcion.Name = "colDescripcion";
            colDescripcion.ReadOnly = true;
            // 
            // colFecha
            // 
            colFecha.FillWeight = 90F;
            colFecha.HeaderText = "Fecha Creación";
            colFecha.MinimumWidth = 80;
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.FillWeight = 60F;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 60;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // CategoriasForm
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
            Name = "CategoriasForm";
            Padding = new Padding(15);
            Text = "Mantenimiento de Categorías";
            Load += CategoriasForm_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblStatusFilter;
        private ComboBox cmbStatusFilter;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblCategoryId;
        private TextBox txtCategoryId;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkActive;
        private Button btnNew;
        private Button btnSave;
        private Button btnDelete;
        private Panel pnlGrid;
        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colEstado;
    }
}
