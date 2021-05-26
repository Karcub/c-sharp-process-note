using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ProcessNote;

namespace c_sharp_process_note
{
    public partial class MainWindow
    {
        public readonly List<ListedProcess> ListedProcesses = new List<ListedProcess>();
        
        public MainWindow()
        {
            InitializeComponent();
            Process[] processes = Process.GetProcesses();
            foreach (Process item in processes)
            {
                ListedProcesses.Add(new ListedProcess() { id = item.Id, Name = item.ProcessName });
            }
            ProcessInfo.ItemsSource = ListedProcesses;
        }
        
        private void Select_Row(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                Refresh_Select();
            }
        }
        
        private void Refresh_Select()
        {
            if (ProcessInfo.SelectedItems != null && ProcessInfo.SelectedItems.Count == 1)
            {
                DataGridRow dgr = ProcessInfo.ItemContainerGenerator.ContainerFromItem(ProcessInfo.SelectedItem) as DataGridRow;
                ListedProcess selectedProcess = dgr.Item as ListedProcess;
                
                CommentsList.ItemsSource = selectedProcess.Comments;
            }
        }

        private void Add_Comment(object sender, RoutedEventArgs e)
        {
            if (ProcessInfo.SelectedItems != null && ProcessInfo.SelectedItems.Count == 1)
            {
                var commentsDialog = new CommentsDialog();
                commentsDialog.ShowDialog();
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (ProcessInfo.SelectedItems.Count == 1)
            {
                DataGridRow dgr = ProcessInfo.ItemContainerGenerator.ContainerFromItem(ProcessInfo.SelectedItem) as DataGridRow;
                ListedProcess selectedProcess = dgr.Item as ListedProcess;
                Process.Start("http://google.com/search?q=" + selectedProcess.Name);
            }
        }
    }
    public class ListedProcess
    {
        public int id { get; set; }
        public string Name { get; set; }
        
        public readonly List<string> Comments = new List<string>();
    }
}