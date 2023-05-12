using Welcome.Model;
using Welcome.Others;
using Welcome.View;
using Welcome.ViewModel;
using WelcomeExtended.Data;
using WelcomeExtended.Helpers;
using static WelcomeExtended.Others.Delegates;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            UserData userData = new UserData();
            var user = new User
            {
                Name = "test",
                Password = "123",
                Role = UserRolesEnum.STUDENT
            };
            var user1 = new User
            {
                Name = "Smith John",
                Password = "password123",
                Role = UserRolesEnum.STUDENT
            };
            var user2 = new User
            {
                Name = "Abbie Brown",
                Password = "password123",
                Role = UserRolesEnum.PROFESSOR
            };
            var user3 = new User
            {
                Name = "Sam Smith",
                Password = "password123",
                Role = UserRolesEnum.ADMIN
            };



            userData.AddUser(user);
            userData.AddUser(user1);
            userData.AddUser(user2);
            userData.AddUser(user3);

            string username;
            string password;

            Console.WriteLine("enter your credentials");
            Console.Write("username: ");
            username = Console.ReadLine();
            Console.WriteLine();
            Console.Write("password: ");
            password = Console.ReadLine();
            Console.WriteLine();
            //Console.WriteLine(username);
            //Console.WriteLine(password);
            User newUser = userData.GetUser(username, password);
            if (newUser == null)
            {
                throw new Exception("bad credentials!");
            }

            bool check = userData.ValidateCredentials(newUser);
            Console.WriteLine(check);
            if (check && newUser != null)
            {
                newUser.MyToString();
            }
            else
            {
                Console.WriteLine("wrong Username or password");
            }


            //var viewModel = new UserViewModel(user);

            //var view = new UserView(viewModel);

            //view.DisplayError();
        }
        catch (Exception e) 
        {
            var log = new ActionOnError(Log);
            log(e.Message);
            
        }
        finally
        {
            Console.WriteLine("Executed in any case!");
        }
    }
}