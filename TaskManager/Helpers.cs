using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class Helpers
    {
        public static DateOnly DateInput()
        { 
            var DueDate = DateOnly.MinValue;

            while (true)
            {
                Console.WriteLine("Enter the due date (yyyy-MM-dd): ");
                string input = Console.ReadLine();

                if (DateOnly.TryParse(input, out DateOnly dueDate))
                {
                    DateOnly currentDate = CurrentDate();
                    if (dueDate >= currentDate)
                    {
                        DueDate = dueDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The due date cannot be before the current date.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }

            return DueDate;
        }

        public static TaskPriority PriorityInput()
        {
            Console.Write("Enter task priority (Low/Normal/High/Urgent): ");
            string input = Console.ReadLine();

            if (Enum.TryParse<TaskPriority>(input, out TaskPriority priority))
            {
                return priority;
            }
            else
            {
                Console.WriteLine("Invalid task priority.");
            }
            return TaskPriority.Normal;
        }

        public static DateOnly CurrentDate()
        {
            DateTime currentUtcDateTime = DateTime.UtcNow.Date;
            DateOnly currentDate = new DateOnly(currentUtcDateTime.Year, currentUtcDateTime.Month, currentUtcDateTime.Day);

            return currentDate;

        }

        public static string GetValidStringInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(input));

            return input;
        }

    }
}

