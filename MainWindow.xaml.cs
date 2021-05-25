using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ProcessNote;

namespace c_sharp_process_note
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Process[] processes;
        List<ListedProcess> processlist = new List<ListedProcess>();
        
        public MainWindow()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            foreach (Process item in processes)
            {
                processlist.Add(new ListedProcess() { id = item.Id, name = item.ProcessName });
                
            }
            ProcessInfo.ItemsSource = processlist;
        }
        
        private void dataGrid_selectedRow(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                refreshData();
            }
        }
        
        private void refreshData()
        {
            if (ProcessInfo.SelectedItems != null && ProcessInfo.SelectedItems.Count == 1)
            {
                DataGridRow dgr = ProcessInfo.ItemContainerGenerator.ContainerFromItem(ProcessInfo.SelectedItem) as DataGridRow;
                ListedProcess process = dgr.Item as ListedProcess;
                
                commentsList.ItemsSource = process.Comments;
            }
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessInfo.SelectedItems != null && ProcessInfo.SelectedItems.Count == 1)
            {
                var dialog = new commentsDialog();
                dialog.ShowDialog();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessInfo.SelectedItems != null && ProcessInfo.SelectedItems.Count == 1)
            {
                DataGridRow dgr = ProcessInfo.ItemContainerGenerator.ContainerFromItem(ProcessInfo.SelectedItem) as DataGridRow;
                ListedProcess process = dgr.Item as ListedProcess;
                Process.Start("http://google.com/search?q="+process.name);
            }
            else
            {
                Process.Start("https://github.com/CodecoolGlobal/c-sharp-process-note-c-_processnote");
            }
        }
    }
    public class ListedProcess
    {
        public int id { get; set; }
        public string name { get; set; }
        
        public List<string> Comments = new List<string>();
    }
}