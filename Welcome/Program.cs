using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;

internal class Program
{
    private static void Main(string[] args)
    {
        User user = new User();
        user.Id = 1;
        user.Name = "Joro";
        user.Email = "Joro@abv.bg";
        user.Password = "123456";
        user.Role = Welcome.Others.UserRolesEnum.ADMIN;

        UserViewModel uvm = new UserViewModel(user);

        UserView view = new UserView(uvm);

        view.Display();
    }
}