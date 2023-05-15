using DataLayer.Database;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using Welcome.Model;
using Welcome.Others;
using WelcomeExtended.Data;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
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
                
                var ret = from user in context.Users
                          where user.Name == UsernameBox.Text && user.Password == PasswordBox.Password
                          select user;
                if (ret != null && ret.Any())
                {
                    NavigationService.Navigate(new HomePage(ret.First()));
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.");
                }
            }
        }
        private void SignUp_MouseDown(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage());
        }
    }
}
