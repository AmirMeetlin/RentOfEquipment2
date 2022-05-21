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
using LibraryRentOfEquipment;

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
            
            if(GetClient!=null)
            {
                btnChooseClient.FontSize = 15;
                btnChooseClient.Content = GetClient.FIO;
            }
            
            
        }

        private void btnChooseEmployee_Click(object sender, RoutedEventArgs e)
        {
            ChooseEmployee chooseEmployee = new ChooseEmployee();
            chooseEmployee.ShowDialog();
            
            if (GetEmployee != null)
            {
                btnChooseEmployee.FontSize = 15;
                btnChooseEmployee.Content = GetEmployee.FIO;
            }
        }

        private void btnChooseEquipment_Click(object sender, RoutedEventArgs e)
        {
            ChooseEquipment chooseEquipment = new ChooseEquipment();
            chooseEquipment.ShowDialog();
            
            if (GetEquipment!= null)
            {
                btnChooseEquipment.FontSize = 15;
                btnChooseEquipment.Content = GetEquipment.Product1;
            }

            if (dpBegin.SelectedDate!= null && dpEnd.SelectedDate!= null && GetEquipment != null&& GetEmployee != null&& GetClient != null)
            {
                int DateOfBegin = dpBegin.SelectedDate.Value.DayOfYear;
                int DateOfEnd = dpEnd.SelectedDate.Value.DayOfYear;
                decimal Cost = GetEquipment.Cost;
                tbCost.Text = Calculations.CostOfRent(Calculations.DaysInRent(DateOfBegin,DateOfEnd), Cost).ToString();
            }
            
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (GetClient==null)
            {
                MessageBox.Show("Необходимо выбрать КЛИЕНТА", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (GetEmployee == null)
            {
                MessageBox.Show("Необходимо выбрать СОТРУДНИКА", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (GetEquipment == null)
            {
                MessageBox.Show("Необходимо выбрать ОБОРУДОВАНИЕ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpBegin.SelectedDate == null)
            {
                MessageBox.Show("Поле ДАТА НАЧАЛА не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpEnd.SelectedDate == null)
            {
                MessageBox.Show("Поле ДАТЫ КОНЦА не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
                int DateOfBegin = dpBegin.SelectedDate.Value.DayOfYear;
                int DateOfEnd = dpEnd.SelectedDate.Value.DayOfYear;
                decimal Cost = GetEquipment.Cost;
                tbCost.Text = Calculations.CostOfRent(Calculations.DaysInRent(DateOfBegin, DateOfEnd), Cost).ToString();
            }
        }

        private void dpBegin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpBegin.SelectedDate != null && dpEnd.SelectedDate != null && GetEquipment != null)
            {
                int DateOfBegin = dpBegin.SelectedDate.Value.DayOfYear;
                int DateOfEnd = dpEnd.SelectedDate.Value.DayOfYear;
                decimal Cost = GetEquipment.Cost;
                tbCost.Text = Calculations.CostOfRent(Calculations.DaysInRent(DateOfBegin, DateOfEnd), Cost).ToString();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            GetClient = null;
            GetEmployee = null;
            GetEquipment = null;
        }
    }
}
