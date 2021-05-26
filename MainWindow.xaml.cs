using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ProcessNote;

namespace c_sharp_process_note
{
    public partial class MainWindow
    {
        public readonly List<ListedProcess> ListedProcesses = new List<ListedProcess>();
        public Process[] processes = Process.GetProcesses();

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

        private void DataGridRow_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            ListedProcess ls = (ListedProcess)row.Item;
     
            foreach(Process process1 in processes)
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
    }

    public class ListedProcess
    {
        public int id { get; set; }
        public string Name { get; set; }
        
        public readonly List<string> Comments = new List<string>();
    }
}
    