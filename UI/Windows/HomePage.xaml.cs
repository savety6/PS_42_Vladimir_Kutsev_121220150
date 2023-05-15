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
using UI.Components;
using Welcome.Model;
using Welcome.Others;
namespace UI.Windows
{
    public partial class HomePage : Page
    {
        private DatabaseUser _user;
        public HomePage(DatabaseUser user)
        {
            InitializeComponent();
            
            
            this._user = user;
            Greetings.Text = $"Welcome to the home page {user.Name}!";

            studentsListControl.UpdateData(user.Role);
            studentsListControl.IsSelectionEnabled = user.Role != UserRolesEnum.STUDENT ? true : false;
            studentsListControl.CurrentUserRole = user.Role;

            logsBtn.Visibility = user.Role != UserRolesEnum.ADMIN ? Visibility.Hidden : Visibility.Visible;

        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ErrorLogPage());
        }
    }
}
