using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Models;

namespace WpfClient.ViewModels
{
    class UserViewModel : ObservableRecipient
    {
        public RestCollection<User> Users { get; set; }

        private User selectedUser;

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

        public ICommand CreateUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public UserViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:59674/", "User", "hub");

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
            }
        }
    }
}
