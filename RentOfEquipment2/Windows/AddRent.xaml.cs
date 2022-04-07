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
            ChooseEquipment chooseEquipment = new ChooseEquipment();
            chooseEquipment.ShowDialog();
            btnChooseEquipment.FontSize = 15;
            btnChooseEquipment.Content = GetEquipment.Product1;

            if(dpBegin.SelectedDate!= null && dpEnd.SelectedDate!= null && GetEquipment != null)
            {
                int NumberOfDayOfBegin = dpBegin.SelectedDate.Value.DayOfYear;
                int NumberOfDayOfEnd = dpEnd.SelectedDate.Value.DayOfYear;
                tbCost.Text = (GetEquipment.Cost * (NumberOfDayOfEnd - NumberOfDayOfBegin + 1)).ToString();
            }
            
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var resClick = MessageBox.Show("Добавить запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resClick == MessageBoxResult.No)
            {
                return;
            }

            EF.Rent rent = new EF.Rent();
            rent.IDClient = GetClient.ID;
            rent.IDEmployee = GetEmployee.ID;
            rent.IDProduct = GetEquipment.ID;
            rent.TimeRent =Convert.ToDateTime(dpBegin.SelectedDate);
            rent.TimeRentEnd =Convert.ToDateTime(dpEnd.SelectedDate);
            rent.TotalCost = Convert.ToDecimal(tbCost.Text);
            ClassHelper.AppData.Conrext.Rent.Add(rent);
            ClassHelper.AppData.Conrext.SaveChanges();
            MessageBox.Show("Запись об аренде успешно добавлена!");
            this.Close();
        }

        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpBegin.SelectedDate != null && dpEnd.SelectedDate != null && GetEquipment!=null)
            {
                int NumberOfDayOfBegin = dpBegin.SelectedDate.Value.DayOfYear;
                int NumberOfDayOfEnd = dpEnd.SelectedDate.Value.DayOfYear;
                tbCost.Text = (GetEquipment.Cost * (NumberOfDayOfEnd - NumberOfDayOfBegin + 1)).ToString();
            }
        }

        private void dpBegin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpBegin.SelectedDate != null && dpEnd.SelectedDate != null && GetEquipment != null)
            {
                int NumberOfDayOfBegin = dpBegin.SelectedDate.Value.DayOfYear;
                int NumberOfDayOfEnd = dpEnd.SelectedDate.Value.DayOfYear;
                tbCost.Text = (GetEquipment.Cost * (NumberOfDayOfEnd - NumberOfDayOfBegin + 1)).ToString();
            }
        }
    }
}
