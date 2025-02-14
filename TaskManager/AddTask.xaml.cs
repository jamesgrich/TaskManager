using System.Windows;
using TaskManager.Model;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public event EventHandler<TaskEventArgs> TaskAdded;

        public AddTask()
        {
            InitializeComponent();
        }

        private void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            TaskModel Task = new TaskModel
            {
                Title = TitleTB.Text,
                Description = DescriptionTB.Text,
                DueDate = DueDateTB.SelectedDate ?? DateTime.Now,
                Status = StatusTB.Text,
            };

            TaskAdded?.Invoke(this, new TaskEventArgs(Task));
            this.Close();
        }
    }
}
