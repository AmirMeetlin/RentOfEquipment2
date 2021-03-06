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
    /// Логика взаимодействия для ChooseEmployee.xaml
    /// </summary>
    public partial class ChooseEmployee : Window
    {
        List<string> listSort = new List<string>()
            {
                "По умолчанию",
                "По фамилии",
                "По имени",
                "По логину",
                "По должности"
            };

        public ChooseEmployee()
        {
            InitializeComponent();
            Filter();
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }
        private void Filter()
        {
            List<EF.Employee> listEmployee = new List<EF.Employee>();
            listEmployee = ClassHelper.AppData.Conrext.Employee.Where(i => i.IsDeleted == false).ToList();

            listEmployee = listEmployee.
                Where(i => i.SecondName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Patronymic.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Login.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Phone.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FIO.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listEmployee = listEmployee.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listEmployee = listEmployee.OrderBy(i => i.SecondName).ToList();
                    break;
                case 2:
                    listEmployee = listEmployee.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    listEmployee = listEmployee.OrderBy(i => i.Login).ToList();
                    break;
                case 4:
                    listEmployee = listEmployee.OrderBy(i => i.IDRole).ToList();
                    break;
                default:
                    listEmployee = listEmployee.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvEmployee.ItemsSource = listEmployee;
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.ShowDialog();
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
        private void lvEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetEmployee = lvEmployee.SelectedItem as EF.Employee;
            Close();
        }
    }
}
