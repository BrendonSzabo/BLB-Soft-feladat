using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfClient.ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Description_Click(object sender, RoutedEventArgs e)
        {
            popuptext.Content = ((Button)sender).Tag as string;
            popup.Visibility = Visibility.Visible;
        }

        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            popuptext.Content = string.Empty;
            popup.Visibility = Visibility.Hidden;
        }

        private void taskitembutton_Click(object sender, RoutedEventArgs e)
        {
            //int id = int.Parse(((Button)sender).Tag.ToString());
            //Task task = new Task();
            //if (id != null)
            //{
            //    task = ((UnifiedViewModel)Application.Current.MainWindow.DataContext).Tasks.FirstOrDefault(x => x.Id == id);
            //}
            //if (task.Title != null)
            //{
            //    TaskWindow taskWindow = new TaskWindow(task, IndexWindow);
            //    taskWindow.Show();
            //}
        }

        private void useritembutton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(((Button)sender).Tag.ToString());
            User user = new User();
            if (id != null)
            {
                user = ((UnifiedViewModel)Application.Current.MainWindow.DataContext).Users.FirstOrDefault(x => x.Id == id);
            }
            if (user.Username != null)
            {
                UserWindow userWindow = new UserWindow(user, IndexWindow);
                userWindow.Show();
            }
        }
    }
}
