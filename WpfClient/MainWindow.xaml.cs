using System.Windows;
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

        private void DatapieceHover(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void ContinueTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskInputBox.Visibility = Visibility.Collapsed;
            UsernameBox.Text = string.Empty;
            TitleBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
            DateBox.Text = string.Empty;
            TaskPopup.Content = string.Empty;
            ContinueTask.Command = null;

        }
        private void ContinueUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.Visibility = Visibility.Collapsed;
            UsernameBox.Text = string.Empty;
            UserPopup.Content = string.Empty;
            ContinueUser.Command = null;
            
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            TaskInputBox.Visibility = Visibility.Collapsed;
            UserInputBox.Visibility = Visibility.Collapsed;
            UsernameBox.Text = string.Empty;
            TitleBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
            DateBox.Text = string.Empty;
            UserPopup.Content = string.Empty;
            TaskPopup.Content = string.Empty;
            ContinueUser.Command = null;
            ContinueTask.Command = null;
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.Content = "Create User";
            ContinueUser.Command = ((UserPopup.DataContext as UserViewModel).CreateUserCommand);
            UserInputBox.Visibility= Visibility.Visible;
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.Content = "Update User";
            Models.User user = (UserPopup.DataContext as UserViewModel).SelectedUser;
            UsernameBox.Text = user.Username;
            ContinueUser.Command = ((UserPopup.DataContext as UserViewModel).UpdateUserCommand);
            UserInputBox.Visibility = Visibility.Visible;
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.Content = "Delete User";
            ContinueUser.Command = ((UserPopup.DataContext as UserViewModel).DeleteUserCommand);
            UserInputBox.Visibility = Visibility.Visible;
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskPopup.Content = "Create Task";
            ContinueTask.Command = ((TaskPopup.DataContext as TaskViewModel).CreateTaskCommand);
            UserInputBox.Visibility = Visibility.Visible;
        }

        private void UpdateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskPopup.Content = "Update Task";
            Models.Task task = (TaskPopup.DataContext as TaskViewModel).SelectedTask;
            TitleBox.Text = task.Title;
            DescriptionBox.Text = task.Description;
            DateBox.Text = task.Date;
            ContinueTask.Command = (TaskPopup.DataContext as TaskViewModel).UpdateTaskCommand;
            UserInputBox.Visibility = Visibility.Visible;
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskPopup.Content = "Delete Task";
            ContinueTask.Command = ((TaskPopup.DataContext as TaskViewModel).DeleteTaskCommand);
            UserInputBox.Visibility = Visibility.Visible;
        }
    }
}
