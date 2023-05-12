using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;
using WelcomeExtended.Data;

namespace WelcomeExtended.Helpers
{
    public static class UserHelper
    {
        public static string MyToString(this User user)
        {
            return $"User: {user.Name} Password: {user.Password} Role: {user.Role} ID: {user.Id}";
        }

        public static bool ValidateCredentials(this UserData userList, User user)
        {
            if (user.Name == null) { Console.WriteLine("The name cannot be empty"); return false; }
            if (user.Password == null) { Console.WriteLine("The password cannot be empty"); return false; }
            if (userList.ValidateUser(user.Name, user.Password)) { userList.AddUser(user); return true; }
            Console.WriteLine("no user found");
            return false;
        }

        public static User MyGetUser(this UserData userList, string Name, string Password)
        {
            return userList.GetUser(Name, Password);
        }

        public static void createNewUser(this UserData users) 
        {
            Console.Write("Usernaem: ");
            string uname = Console.ReadLine();
            Console.WriteLine("Password: ");
            string pass =  Console.ReadLine();
            Console.WriteLine("Emile: ");
            string emile = Console.ReadLine();
            User user = new User{
                Name = uname,
                Password = pass,
                Email = emile,
                Active = DateTime.Now.AddYears(3),
                Role = UserRolesEnum.STUDENT 
            };

            if (!users.ValidateCredentials(user))
            {
                users.AddUser(user);
                Console.WriteLine("addet succesfully!");
            }
            else
            {
                Console.WriteLine("Invalid Credentials");
            }
        }

        public static void deleteUser(this UserData users)
        {
            Console.WriteLine("Enter the name of the user you want to delete");
            string name = Console.ReadLine();
            User user = users.GetUserByName(name);
            if (user != null)
            {
                users.RemoveUser(user);
                Console.WriteLine("User deleted");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }
    }
}
