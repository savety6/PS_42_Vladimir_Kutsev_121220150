using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;
using WelcomeExtended.Helpers;

namespace WelcomeExtended.Data
{
    public class UserData
    {
        private List<User> _users;

        private int _nextId;

        public List<User> getUsers()
        {
            return _users;
        }

        public UserData() 
        {
            _nextId = 0;
            _users = new List<User> {};
        }

        public void AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public void RemoveUser(User user)
        {
            _users.Remove(user);
        }

        public void showAllUsers()
        {
            foreach (User user in _users)
            {
                Console.WriteLine(user.MyToString());
            }
        }

        public bool ValidateUser(string name,  string password)
        {
            foreach (var user in _users)
            {
                if (user.Name == name &&  user.Password == password) return true;

            }
            return false;
        }

        public bool ValidateUserLambda(string name, string password)
        {
            return _users.Where(x => x.Name == name && x.Password == password).FirstOrDefault() != null ? true: false;
        }

        public bool ValidateUserLinq(string name, string password)
        {
            var ret = from user in _users
                      where user.Name == name && user.Password == password
                      select user.Id;
            return ret != null ? true : false;
        }

        public User GetUser(string name, string password)
        {
            return _users.Where(x => x.Name == name && x.Password == password).FirstOrDefault();
        }
        public User GetUserByName(string name)
        {
            return _users.Where(x => x.Name == name).FirstOrDefault();
        }
        public User GetUserLambda(string name, string password)
        {
            return _users.Where(x => x.Name == name && x.Password == password).FirstOrDefault();
        }

        public User GetUserLinq(string name, string password)
        {
            var ret = from user in _users
                      where user.Name == name && user.Password == password
                      select user;
            return ret.FirstOrDefault();
        }

        //methot that sets user expiration date
        public void SetActive(string name, DateTime date)
        {
            var user = _users.Where(x => x.Name == name).FirstOrDefault();
            user.Active = date;
        }

        public void SetActiveLambda(string name, DateTime date)
        {
            var user = _users.Where(x => x.Name == name).FirstOrDefault();
            user.Active = date;
        }

        public void SetActiveLinq(string name, DateTime date)
        {
            var ret = from user in _users
                      where user.Name == name
                      select user;
            ret.FirstOrDefault().Active = date;
        }

        public void AsssignUserRole(string name, UserRolesEnum role)
        {
            var user = _users.Where(x => x.Name == name).FirstOrDefault();
            user.Role = role;
        }

        public void AsssignUserRoleLambda(string name, UserRolesEnum role)
        {
            var user = _users.Where(x => x.Name == name).FirstOrDefault();
            user.Role = role;
        }

        public void AsssignUserRoleLinq(string name, UserRolesEnum role)
        {
            var ret = from user in _users
                      where user.Name == name
                      select user;
            ret.FirstOrDefault().Role = role;
        }
    }
}
