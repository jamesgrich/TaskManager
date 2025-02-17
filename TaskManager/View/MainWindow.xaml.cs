using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TaskManager.Model;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public TaskModel SelectedTask { get; set; }
        private int taskId = 0;
        private ICollectionView TaskStatusView { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskModel>();
            DataContext = this;
            taskId = 1;
            TaskStatusView = CollectionViewSource.GetDefaultView(Tasks);
        }

        public bool IsTasksEmpty => !Tasks.Any();

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask();
            addTask.TaskAdded += AddTaskWindow_TaskAdded;
            addTask.ShowDialog();
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask != null)
            {
                EditTask editTask = new EditTask(SelectedTask);
                editTask.TaskUpdated += EditTask_TaskUpdated;
                editTask.ShowDialog();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selectedTasks = Tasks.Where(t => t.IsSelected).ToList();
            if (selectedTasks.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected tasks?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var task in selectedTasks)
                    {
                        Tasks.Remove(task);
                    }
                }
            }
            else
            {
                MessageBox.Show("No tasks selected for deletion.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            TaskStatusView.Refresh();
        }

        private void AddTaskWindow_TaskAdded(object? sender, TaskEventArgs e)
        {
            if (e.Task != null)
            {
                e.Task.ID = taskId++;
                Tasks.Add(e.Task);

                AllRadioButton.IsEnabled = true;
                PendingRadioButton.IsEnabled = true;
                InProgressRadioButton.IsEnabled = true;
                CompletedRadioButton.IsEnabled = true;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var task = checkBox?.DataContext as TaskModel;
            if (task != null)
            {
                task.IsSelected = checkBox.IsChecked.GetValueOrDefault();

                EditTaskButton.IsEnabled = true;
                DeleteTaskButton.IsEnabled = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            EditTaskButton.IsEnabled = false;
            DeleteTaskButton.IsEnabled = false;
        }

        private void EditTask_TaskUpdated(object? sender, TaskEventArgs e)
        {
            if (e.Task != null)
            {
                var taskToUpdate = Tasks.FirstOrDefault(t => t.ID == e.Task.ID);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Title = e.Task.Title;
                    taskToUpdate.Description = e.Task.Description;
                    taskToUpdate.DueDate = e.Task.DueDate;
                    taskToUpdate.Status = e.Task.Status;
                    TaskStatusView.Refresh();
                }
            }
        }

        private void FilterTasks(string status)
        {
            if (TaskStatusView != null)
            {
                if (status == "All")
                {
                    TaskStatusView.Filter = null;
                }
                else
                {
                    TaskStatusView.Filter = task => ((TaskModel)task).Status == status;
                }
                TaskStatusView.Refresh();
            }
        }

        private void StatusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                string status = radioButton.Content.ToString();
                FilterTasks(status);
            }
        }

    }
}