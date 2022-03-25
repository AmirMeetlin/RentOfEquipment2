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
    /// Логика взаимодействия для EquipmentList.xaml
    /// </summary>
    public partial class EquipmentList : Window
    {
        List<string> listSort = new List<string>()
            {
                "По умолчанию",
                "По названию",
                "По цене",
                "По статусу",
                "По типу"
            };
        public EquipmentList()
        {
            InitializeComponent();
            lvEquipment.ItemsSource = ClassHelper.AppData.Conrext.Product.ToList();
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }
        private void Filter()
        {
            List<EF.Product> listClient = new List<EF.Product>();
            listClient = ClassHelper.AppData.Conrext.Product.Where(i => i.IsDeleted == false).ToList();

            listClient = listClient.
                Where(i => i.Product1.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Description.ToLower().Contains(tbSearch.Text.ToLower())
                || i.TypeProduct.TypeProduct1.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.OrderBy(i => i.Product1).ToList();
                    break;
                case 2:
                    listClient = listClient.OrderBy(i => i.Cost).ToList();
                    break;
                case 3:
                    listClient = listClient.OrderBy(i => i.IsRent).ToList();
                    break;
                case 4:
                    listClient = listClient.OrderBy(i => i.TypeProduct.TypeProduct1).ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvEquipment.ItemsSource = listClient;
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            AddEquipment addEquipment = new AddEquipment();
            addEquipment.ShowDialog();
            Filter();
        }

        private void lvEquipment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var equipment = lvEquipment.SelectedItem as EF.Product;
            AddEquipment addEquipment = new AddEquipment(equipment);
            addEquipment.ShowDialog();
            Filter();
        }

        private void lvEquipment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show("Удалить товар?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                try
                {
                    if (lvEquipment.SelectedItem is EF.Product)
                    {
                        var equip = lvEquipment.SelectedItem as EF.Product;

                        equip.IsDeleted = true;

                        ClassHelper.AppData.Conrext.SaveChanges();

                        MessageBox.Show("Товар успешно удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
