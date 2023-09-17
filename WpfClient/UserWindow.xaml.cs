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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentuser">The currently selected user</param>
        /// <param name="indexWindow">The Main window the selection originated from</param>
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
        /// <summary>
        /// Shows the description of a task in a popup "window"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Description_Click(object sender, RoutedEventArgs e)
        {
            popuptext.Content = ((Button)sender).Tag as string;
            popup.Visibility = Visibility.Visible;
        }

        private void taskitembutton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var task = TaskList.SelectedItem;
        }
        /// <summary>
        /// Closes the description popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            popuptext.Content = string.Empty;
            popup.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Adds the selected task to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// closes the task selector popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Closetask_Click(object sender, RoutedEventArgs e)
        {
            taskselector.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Opens the task selector popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            taskselector.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Removes the selected task from the users tasklist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            User newuser = Currentuser;///gets the current user
            newuser.Tasks.Remove((Task)TaskList.SelectedItem);///removes the task
            ((UnifiedViewModel)Application.Current.MainWindow.DataContext).SelectedUser = newuser;///switches the selected user to the user with the removed task so the update command can work
            ((UnifiedViewModel)Application.Current.MainWindow.DataContext).UpdateUserCommand.Execute(null);///update command to finalize the switch in the database
            TaskList.ItemsSource = Currentuser.Tasks;///reloads the task list might not be necessary
        }
        /// <summary>
        /// exports the current user into a xlsx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportbutton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var task in Currentuser.Tasks)
            {
                for (int i = 0; i < task.Users.Count; i++)
                {
                    task.Users.ToArray()[i] = null;
                }
            }
            /// Convert to User obj to json
            var json = JsonConvert.SerializeObject(Currentuser);
            /// Create a Workbook object
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];

            /// Set JsonLayoutOptions
            JsonLayoutOptions options = new JsonLayoutOptions();
            options.ArrayAsTable = true;

            /// Import JSON Data
            JsonUtility.ImportData(json, worksheet.Cells, 0, 0, options);

            /// Save Excel file
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
