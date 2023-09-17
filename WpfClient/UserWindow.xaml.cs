using Models;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using WpfClient.ViewModels;
using Newtonsoft.Json;
using Aspose.Cells.Utility;
using Aspose.Cells;
using System.Linq;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        User Currentuser = new User();
        MainWindow IndexWindow = new MainWindow();
        public UserWindow()
        {
            InitializeComponent();
        }
        public UserWindow(User currentuser, MainWindow indexWindow)
        {
            InitializeComponent();
            TaskList.ItemsSource = Currentuser.Tasks;
            this.Currentuser = currentuser;
            this.IndexWindow = indexWindow;
            username.Content = currentuser.Username;
        }

        private void WindowWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IndexWindow.Show();
        }

        private void Description_Click(object sender, RoutedEventArgs e)
        {
            popuptext.Content = ((Button)sender).Tag as string;
            popup.Visibility = Visibility.Visible;
        }

        private void taskitembutton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var task = TaskList.SelectedItem;
        }

        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            popuptext.Content = string.Empty;
            popup.Visibility = Visibility.Hidden;
        }

        private void Addtask_Click(object sender, RoutedEventArgs e)
        {
            User newuser = Currentuser;
            newuser.Tasks.Add((Task)selecttask.SelectedItem);
            Currentuser = newuser;
            ((UnifiedViewModel)Application.Current.MainWindow.DataContext).SelectedUser = newuser;
            ((UnifiedViewModel)Application.Current.MainWindow.DataContext).UpdateUserCommand.Execute(null);
            TaskList.ItemsSource = Currentuser.Tasks;
            taskselector.Visibility = Visibility.Hidden;
        }

        private void Closetask_Click(object sender, RoutedEventArgs e)
        {
            taskselector.Visibility = Visibility.Hidden;
        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            taskselector.Visibility = Visibility.Visible;
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            User newuser = Currentuser;
            newuser.Tasks.Remove((Task)TaskList.SelectedItem);
            ((UnifiedViewModel)Application.Current.MainWindow.DataContext).SelectedUser = newuser;
            ((UnifiedViewModel)Application.Current.MainWindow.DataContext).UpdateUserCommand.Execute(null);
            TaskList.ItemsSource = Currentuser.Tasks;
        }

        private void exportbutton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var task in Currentuser.Tasks)
            {
                for (int i = 0; i < task.Users.Count; i++)
                {
                    task.Users.ToArray()[i] = null;
                }
            }
            // Convert to User obj to json
            var json = JsonConvert.SerializeObject(Currentuser);
            // Create a Workbook object
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];

            // Set JsonLayoutOptions
            JsonLayoutOptions options = new JsonLayoutOptions();
            options.ArrayAsTable = true;

            // Import JSON Data
            JsonUtility.ImportData(json, worksheet.Cells, 0, 0, options);

            // Save Excel file
            workbook.Save($"{Currentuser.Username}.xlsx");
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
    }
}
