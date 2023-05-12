using DataLayer.Database;
using DataLayer.Model;
using Welcome.Model;
using WelcomeExtended.Data;
using WelcomeExtended.Helpers;
using WelcomeExtended.Others;

public class Program
{
    private static void Main(string[] args)
    {
        using (var context = new DatabaseContext())
        {
            context.Database.EnsureCreated();
            //context.Add<DatabaseUser>(new DatabaseUser
            //{
            //    Name = "user",
            //    Password = "password",
            //    Email = "email@gmail.com",
            //    Role = Welcome.Others.UserRolesEnum.STUDENT,
            //    Active = DateTime.Now,
            //});
            context.SaveChanges();
            UserData users = new UserData();
            var dbUsers = context.Users.ToList();
            foreach (DatabaseUser user in dbUsers)
            {
                users.AddUser(user);
            }
            Console.WriteLine("log in:");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            var check = users.GetUserLinq(username, password);

            if (check != null)
            {
                Console.WriteLine($"Greetings {check.MyToString()}");
            }
            else
            {
                Console.WriteLine("there is no such user!");
                return;
            }

            do
            {
                Console.WriteLine("1: get All users, 2: add new user: 3: delete a user, 4: exit");
                var intent = Console.ReadLine();
                switch (intent)
                {
                    case "1":
                        users.showAllUsers();
                        break;
                    case "2":
                        users.createNewUser();
                        break;
                    case "3":
                        users.deleteUser();
                        break;
                    case "4":
                        // Delete all the records from the Users table
                        var usersToDelete = context.Users.ToList();
                        context.Users.RemoveRange(usersToDelete);
                        context.SaveChanges();


                        // Insert the new user data into the Users table
                        foreach (var user in users.getUsers())
                        {
                            context.Add<DatabaseUser>(new DatabaseUser
                            {
                                Name = user.Name,
                                Password = user.Password,
                                Email = user.Email,
                                Role = user.Role,
                                Active = user.Active,
                            });
                        }
                        context.SaveChanges();
                        Console.WriteLine("goodbye!");
                        return;
                        case "5":
                        Console.WriteLine(Delegates.logger.ToString());
                        break;
                    default:
                        Console.WriteLine("bad comand! Try again.");
                        break;
                }

            } while (true);
        }

    }
} 