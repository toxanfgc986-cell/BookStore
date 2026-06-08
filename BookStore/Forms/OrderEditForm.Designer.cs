namespace BookStore.Forms
{
    partial class OrderEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblArticle;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblPickup;
        private System.Windows.Forms.TextBox txtPickup;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.CheckBox chkDeliveryDate;
        private System.Windows.Forms.Button btnSave;
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
            this.lblArticle = new System.Windows.Forms.Label();
            this.txtArticle = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblPickup = new System.Windows.Forms.Label();
            this.txtPickup = new System.Windows.Forms.TextBox();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.chkDeliveryDate = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            int y = 20; int left = 20; int fx = 180; int fw = 300;
            this.lblArticle.Location = new System.Drawing.Point(left, y); this.lblArticle.Text = "Артикул заказа:"; this.lblArticle.AutoSize = true;
            this.txtArticle.Location = new System.Drawing.Point(fx, y); this.txtArticle.Size = new System.Drawing.Size(fw, 20);
            y += 35;
            this.lblStatus.Location = new System.Drawing.Point(left, y); this.lblStatus.Text = "Статус заказа:"; this.lblStatus.AutoSize = true;
            this.cmbStatus.Location = new System.Drawing.Point(fx, y); this.cmbStatus.Size = new System.Drawing.Size(fw, 21); this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += 35;
            this.lblPickup.Location = new System.Drawing.Point(left, y); this.lblPickup.Text = "Адрес пункта выдачи:"; this.lblPickup.AutoSize = true;
            this.txtPickup.Location = new System.Drawing.Point(fx, y); this.txtPickup.Size = new System.Drawing.Size(fw, 20);
            y += 35;
            this.lblOrderDate.Location = new System.Drawing.Point(left, y); this.lblOrderDate.Text = "Дата заказа:"; this.lblOrderDate.AutoSize = true;
            this.dtpOrderDate.Location = new System.Drawing.Point(fx, y); this.dtpOrderDate.Size = new System.Drawing.Size(fw, 20); this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            y += 35;
            this.lblDeliveryDate.Location = new System.Drawing.Point(left, y); this.lblDeliveryDate.Text = "Дата доставки:"; this.lblDeliveryDate.AutoSize = true;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(fx, y); this.dtpDeliveryDate.Size = new System.Drawing.Size(fw, 20); this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.chkDeliveryDate.Location = new System.Drawing.Point(fx, y + 28); this.chkDeliveryDate.Text = "Указать дату доставки"; this.chkDeliveryDate.AutoSize = true; this.chkDeliveryDate.CheckedChanged += new System.EventHandler(this.ChkDeliveryDate_CheckedChanged);
            y += 65;
            this.btnSave.Location = new System.Drawing.Point(fx, y); this.btnSave.Size = new System.Drawing.Size(100, 30); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.btnBack.Location = new System.Drawing.Point(fx + 110, y); this.btnBack.Size = new System.Drawing.Size(100, 30); this.btnBack.Text = "Назад"; this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            this.ClientSize = new System.Drawing.Size(520, y + 60);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblArticle, this.txtArticle, this.lblStatus, this.cmbStatus, this.lblPickup, this.txtPickup,
                this.lblOrderDate, this.dtpOrderDate, this.lblDeliveryDate, this.dtpDeliveryDate, this.chkDeliveryDate,
                this.btnSave, this.btnBack});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заказ";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
