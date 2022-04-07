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
using static RentOfEquipment2.ClassHelper.DataTransmission;


namespace RentOfEquipment2.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddRent.xaml
    /// </summary>
    public partial class AddRent : Window
    {
        public AddRent()
        {
            InitializeComponent();
        }

        private void btnChooseClient_Click(object sender, RoutedEventArgs e)
        {
            ChooseClient chooseClient = new ChooseClient();
            chooseClient.ShowDialog();
            btnChooseClient.FontSize = 15;
            btnChooseClient.Content = GetClient.FIO;
            
        }

        private void btnChooseEmployee_Click(object sender, RoutedEventArgs e)
        {
            ChooseEmployee chooseEmployee = new ChooseEmployee();
            chooseEmployee.ShowDialog();
            btnChooseEmployee.FontSize = 15;
            btnChooseEmployee.Content = GetEmployee.FIO;
        }

        private void btnChooseEquipment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
