namespace BookStore.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.ComboBox cmbDiscountFilter;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.FlowLayoutPanel flowProducts;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.cmbDiscountFilter = new System.Windows.Forms.ComboBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.flowProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(46, 90, 172);
            this.panelHeader.Controls.Add(this.lblUserInfo);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Size = new System.Drawing.Size(1100, 50);
            // lblHeader
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 14);
            this.lblHeader.Text = "Магазин книг — Список товаров";
            // lblUserInfo
            this.lblUserInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblUserInfo.ForeColor = System.Drawing.Color.White;
            this.lblUserInfo.Location = new System.Drawing.Point(700, 16);
            this.lblUserInfo.Size = new System.Drawing.Size(380, 20);
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(12, 60);
            this.txtSearch.Size = new System.Drawing.Size(220, 20);
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // cmbSort
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.Location = new System.Drawing.Point(250, 60);
            this.cmbSort.Size = new System.Drawing.Size(200, 21);
            this.cmbSort.Items.AddRange(new object[] {
                "Без сортировки",
                "Цена по возрастанию",
                "Цена по убыванию",
                "Количество по возрастанию",
                "Количество по убыванию"});
            this.cmbSort.SelectedIndex = 0;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.CmbSort_SelectedIndexChanged);
            // cmbDiscountFilter
            this.cmbDiscountFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiscountFilter.Location = new System.Drawing.Point(470, 60);
            this.cmbDiscountFilter.Size = new System.Drawing.Size(160, 21);
            this.cmbDiscountFilter.Items.AddRange(new object[] {
                "Все диапазоны",
                "0 - 12.99%",
                "13 - 16.99%",
                "17% и более"});
            this.cmbDiscountFilter.SelectedIndex = 0;
            this.cmbDiscountFilter.SelectedIndexChanged += new System.EventHandler(this.CmbDiscountFilter_SelectedIndexChanged);
            // btnLogout
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLogout.Location = new System.Drawing.Point(1000, 58);
            this.btnLogout.Size = new System.Drawing.Size(80, 25);
            this.btnLogout.Text = "Выйти";
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // btnOrders
            this.btnOrders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnOrders.Location = new System.Drawing.Point(910, 58);
            this.btnOrders.Size = new System.Drawing.Size(80, 25);
            this.btnOrders.Text = "Заказы";
            this.btnOrders.Click += new System.EventHandler(this.BtnOrders_Click);
            // btnAddProduct
            this.btnAddProduct.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnAddProduct.Location = new System.Drawing.Point(800, 58);
            this.btnAddProduct.Size = new System.Drawing.Size(100, 25);
            this.btnAddProduct.Text = "Добавить товар";
            this.btnAddProduct.Click += new System.EventHandler(this.BtnAddProduct_Click);
            // btnDeleteProduct
            this.btnDeleteProduct.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnDeleteProduct.Location = new System.Drawing.Point(680, 58);
            this.btnDeleteProduct.Size = new System.Drawing.Size(110, 25);
            this.btnDeleteProduct.Text = "Удалить товар";
            this.btnDeleteProduct.Click += new System.EventHandler(this.BtnDeleteProduct_Click);
            // flowProducts
            this.flowProducts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.flowProducts.AutoScroll = true;
            this.flowProducts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowProducts.Location = new System.Drawing.Point(12, 95);
            this.flowProducts.Size = new System.Drawing.Size(1068, 555);
            this.flowProducts.WrapContents = false;
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 670);
            this.Controls.Add(this.flowProducts);
            this.Controls.Add(this.btnDeleteProduct);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.cmbDiscountFilter);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.panelHeader);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список товаров";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
