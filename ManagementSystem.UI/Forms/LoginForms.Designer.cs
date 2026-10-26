namespace ManagementSystem.UI.Forms
{
    partial class LoginForms
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
            pnlEncabezado = new Panel();
            lblSubtituloEncabezado = new Label();
            lblTituloEncabezado = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            chkMostrarContrasena = new CheckBox();
            btnIniciarSesion = new Button();
            btnSalir = new Button();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(30, 58, 138);
            pnlEncabezado.Controls.Add(lblSubtituloEncabezado);
            pnlEncabezado.Controls.Add(lblTituloEncabezado);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Margin = new Padding(3, 4, 3, 4);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(460, 118);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblSubtituloEncabezado
            // 
            lblSubtituloEncabezado.AutoSize = false;
            lblSubtituloEncabezado.Font = new Font("Segoe UI", 9.5F);
            lblSubtituloEncabezado.ForeColor = Color.FromArgb(209, 213, 219);
            lblSubtituloEncabezado.Location = new Point(32, 66);
            lblSubtituloEncabezado.Name = "lblSubtituloEncabezado";
            lblSubtituloEncabezado.Size = new Size(396, 28);
            lblSubtituloEncabezado.TabIndex = 1;
            lblSubtituloEncabezado.Text = "Ingrese sus credenciales de acceso";
            lblSubtituloEncabezado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTituloEncabezado
            // 
            lblTituloEncabezado.AutoSize = false;
            lblTituloEncabezado.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTituloEncabezado.ForeColor = Color.White;
            lblTituloEncabezado.Location = new Point(30, 22);
            lblTituloEncabezado.Name = "lblTituloEncabezado";
            lblTituloEncabezado.Size = new Size(400, 40);
            lblTituloEncabezado.TabIndex = 0;
            lblTituloEncabezado.Text = "SISTEMA DE GESTIÓN";
            lblTituloEncabezado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = false;
            lblUsuario.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.FromArgb(55, 65, 81);
            lblUsuario.Location = new Point(45, 148);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(370, 26);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuario:";
            lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 10.5F);
            txtUsuario.Location = new Point(45, 178);
            txtUsuario.Margin = new Padding(3, 4, 3, 4);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Ingrese su nombre de usuario";
            txtUsuario.Size = new Size(370, 31);
            txtUsuario.TabIndex = 2;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = false;
            lblContrasena.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblContrasena.ForeColor = Color.FromArgb(55, 65, 81);
            lblContrasena.Location = new Point(45, 232);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(370, 26);
            lblContrasena.TabIndex = 3;
            lblContrasena.Text = "Contraseña:";
            lblContrasena.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtContrasena
            // 
            txtContrasena.Font = new Font("Segoe UI", 10.5F);
            txtContrasena.Location = new Point(45, 262);
            txtContrasena.Margin = new Padding(3, 4, 3, 4);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PlaceholderText = "••••••••";
            txtContrasena.Size = new Size(370, 31);
            txtContrasena.TabIndex = 4;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // chkMostrarContrasena
            // 
            chkMostrarContrasena.AutoSize = true;
            chkMostrarContrasena.Cursor = Cursors.Hand;
            chkMostrarContrasena.Font = new Font("Segoe UI", 9.25F);
            chkMostrarContrasena.ForeColor = Color.FromArgb(75, 85, 99);
            chkMostrarContrasena.Location = new Point(45, 308);
            chkMostrarContrasena.Margin = new Padding(3, 4, 3, 4);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Size = new Size(165, 25);
            chkMostrarContrasena.TabIndex = 5;
            chkMostrarContrasena.Text = "Mostrar contraseña";
            chkMostrarContrasena.UseVisualStyleBackColor = true;
            chkMostrarContrasena.CheckedChanged += chkMostrarContrasena_CheckedChanged;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.BackColor = Color.FromArgb(30, 58, 138);
            btnIniciarSesion.Cursor = Cursors.Hand;
            btnIniciarSesion.FlatAppearance.BorderSize = 0;
            btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            btnIniciarSesion.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnIniciarSesion.ForeColor = Color.White;
            btnIniciarSesion.Location = new Point(45, 360);
            btnIniciarSesion.Margin = new Padding(3, 4, 3, 4);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(370, 50);
            btnIniciarSesion.TabIndex = 6;
            btnIniciarSesion.Text = "Iniciar Sesión";
            btnIniciarSesion.UseVisualStyleBackColor = false;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(243, 244, 246);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9.75F);
            btnSalir.ForeColor = Color.FromArgb(75, 85, 99);
            btnSalir.Location = new Point(45, 424);
            btnSalir.Margin = new Padding(3, 4, 3, 4);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(370, 44);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // LoginForms
            // 
            AcceptButton = btnIniciarSesion;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnSalir;
            ClientSize = new Size(460, 515);
            Controls.Add(btnSalir);
            Controls.Add(btnIniciarSesion);
            Controls.Add(chkMostrarContrasena);
            Controls.Add(txtContrasena);
            Controls.Add(lblContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Acceso al Sistema";
            pnlEncabezado.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTituloEncabezado;
        private System.Windows.Forms.Label lblSubtituloEncabezado;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.CheckBox chkMostrarContrasena;
        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.Button btnSalir;
    }
}
