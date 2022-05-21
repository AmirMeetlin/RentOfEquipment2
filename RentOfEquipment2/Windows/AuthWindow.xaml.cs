using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentOfEquipment2.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var authUser = ClassHelper.AppData.Conrext.Employee.ToList().
                Where(i => i.Login == tbLogin.Text && i.Password == tbPass.Password).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Введите логин!");
            }
            else if (string.IsNullOrWhiteSpace(tbPass.Password))
            {
                MessageBox.Show("Введите пароль!");
            }
            else if(authUser != null)
            {               
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.ShowDialog();
                this.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}
