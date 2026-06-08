using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Forms
{
    public partial class OrderEditForm : Form
    {
        private readonly DatabaseHelper _database = new DatabaseHelper();
        private readonly Order _order;
        private readonly Action _onSaved;

        public OrderEditForm(Order order, Action onSaved)
        {
            _order = order;
            _onSaved = onSaved;
            InitializeComponent();
            BindStatuses();
            LoadData();
        }

        private void BindStatuses()
        {
            List<KeyValuePair<int, string>> statuses = _database.GetLookup("order_statuses");
            cmbStatus.DisplayMember = "Value";
            cmbStatus.ValueMember = "Key";
            cmbStatus.DataSource = statuses;
        }

        private void LoadData()
        {
            Text = _order == null ? "Добавление заказа" : "Редактирование заказа";
            if (_order != null)
            {
                txtArticle.Text = _order.Article;
                cmbStatus.SelectedValue = _order.StatusId;
                txtPickup.Text = _order.PickupAddress;
                dtpOrderDate.Value = _order.OrderDate;
                if (_order.DeliveryDate.HasValue)
                {
                    dtpDeliveryDate.Value = _order.DeliveryDate.Value;
                    chkDeliveryDate.Checked = true;
                }
                else
                {
                    chkDeliveryDate.Checked = false;
                }
            }
            else
            {
                dtpOrderDate.Value = DateTime.Today;
                chkDeliveryDate.Checked = false;
            }
            dtpDeliveryDate.Enabled = chkDeliveryDate.Checked;
        }

        private void ChkDeliveryDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpDeliveryDate.Enabled = chkDeliveryDate.Checked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Укажите артикул заказа.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPickup.Text))
            {
                MessageBox.Show("Укажите адрес пункта выдачи.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Order model = new Order
            {
                Article = txtArticle.Text.Trim(),
                StatusId = Convert.ToInt32(cmbStatus.SelectedValue),
                PickupAddress = txtPickup.Text.Trim(),
                OrderDate = dtpOrderDate.Value.Date,
                DeliveryDate = chkDeliveryDate.Checked ? dtpDeliveryDate.Value.Date : (DateTime?)null
            };

            try
            {
                if (_order == null)
                {
                    _database.InsertOrder(model);
                    MessageBox.Show("Заказ успешно добавлен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    model.Id = _order.Id;
                    _database.UpdateOrder(model);
                    MessageBox.Show("Заказ успешно обновлён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _onSaved();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить заказ.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
