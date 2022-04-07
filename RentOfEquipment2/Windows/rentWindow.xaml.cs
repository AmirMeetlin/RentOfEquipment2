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
    /// Логика взаимодействия для rentWindow.xaml
    /// </summary>
    public partial class rentWindow : Window
    {
        public rentWindow()
        {
            InitializeComponent();
            lvRent.ItemsSource= ClassHelper.AppData.Conrext.Rent.ToList();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddRent_Click(object sender, RoutedEventArgs e)
        {
            AddRent addRent = new AddRent();
            this.Hide();
            addRent.ShowDialog();
            this.Show();
            lvRent.ItemsSource = ClassHelper.AppData.Conrext.Rent.ToList();
        }

    }
}
