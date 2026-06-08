using System;
using System.Windows.Forms;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Forms
{
    public partial class OrdersForm : Form
    {
        private readonly DatabaseHelper _database = new DatabaseHelper();
        private readonly User _user;

        public OrdersForm(User user)
        {
            _user = user;
            InitializeComponent();
            btnAdd.Enabled = _user.RoleId == AppConstants.RoleAdmin;
            btnDelete.Enabled = _user.RoleId == AppConstants.RoleAdmin;
            colActions.Visible = _user.RoleId == AppConstants.RoleAdmin;
            LoadOrders();
        }

        private void LoadOrders()
        {
            gridOrders.Rows.Clear();
            foreach (Order order in _database.GetOrders())
            {
                int rowIndex = gridOrders.Rows.Add(
                    order.Article,
                    order.StatusName,
                    order.PickupAddress,
                    order.OrderDate.ToString("yyyy-MM-dd"),
                    order.DeliveryDate.HasValue ? order.DeliveryDate.Value.ToString("yyyy-MM-dd") : string.Empty);
                gridOrders.Rows[rowIndex].Tag = order.Id;
            }
        }

        private int? GetSelectedOrderId()
        {
            if (gridOrders.SelectedRows.Count == 0)
            {
                return null;
            }
            return (int)gridOrders.SelectedRows[0].Tag;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OrderEditForm form = new OrderEditForm(null, LoadOrders);
            form.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int? orderId = GetSelectedOrderId();
            if (!orderId.HasValue)
            {
                MessageBox.Show("Выберите заказ для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Order order = _database.GetOrderById(orderId.Value);
            OrderEditForm form = new OrderEditForm(order, LoadOrders);
            form.ShowDialog();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int? orderId = GetSelectedOrderId();
            if (!orderId.HasValue)
            {
                MessageBox.Show("Выберите заказ для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Order order = _database.GetOrderById(orderId.Value);
            DialogResult result = MessageBox.Show(
                "Удалить заказ «" + order.Article + "»?\nЭто действие необратимо.",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }
            _database.DeleteOrder(order.Id);
            MessageBox.Show("Заказ удалён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrders();
        }

        private void GridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_user.RoleId != AppConstants.RoleAdmin || e.RowIndex < 0)
            {
                return;
            }
            BtnEdit_Click(sender, e);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
