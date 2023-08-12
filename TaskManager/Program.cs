using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using Colors.Net.StringColorExtensions;
using Colors.Net;

namespace TaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TodoManager todoManager = new TodoManager();
            FileHandler fileHandler = new FileHandler();

            bool isRunning = true;
            while (isRunning)
            {
                ColoredConsole.WriteLine("\nMain Menu:");
                ColoredConsole.WriteLine($"1. {("Add a task".Green())}");
                ColoredConsole.WriteLine($"2. {("View all tasks".Yellow())}");
                ColoredConsole.WriteLine($"3. {("Mark a task as completed".Blue())}");
                ColoredConsole.WriteLine($"4. {("Update a task".Magenta())}");
                ColoredConsole.WriteLine($"5. {("Delete a task".Red())}");
                ColoredConsole.WriteLine($"6. {("Save tasks to a file".Gray())}");
                ColoredConsole.WriteLine($"7. {("Exit".DarkGray())}");

                Console.Write("Enter your choice: ");
                char choice = Console.ReadLine()[0];
                Console.Clear();

                switch (choice)
                {
                    case '1':
                        Console.Write("Enter task title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter task description: ");
                        string description = Console.ReadLine();

                        TaskPriority Priority = Helpers.PriorityInput();
                        DateOnly DueDate = Helpers.DateInput();
                        DateOnly CurrentDate = Helpers.CurrentDate();



                        todoManager.AddTask(title, description, Priority, CurrentDate ,DueDate);
                        ColoredConsole.WriteLine("Task added successfully.".Green());

                        break;

                    case '2':
                        todoManager.ViewAllTasks();
                        todoManager.HandlePostTableView();
                        break;

                    case '3':
                        Console.Write("Enter task ID to mark as completed: ");
                        int taskId = int.Parse(Console.ReadLine());
                        todoManager.MarkTaskAsCompleted(taskId);
                        break;

                    case '4':
                        Console.Write("Enter task ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateTaskId))
                        {
                            TaskItem taskToUpdate = todoManager.FindTaskById(updateTaskId);
                            if (taskToUpdate != null)
                            {
                                Console.WriteLine("Choose an option:");
                                Console.WriteLine("1. Update all properties");
                                Console.WriteLine("2. Update a specific property");
                                Console.Write("Enter your choice: ");

                                char updateChoice = Console.ReadLine()[0];
                                Console.Clear();

                                switch (updateChoice)
                                {
                                    case '1':
                                        todoManager.UpdateAllProperties(taskToUpdate);
                                        break;

                                    case '2':
                                        todoManager.UpdateSpecificProperty(taskToUpdate);
                                        break;

                                    default:
                                        ColoredConsole.WriteLine("Invalid choice. Please choose a valid option.".Red());
                                        break;
                                }
                            }
                            else
                            {
                                ColoredConsole.WriteLine($"Task with ID {updateTaskId} not found.".Red());
                            }
                        }
                        else
                        {
                            ColoredConsole.WriteLine("Invalid input. Please enter a valid task ID.".Red());
                        }
                        break;

                    case '5':
                        Console.Write("Enter task ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteTaskId))
                        {
                            TaskItem taskToDelete = todoManager.FindTaskById(deleteTaskId);
                            if (taskToDelete != null)
                            {
                                todoManager.DeleteTask(deleteTaskId);
                                ColoredConsole.WriteLine("Task Deleted successfully.".Red());
                            }

                            else
                            {
                                ColoredConsole.WriteLine($"Task with ID {deleteTaskId} not found.".Red());
                            }
                        }
                        else
                        {
                            ColoredConsole.WriteLine("Invalid input. Please enter a valid task ID.".Red());
                        }
                        break;


                    case '6':
                        todoManager.SaveTasks();
                        ColoredConsole.WriteLine("Task Saved successfully.".Green());

                        break;

                    case '7':
                        isRunning = false;
                        break;

                    default:
                        ColoredConsole.WriteLine("Invalid choice. Please choose a valid option.".Red());
                        break;
                }
            }
        }
    }
}
   