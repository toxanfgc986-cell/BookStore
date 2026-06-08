namespace BookStore.Forms
{
    partial class ProductEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnChooseImage;
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
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            int y = 15;
            int left = 20;
            int fieldX = 160;
            int fieldW = 360;
            // lblId
            this.lblId.Location = new System.Drawing.Point(left, y); this.lblId.Text = "ID:"; this.lblId.AutoSize = true;
            this.txtId.Location = new System.Drawing.Point(fieldX, y); this.txtId.Size = new System.Drawing.Size(fieldW, 20); this.txtId.ReadOnly = true;
            y += 30;
            this.lblName.Location = new System.Drawing.Point(left, y); this.lblName.Text = "Наименование:"; this.lblName.AutoSize = true;
            this.txtName.Location = new System.Drawing.Point(fieldX, y); this.txtName.Size = new System.Drawing.Size(fieldW, 20);
            y += 30;
            this.lblCategory.Location = new System.Drawing.Point(left, y); this.lblCategory.Text = "Категория:"; this.lblCategory.AutoSize = true;
            this.cmbCategory.Location = new System.Drawing.Point(fieldX, y); this.cmbCategory.Size = new System.Drawing.Size(fieldW, 21); this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += 30;
            this.lblDescription.Location = new System.Drawing.Point(left, y); this.lblDescription.Text = "Описание:"; this.lblDescription.AutoSize = true;
            this.txtDescription.Location = new System.Drawing.Point(fieldX, y); this.txtDescription.Size = new System.Drawing.Size(fieldW, 60); this.txtDescription.Multiline = true;
            y += 70;
            this.lblManufacturer.Location = new System.Drawing.Point(left, y); this.lblManufacturer.Text = "Производитель:"; this.lblManufacturer.AutoSize = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(fieldX, y); this.cmbManufacturer.Size = new System.Drawing.Size(fieldW, 21); this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += 30;
            this.lblSupplier.Location = new System.Drawing.Point(left, y); this.lblSupplier.Text = "Поставщик:"; this.lblSupplier.AutoSize = true;
            this.cmbSupplier.Location = new System.Drawing.Point(fieldX, y); this.cmbSupplier.Size = new System.Drawing.Size(fieldW, 21); this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += 30;
            this.lblPrice.Location = new System.Drawing.Point(left, y); this.lblPrice.Text = "Цена:"; this.lblPrice.AutoSize = true;
            this.txtPrice.Location = new System.Drawing.Point(fieldX, y); this.txtPrice.Size = new System.Drawing.Size(fieldW, 20);
            y += 30;
            this.lblUnit.Location = new System.Drawing.Point(left, y); this.lblUnit.Text = "Ед. измерения:"; this.lblUnit.AutoSize = true;
            this.txtUnit.Location = new System.Drawing.Point(fieldX, y); this.txtUnit.Size = new System.Drawing.Size(fieldW, 20);
            y += 30;
            this.lblQuantity.Location = new System.Drawing.Point(left, y); this.lblQuantity.Text = "Количество:"; this.lblQuantity.AutoSize = true;
            this.txtQuantity.Location = new System.Drawing.Point(fieldX, y); this.txtQuantity.Size = new System.Drawing.Size(fieldW, 20);
            y += 30;
            this.lblDiscount.Location = new System.Drawing.Point(left, y); this.lblDiscount.Text = "Скидка (%):"; this.lblDiscount.AutoSize = true;
            this.txtDiscount.Location = new System.Drawing.Point(fieldX, y); this.txtDiscount.Size = new System.Drawing.Size(fieldW, 20);
            y += 35;
            this.picPreview.Location = new System.Drawing.Point(fieldX, y); this.picPreview.Size = new System.Drawing.Size(300, 200); this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            y += 210;
            this.btnChooseImage.Location = new System.Drawing.Point(fieldX, y); this.btnChooseImage.Size = new System.Drawing.Size(180, 25); this.btnChooseImage.Text = "Выбрать изображение"; this.btnChooseImage.Click += new System.EventHandler(this.BtnChooseImage_Click);
            y += 40;
            this.btnSave.Location = new System.Drawing.Point(fieldX, y); this.btnSave.Size = new System.Drawing.Size(100, 30); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.btnBack.Location = new System.Drawing.Point(fieldX + 110, y); this.btnBack.Size = new System.Drawing.Size(100, 30); this.btnBack.Text = "Назад"; this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            this.ClientSize = new System.Drawing.Size(560, y + 60);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblId, this.txtId, this.lblName, this.txtName, this.lblCategory, this.cmbCategory,
                this.lblDescription, this.txtDescription, this.lblManufacturer, this.cmbManufacturer,
                this.lblSupplier, this.cmbSupplier, this.lblPrice, this.txtPrice, this.lblUnit, this.txtUnit,
                this.lblQuantity, this.txtQuantity, this.lblDiscount, this.txtDiscount,
                this.picPreview, this.btnChooseImage, this.btnSave, this.btnBack});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Товар";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
