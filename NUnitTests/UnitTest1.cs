using NUnit.Framework;
using TaskManager;

namespace NUnitTests
{
    public class TodoManagerTests
    {
        private TodoManager todoManager;

        [SetUp]
        public void Setup()
        {
            todoManager = new TodoManager();
        }

        [Test]
        public void AddTask_ShouldIncreaseTaskCount()
        {
            int initialCount = todoManager.GetTaskCount();

            todoManager.AddTask("Test Title", "Test Description", TaskPriority.Normal, new DateOnly(2023, 8, 11), new DateOnly(2023, 8, 18));

            int finalCount = todoManager.GetTaskCount();
            Assert.AreEqual(initialCount + 1, finalCount);
        }

        [Test]
        public void MarkTaskAsCompleted_ShouldSetIsCompletedToTrue()
        {
            // Arrange
            int taskId = 1; 
            TaskItem task = todoManager.FindTaskById(taskId);

            // Act
            todoManager.MarkTaskAsCompleted(taskId);

            // Assert
            TaskItem updatedTask = todoManager.FindTaskById(taskId);
            Assert.NotNull(task);
            Assert.IsTrue(task.IsCompleted); 
            Assert.IsTrue(updatedTask.IsCompleted); 
        }

       

        [Test]
        public void FilterTasksByPriority_ShouldReturnCorrectCount()
        {
            List<TaskItem> filteredTasks = todoManager.FilterTasksByPriority("High");

            int count = filteredTasks.Count;

            Assert.AreEqual(1, count); 
        }

        [Test]
        public void DeleteTask_ShouldDecreaseTaskCount()
        {
            // Arrange
            int initialCount = todoManager.GetTaskCount();
            int taskIdToDelete = 1; 

            // Act
            todoManager.DeleteTask(taskIdToDelete);

            // Assert
            int finalCount = todoManager.GetTaskCount();
            Assert.AreEqual(initialCount - 1, finalCount);
        }

    }
}
