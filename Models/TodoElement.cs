using System;
using System.ComponentModel;

namespace TodoApp.Models
{
    public class TodoElement : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDone
        {
            get => isDone;
            set 
            {
                if (value == isDone) return;

                isDone = value;
                OnPropertyChanged("IsDone");
            }
        }
        public string Task
        {
            get => task;
            set 
            {
                if (value == task) return;

                task = value;
                OnPropertyChanged("Task");
            }
        }

        private string task;
        private bool isDone;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
