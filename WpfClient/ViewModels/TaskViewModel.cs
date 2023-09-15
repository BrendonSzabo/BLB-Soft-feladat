using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace WpfClient.ViewModels
{
    class TaskViewModel : ObservableRecipient
    {
        public RestCollection<Models.Task> Tasks { get; set; }

        private Models.Task selectedTask;
        public ICommand CreateTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand UpdateTaskCommand { get; set; }

        public Models.Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                if (value != null)
                {
                    selectedTask = new Models.Task()
                    {
                        Title = value.Title,
                        Id = value.Id
                    };
                }
                OnPropertyChanged();
                (DeleteTaskCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        //For some reason i cant do it without this
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public TaskViewModel()
        {
            if (!IsInDesignMode)
            {
                Tasks = new RestCollection<Models.Task>("http://localhost:12023/", "Task", "hub");

                CreateTaskCommand = new RelayCommand(() =>
                {
                    Tasks.Add(new Models.Task()
                    {
                        Title = SelectedTask.Title
                    });
                });

                DeleteTaskCommand = new RelayCommand(() =>
                {
                    Tasks.Delete(SelectedTask.Id);
                },
                () =>
                {
                    return SelectedTask != null;
                });

                UpdateTaskCommand = new RelayCommand(() =>
                {
                    Tasks.Update(SelectedTask);
                });

                SelectedTask = new Models.Task();
            }
        }
    }
}
