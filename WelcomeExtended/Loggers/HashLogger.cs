using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeExtended.Loggers
{
    public class HashLogger :ILogger
    {
        private readonly ConcurrentDictionary<int, string> _logMessager;
        private readonly string _name;
        

        public HashLogger(string name)
        {
            _name = name;
            _logMessager = new ConcurrentDictionary<int, string>();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            //throw new NotImplementedException();
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);
            switch (logLevel)
            {
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red; 
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine("- LOGGER -");
            var messageToBelLogged = new StringBuilder();
            messageToBelLogged.AppendLine($"[{logLevel}]");
            messageToBelLogged.AppendFormat("[{0}] ", _name);
            Console.WriteLine(messageToBelLogged);
            Console.WriteLine($" {formatter(state, exception)}");
            Console.WriteLine("- LOGGER -");
            Console.ResetColor();
            _logMessager[eventId.Id] = message;
            SaveToFile("logs.txt");
        }
        
        //methot that returns all masages
        public void showAllMessages()
        {
            Console.WriteLine("All messages:");
            foreach (var item in _logMessager)
            {
                Console.WriteLine($"[{item.Key}] {item.Value}");
            }
        }

        //method that returns all messages until specifick id
        public void showMessagesUntilId(int id)
        {
            Console.WriteLine($"All messages until id {id}:");
            foreach (var item in _logMessager)
            {
                if (item.Key <= id)
                {
                    Console.WriteLine($"[{item.Key}] {item.Value}");
                }
            }
        }

        //method that deletes all messages 
        public void deleteAllMessages()
        {
            _logMessager.Clear();
            Console.WriteLine("All messages are deleted!");
        }

        //method that deletes all messages until specifick id
        public void deleteMessagesUntilId(int id)
        {
            foreach (var item in _logMessager)
            {
                if (item.Key <= id)
                {
                    _logMessager.Remove(item.Key, out string value);
                }
            }
            Console.WriteLine($"All messages until id {id} are deleted!");
        }

        //method that deletes message from specifick id
        public void deleteMessageById(int id)
        {
            _logMessager.Remove(id, out string value);
            Console.WriteLine($"Message with id {id} is deleted!");
        }

        public void SaveToFile(string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            using (var writer = File.AppendText(filePath))
            {
                foreach (var item in _logMessager)
                {
                    writer.WriteLine($"[{item.Key}] {item.Value} {DateTime.Now}");
                }
            }
            Console.WriteLine($"All messages are saved to file: {filePath}");
        }

    }
}
