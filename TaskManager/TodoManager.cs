using Colors.Net.StringColorExtensions;
using Colors.Net;
using ConsoleTables;
using Spectre.Console;

namespace TaskManager
{
    public class TodoManager
    {
        FileHandler fileHandler = new FileHandler();

        private List<TaskItem> tasks = new List<TaskItem>();

        public TodoManager()
        {
            tasks = fileHandler.LoadTasks();
        }
        public int GetTaskCount()
        {
            return tasks.Count;
        }


        public void AddTask(string title, string description, TaskPriority priority, DateOnly CurrentDate, DateOnly dueDate)
        {   
            int newId = fileHandler.LoadId() + 1;
            fileHandler.SaveId(newId);
            TaskItem newTask = new TaskItem
            {
                Id = newId,
                Title = title,
                Description = description,
                IsCompleted = false,
                DueDate = dueDate,
                Priority = priority,
                CreatedAt = CurrentDate
            };
            tasks.Add(newTask);

            ColoredConsole.WriteLine($"Task with ID {newId} created, Please save It.");

        }

        public void ViewAllTasks()
        {
            if (tasks.Count == 0)
            {
                ColoredConsole.WriteLine("No tasks to display.".Yellow());
                return;
            }

            var table = new ConsoleTable("ID", "Title", "Description", "Priority", "Due Date", "isCompleted");

            foreach (TaskItem task in tasks)
            {
                table.AddRow(task.Id, task.Title, task.Description, task.Priority, task.DueDate.ToShortDateString(), task.IsCompleted ? "Completed" : "");
            }

            ColoredConsole.WriteLine(table.ToString().Cyan());

            //foreach (var task in tasks)
            //{
            //    Console.WriteLine($"Task ID: {task.Id}\nTitle: {task.Title}\nDescription: {task.Description}\nCompleted: {task.IsCompleted}\n");
            //}
        }
        public void HandlePostTableView()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1. View a task by ID");
            Console.WriteLine("2. Filter tasks by priority");
            Console.WriteLine("3. Sort tasks by due date");
            Console.WriteLine("4. Sort tasks by title");
            Console.WriteLine("5. Go back to the main menu");

            Console.Write("Enter your choice: ");
            char choice = Console.ReadLine()[0];
            Console.Clear();

            switch (choice)
            {
                case '1':
                    ViewTaskById();
                    break;

                case '2':
                    FilterByPriority();
                    break;

                case '3':
                    SortTasksByDueDate();
                    ColoredConsole.WriteLine("Tasks sorted according the Due date");
                    ViewAllTasks();
                    break;

                case '4':
                    SortTasksByTitle();
                    ColoredConsole.WriteLine("Tasks sorted according the Title");
                    ViewAllTasks();
                    break;

                case '5':
                    // Go back to the main menu
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please choose a valid option.".Red());
                    break;
            }
        }

        public void ViewTaskById()
        {
            Console.Write("Enter task ID: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                TaskItem task = FindTaskById(taskId); 
                if (task != null)
                {
                    Console.WriteLine($"Task ID: {task.Id}");
                    Console.WriteLine($"Title: {task.Title}");
                    Console.WriteLine($"Description: {task.Description}");
                    Console.WriteLine($"Priority: {task.Priority}");
                    Console.WriteLine($"Due Date: {task.DueDate.ToShortDateString()}");
                }
                else
                {
                    Console.WriteLine($"Task with ID {taskId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid task ID.");
            }
        }

        public TaskItem FindTaskById(int id)
        { 
            return tasks.FirstOrDefault(task => task.Id == id);
        }

        public void MarkTaskAsCompleted(int id)
        {

            TaskItem task = FindTaskById(id);
            if (task != null)
            {
                task.IsCompleted = true;
            }

        }

        public void UpdateTask(int id, string newTitle, string newDescription, TaskPriority priority, DateOnly DueDate)
        {
            
            TaskItem task = FindTaskById(id);
            if (task != null)
            {
                task.Title = newTitle;
                task.Description = newDescription;
                task.Priority = priority;
                task.DueDate = DueDate;
            }

        }

        public void UpdateAllProperties(TaskItem task)
        {
            string newTitle = Helpers.GetValidStringInput("Enter new title: ");
            string newDescription = Helpers.GetValidStringInput("Enter new description: ");
            TaskPriority newPriority = Helpers.PriorityInput();
            DateOnly newDueDate = Helpers.DateInput();

            UpdateTask(task.Id, newTitle, newDescription, newPriority, newDueDate);
            ColoredConsole.WriteLine("Task updated successfully.".Green());
        }

        public void UpdateSpecificProperty(TaskItem task)
        {
            Console.WriteLine("Choose a property to update:");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Priority");
            Console.WriteLine("4. Due Date");
            Console.Write("Enter your choice: ");
            char propertyChoice = Console.ReadLine()[0];

            string newValue;
            switch (propertyChoice)
            {
                case '1':
                    newValue = Helpers.GetValidStringInput("Enter new title: ");
                    UpdateTask(task.Id, newValue, task.Description, task.Priority, task.DueDate);
                    break;

                case '2':
                    newValue = Helpers.GetValidStringInput("Enter new description: ");
                    UpdateTask(task.Id, task.Title, newValue, task.Priority, task.DueDate);
                    break;

                case '3':
                    TaskPriority newPriority = Helpers.PriorityInput();
                    UpdateTask(task.Id, task.Title, task.Description, newPriority, task.DueDate);
                    break;

                case '4':
                    DateOnly newDueDate = Helpers.DateInput();
                    UpdateTask(task.Id, task.Title, task.Description, task.Priority, newDueDate);
                    break;

                default:
                    ColoredConsole.WriteLine("Invalid choice. No changes made.".Yellow());
                    return;
            }

            ColoredConsole.WriteLine("Property updated successfully.".Green());
        }

        public void DeleteTask(int id)
        {
            TaskItem task = FindTaskById(id);
            if (task != null)
            {
                tasks.Remove(task);
            }
        }

        public void SaveTasks()
        {
            fileHandler.SaveTasks(tasks);
        }

        public void FilterByPriority()
        {
            Console.WriteLine("Choose priority level to filter by:");
            Console.WriteLine("1. High");
            Console.WriteLine("2. Urgent");
            Console.WriteLine("3. Normal");
            Console.WriteLine("4. Low");
            Console.WriteLine("5. Go back");

            Console.Write("Enter your choice: ");
            char choice = Console.ReadLine()[0];
            Console.Clear();

            string priorityToFilter = string.Empty;

            switch (choice)
            {
                case '1':
                    priorityToFilter = "High";
                    break;

                case '2':
                    priorityToFilter = "Urgent";
                    break;

                case '3':
                    priorityToFilter = "Normal";
                    break;

                case '4':
                    priorityToFilter = "Low";
                    break;

                case '5':
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please choose a valid option.".Red());
                    return;
            }

            List<TaskItem> filteredTasks = FilterTasksByPriority(priorityToFilter); 

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine($"No tasks with priority '{priorityToFilter}' found.");
            }
            else
            {
                Console.WriteLine($"Tasks with priority '{priorityToFilter}':");

                if (tasks.Count == 0)
                {
                    ColoredConsole.WriteLine("No tasks to display.".Yellow());
                    return;
                }

                var table = new ConsoleTable("ID", "Title", "Description", "Priority", "Due Date");

                foreach (TaskItem task in filteredTasks)
                {
                    table.AddRow(task.Id, task.Title, task.Description, task.Priority, task.DueDate.ToShortDateString());
                }

                ColoredConsole.WriteLine(table.ToString().Cyan());
            }
        }

        public List<TaskItem> FilterTasksByPriority(string priority)
        {
            List<TaskItem> filteredTasks = new List<TaskItem>();

            foreach (TaskItem task in tasks)
            {
                if (task.Priority.ToString().ToLower() == priority.ToLower())
                {
                    filteredTasks.Add(task);
                }
            }

            return filteredTasks;
        }


        public void SortTasksByDueDate()
        {
            tasks.Sort((task1, task2) => task1.DueDate.CompareTo(task2.DueDate));
        }

        public void SortTasksByTitle()
        {
            tasks.Sort((task1, task2) => string.Compare(task1.Title, task2.Title, StringComparison.OrdinalIgnoreCase));
        }


    }

}

