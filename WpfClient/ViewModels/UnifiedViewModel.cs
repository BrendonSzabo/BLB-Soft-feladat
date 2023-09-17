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
using Models;

namespace WpfClient.ViewModels
{
    /// <summary>
    /// Unified view model for Users and Tasks
    /// </summary>
    public class UnifiedViewModel : ObservableRecipient
    {
        /// <summary>
        /// All Users from the database 
        /// </summary>
        public RestCollection<User> Users { get; set; }
        private User selectedUser;
        /// <summary>
        /// Calls the create command through a post request for the selected user
        /// </summary>
        public ICommand CreateUserCommand { get; set; }
        /// <summary>
        /// Calls a delete command for the selected user
        /// </summary>
        public ICommand DeleteUserCommand { get; set; }
        /// <summary>
        /// Calls the update command through a put request for the selected user
        /// </summary>
        public ICommand UpdateUserCommand { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value != null)
                {
                    selectedUser = new User()
                    {
                        Username = value.Username,
                        Id = value.Id
                    };
                }
                OnPropertyChanged();
                (DeleteUserCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// All tasks from the database 
        /// </summary>
        public RestCollection<Models.Task> Tasks { get; set; }
        private Models.Task selectedTask;
        /// <summary>
        /// Calls the create command through a post request for the selected task
        /// </summary>
        public ICommand CreateTaskCommand { get; set; }
        /// <summary>
        /// Calls a delete command for the selected task
        /// </summary>
        public ICommand DeleteTaskCommand { get; set; }
        /// <summary>
        /// Calls the update command through a put request for the selected task
        /// </summary>
        public ICommand UpdateTaskCommand { get; set; }
        /// <summary>
        /// Currently selected task
        /// </summary>
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
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public UnifiedViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:5213/", "User", "hub");

                CreateUserCommand = new RelayCommand(() =>
                {
                    Users.Add(new User()
                    {
                        Username = SelectedUser.Username
                    });
                });

                DeleteUserCommand = new RelayCommand(() =>
                {
                    Users.Delete(SelectedUser.Id);
                },
                () =>
                {
                    return SelectedUser != null;
                });

                UpdateUserCommand = new RelayCommand(() =>
                {
                    Users.Update(SelectedUser);
                });

                SelectedUser = new User();

                Tasks = new RestCollection<Models.Task>("http://localhost:5213/", "Task", "hub");

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
