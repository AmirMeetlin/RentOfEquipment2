﻿using System;
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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
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

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

            var authUser = ClassHelper.AppData.Conrext.Employee.ToList().
                Where(i => i.Login == tbLogin.Text).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageBox.Show("Поле ИМЯ не должно быть пустым","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
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
            if (!Int64.TryParse(tbPhone.Text, out long res))
            {
                MessageBox.Show("Введены недопустимые символы в поле НОМЕР ТЕЛЕФОНА", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(tbPassword.Password!=tbRepeatPaswword.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (authUser!=null)
            {
                MessageBox.Show("Пользователь с данным ЛОГИНОМ уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EF.Employee newEmployee = new EF.Employee();
            newEmployee.FirstName = tbFirstName.Text;
            newEmployee.SecondName = tbSecondName.Text;
            newEmployee.Patronymic = tbPatronymic.Text;
            newEmployee.Phone = tbPhone.Text;
            newEmployee.IDGender = cbGender.SelectedIndex+1;
            newEmployee.IDRole = cbRole.SelectedIndex+1;
            newEmployee.Login = tbLogin.Text;
            newEmployee.Password = tbPassword.Password;

            ClassHelper.AppData.Conrext.Employee.Add(newEmployee);
            ClassHelper.AppData.Conrext.SaveChanges();

            this.Close();

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
    }
}
