using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Forms
{
    public partial class ProductEditForm : Form
    {
        private static bool _isOpen;
        private readonly DatabaseHelper _database = new DatabaseHelper();
        private readonly Product _product;
        private readonly Action _onSaved;
        private string _imagePath;
        private string _newImageSource;

        public ProductEditForm(Product product, Action onSaved)
        {
            if (_isOpen)
            {
                MessageBox.Show(
                    "Окно редактирования товара уже открыто.\nЗакройте его перед открытием нового.",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                throw new InvalidOperationException("Edit form already open");
            }

            _isOpen = true;
            _product = product;
            _onSaved = onSaved;
            _imagePath = product != null ? product.ImagePath : null;
            InitializeComponent();
            LoadLookups();
            LoadData();
            FormClosed += ProductEditForm_FormClosed;
        }

        private void ProductEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isOpen = false;
        }

        private void LoadLookups()
        {
            BindCombo(cmbCategory, _database.GetLookup("categories"));
            BindCombo(cmbManufacturer, _database.GetLookup("manufacturers"), true);
            BindCombo(cmbSupplier, _database.GetLookup("suppliers"));
        }

        private static void BindCombo(ComboBox combo, List<KeyValuePair<int, string>> items, bool allowEmpty = false)
        {
            combo.DisplayMember = "Value";
            combo.ValueMember = "Key";
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();
            if (allowEmpty)
            {
                data.Add(new KeyValuePair<int, string>(0, "—"));
            }
            data.AddRange(items);
            combo.DataSource = data;
        }

        private void LoadData()
        {
            Text = _product == null ? "Добавление товара" : "Редактирование товара";

            if (_product != null)
            {
                txtId.Visible = true;
                lblId.Visible = true;
                txtId.Text = _product.Id.ToString();
                txtName.Text = _product.Name;
                cmbCategory.SelectedValue = _product.CategoryId;
                txtDescription.Text = _product.Description;
                cmbManufacturer.SelectedValue = _product.ManufacturerId ?? 0;
                cmbSupplier.SelectedValue = _product.SupplierId ?? 0;
                txtPrice.Text = _product.Price.ToString("0.00", CultureInfo.InvariantCulture);
                txtUnit.Text = _product.Unit;
                txtQuantity.Text = _product.Quantity.ToString();
                txtDiscount.Text = _product.Discount.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                txtId.Visible = false;
                lblId.Visible = false;
                txtUnit.Text = "шт.";
                txtDiscount.Text = "0";
                txtQuantity.Text = "0";
            }

            RefreshPreview();
        }

        private void RefreshPreview()
        {
            string path = ImageHelper.GetDisplayImagePath(_newImageSource ?? _imagePath);
            picPreview.Image = Image.FromFile(path);
        }

        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _newImageSource = dialog.FileName;
                    RefreshPreview();
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out Product model))
            {
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(_newImageSource))
                {
                    model.ImagePath = ImageHelper.SaveProductImage(_newImageSource, _imagePath);
                }
                else
                {
                    model.ImagePath = _imagePath;
                }

                if (_product == null)
                {
                    _database.InsertProduct(model);
                    MessageBox.Show("Товар успешно добавлен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    model.Id = _product.Id;
                    _database.UpdateProduct(model);
                    MessageBox.Show("Товар успешно обновлён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _onSaved();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить товар.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput(out Product model)
        {
            model = new Product();

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Укажите наименование товара.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть числом не меньше 0.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Количество должно быть целым числом не меньше 0.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtDiscount.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal discount) || discount < 0 || discount > 100)
            {
                MessageBox.Show("Скидка должна быть от 0 до 100%.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int manufacturerId = Convert.ToInt32(cmbManufacturer.SelectedValue);
            model.Name = txtName.Text.Trim();
            model.CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            model.Description = txtDescription.Text.Trim();
            model.ManufacturerId = manufacturerId == 0 ? (int?)null : manufacturerId;
            model.SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
            model.Price = decimal.Round(price, 2);
            model.Unit = string.IsNullOrWhiteSpace(txtUnit.Text) ? "шт." : txtUnit.Text.Trim();
            model.Quantity = quantity;
            model.Discount = discount;
            return true;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
