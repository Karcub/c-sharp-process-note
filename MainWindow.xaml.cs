using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProcessNote;

namespace c_sharp_process_note
{
    public partial class MainWindow
    {
        public readonly List<ListedProcess> ListedProcesses = new List<ListedProcess>();
        public readonly Process[] Processes = Process.GetProcesses();

        public MainWindow()
        {
            InitializeComponent();
            FontFamily = new FontFamily("Lucida Console");
            Process[] processes = Process.GetProcesses();
            foreach (Process item in processes)
            {
                ListedProcesses.Add(new ListedProcess() { id = item.Id, Name = item.ProcessName });
            }
            ProcessInfo.ItemsSource = ListedProcesses;
        }
        
        private void Select_Row(object sender, SelectionChangedEventArgs e)
        {
            DataGridRow dgr = ProcessInfo.ItemContainerGenerator.ContainerFromItem(ProcessInfo.SelectedItem) as DataGridRow;
            ListedProcess selectedProcess = dgr.Item as ListedProcess;
            CommentsList.ItemsSource = selectedProcess.Comments;
        }

        private void Add_Comment(object sender, RoutedEventArgs e)
        {
            var commentsDialog = new CommentsDialog();
            commentsDialog.ShowDialog();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            DataGridRow dgr = ProcessInfo.ItemContainerGenerator.ContainerFromItem(ProcessInfo.SelectedItem) as DataGridRow;
            ListedProcess selectedProcess = dgr.Item as ListedProcess;
            Process.Start("http://google.com/search?q=" + selectedProcess.Name);
        }

        private void DataGridRow_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            ListedProcess ls = (ListedProcess)row.Item;
     
            foreach(Process process1 in Processes)
            {
                if (process1.Id.Equals(ls.id))
                {
                    try
                    {
                        printCpuUsage(process1);
                        printRunTime(process1);
                        printMemoryUsage(process1);
                        printStartTime(process1);
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Current process is not running");
                        continue;
                    } 
                }
            }
        }
        private void printCpuUsage(Process process1)
        {
            
            Process_name_label.Content = process1.ProcessName;
        }
        private void printRunTime(Process process1)
        {
            Run_time_label.Content = process1.TotalProcessorTime;
        }
        private void printMemoryUsage(Process process1)
        {
            Memory_usage_label.Content = process1.PeakWorkingSet64/2048 +" mb";
        }
        private void printStartTime(Process process1)
        {
            
            Start_time_label.Content = process1.StartTime.ToString("f");
        }
        
        private void Window_On_Top_Deactivated(object sender, EventArgs e)
        {
            var mainWindow = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.Topmost = false;
        }
        
        private void Window_On_Top_Activated(object sender, EventArgs e)
        {
            var mainWindow = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.Topmost = true;
            
        }

        private void CommentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class ListedProcess
    {
        public int id { get; set; }
        public string Name { get; set; }
        
        public readonly List<string> Comments = new List<string>();
    }
}
    