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
                Process.Start("http://google.com/search?q=" + process.name);
            }
            else
            {
                Process.Start("https://github.com/CodecoolGlobal/c-sharp-process-note-c-_processnote");
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
                    if (process1!=null)
                    {
                        printCpuUsage(process1);
                        printRunTime(process1);
                        printMemoryUsage(process1);
                        printStartTime(process1);
                    } 
                }
            }
        }
        private void printCpuUsage(Process process1)
        {
            CPU_usage_label.Content = Process.GetProcessById(process1.Id).Id;
        }
        private void printRunTime(Process process1)
        {
            Run_time_label.Content = Process.GetProcessById(process1.Id).Id;
        }
        private void printMemoryUsage(Process process1)
        {
            Memory_usage_label.Content = Process.GetProcessById(process1.Id).Id;
        }
        private void printStartTime(Process process1)
        {
            Start_time_label.Content = Process.GetProcessById(process1.Id).Id;
        }
    }

    public class ListedProcess
    {
        public int id { get; set; }
        public string name { get; set; }
        /*public string CPUUsage { get; set; }
        public string MemoryUsage { get; set; }
        public string RunningTime { get; set; }
        public string StartTime { get; set; }*/

        public List<string> Comments = new List<string>();

    }
}
    