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
        List<string> listSort = new List<string>()
            {
                "По умолчанию",
                "По клиенту",
                "По сотруднику",
                "По названию",
                "По цене"
            };
        public rentWindow()
        {
            InitializeComponent();
            lvRent.ItemsSource= ClassHelper.AppData.Conrext.Rent.ToList();
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }
        private void Filter()
        {
            List<EF.Rent> listRent = new List<EF.Rent>();
            listRent = ClassHelper.AppData.Conrext.Rent.Where(i => i.IsDeleted == false).ToList();

            listRent = listRent.
                Where(i => i.Client.SecondName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Employee.SecondName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Product.Product1.ToLower().Contains(tbSearch.Text.ToLower())
                || i.TotalCost.ToString().ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listRent = listRent.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listRent = listRent.OrderBy(i => i.Client.SecondName).ToList();
                    break;
                case 2:
                    listRent = listRent.OrderBy(i => i.Employee.SecondName).ToList();
                    break;
                case 3:
                    listRent = listRent.OrderBy(i => i.Product.Product1).ToList();
                    break;
                case 4:
                    listRent = listRent.OrderBy(i => i.TotalCost).ToList();
                    break;
            }

            lvRent.ItemsSource = listRent;
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddRent_Click(object sender, RoutedEventArgs e)
        {
            AddRent addRent = new AddRent();
            this.Hide();
            addRent.ShowDialog();
            this.Show();
            Filter();
        }

        private void lvRent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var rent = lvRent.SelectedItem as EF.Rent;
            ReturnOfEquipment returnOfEquipment = new ReturnOfEquipment(rent);
            returnOfEquipment.ShowDialog();
            Filter();
        }

    }
}
