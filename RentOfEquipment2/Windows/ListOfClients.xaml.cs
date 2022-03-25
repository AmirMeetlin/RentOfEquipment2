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
    /// Логика взаимодействия для ListOfClients.xaml
    /// </summary>
    public partial class ListOfClients : Window
    {

        List<string> listSort = new List<string>()
            {
                "По умолчанию",
                "По фамилии",
                "По имени",
                "По дню рождения",
                "По почте"
            };
        public ListOfClients()
        {
            InitializeComponent();
            lvClient.ItemsSource = ClassHelper.AppData.Conrext.Client.ToList();
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }

        private void Filter()
        {
            List<EF.Client> listClient = new List<EF.Client>();
            listClient = ClassHelper.AppData.Conrext.Client.Where(i => i.IsDeleted==false).ToList();

            listClient = listClient.
                Where(i => i.SecondName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Patronymic.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Email.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Phone.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FIO.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.OrderBy(i => i.SecondName).ToList();
                    break;
                case 2:
                    listClient = listClient.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    listClient = listClient.OrderBy(i => i.Birthday).ToList();
                    break;
                case 4:
                    listClient = listClient.OrderBy(i => i.Email).ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvClient.ItemsSource = listClient;
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
            Filter();
        }

        private void lvClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show("Удалить пользователя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                try
                {
                    if (lvClient.SelectedItem is EF.Client)
                    {
                        var empl = lvClient.SelectedItem as EF.Client;

                        empl.IsDeleted = true;

                        ClassHelper.AppData.Conrext.SaveChanges();

                        MessageBox.Show("Пользователь успешно удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var client = lvClient.SelectedItem as EF.Client;
            AddClient addClient = new AddClient(client);
            addClient.ShowDialog();
            Filter();
        }
    }
}
