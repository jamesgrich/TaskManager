using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
