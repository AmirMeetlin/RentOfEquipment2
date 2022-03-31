using RentOfEquipment2.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentOfEquipment2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            this.Hide();
            employeeWindow.ShowDialog();
            this.Show();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ListOfClients listOfClients = new ListOfClients();
            this.Hide();
            listOfClients.ShowDialog();
            this.Show();
        }

        private void btnEquipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentList equipmentList = new EquipmentList();
            this.Hide();
            equipmentList.ShowDialog();
            this.Show();
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            rentWindow rentWindow = new rentWindow();
            this.Hide();
            rentWindow.ShowDialog();
            this.Show();
        }
    }
}
