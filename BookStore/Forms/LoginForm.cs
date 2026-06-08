using System;
using System.Windows.Forms;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Forms
{
    public partial class LoginForm : Form
    {
        private readonly DatabaseHelper _database = new DatabaseHelper();

        public LoginForm()
        {
            InitializeComponent();
            ImageHelper.CreatePlaceholderIfMissing();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Введите логин и пароль.\nДля входа без учётной записи нажмите «Войти как гость».",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            User user = _database.Authenticate(login, password);
            if (user == null)
            {
                MessageBox.Show(
                    "Неверный логин или пароль.\nПроверьте данные и повторите попытку.",
                    "Ошибка авторизации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            OpenMainForm(user);
        }

        private void BtnGuest_Click(object sender, EventArgs e)
        {
            OpenMainForm(DatabaseHelper.CreateGuestUser());
        }

        private void OpenMainForm(User user)
        {
            Hide();
            MainForm mainForm = new MainForm(user, this);
            mainForm.Show();
        }
    }
}
