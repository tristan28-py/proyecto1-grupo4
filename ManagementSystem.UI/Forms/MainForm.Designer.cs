namespace ManagementSystem.UI.Forms
{
    partial class MainForm
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
            pnlMenuLateral = new Panel();
            btnCerrarSesion = new Button();
            btnHistorial = new Button();
            btnReportes = new Button();
            btnPedidos = new Button();
            btnVentas = new Button();
            btnClientes = new Button();
            btnCategorias = new Button();
            btnProductos = new Button();
            btnInicio = new Button();
            pnlEncabezadoLogo = new Panel();
            lblSubtituloLogo = new Label();
            lblTituloLogo = new Label();
            pnlEncabezado = new Panel();
            lblInfoUsuario = new Label();
            lblTituloSeccion = new Label();
            pnlContenido = new Panel();
            pnlBienvenida = new Panel();
            lblDescripcionBienvenida = new Label();
            lblTituloBienvenida = new Label();
            pnlMenuLateral.SuspendLayout();
            pnlEncabezadoLogo.SuspendLayout();
            pnlEncabezado.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlBienvenida.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenuLateral
            // 
            pnlMenuLateral.BackColor = Color.FromArgb(30, 58, 138);
            pnlMenuLateral.Controls.Add(btnCerrarSesion);
            pnlMenuLateral.Controls.Add(btnHistorial);
            pnlMenuLateral.Controls.Add(btnReportes);
            pnlMenuLateral.Controls.Add(btnPedidos);
            pnlMenuLateral.Controls.Add(btnVentas);
            pnlMenuLateral.Controls.Add(btnClientes);
            pnlMenuLateral.Controls.Add(btnCategorias);
            pnlMenuLateral.Controls.Add(btnProductos);
            pnlMenuLateral.Controls.Add(btnInicio);
            pnlMenuLateral.Controls.Add(pnlEncabezadoLogo);
            pnlMenuLateral.Dock = DockStyle.Left;
            pnlMenuLateral.Location = new Point(0, 0);
            pnlMenuLateral.Margin = new Padding(3, 4, 3, 4);
            pnlMenuLateral.Name = "pnlMenuLateral";
            pnlMenuLateral.Size = new Size(240, 760);
            pnlMenuLateral.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.BackColor = Color.FromArgb(23, 44, 105);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Dock = DockStyle.Bottom;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.Font = new Font("Segoe UI", 9.5F);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Location = new Point(0, 710);
            btnCerrarSesion.Margin = new Padding(0);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Padding = new Padding(20, 0, 10, 0);
            btnCerrarSesion.Size = new Size(240, 50);
            btnCerrarSesion.TabIndex = 9;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.TextAlign = ContentAlignment.MiddleLeft;
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnHistorial
            // 
            btnHistorial.Cursor = Cursors.Hand;
            btnHistorial.Dock = DockStyle.Top;
            btnHistorial.FlatAppearance.BorderSize = 0;
            btnHistorial.FlatStyle = FlatStyle.Flat;
            btnHistorial.Font = new Font("Segoe UI", 9.5F);
            btnHistorial.ForeColor = Color.White;
            btnHistorial.Location = new Point(0, 412);
            btnHistorial.Margin = new Padding(0);
            btnHistorial.Name = "btnHistorial";
            btnHistorial.Padding = new Padding(20, 0, 10, 0);
            btnHistorial.Size = new Size(240, 46);
            btnHistorial.TabIndex = 8;
            btnHistorial.Text = "Historial";
            btnHistorial.TextAlign = ContentAlignment.MiddleLeft;
            btnHistorial.UseVisualStyleBackColor = true;
            btnHistorial.Click += btnHistorial_Click;
            // 
            // btnReportes
            // 
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 9.5F);
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(0, 366);
            btnReportes.Margin = new Padding(0);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(20, 0, 10, 0);
            btnReportes.Size = new Size(240, 46);
            btnReportes.TabIndex = 7;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnPedidos
            // 
            btnPedidos.Cursor = Cursors.Hand;
            btnPedidos.Dock = DockStyle.Top;
            btnPedidos.FlatAppearance.BorderSize = 0;
            btnPedidos.FlatStyle = FlatStyle.Flat;
            btnPedidos.Font = new Font("Segoe UI", 9.5F);
            btnPedidos.ForeColor = Color.White;
            btnPedidos.Location = new Point(0, 320);
            btnPedidos.Margin = new Padding(0);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.Padding = new Padding(20, 0, 10, 0);
            btnPedidos.Size = new Size(240, 46);
            btnPedidos.TabIndex = 6;
            btnPedidos.Text = "Pedidos";
            btnPedidos.TextAlign = ContentAlignment.MiddleLeft;
            btnPedidos.UseVisualStyleBackColor = true;
            btnPedidos.Click += btnPedidos_Click;
            // 
            // btnVentas
            // 
            btnVentas.Cursor = Cursors.Hand;
            btnVentas.Dock = DockStyle.Top;
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.FlatStyle = FlatStyle.Flat;
            btnVentas.Font = new Font("Segoe UI", 9.5F);
            btnVentas.ForeColor = Color.White;
            btnVentas.Location = new Point(0, 274);
            btnVentas.Margin = new Padding(0);
            btnVentas.Name = "btnVentas";
            btnVentas.Padding = new Padding(20, 0, 10, 0);
            btnVentas.Size = new Size(240, 46);
            btnVentas.TabIndex = 5;
            btnVentas.Text = "Registrar Venta";
            btnVentas.TextAlign = ContentAlignment.MiddleLeft;
            btnVentas.UseVisualStyleBackColor = true;
            btnVentas.Click += btnVentas_Click;
            // 
            // btnClientes
            // 
            btnClientes.Cursor = Cursors.Hand;
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 9.5F);
            btnClientes.ForeColor = Color.White;
            btnClientes.Location = new Point(0, 228);
            btnClientes.Margin = new Padding(0);
            btnClientes.Name = "btnClientes";
            btnClientes.Padding = new Padding(20, 0, 10, 0);
            btnClientes.Size = new Size(240, 46);
            btnClientes.TabIndex = 4;
            btnClientes.Text = "Clientes y Personas";
            btnClientes.TextAlign = ContentAlignment.MiddleLeft;
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnCategorias
            // 
            btnCategorias.Cursor = Cursors.Hand;
            btnCategorias.Dock = DockStyle.Top;
            btnCategorias.FlatAppearance.BorderSize = 0;
            btnCategorias.FlatStyle = FlatStyle.Flat;
            btnCategorias.Font = new Font("Segoe UI", 9.5F);
            btnCategorias.ForeColor = Color.White;
            btnCategorias.Location = new Point(0, 182);
            btnCategorias.Margin = new Padding(0);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Padding = new Padding(20, 0, 10, 0);
            btnCategorias.Size = new Size(240, 46);
            btnCategorias.TabIndex = 3;
            btnCategorias.Text = "Categorías";
            btnCategorias.TextAlign = ContentAlignment.MiddleLeft;
            btnCategorias.UseVisualStyleBackColor = true;
            btnCategorias.Click += btnCategorias_Click;
            // 
            // btnProductos
            // 
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.Dock = DockStyle.Top;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 9.5F);
            btnProductos.ForeColor = Color.White;
            btnProductos.Location = new Point(0, 136);
            btnProductos.Margin = new Padding(0);
            btnProductos.Name = "btnProductos";
            btnProductos.Padding = new Padding(20, 0, 10, 0);
            btnProductos.Size = new Size(240, 46);
            btnProductos.TabIndex = 2;
            btnProductos.Text = "Productos";
            btnProductos.TextAlign = ContentAlignment.MiddleLeft;
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnInicio
            // 
            btnInicio.BackColor = Color.FromArgb(23, 44, 105);
            btnInicio.Cursor = Cursors.Hand;
            btnInicio.Dock = DockStyle.Top;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnInicio.ForeColor = Color.White;
            btnInicio.Location = new Point(0, 90);
            btnInicio.Margin = new Padding(0);
            btnInicio.Name = "btnInicio";
            btnInicio.Padding = new Padding(20, 0, 10, 0);
            btnInicio.Size = new Size(240, 46);
            btnInicio.TabIndex = 1;
            btnInicio.Text = "Inicio";
            btnInicio.TextAlign = ContentAlignment.MiddleLeft;
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Click += btnInicio_Click;
            // 
            // pnlEncabezadoLogo
            // 
            pnlEncabezadoLogo.BackColor = Color.FromArgb(23, 44, 105);
            pnlEncabezadoLogo.Controls.Add(lblSubtituloLogo);
            pnlEncabezadoLogo.Controls.Add(lblTituloLogo);
            pnlEncabezadoLogo.Dock = DockStyle.Top;
            pnlEncabezadoLogo.Location = new Point(0, 0);
            pnlEncabezadoLogo.Margin = new Padding(3, 4, 3, 4);
            pnlEncabezadoLogo.Name = "pnlEncabezadoLogo";
            pnlEncabezadoLogo.Size = new Size(240, 90);
            pnlEncabezadoLogo.TabIndex = 0;
            // 
            // lblSubtituloLogo
            // 
            lblSubtituloLogo.AutoSize = false;
            lblSubtituloLogo.Font = new Font("Segoe UI", 8.75F);
            lblSubtituloLogo.ForeColor = Color.FromArgb(209, 213, 219);
            lblSubtituloLogo.Location = new Point(18, 52);
            lblSubtituloLogo.Name = "lblSubtituloLogo";
            lblSubtituloLogo.Size = new Size(204, 24);
            lblSubtituloLogo.TabIndex = 1;
            lblSubtituloLogo.Text = "Inventario y Ventas";
            lblSubtituloLogo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTituloLogo
            // 
            lblTituloLogo.AutoSize = false;
            lblTituloLogo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloLogo.ForeColor = Color.White;
            lblTituloLogo.Location = new Point(18, 16);
            lblTituloLogo.Name = "lblTituloLogo";
            lblTituloLogo.Size = new Size(204, 32);
            lblTituloLogo.TabIndex = 0;
            lblTituloLogo.Text = "SISTEMA DE GESTIÓN";
            lblTituloLogo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.White;
            pnlEncabezado.Controls.Add(lblInfoUsuario);
            pnlEncabezado.Controls.Add(lblTituloSeccion);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(240, 0);
            pnlEncabezado.Margin = new Padding(3, 4, 3, 4);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1040, 68);
            pnlEncabezado.TabIndex = 1;
            // 
            // lblInfoUsuario
            // 
            lblInfoUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInfoUsuario.AutoSize = false;
            lblInfoUsuario.Font = new Font("Segoe UI", 9.25F);
            lblInfoUsuario.ForeColor = Color.FromArgb(75, 85, 99);
            lblInfoUsuario.Location = new Point(580, 20);
            lblInfoUsuario.Name = "lblInfoUsuario";
            lblInfoUsuario.Size = new Size(436, 28);
            lblInfoUsuario.TabIndex = 1;
            lblInfoUsuario.Text = "Usuario: admin | Rol: Administrador";
            lblInfoUsuario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTituloSeccion
            // 
            lblTituloSeccion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTituloSeccion.AutoSize = false;
            lblTituloSeccion.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTituloSeccion.ForeColor = Color.FromArgb(30, 58, 138);
            lblTituloSeccion.Location = new Point(22, 16);
            lblTituloSeccion.Name = "lblTituloSeccion";
            lblTituloSeccion.Size = new Size(540, 36);
            lblTituloSeccion.TabIndex = 0;
            lblTituloSeccion.Text = "Inicio";
            lblTituloSeccion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(243, 244, 246);
            pnlContenido.Controls.Add(pnlBienvenida);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(240, 68);
            pnlContenido.Margin = new Padding(3, 4, 3, 4);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(12);
            pnlContenido.Size = new Size(1040, 692);
            pnlContenido.TabIndex = 2;
            // 
            // pnlBienvenida
            // 
            pnlBienvenida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBienvenida.BackColor = Color.White;
            pnlBienvenida.Controls.Add(lblDescripcionBienvenida);
            pnlBienvenida.Controls.Add(lblTituloBienvenida);
            pnlBienvenida.Location = new Point(16, 16);
            pnlBienvenida.Margin = new Padding(3, 4, 3, 4);
            pnlBienvenida.Name = "pnlBienvenida";
            pnlBienvenida.Padding = new Padding(25);
            pnlBienvenida.Size = new Size(1008, 170);
            pnlBienvenida.TabIndex = 0;
            // 
            // lblDescripcionBienvenida
            // 
            lblDescripcionBienvenida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDescripcionBienvenida.AutoSize = false;
            lblDescripcionBienvenida.Font = new Font("Segoe UI", 10F);
            lblDescripcionBienvenida.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescripcionBienvenida.Location = new Point(25, 70);
            lblDescripcionBienvenida.Name = "lblDescripcionBienvenida";
            lblDescripcionBienvenida.Size = new Size(958, 72);
            lblDescripcionBienvenida.TabIndex = 1;
            lblDescripcionBienvenida.Text = "Seleccione una opción del menú lateral para administrar el inventario, categorías, clientes, ventas, pedidos pendientes y reportes.";
            // 
            // lblTituloBienvenida
            // 
            lblTituloBienvenida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTituloBienvenida.AutoSize = false;
            lblTituloBienvenida.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTituloBienvenida.ForeColor = Color.FromArgb(30, 58, 138);
            lblTituloBienvenida.Location = new Point(25, 22);
            lblTituloBienvenida.Name = "lblTituloBienvenida";
            lblTituloBienvenida.Size = new Size(958, 38);
            lblTituloBienvenida.TabIndex = 0;
            lblTituloBienvenida.Text = "Bienvenido al Sistema de Gestión";
            lblTituloBienvenida.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 760);
            Controls.Add(pnlContenido);
            Controls.Add(pnlEncabezado);
            Controls.Add(pnlMenuLateral);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1150, 680);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Inventario y Ventas";
            pnlMenuLateral.ResumeLayout(false);
            pnlEncabezadoLogo.ResumeLayout(false);
            pnlEncabezado.ResumeLayout(false);
            pnlContenido.ResumeLayout(false);
            pnlBienvenida.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenuLateral;
        private Panel pnlEncabezadoLogo;
        private Label lblTituloLogo;
        private Label lblSubtituloLogo;
        private Button btnInicio;
        private Button btnProductos;
        private Button btnCategorias;
        private Button btnClientes;
        private Button btnVentas;
        private Button btnPedidos;
        private Button btnReportes;
        private Button btnHistorial;
        private Button btnCerrarSesion;
        private Panel pnlEncabezado;
        private Label lblTituloSeccion;
        private Label lblInfoUsuario;
        private Panel pnlContenido;
        private Panel pnlBienvenida;
        private Label lblTituloBienvenida;
        private Label lblDescripcionBienvenida;
    }
}
