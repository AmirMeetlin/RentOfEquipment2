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
        public ListOfClients()
        {
            InitializeComponent();
            lvClient.ItemsSource = ClassHelper.AppData.Conrext.Client.ToList();
        }
    }
}
