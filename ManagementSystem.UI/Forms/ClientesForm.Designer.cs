namespace ManagementSystem.UI.Forms
{
    partial class ClientesForm
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
            cmbTypeFilter = new ComboBox();
            lblTypeFilter = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlDetail = new Panel();
            chkActive = new CheckBox();
            btnDelete = new Button();
            btnSave = new Button();
            btnNew = new Button();
            nudCreditLimit = new NumericUpDown();
            lblCreditLimit = new Label();
            cmbPersonType = new ComboBox();
            lblPersonType = new Label();
            txtPhone = new TextBox();
            lblPhone = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtIdentification = new TextBox();
            lblIdentification = new Label();
            txtFullName = new TextBox();
            lblFullName = new Label();
            txtClientId = new TextBox();
            lblClientId = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvClients = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colIdentificacion = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colCredito = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlTop.SuspendLayout();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCreditLimit).BeginInit();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(cmbTypeFilter);
            pnlTop.Controls.Add(lblTypeFilter);
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
            // cmbTypeFilter
            // 
            cmbTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTypeFilter.Font = new Font("Segoe UI", 9F);
            cmbTypeFilter.FormattingEnabled = true;
            cmbTypeFilter.Items.AddRange(new object[] { "Todos", "Cliente", "Empleado", "Usuario" });
            cmbTypeFilter.Location = new Point(620, 18);
            cmbTypeFilter.Name = "cmbTypeFilter";
            cmbTypeFilter.Size = new Size(160, 28);
            cmbTypeFilter.TabIndex = 3;
            cmbTypeFilter.SelectedIndexChanged += cmbTypeFilter_SelectedIndexChanged;
            // 
            // lblTypeFilter
            // 
            lblTypeFilter.AutoSize = true;
            lblTypeFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTypeFilter.ForeColor = Color.FromArgb(75, 85, 99);
            lblTypeFilter.Location = new Point(495, 21);
            lblTypeFilter.Name = "lblTypeFilter";
            lblTypeFilter.Size = new Size(118, 20);
            lblTypeFilter.TabIndex = 2;
            lblTypeFilter.Text = "Filtrar por Tipo:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.Location = new Point(155, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nombre o identificación...";
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
            lblSearch.Size = new Size(134, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Buscar Persona:";
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(chkActive);
            pnlDetail.Controls.Add(btnDelete);
            pnlDetail.Controls.Add(btnSave);
            pnlDetail.Controls.Add(btnNew);
            pnlDetail.Controls.Add(nudCreditLimit);
            pnlDetail.Controls.Add(lblCreditLimit);
            pnlDetail.Controls.Add(cmbPersonType);
            pnlDetail.Controls.Add(lblPersonType);
            pnlDetail.Controls.Add(txtPhone);
            pnlDetail.Controls.Add(lblPhone);
            pnlDetail.Controls.Add(txtEmail);
            pnlDetail.Controls.Add(lblEmail);
            pnlDetail.Controls.Add(txtIdentification);
            pnlDetail.Controls.Add(lblIdentification);
            pnlDetail.Controls.Add(txtFullName);
            pnlDetail.Controls.Add(lblFullName);
            pnlDetail.Controls.Add(txtClientId);
            pnlDetail.Controls.Add(lblClientId);
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
            chkActive.Location = new Point(20, 470);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(148, 25);
            chkActive.TabIndex = 15;
            chkActive.Text = "Registro Activo";
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
            btnDelete.Location = new Point(235, 515);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 38);
            btnDelete.TabIndex = 18;
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
            btnSave.Location = new Point(125, 515);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 38);
            btnSave.TabIndex = 17;
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
            btnNew.Location = new Point(20, 515);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(95, 38);
            btnNew.TabIndex = 16;
            btnNew.Text = "Nuevo";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // nudCreditLimit
            // 
            nudCreditLimit.DecimalPlaces = 2;
            nudCreditLimit.Font = new Font("Segoe UI", 9F);
            nudCreditLimit.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            nudCreditLimit.Location = new Point(20, 420);
            nudCreditLimit.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudCreditLimit.Name = "nudCreditLimit";
            nudCreditLimit.Size = new Size(315, 27);
            nudCreditLimit.TabIndex = 14;
            // 
            // lblCreditLimit
            // 
            lblCreditLimit.AutoSize = true;
            lblCreditLimit.Font = new Font("Segoe UI", 8.5F);
            lblCreditLimit.ForeColor = Color.FromArgb(75, 85, 99);
            lblCreditLimit.Location = new Point(20, 397);
            lblCreditLimit.Name = "lblCreditLimit";
            lblCreditLimit.Size = new Size(165, 20);
            lblCreditLimit.TabIndex = 13;
            lblCreditLimit.Text = "Límite Crédito / Salario:";
            // 
            // cmbPersonType
            // 
            cmbPersonType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPersonType.Font = new Font("Segoe UI", 9F);
            cmbPersonType.FormattingEnabled = true;
            cmbPersonType.Items.AddRange(new object[] { "Cliente", "Empleado", "Usuario" });
            cmbPersonType.Location = new Point(20, 355);
            cmbPersonType.Name = "cmbPersonType";
            cmbPersonType.Size = new Size(315, 28);
            cmbPersonType.TabIndex = 12;
            // 
            // lblPersonType
            // 
            lblPersonType.AutoSize = true;
            lblPersonType.Font = new Font("Segoe UI", 8.5F);
            lblPersonType.ForeColor = Color.FromArgb(75, 85, 99);
            lblPersonType.Location = new Point(20, 332);
            lblPersonType.Name = "lblPersonType";
            lblPersonType.Size = new Size(138, 20);
            lblPersonType.TabIndex = 11;
            lblPersonType.Text = "Tipo de Persona (*):";
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Segoe UI", 9F);
            txtPhone.Location = new Point(185, 290);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "8888-8888";
            txtPhone.Size = new Size(150, 27);
            txtPhone.TabIndex = 10;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 8.5F);
            lblPhone.ForeColor = Color.FromArgb(75, 85, 99);
            lblPhone.Location = new Point(185, 267);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(68, 20);
            lblPhone.TabIndex = 9;
            lblPhone.Text = "Teléfono:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(20, 290);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "correo@ejemplo.com";
            txtEmail.Size = new Size(150, 27);
            txtEmail.TabIndex = 8;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 8.5F);
            lblEmail.ForeColor = Color.FromArgb(75, 85, 99);
            lblEmail.Location = new Point(20, 267);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(56, 20);
            lblEmail.TabIndex = 7;
            lblEmail.Text = "Correo:";
            // 
            // txtIdentification
            // 
            txtIdentification.Font = new Font("Segoe UI", 9F);
            txtIdentification.Location = new Point(20, 220);
            txtIdentification.Name = "txtIdentification";
            txtIdentification.PlaceholderText = "Ej. 1-1234-5678";
            txtIdentification.Size = new Size(315, 27);
            txtIdentification.TabIndex = 6;
            // 
            // lblIdentification
            // 
            lblIdentification.AutoSize = true;
            lblIdentification.Font = new Font("Segoe UI", 8.5F);
            lblIdentification.ForeColor = Color.FromArgb(75, 85, 99);
            lblIdentification.Location = new Point(20, 197);
            lblIdentification.Name = "lblIdentification";
            lblIdentification.Size = new Size(122, 20);
            lblIdentification.TabIndex = 5;
            lblIdentification.Text = "Identificación (*):";
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.Location = new Point(20, 150);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Nombre y apellidos";
            txtFullName.Size = new Size(315, 27);
            txtFullName.TabIndex = 4;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 8.5F);
            lblFullName.ForeColor = Color.FromArgb(75, 85, 99);
            lblFullName.Location = new Point(20, 127);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(154, 20);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "Nombre Completo (*):";
            // 
            // txtClientId
            // 
            txtClientId.BackColor = Color.FromArgb(243, 244, 246);
            txtClientId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtClientId.Location = new Point(20, 80);
            txtClientId.Name = "txtClientId";
            txtClientId.ReadOnly = true;
            txtClientId.Size = new Size(100, 27);
            txtClientId.TabIndex = 2;
            txtClientId.Text = "0";
            // 
            // lblClientId
            // 
            lblClientId.AutoSize = true;
            lblClientId.Font = new Font("Segoe UI", 8.5F);
            lblClientId.ForeColor = Color.FromArgb(75, 85, 99);
            lblClientId.Location = new Point(20, 57);
            lblClientId.Name = "lblClientId";
            lblClientId.Size = new Size(27, 20);
            lblClientId.TabIndex = 1;
            lblClientId.Text = "ID:";
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = true;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(15, 15);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(225, 25);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Datos de Cliente / Persona";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvClients);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(15, 80);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 0, 15, 0);
            pnlGrid.Size = new Size(705, 605);
            pnlGrid.TabIndex = 2;
            // 
            // dgvClients
            // 
            dgvClients.AllowUserToAddRows = false;
            dgvClients.AllowUserToDeleteRows = false;
            dgvClients.AllowUserToResizeRows = false;
            dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClients.BackgroundColor = Color.White;
            dgvClients.BorderStyle = BorderStyle.None;
            dgvClients.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClients.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvClients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvClients.ColumnHeadersHeight = 35;
            dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvClients.Columns.AddRange(new DataGridViewColumn[] { colId, colIdentificacion, colNombre, colTipo, colTelefono, colCredito, colEstado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvClients.DefaultCellStyle = dataGridViewCellStyle2;
            dgvClients.Dock = DockStyle.Fill;
            dgvClients.EnableHeadersVisualStyles = false;
            dgvClients.GridColor = Color.FromArgb(229, 231, 235);
            dgvClients.Location = new Point(0, 0);
            dgvClients.MultiSelect = false;
            dgvClients.Name = "dgvClients";
            dgvClients.ReadOnly = true;
            dgvClients.RowHeadersVisible = false;
            dgvClients.RowHeadersWidth = 51;
            dgvClients.RowTemplate.Height = 30;
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.Size = new Size(690, 605);
            dgvClients.TabIndex = 0;
            dgvClients.SelectionChanged += dgvClients_SelectionChanged;
            // 
            // colId
            // 
            colId.FillWeight = 40F;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 40;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colIdentificacion
            // 
            colIdentificacion.FillWeight = 90F;
            colIdentificacion.HeaderText = "Identificación";
            colIdentificacion.MinimumWidth = 80;
            colIdentificacion.Name = "colIdentificacion";
            colIdentificacion.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 150F;
            colNombre.HeaderText = "Nombre Completo";
            colNombre.MinimumWidth = 120;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colTipo
            // 
            colTipo.FillWeight = 80F;
            colTipo.HeaderText = "Tipo";
            colTipo.MinimumWidth = 70;
            colTipo.Name = "colTipo";
            colTipo.ReadOnly = true;
            // 
            // colTelefono
            // 
            colTelefono.FillWeight = 80F;
            colTelefono.HeaderText = "Teléfono";
            colTelefono.MinimumWidth = 70;
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            // 
            // colCredito
            // 
            colCredito.FillWeight = 85F;
            colCredito.HeaderText = "Crédito / Salario";
            colCredito.MinimumWidth = 75;
            colCredito.Name = "colCredito";
            colCredito.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.FillWeight = 60F;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 55;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // ClientesForm
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
            Name = "ClientesForm";
            Padding = new Padding(15);
            Text = "Mantenimiento de Clientes y Personas";
            Load += ClientesForm_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCreditLimit).EndInit();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblTypeFilter;
        private ComboBox cmbTypeFilter;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblClientId;
        private TextBox txtClientId;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblIdentification;
        private TextBox txtIdentification;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblPersonType;
        private ComboBox cmbPersonType;
        private Label lblCreditLimit;
        private NumericUpDown nudCreditLimit;
        private CheckBox chkActive;
        private Button btnNew;
        private Button btnSave;
        private Button btnDelete;
        private Panel pnlGrid;
        private DataGridView dgvClients;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colIdentificacion;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colCredito;
        private DataGridViewTextBoxColumn colEstado;
    }
}
