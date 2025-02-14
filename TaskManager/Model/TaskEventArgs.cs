using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class TaskEventArgs : EventArgs
    {
        public TaskModel Task { get; }

        public TaskEventArgs(TaskModel task)
        {
            Task = task;
        }

        public int ID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }

        public DateTime DueDate { get; set; }
        public enum StatusList
        {
            Pending,
            InProgress,
            Completed
        }
        public String Status { get; set; }
    }
}
