using LibraryRentOfEquipment;
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
    /// Логика взаимодействия для ReturnOfEquipment.xaml
    /// </summary>
    public partial class ReturnOfEquipment : Window
    {
        EF.Rent returnRent = new EF.Rent();
        public ReturnOfEquipment(EF.Rent rent)
        {
            InitializeComponent();
            tbClient.Text = rent.Client.FirstName;
            tbEquipment.Text = rent.Product.Product1;
            tbDateOfBegin.Text = rent.TimeRent.ToString();
            tbDateOfEnd.Text = rent.TimeRentEnd.ToString();
            tbCost.Text = rent.TotalCost.ToString();

            returnRent = rent;
        }

        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Calculations.DaysInRent(returnRent.TimeRent.DayOfYear,returnRent.TimeRentEnd.DayOfYear) - Calculations.DaysInRent(returnRent.TimeRent.DayOfYear,dpEnd.SelectedDate.Value.DayOfYear) == 0)
            {
                tbCost.Text = returnRent.TotalCost.ToString();
            }
            else if(Calculations.DaysInRent(returnRent.TimeRent.DayOfYear, returnRent.TimeRentEnd.DayOfYear) - Calculations.DaysInRent(returnRent.TimeRent.DayOfYear, dpEnd.SelectedDate.Value.DayOfYear) > 0)
            {
               tbCost.Text= Calculations.Discount(returnRent.TimeRent.DayOfYear, returnRent.TimeRentEnd.DayOfYear, returnRent.Product.Cost, dpEnd.SelectedDate.Value.DayOfYear).ToString();
            }
            else
            {
                tbCost.Text = Calculations.Penality(returnRent.TimeRent.DayOfYear, returnRent.TimeRentEnd.DayOfYear, returnRent.Product.Cost, dpEnd.SelectedDate.Value.DayOfYear).ToString();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if(dpEnd.SelectedDate==null)
            {
                MessageBox.Show("Поле ФАКТИЧЕСКОЙ ДАТЫ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var resClick = MessageBox.Show("Сдать товар в аренду?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resClick == MessageBoxResult.No)
            {
                return;
            }

            returnRent.TotalCost =Convert.ToDecimal(tbCost.Text);
            returnRent.IsDeleted = true;

            ClassHelper.AppData.Conrext.SaveChanges();
            MessageBox.Show("Товар возвращен");

            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
