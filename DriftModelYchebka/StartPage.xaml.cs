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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Drift;

namespace DriftModelYchebka
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void ShowOnPassword(object sender, MouseButtonEventArgs e)
        {
            PrevPasswordBoxText.Visibility = Visibility.Visible;
            PasswordBoxText.Visibility = Visibility.Visible;
            ShowPasswordBoxText.Visibility = Visibility.Hidden;
            PasswordIcon.Fill = (Brush)new BrushConverter().ConvertFrom("#000000");
        }

        private void ShowOffPassword(object sender, MouseButtonEventArgs e)
        {
            PrevPasswordBoxText.Visibility = Visibility.Hidden;
            PasswordBoxText.Visibility = Visibility.Hidden;
            ShowPasswordBoxText.Visibility = Visibility.Visible;
            PasswordIcon.Fill = (Brush)new BrushConverter().ConvertFrom("#9E328D");
            ShowPasswordBoxText.Text = PasswordBoxText.Password;
        }

        private void UserNameTextFocus(object sender, RoutedEventArgs e)
        {
            if (UserNameText.Text != "" || UserNameText.IsKeyboardFocused == true)
            {
                UserNameText.Opacity = 1;
            }
            else
            {
                UserNameText.Opacity = 0;
            }
        }

        private void PasswordBoxTextFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxText.Password != "" || PasswordBoxText.IsKeyboardFocused == true)
            {
                PasswordBoxText.Opacity = 1;
            }
            else
            {
                PasswordBoxText.Opacity = 0;
            }
        }

        private void LogInClick(object sender, MouseButtonEventArgs e)
        {
            List<User> userList = App.dbContext.users.Where(us => us.Login == UserNameText.Text).ToList();
            App.user = userList.Count != 0 ? userList.First() : null;
            if (App.user != null && App.user.Password == PasswordBoxText.Password)
            {
                switch (App.user.Role)
                {
                    case 1:
                        NavigationService.Navigate(new MainPage());
                        break;
                    case 2:
                        NavigationService.Navigate(new MainPage());
                        break;
                    case 3:
                        NavigationService.Navigate(new MainPage());
                        break;
                    default:
                        MessageBox.Show("Обратитесь к администратору!", "Ошибка роли", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                ErrorLogIn.Visibility = Visibility.Visible;
            }
        }
    }
}
