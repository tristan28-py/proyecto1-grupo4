namespace ManagementSystem.UI.Forms
{
    partial class MainForm
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
            pnlSidebar = new Panel();
            btnLogout = new Button();
            btnHistory = new Button();
            btnReports = new Button();
            btnOrders = new Button();
            btnSales = new Button();
            btnClients = new Button();
            btnCategories = new Button();
            btnProducts = new Button();
            btnDashboard = new Button();
            pnlLogo = new Panel();
            lblLogoSubtitle = new Label();
            lblLogoTitle = new Label();
            pnlHeader = new Panel();
            lblUserInfo = new Label();
            lblSectionTitle = new Label();
            pnlContent = new Panel();
            pnlWelcome = new Panel();
            lblWelcomeDesc = new Label();
            lblWelcomeTitle = new Label();
            pnlSidebar.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlWelcome.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(30, 58, 138);
            pnlSidebar.Controls.Add(btnLogout);
            pnlSidebar.Controls.Add(btnHistory);
            pnlSidebar.Controls.Add(btnReports);
            pnlSidebar.Controls.Add(btnOrders);
            pnlSidebar.Controls.Add(btnSales);
            pnlSidebar.Controls.Add(btnClients);
            pnlSidebar.Controls.Add(btnCategories);
            pnlSidebar.Controls.Add(btnProducts);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(pnlLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(3, 4, 3, 4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(230, 700);
            pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(23, 44, 105);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9.5F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 650);
            btnLogout.Margin = new Padding(0);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(20, 0, 0, 0);
            btnLogout.Size = new Size(230, 50);
            btnLogout.TabIndex = 9;
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnHistory
            // 
            btnHistory.Cursor = Cursors.Hand;
            btnHistory.Dock = DockStyle.Top;
            btnHistory.FlatAppearance.BorderSize = 0;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 9.5F);
            btnHistory.ForeColor = Color.White;
            btnHistory.Location = new Point(0, 395);
            btnHistory.Margin = new Padding(0);
            btnHistory.Name = "btnHistory";
            btnHistory.Padding = new Padding(20, 0, 0, 0);
            btnHistory.Size = new Size(230, 45);
            btnHistory.TabIndex = 8;
            btnHistory.Text = "Historial";
            btnHistory.TextAlign = ContentAlignment.MiddleLeft;
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // btnReports
            // 
            btnReports.Cursor = Cursors.Hand;
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 9.5F);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 350);
            btnReports.Margin = new Padding(0);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(20, 0, 0, 0);
            btnReports.Size = new Size(230, 45);
            btnReports.TabIndex = 7;
            btnReports.Text = "Reportes";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;
            // 
            // btnOrders
            // 
            btnOrders.Cursor = Cursors.Hand;
            btnOrders.Dock = DockStyle.Top;
            btnOrders.FlatAppearance.BorderSize = 0;
            btnOrders.FlatStyle = FlatStyle.Flat;
            btnOrders.Font = new Font("Segoe UI", 9.5F);
            btnOrders.ForeColor = Color.White;
            btnOrders.Location = new Point(0, 305);
            btnOrders.Margin = new Padding(0);
            btnOrders.Name = "btnOrders";
            btnOrders.Padding = new Padding(20, 0, 0, 0);
            btnOrders.Size = new Size(230, 45);
            btnOrders.TabIndex = 6;
            btnOrders.Text = "Pedidos";
            btnOrders.TextAlign = ContentAlignment.MiddleLeft;
            btnOrders.UseVisualStyleBackColor = true;
            btnOrders.Click += btnOrders_Click;
            // 
            // btnSales
            // 
            btnSales.Cursor = Cursors.Hand;
            btnSales.Dock = DockStyle.Top;
            btnSales.FlatAppearance.BorderSize = 0;
            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.Font = new Font("Segoe UI", 9.5F);
            btnSales.ForeColor = Color.White;
            btnSales.Location = new Point(0, 260);
            btnSales.Margin = new Padding(0);
            btnSales.Name = "btnSales";
            btnSales.Padding = new Padding(20, 0, 0, 0);
            btnSales.Size = new Size(230, 45);
            btnSales.TabIndex = 5;
            btnSales.Text = "Registrar Venta";
            btnSales.TextAlign = ContentAlignment.MiddleLeft;
            btnSales.UseVisualStyleBackColor = true;
            btnSales.Click += btnSales_Click;
            // 
            // btnClients
            // 
            btnClients.Cursor = Cursors.Hand;
            btnClients.Dock = DockStyle.Top;
            btnClients.FlatAppearance.BorderSize = 0;
            btnClients.FlatStyle = FlatStyle.Flat;
            btnClients.Font = new Font("Segoe UI", 9.5F);
            btnClients.ForeColor = Color.White;
            btnClients.Location = new Point(0, 215);
            btnClients.Margin = new Padding(0);
            btnClients.Name = "btnClients";
            btnClients.Padding = new Padding(20, 0, 0, 0);
            btnClients.Size = new Size(230, 45);
            btnClients.TabIndex = 4;
            btnClients.Text = "Clientes y Personas";
            btnClients.TextAlign = ContentAlignment.MiddleLeft;
            btnClients.UseVisualStyleBackColor = true;
            btnClients.Click += btnClients_Click;
            // 
            // btnCategories
            // 
            btnCategories.Cursor = Cursors.Hand;
            btnCategories.Dock = DockStyle.Top;
            btnCategories.FlatAppearance.BorderSize = 0;
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.Font = new Font("Segoe UI", 9.5F);
            btnCategories.ForeColor = Color.White;
            btnCategories.Location = new Point(0, 170);
            btnCategories.Margin = new Padding(0);
            btnCategories.Name = "btnCategories";
            btnCategories.Padding = new Padding(20, 0, 0, 0);
            btnCategories.Size = new Size(230, 45);
            btnCategories.TabIndex = 3;
            btnCategories.Text = "Categorías";
            btnCategories.TextAlign = ContentAlignment.MiddleLeft;
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.Click += btnCategories_Click;
            // 
            // btnProducts
            // 
            btnProducts.Cursor = Cursors.Hand;
            btnProducts.Dock = DockStyle.Top;
            btnProducts.FlatAppearance.BorderSize = 0;
            btnProducts.FlatStyle = FlatStyle.Flat;
            btnProducts.Font = new Font("Segoe UI", 9.5F);
            btnProducts.ForeColor = Color.White;
            btnProducts.Location = new Point(0, 125);
            btnProducts.Margin = new Padding(0);
            btnProducts.Name = "btnProducts";
            btnProducts.Padding = new Padding(20, 0, 0, 0);
            btnProducts.Size = new Size(230, 45);
            btnProducts.TabIndex = 2;
            btnProducts.Text = "Productos";
            btnProducts.TextAlign = ContentAlignment.MiddleLeft;
            btnProducts.UseVisualStyleBackColor = true;
            btnProducts.Click += btnProducts_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(23, 44, 105);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 80);
            btnDashboard.Margin = new Padding(0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(20, 0, 0, 0);
            btnDashboard.Size = new Size(230, 45);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "Inicio";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = Color.FromArgb(23, 44, 105);
            pnlLogo.Controls.Add(lblLogoSubtitle);
            pnlLogo.Controls.Add(lblLogoTitle);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Margin = new Padding(3, 4, 3, 4);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(230, 80);
            pnlLogo.TabIndex = 0;
            // 
            // lblLogoSubtitle
            // 
            lblLogoSubtitle.AutoSize = true;
            lblLogoSubtitle.Font = new Font("Segoe UI", 8F);
            lblLogoSubtitle.ForeColor = Color.FromArgb(209, 213, 219);
            lblLogoSubtitle.Location = new Point(20, 45);
            lblLogoSubtitle.Name = "lblLogoSubtitle";
            lblLogoSubtitle.Size = new Size(127, 19);
            lblLogoSubtitle.TabIndex = 1;
            lblLogoSubtitle.Text = "Inventario y Ventas";
            // 
            // lblLogoTitle
            // 
            lblLogoTitle.AutoSize = true;
            lblLogoTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblLogoTitle.ForeColor = Color.White;
            lblLogoTitle.Location = new Point(18, 18);
            lblLogoTitle.Name = "lblLogoTitle";
            lblLogoTitle.Size = new Size(187, 28);
            lblLogoTitle.TabIndex = 0;
            lblLogoTitle.Text = "SISTEMA GESTIÓN";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblUserInfo);
            pnlHeader.Controls.Add(lblSectionTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(230, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(870, 70);
            pnlHeader.TabIndex = 1;
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 9F);
            lblUserInfo.ForeColor = Color.FromArgb(75, 85, 99);
            lblUserInfo.Location = new Point(600, 26);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(269, 20);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Usuario: admin | Rol: Administrador";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSectionTitle
            // 
            lblSectionTitle.AutoSize = true;
            lblSectionTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSectionTitle.ForeColor = Color.FromArgb(30, 58, 138);
            lblSectionTitle.Location = new Point(25, 18);
            lblSectionTitle.Name = "lblSectionTitle";
            lblSectionTitle.Size = new Size(227, 32);
            lblSectionTitle.TabIndex = 0;
            lblSectionTitle.Text = "Inicio";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(243, 244, 246);
            pnlContent.Controls.Add(pnlWelcome);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(230, 70);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(25);
            pnlContent.Size = new Size(870, 630);
            pnlContent.TabIndex = 2;
            // 
            // pnlWelcome
            // 
            pnlWelcome.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlWelcome.BackColor = Color.White;
            pnlWelcome.Controls.Add(lblWelcomeDesc);
            pnlWelcome.Controls.Add(lblWelcomeTitle);
            pnlWelcome.Location = new Point(25, 25);
            pnlWelcome.Margin = new Padding(3, 4, 3, 4);
            pnlWelcome.Name = "pnlWelcome";
            pnlWelcome.Padding = new Padding(25);
            pnlWelcome.Size = new Size(820, 160);
            pnlWelcome.TabIndex = 0;
            // 
            // lblWelcomeDesc
            // 
            lblWelcomeDesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblWelcomeDesc.Font = new Font("Segoe UI", 9.5F);
            lblWelcomeDesc.ForeColor = Color.FromArgb(75, 85, 99);
            lblWelcomeDesc.Location = new Point(25, 70);
            lblWelcomeDesc.Name = "lblWelcomeDesc";
            lblWelcomeDesc.Size = new Size(770, 65);
            lblWelcomeDesc.TabIndex = 1;
            lblWelcomeDesc.Text = "Seleccione una opción del menú lateral para administrar el inventario, clientes, ventas, pedidos y reportes.";
            // 
            // lblWelcomeTitle
            // 
            lblWelcomeTitle.AutoSize = true;
            lblWelcomeTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWelcomeTitle.ForeColor = Color.FromArgb(30, 58, 138);
            lblWelcomeTitle.Location = new Point(25, 25);
            lblWelcomeTitle.Name = "lblWelcomeTitle";
            lblWelcomeTitle.Size = new Size(409, 32);
            lblWelcomeTitle.TabIndex = 0;
            lblWelcomeTitle.Text = "Bienvenido al Sistema de Gestión";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 700);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Inventario y Ventas";
            pnlSidebar.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlLogo.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlWelcome.ResumeLayout(false);
            pnlWelcome.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlLogo;
        private Label lblLogoTitle;
        private Label lblLogoSubtitle;
        private Button btnDashboard;
        private Button btnProducts;
        private Button btnCategories;
        private Button btnClients;
        private Button btnSales;
        private Button btnOrders;
        private Button btnReports;
        private Button btnHistory;
        private Button btnLogout;
        private Panel pnlHeader;
        private Label lblSectionTitle;
        private Label lblUserInfo;
        private Panel pnlContent;
        private Panel pnlWelcome;
        private Label lblWelcomeTitle;
        private Label lblWelcomeDesc;
    }
}
