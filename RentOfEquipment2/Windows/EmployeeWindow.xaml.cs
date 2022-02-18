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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        List<string> listSort = new List<string>()
            {
                "По умолчанию",
                "По фамилии",
                "По имени",
                "По логину",
                "По должности"
            };

        public EmployeeWindow()
        {
            InitializeComponent();
            Filter();
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }
        private void Filter()
        {
            List<EF.Employee> listEmployee = new List<EF.Employee>();
            listEmployee = ClassHelper.AppData.Conrext.Employee.ToList();

            listEmployee = listEmployee.
                Where(i => i.SecondName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Patronymic.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Login.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Phone.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FIO.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch(cbSort.SelectedIndex)
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

        private void lvEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Delete || e.Key==Key.Back)
            {
                var resClick = MessageBox.Show("Удалить пользователя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(resClick==MessageBoxResult.No)
                {
                    return;
                }

                try
                {

                    if (lvEmployee.SelectedItem is EF.Employee)
                    {
                        var empl = lvEmployee.SelectedItem as EF.Employee;

                        ClassHelper.AppData.Conrext.Employee.Remove(empl);

                        ClassHelper.AppData.Conrext.SaveChanges();

                        MessageBox.Show("Пользователь успешно удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void lvEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var empl = lvEmployee.SelectedItem as EF.Employee;
            AddEmployee addEmployee = new AddEmployee(empl);
            addEmployee.ShowDialog();
            Filter();

        }
    }
}
