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
using Welcome.Model;
using Welcome.Others;

namespace UI.Components
{
    public partial class StudentsList : UserControl
    {
        public bool IsSelectionEnabled { get; set; } = true;
        public UserRolesEnum CurrentUserRole { get; set; }
        public StudentsList()
        {
            InitializeComponent();
            using (var context = new DatabaseContext())
            {
                var records = context.Users.ToList();
                student.DataContext = records;
            }
        }
        public void UpdateData(UserRolesEnum role)
        {
            using (var context = new DatabaseContext())
            {
                if (role == UserRolesEnum.STUDENT)
                {
                    var records = from user in context.Users
                            where user.Role == role
                            select user ;
                    student.DataContext = records.ToList();
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsSelectionEnabled && student.SelectedItem != null)
            {
                // Handle the selection change event here
                if (student.SelectedItem != null)
                {
                    // Retrieve the selected item from the DataGrid
                    var selectedUser = (DatabaseUser)student.SelectedItem;

                    // Perform the desired action with the selected item
                    // For example, display a message box with the selected user's details
                    MessageBox.Show($"Selected User: {selectedUser.Name}, {selectedUser.Email}");
                    PopupWindow popupWindow = new PopupWindow(selectedUser, CurrentUserRole);
                    popupWindow.ShowDialog();
                }
            }
        }
    }
}
