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
    /// Логика взаимодействия для AddEquipment.xaml
    /// </summary>
    public partial class AddEquipment : Window
    {

        bool isEdit = false;
        EF.Product editEquipment = new EF.Product();
        public AddEquipment()
        {
            InitializeComponent();
            cbTypeOfProduct.ItemsSource = ClassHelper.AppData.Conrext.TypeProduct.ToList();
            cbTypeOfProduct.DisplayMemberPath = "TypeProduct1";
            cbTypeOfProduct.SelectedIndex = 0;
            cbRentStatus.Items.Add("Нет");
            cbRentStatus.Items.Add("Да");
            cbRentStatus.SelectedIndex = 0;
        }
        public AddEquipment(EF.Product equipment)
        {
            InitializeComponent();
            cbTypeOfProduct.ItemsSource = ClassHelper.AppData.Conrext.TypeProduct.ToList();
            cbTypeOfProduct.DisplayMemberPath = "TypeProduct1";
            cbTypeOfProduct.SelectedIndex = 0;
            cbRentStatus.Items.Add("Нет");
            cbRentStatus.Items.Add("Да");
            cbRentStatus.SelectedIndex = 0;

            tbTitle.Text = equipment.Product1;
            tbDescription.Text = equipment.Description;
            tbPrice.Text=Convert.ToString(equipment.Cost);
            tbWarranty.Text=Convert.ToString(equipment.Warranty);
            cbTypeOfProduct.SelectedIndex = equipment.IDTypeProduct-1;
            int status;
            if (equipment.IsRent == false)
            {
                status = 0;
            }
            else
            {
                status = 1;
            }
            cbRentStatus.SelectedItem = status;

            tbName.Text = "Изменение работников";

            isEdit = true;
            editEquipment = equipment;
        }
        private void textBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Поле ОПИСАНИЕ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Поле НАЗВАНИЕ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                MessageBox.Show("Поле ЦЕНА не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbWarranty.Text))
            {
                MessageBox.Show("Поле ГАРАНТИЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(isEdit)
            {
                var resClick = MessageBox.Show("Изменить товар", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                editEquipment.Product1 = tbTitle.Text;
                editEquipment.Description = tbDescription.Text;
                editEquipment.Cost = Convert.ToDecimal(tbPrice.Text);
                editEquipment.Warranty = Convert.ToDateTime(tbWarranty.Text);
                editEquipment.IDTypeProduct = cbTypeOfProduct.SelectedIndex + 1;
                bool status;
                if (cbRentStatus.SelectedIndex == 0)
                {
                    status = false;
                }
                else
                {
                    status = true;
                }
                editEquipment.IsRent = status;

                ClassHelper.AppData.Conrext.SaveChanges();
                MessageBox.Show("Товар изменен");

                this.Close();
            }
            else
            {
                var resClick = MessageBox.Show("Добавить товар", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                EF.Product product = new EF.Product();
                product.Product1 = tbTitle.Text;
                product.Description = tbDescription.Text;
                product.Cost = Convert.ToDecimal(tbPrice.Text);
                product.Warranty = Convert.ToDateTime(tbWarranty.Text);
                product.IDTypeProduct = cbTypeOfProduct.SelectedIndex + 1;
                bool status;
                if (cbRentStatus.SelectedIndex == 0)
                {
                    status = false;
                }
                else
                {
                    status = true;
                }
                product.IsRent = status;

                ClassHelper.AppData.Conrext.Product.Add(product);
                ClassHelper.AppData.Conrext.SaveChanges();
                MessageBox.Show("Товар добавлен");
                this.Close();
            }

            

        }
    }
}
