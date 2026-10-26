namespace ManagementSystem.UI.Forms
{
    partial class HistorialForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlTop = new Panel();
            lblPeekAction = new Label();
            lblStackCount = new Label();
            pnlDetail = new Panel();
            btnClear = new Button();
            btnPop = new Button();
            btnPush = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            cmbModule = new ComboBox();
            lblModule = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvHistory = new DataGridView();
            colPosicion = new DataGridViewTextBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            colModulo = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            pnlTop.SuspendLayout();
            pnlDetail.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(lblPeekAction);
            pnlTop.Controls.Add(lblStackCount);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(10, 10);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15);
            pnlTop.Size = new Size(1000, 68);
            pnlTop.TabIndex = 0;
            // 
            // lblPeekAction
            // 
            lblPeekAction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPeekAction.AutoEllipsis = true;
            lblPeekAction.AutoSize = false;
            lblPeekAction.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPeekAction.ForeColor = Color.FromArgb(75, 85, 99);
            lblPeekAction.Location = new Point(245, 12);
            lblPeekAction.Name = "lblPeekAction";
            lblPeekAction.Size = new Size(738, 44);
            lblPeekAction.TabIndex = 1;
            lblPeekAction.Text = "Última acción: Ninguna";
            lblPeekAction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStackCount
            // 
            lblStackCount.AutoSize = false;
            lblStackCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStackCount.ForeColor = Color.FromArgb(30, 58, 138);
            lblStackCount.Location = new Point(15, 12);
            lblStackCount.Name = "lblStackCount";
            lblStackCount.Size = new Size(220, 44);
            lblStackCount.TabIndex = 0;
            lblStackCount.Text = "Acciones registradas: 0";
            lblStackCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(btnClear);
            pnlDetail.Controls.Add(btnPop);
            pnlDetail.Controls.Add(btnPush);
            pnlDetail.Controls.Add(txtDescription);
            pnlDetail.Controls.Add(lblDescription);
            pnlDetail.Controls.Add(cmbModule);
            pnlDetail.Controls.Add(lblModule);
            pnlDetail.Controls.Add(lblDetailHeader);
            pnlDetail.Dock = DockStyle.Right;
            pnlDetail.Location = new Point(625, 78);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(16);
            pnlDetail.Size = new Size(385, 592);
            pnlDetail.TabIndex = 1;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(243, 244, 246);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F);
            btnClear.ForeColor = Color.FromArgb(55, 65, 81);
            btnClear.Location = new Point(18, 350);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(348, 36);
            btnClear.TabIndex = 7;
            btnClear.Text = "Limpiar";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnPop
            // 
            btnPop.BackColor = Color.FromArgb(220, 38, 38);
            btnPop.Cursor = Cursors.Hand;
            btnPop.FlatAppearance.BorderSize = 0;
            btnPop.FlatStyle = FlatStyle.Flat;
            btnPop.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPop.ForeColor = Color.White;
            btnPop.Location = new Point(18, 300);
            btnPop.Name = "btnPop";
            btnPop.Size = new Size(348, 40);
            btnPop.TabIndex = 6;
            btnPop.Text = "Deshacer Última Acción";
            btnPop.UseVisualStyleBackColor = false;
            btnPop.Click += btnPop_Click;
            // 
            // btnPush
            // 
            btnPush.BackColor = Color.FromArgb(30, 58, 138);
            btnPush.Cursor = Cursors.Hand;
            btnPush.FlatAppearance.BorderSize = 0;
            btnPush.FlatStyle = FlatStyle.Flat;
            btnPush.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPush.ForeColor = Color.White;
            btnPush.Location = new Point(18, 250);
            btnPush.Name = "btnPush";
            btnPush.Size = new Size(348, 40);
            btnPush.TabIndex = 5;
            btnPush.Text = "Registrar Acción";
            btnPush.UseVisualStyleBackColor = false;
            btnPush.Click += btnPush_Click;
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.Location = new Point(18, 144);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Ej. Se actualizó el precio del producto #1";
            txtDescription.Size = new Size(348, 86);
            txtDescription.TabIndex = 4;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = false;
            lblDescription.Font = new Font("Segoe UI", 8.75F);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Location = new Point(18, 120);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(348, 22);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Descripción de la Acción (*):";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbModule
            // 
            cmbModule.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModule.Font = new Font("Segoe UI", 9F);
            cmbModule.FormattingEnabled = true;
            cmbModule.Items.AddRange(new object[] { "Productos", "Categorías", "Clientes", "Ventas", "Pedidos" });
            cmbModule.Location = new Point(18, 72);
            cmbModule.Name = "cmbModule";
            cmbModule.Size = new Size(348, 28);
            cmbModule.TabIndex = 2;
            // 
            // lblModule
            // 
            lblModule.AutoSize = false;
            lblModule.Font = new Font("Segoe UI", 8.75F);
            lblModule.ForeColor = Color.FromArgb(75, 85, 99);
            lblModule.Location = new Point(18, 48);
            lblModule.Name = "lblModule";
            lblModule.Size = new Size(348, 22);
            lblModule.TabIndex = 1;
            lblModule.Text = "Módulo:";
            lblModule.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = false;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(16, 12);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(348, 28);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Registro de Actividades";
            lblDetailHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvHistory);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(10, 78);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 10, 12, 0);
            pnlGrid.Size = new Size(615, 592);
            pnlGrid.TabIndex = 2;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvHistory.ColumnHeadersHeight = 40;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { colPosicion, colId, colModulo, colDescripcion, colFecha });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvHistory.DefaultCellStyle = dataGridViewCellStyle2;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.GridColor = Color.FromArgb(229, 231, 235);
            dgvHistory.Location = new Point(0, 10);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.RowTemplate.Height = 32;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(633, 582);
            dgvHistory.TabIndex = 0;
            // 
            // colPosicion
            // 
            colPosicion.FillWeight = 62F;
            colPosicion.HeaderText = "Posición";
            colPosicion.MinimumWidth = 58;
            colPosicion.Name = "colPosicion";
            colPosicion.ReadOnly = true;
            // 
            // colId
            // 
            colId.FillWeight = 45F;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 42;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colModulo
            // 
            colModulo.FillWeight = 90F;
            colModulo.HeaderText = "Módulo";
            colModulo.MinimumWidth = 78;
            colModulo.Name = "colModulo";
            colModulo.ReadOnly = true;
            // 
            // colDescripcion
            // 
            colDescripcion.FillWeight = 225F;
            colDescripcion.HeaderText = "Descripción de la Acción";
            colDescripcion.MinimumWidth = 150;
            colDescripcion.Name = "colDescripcion";
            colDescripcion.ReadOnly = true;
            // 
            // colFecha
            // 
            colFecha.FillWeight = 80F;
            colFecha.HeaderText = "Hora";
            colFecha.MinimumWidth = 68;
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // HistorialForm
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
            Name = "HistorialForm";
            Padding = new Padding(10);
            Text = "Historial de Acciones";
            Load += HistorialForm_Load;
            pnlTop.ResumeLayout(false);
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblStackCount;
        private Label lblPeekAction;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblModule;
        private ComboBox cmbModule;
        private Label lblDescription;
        private TextBox txtDescription;
        private Button btnPush;
        private Button btnPop;
        private Button btnClear;
        private Panel pnlGrid;
        private DataGridView dgvHistory;
        private DataGridViewTextBoxColumn colPosicion;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colModulo;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colFecha;
    }
}
