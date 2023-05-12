using DataLayer.Database;
using DataLayer.Model;
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
using Welcome.Others;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void LogIn_MouseDown(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                bool isDataBaseCreated = context.Database.EnsureCreated();
                if (isDataBaseCreated)
                {
                    context.Add<DatabaseUser>(new DatabaseUser
                    {
                        Name = "admin",
                        Password = "admin",
                        Email = "admin@gmail.com",
                        Role = UserRolesEnum.ADMIN,
                        Active = DateTime.Now.AddYears(10),
                    });
                    context.SaveChanges();
                }
                bool userExists = context.Users.Any(u => u.Name == UsernameBox.Text);
                if (!userExists)
                {
                    if (PasswordBox.Password == SecPasswordBox.Password)
                    {
                        var user = new DatabaseUser
                        {
                            Name = UsernameBox.Text,
                            Password = PasswordBox.Password,
                            Email = EmailBox.Text,
                            Role = UserRolesEnum.STUDENT,
                            Active = DateTime.Now.AddYears(4),
                        };
                        context.Add<DatabaseUser>(user);
                        context.SaveChanges();
                        NavigationService.Navigate(new HomePage(user));
                    }
                    else
                    {
                        MessageBox.Show("passwords must match");
                    }
                }
                else
                {
                    MessageBox.Show("User with this name already exists.");
                }
            }  
        }
    }
}
