namespace BookStore.Forms
{
    partial class OrdersForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArticle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPickup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeliveryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBack;

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
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.colArticle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPickup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
            this.SuspendLayout();
            this.btnAdd.Location = new System.Drawing.Point(12, 12); this.btnAdd.Size = new System.Drawing.Size(120, 25); this.btnAdd.Text = "Добавить заказ"; this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            this.btnEdit.Location = new System.Drawing.Point(140, 12); this.btnEdit.Size = new System.Drawing.Size(90, 25); this.btnEdit.Text = "Изменить"; this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            this.btnDelete.Location = new System.Drawing.Point(236, 12); this.btnDelete.Size = new System.Drawing.Size(90, 25); this.btnDelete.Text = "Удалить"; this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBack.Location = new System.Drawing.Point(800, 12); this.btnBack.Size = new System.Drawing.Size(80, 25); this.btnBack.Text = "Назад"; this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            this.gridOrders.AllowUserToAddRows = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            this.gridOrders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gridOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrders.Location = new System.Drawing.Point(12, 45);
            this.gridOrders.Size = new System.Drawing.Size(868, 400);
            this.gridOrders.ReadOnly = true;
            this.gridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.MultiSelect = false;
            this.gridOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridOrders_CellDoubleClick);
            this.colArticle.HeaderText = "Артикул заказа";
            this.colStatus.HeaderText = "Статус заказа";
            this.colPickup.HeaderText = "Адрес пункта выдачи";
            this.colOrderDate.HeaderText = "Дата заказа";
            this.colDeliveryDate.HeaderText = "Дата доставки";
            this.colActions.HeaderText = "";
            this.colActions.Visible = false;
            this.gridOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colArticle, this.colStatus, this.colPickup, this.colOrderDate, this.colDeliveryDate, this.colActions});
            this.ClientSize = new System.Drawing.Size(900, 460);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gridOrders);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заказы";
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
