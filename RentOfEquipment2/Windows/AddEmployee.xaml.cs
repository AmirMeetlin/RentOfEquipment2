using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        bool isEdit = false;
        EF.Employee editEmployee = new EF.Employee();
        string Login;
        string pathPhoto = null;
        public AddEmployee()
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Conrext.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            cbGender.SelectedIndex = 0;
            cbRole.ItemsSource = ClassHelper.AppData.Conrext.Role.ToList();
            cbRole.DisplayMemberPath = "Role1";
            cbRole.SelectedIndex = 0;

        }
        public AddEmployee(EF.Employee employee)
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Conrext.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            cbRole.ItemsSource = ClassHelper.AppData.Conrext.Role.ToList();
            cbRole.DisplayMemberPath = "Role1";

            tbFirstName.Text = employee.FirstName;
            tbSecondName.Text = employee.SecondName;
            tbPatronymic.Text = employee.Patronymic;
            tbPhone.Text = employee.Phone;
            cbGender.SelectedIndex = employee.IDGender - 1;
            cbRole.SelectedIndex = employee.IDRole - 1;
            tbLogin.Text = employee.Login;
            Login = employee.Login;
            tbPassword.Password = employee.Password;

            if (employee.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(employee.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    photoUser.Source = bitmapImage;
                }

            }

            tbTitle.Text = "Изменение работников";

            isEdit = true;
            editEmployee = employee;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

            var authUser = ClassHelper.AppData.Conrext.Employee.ToList().
                Where(i => i.Login == tbLogin.Text & i.Login!=Login).FirstOrDefault();

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
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Поле ТЕЛЕФОН не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Поле ЛОГИН не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPassword.Password))
            {
                MessageBox.Show("Поле ПАРОЛЬ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbRepeatPaswword.Password))
            {
                MessageBox.Show("Поле ПОВТОРЕНИЕ ПАРОЛЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbPassword.Password != tbRepeatPaswword.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (authUser != null)
            {
                MessageBox.Show("Пользователь с данным ЛОГИНОМ уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                var resClick = MessageBox.Show("Изменить пользователя", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                editEmployee.FirstName = tbFirstName.Text;
                editEmployee.SecondName = tbSecondName.Text;
                editEmployee.Patronymic = tbPatronymic.Text;
                editEmployee.Phone = tbPhone.Text;
                editEmployee.IDGender = cbGender.SelectedIndex + 1;
                editEmployee.IDRole = cbRole.SelectedIndex + 1;
                editEmployee.Login = tbLogin.Text;
                editEmployee.Password = tbPassword.Password;

                if (pathPhoto != null)
                {
                    editEmployee.Photo = File.ReadAllBytes(pathPhoto);
                }

                ClassHelper.AppData.Conrext.SaveChanges();
                MessageBox.Show("Пользователь изменен");

                this.Close();
            }
            else
            {
                var resClick = MessageBox.Show("Добавить пользователя", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                EF.Employee newEmployee = new EF.Employee();
                newEmployee.FirstName = tbFirstName.Text;
                newEmployee.SecondName = tbSecondName.Text;
                newEmployee.Patronymic = tbPatronymic.Text;
                newEmployee.Phone = tbPhone.Text;
                newEmployee.IDGender = cbGender.SelectedIndex + 1;
                newEmployee.IDRole = cbRole.SelectedIndex + 1;
                newEmployee.Login = tbLogin.Text;
                newEmployee.Password = tbPassword.Password;

                if (pathPhoto != null)
                {
                    editEmployee.Photo = File.ReadAllBytes(pathPhoto);
                }

                ClassHelper.AppData.Conrext.Employee.Add(newEmployee);

                ClassHelper.AppData.Conrext.SaveChanges();

                this.Close();
            }

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

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Files|*.jpg;*.jpeg;*.png;";
            if (openFile.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
