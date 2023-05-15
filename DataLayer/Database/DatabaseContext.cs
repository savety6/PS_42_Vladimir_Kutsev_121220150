using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Welcome.Others;

namespace DataLayer.Database
{
    public class DatabaseContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string solutionFolder = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFile = "Welcome.db";
            string databasePath = Path.Combine(solutionFolder, databaseFile);
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseUser>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DatabaseLog>().Property(e => e.Id).ValueGeneratedOnAdd();

            var user = new DatabaseUser()
            {
                Id = 1,
                Name = "admin",
                Password = "123",
                Email = "emile@abv.bg",
                Role = UserRolesEnum.ADMIN,
                Active = DateTime.Now.AddYears(10),
            };

            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader("D:\\uni\\sem6\\PS\\logs.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            foreach (string line in lines)
            {
                Regex idRegex = new Regex(@"\[(\d+)\]");
                Regex messageRegex = new Regex(@"\] (.*?)!");
                Regex timestampRegex = new Regex(@"(\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2})$");

                string log_id = idRegex.Match(line).Groups[1].Value;
                string message = messageRegex.Match(line).Groups[1].Value;
                string timestamp = timestampRegex.Match(line).Groups[1].Value;

                var log = new DatabaseLog() { 
                    Id = int.Parse(log_id),
                    message = message,
                    timestamp = timestamp,
                };

                modelBuilder.Entity<DatabaseLog>()
                .HasData(log);
            }

            modelBuilder.Entity<DatabaseUser>()
                .HasData(user);
        }

        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<DatabaseLog> Log { get; set; }

        public DbSet<DatabaseGrade> Grades { get; set; }
    }
}
