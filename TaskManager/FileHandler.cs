using Newtonsoft.Json;


namespace TaskManager
{
    internal class FileHandler
    {

        private const string FilePath = @"D:\Training\C#\TaskManager\TaskManager\tasks.json";
        private const string TrackPath = @"D:\Training\C#\TaskManager\TaskManager\Id_tracker.txt";


        public void SaveTasks(List<TaskItem> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public List<TaskItem> LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<TaskItem>>(json);
            }
            return new List<TaskItem>();
        }

        public void SaveId(int Id)
        { 
            File.WriteAllText(TrackPath, Id.ToString());
        }

        public int LoadId()
        {
            if (File.Exists(TrackPath))
            {
                return int.Parse(File.ReadAllText(TrackPath));
            }
            return 0;
        }
    }

}

