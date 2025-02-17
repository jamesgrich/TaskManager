using System.Windows;
using TaskManager.Model;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public event EventHandler<TaskEventArgs> TaskUpdated;
        public TaskModel Task { get; set; }
        public EditTask(TaskModel task)
        {
            InitializeComponent();
            Task = task;
            DataContext = Task;
        }

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            TaskUpdated?.Invoke(this, new TaskEventArgs(Task));
            this.Close();
        }

    }
}
