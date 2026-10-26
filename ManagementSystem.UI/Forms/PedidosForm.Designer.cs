namespace ManagementSystem.UI.Forms
{
    partial class PedidosForm
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
            lblPeekOrder = new Label();
            lblQueueCount = new Label();
            pnlDetail = new Panel();
            btnClear = new Button();
            btnDequeue = new Button();
            btnEnqueue = new Button();
            txtNotes = new TextBox();
            lblNotes = new Label();
            nudQuantity = new NumericUpDown();
            lblQuantity = new Label();
            cmbProduct = new ComboBox();
            lblProduct = new Label();
            txtClientName = new TextBox();
            lblClientName = new Label();
            lblDetailHeader = new Label();
            pnlGrid = new Panel();
            dgvOrders = new DataGridView();
            colTurno = new DataGridViewTextBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            colCliente = new DataGridViewTextBoxColumn();
            colProducto = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            colNotas = new DataGridViewTextBoxColumn();
            pnlTop.SuspendLayout();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(lblPeekOrder);
            pnlTop.Controls.Add(lblQueueCount);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(10, 10);
            pnlTop.Margin = new Padding(3, 4, 3, 4);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15);
            pnlTop.Size = new Size(1000, 68);
            pnlTop.TabIndex = 0;
            // 
            // lblPeekOrder
            // 
            lblPeekOrder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPeekOrder.AutoEllipsis = true;
            lblPeekOrder.AutoSize = false;
            lblPeekOrder.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPeekOrder.ForeColor = Color.FromArgb(75, 85, 99);
            lblPeekOrder.Location = new Point(230, 12);
            lblPeekOrder.Name = "lblPeekOrder";
            lblPeekOrder.Size = new Size(752, 44);
            lblPeekOrder.TabIndex = 1;
            lblPeekOrder.Text = "Siguiente en turno: Ninguno";
            lblPeekOrder.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblQueueCount
            // 
            lblQueueCount.AutoSize = false;
            lblQueueCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQueueCount.ForeColor = Color.FromArgb(30, 58, 138);
            lblQueueCount.Location = new Point(15, 12);
            lblQueueCount.Name = "lblQueueCount";
            lblQueueCount.Size = new Size(205, 44);
            lblQueueCount.TabIndex = 0;
            lblQueueCount.Text = "Pedidos en cola: 0";
            lblQueueCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(btnClear);
            pnlDetail.Controls.Add(btnDequeue);
            pnlDetail.Controls.Add(btnEnqueue);
            pnlDetail.Controls.Add(txtNotes);
            pnlDetail.Controls.Add(lblNotes);
            pnlDetail.Controls.Add(nudQuantity);
            pnlDetail.Controls.Add(lblQuantity);
            pnlDetail.Controls.Add(cmbProduct);
            pnlDetail.Controls.Add(lblProduct);
            pnlDetail.Controls.Add(txtClientName);
            pnlDetail.Controls.Add(lblClientName);
            pnlDetail.Controls.Add(lblDetailHeader);
            pnlDetail.Dock = DockStyle.Right;
            pnlDetail.Location = new Point(655, 78);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(16);
            pnlDetail.Size = new Size(355, 592);
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
            btnClear.Location = new Point(18, 456);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(318, 36);
            btnClear.TabIndex = 11;
            btnClear.Text = "Limpiar Campos";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnDequeue
            // 
            btnDequeue.BackColor = Color.FromArgb(16, 185, 129);
            btnDequeue.Cursor = Cursors.Hand;
            btnDequeue.FlatAppearance.BorderSize = 0;
            btnDequeue.FlatStyle = FlatStyle.Flat;
            btnDequeue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDequeue.ForeColor = Color.White;
            btnDequeue.Location = new Point(18, 406);
            btnDequeue.Name = "btnDequeue";
            btnDequeue.Size = new Size(318, 40);
            btnDequeue.TabIndex = 10;
            btnDequeue.Text = "Atender Siguiente";
            btnDequeue.UseVisualStyleBackColor = false;
            btnDequeue.Click += btnDequeue_Click;
            // 
            // btnEnqueue
            // 
            btnEnqueue.BackColor = Color.FromArgb(30, 58, 138);
            btnEnqueue.Cursor = Cursors.Hand;
            btnEnqueue.FlatAppearance.BorderSize = 0;
            btnEnqueue.FlatStyle = FlatStyle.Flat;
            btnEnqueue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEnqueue.ForeColor = Color.White;
            btnEnqueue.Location = new Point(18, 356);
            btnEnqueue.Name = "btnEnqueue";
            btnEnqueue.Size = new Size(318, 40);
            btnEnqueue.TabIndex = 9;
            btnEnqueue.Text = "Agregar a Cola";
            btnEnqueue.UseVisualStyleBackColor = false;
            btnEnqueue.Click += btnEnqueue_Click;
            // 
            // txtNotes
            // 
            txtNotes.Font = new Font("Segoe UI", 9F);
            txtNotes.Location = new Point(18, 262);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(318, 76);
            txtNotes.TabIndex = 8;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = false;
            lblNotes.Font = new Font("Segoe UI", 8.75F);
            lblNotes.ForeColor = Color.FromArgb(75, 85, 99);
            lblNotes.Location = new Point(18, 238);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(318, 22);
            lblNotes.TabIndex = 7;
            lblNotes.Text = "Observaciones:";
            lblNotes.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Segoe UI", 9F);
            nudQuantity.Location = new Point(18, 198);
            nudQuantity.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(152, 27);
            nudQuantity.TabIndex = 6;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = false;
            lblQuantity.Font = new Font("Segoe UI", 8.75F);
            lblQuantity.ForeColor = Color.FromArgb(75, 85, 99);
            lblQuantity.Location = new Point(18, 174);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(318, 22);
            lblQuantity.TabIndex = 5;
            lblQuantity.Text = "Cantidad (*):";
            lblQuantity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbProduct
            // 
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.Font = new Font("Segoe UI", 9F);
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(18, 134);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(318, 28);
            cmbProduct.TabIndex = 4;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = false;
            lblProduct.Font = new Font("Segoe UI", 8.75F);
            lblProduct.ForeColor = Color.FromArgb(75, 85, 99);
            lblProduct.Location = new Point(18, 110);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(318, 22);
            lblProduct.TabIndex = 3;
            lblProduct.Text = "Producto (*):";
            lblProduct.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtClientName
            // 
            txtClientName.Font = new Font("Segoe UI", 9F);
            txtClientName.Location = new Point(18, 70);
            txtClientName.Name = "txtClientName";
            txtClientName.PlaceholderText = "Nombre del cliente";
            txtClientName.Size = new Size(318, 27);
            txtClientName.TabIndex = 2;
            // 
            // lblClientName
            // 
            lblClientName.AutoSize = false;
            lblClientName.Font = new Font("Segoe UI", 8.75F);
            lblClientName.ForeColor = Color.FromArgb(75, 85, 99);
            lblClientName.Location = new Point(18, 46);
            lblClientName.Name = "lblClientName";
            lblClientName.Size = new Size(318, 22);
            lblClientName.TabIndex = 1;
            lblClientName.Text = "Cliente (*):";
            lblClientName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.AutoSize = false;
            lblDetailHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(30, 58, 138);
            lblDetailHeader.Location = new Point(16, 12);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Size = new Size(320, 28);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "Nuevo Pedido (Queue<T>)";
            lblDetailHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvOrders);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(10, 78);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(0, 10, 12, 0);
            pnlGrid.Size = new Size(645, 592);
            pnlGrid.TabIndex = 2;
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AllowUserToResizeRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvOrders.ColumnHeadersHeight = 40;
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvOrders.Columns.AddRange(new DataGridViewColumn[] { colTurno, colId, colCliente, colProducto, colCantidad, colFecha, colNotas });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvOrders.DefaultCellStyle = dataGridViewCellStyle2;
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.GridColor = Color.FromArgb(229, 231, 235);
            dgvOrders.Location = new Point(0, 10);
            dgvOrders.MultiSelect = false;
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.RowTemplate.Height = 32;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(633, 582);
            dgvOrders.TabIndex = 0;
            // 
            // colTurno
            // 
            colTurno.FillWeight = 48F;
            colTurno.HeaderText = "Turno";
            colTurno.MinimumWidth = 45;
            colTurno.Name = "colTurno";
            colTurno.ReadOnly = true;
            // 
            // colId
            // 
            colId.FillWeight = 62F;
            colId.HeaderText = "# Pedido";
            colId.MinimumWidth = 55;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colCliente
            // 
            colCliente.FillWeight = 135F;
            colCliente.HeaderText = "Cliente";
            colCliente.MinimumWidth = 95;
            colCliente.Name = "colCliente";
            colCliente.ReadOnly = true;
            // 
            // colProducto
            // 
            colProducto.FillWeight = 135F;
            colProducto.HeaderText = "Producto";
            colProducto.MinimumWidth = 95;
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.FillWeight = 58F;
            colCantidad.HeaderText = "Cant.";
            colCantidad.MinimumWidth = 48;
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // colFecha
            // 
            colFecha.FillWeight = 72F;
            colFecha.HeaderText = "Hora";
            colFecha.MinimumWidth = 62;
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // colNotas
            // 
            colNotas.FillWeight = 125F;
            colNotas.HeaderText = "Observaciones";
            colNotas.MinimumWidth = 90;
            colNotas.Name = "colNotas";
            colNotas.ReadOnly = true;
            // 
            // PedidosForm
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
            Name = "PedidosForm";
            Padding = new Padding(10);
            Text = "Pedidos Pendientes";
            Load += PedidosForm_Load;
            pnlTop.ResumeLayout(false);
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblQueueCount;
        private Label lblPeekOrder;
        private Panel pnlDetail;
        private Label lblDetailHeader;
        private Label lblClientName;
        private TextBox txtClientName;
        private Label lblProduct;
        private ComboBox cmbProduct;
        private Label lblQuantity;
        private NumericUpDown nudQuantity;
        private Label lblNotes;
        private TextBox txtNotes;
        private Button btnEnqueue;
        private Button btnDequeue;
        private Button btnClear;
        private Panel pnlGrid;
        private DataGridView dgvOrders;
        private DataGridViewTextBoxColumn colTurno;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCliente;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colNotas;
    }
}
