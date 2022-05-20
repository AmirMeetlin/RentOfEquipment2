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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {

        bool isEdit = false;
        EF.Client editClient = new EF.Client();
        string Login;
        string pathPhoto = null;
        public AddClient()
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Conrext.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            cbGender.SelectedIndex = 0;
        }
        public AddClient(EF.Client client)
        {
            EF.Passport Passport = new EF.Passport();
            EF.PassportClient PassportClient = new EF.PassportClient();

            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Conrext.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";

            tbFirstName.Text = client.FirstName;
            tbSecondName.Text = client.SecondName;
            tbPatronymic.Text = client.Patronymic;
            tbPhone.Text = client.Phone;
            tbBirthday.Text =Convert.ToString(client.Birthday);
            tbMail.Text = client.Email;
            cbGender.SelectedIndex = client.IDGender - 1;




            tbTitle.Text = "Изменение сотрудника";
            isEdit = true;
            editClient = client;
            tbSerial.Visibility = Visibility.Hidden;
            tbNumber.Visibility = Visibility.Hidden;
            tblNumber.Visibility = Visibility.Hidden;
            tblSerial.Visibility = Visibility.Hidden;
        }
        private void textBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }
        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageBox.Show("Поле ИМЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbSecondName.Text))
            {
                MessageBox.Show("Поле ФАМИЛИЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPatronymic.Text))
            {
                MessageBox.Show("Поле ОТЧЕСТВО не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Поле ТЕЛЕФОН не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbBirthday.Text))
            {
                MessageBox.Show("Поле ДАТА РОЖДЕНИЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbMail.Text))
            {
                MessageBox.Show("Поле ПОЧТА не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbSerial.Text) && isEdit==false)
            {
                MessageBox.Show("Поле СЕРИЯ ПАСПОРТА не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbNumber.Text) && isEdit==false)
            {
                MessageBox.Show("Поле НОМЕР ПАСПОРТА не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(isEdit)
            {
                var resClick = MessageBox.Show("Изменить клиента", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                editClient.FirstName = tbFirstName.Text;
                editClient.SecondName = tbSecondName.Text;
                editClient.Patronymic = tbPatronymic.Text;
                editClient.Birthday = Convert.ToDateTime(tbBirthday.Text);
                editClient.Email = tbMail.Text;
                editClient.IDGender = cbGender.SelectedIndex + 1;
                editClient.Phone = tbPhone.Text;


                ClassHelper.AppData.Conrext.SaveChanges();
                MessageBox.Show("Клиент изменен");
                this.Close();
            }
            else
            {
                var resClick = MessageBox.Show("Добавить пользователя", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                EF.Client newClient = new EF.Client();
                EF.Passport newPassport = new EF.Passport();
                EF.PassportClient newPassportClient = new EF.PassportClient();
                newClient.FirstName = tbFirstName.Text;
                newClient.SecondName = tbSecondName.Text;
                newClient.Patronymic = tbPatronymic.Text;
                newClient.Birthday = Convert.ToDateTime(tbBirthday.Text);
                newClient.Email = tbMail.Text;
                newClient.IDGender = cbGender.SelectedIndex + 1;
                newClient.Phone = tbPhone.Text;
                ClassHelper.AppData.Conrext.Client.Add(newClient);
                ClassHelper.AppData.Conrext.SaveChanges();
                newPassport.PassportSeries = tbSerial.Text;
                newPassport.PassportNumber = tbNumber.Text;
                ClassHelper.AppData.Conrext.Passport.Add(newPassport);
                ClassHelper.AppData.Conrext.SaveChanges();
                newPassportClient.IDPassport = newPassport.ID;
                newPassportClient.IDClient = newClient.ID;

                ClassHelper.AppData.Conrext.PassportClient.Add(newPassportClient);
                ClassHelper.AppData.Conrext.SaveChanges();
                MessageBox.Show("Клиент добавлен");
                this.Close();
            }
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
