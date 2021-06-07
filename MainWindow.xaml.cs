﻿using System;
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
        public HashSet<ProcessThread> processThreads = new HashSet<ProcessThread>();

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

        private void DataGridRow_MouseLeftButtonUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DataGridRow row =sender as DataGridRow;
            ListedProcess listedProcess = row.Item as ListedProcess;
     
            foreach(Process process in Processes)
            {
                if (process.Id.Equals(listedProcess.id))
                {
                    try
                    {
                        ShowCpuUsage(process);
                        ShowRunTime(process);
                        ShowMemoryUsage(process);
                        ShowStartTime(process);
                        processThreads = new HashSet<ProcessThread>();
                        sendThreads(process);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Current process is not running");
                        
                    } 
                }
            }
        }
        private void sendThreads(Process process1)
        {
            foreach(ProcessThread processThread in process1.Threads)
            {
                processThreads.Add(processThread);
            }
        }
        private void ShowCpuUsage(Process process1)
        {
            Process_name_label.Content = process1.ProcessName;
        }
        private void ShowRunTime(Process process1)
        {
            Run_time_label.Content = process1.TotalProcessorTime;
        }
        private void ShowMemoryUsage(Process process1)
        {
            Memory_usage_label.Content = process1.PeakWorkingSet64/2048 +" mb";
        }
        private void ShowStartTime(Process process1)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string threads = "";
            foreach(ProcessThread thread in processThreads)
            {
                threads += "Thread id: "+thread.Id+"\t state: "+thread.ThreadState+"\n";
            }
            MessageBox.Show(threads);
            
        }
    }

    public class ListedProcess
    {
        public int id { get; set; }
        public string Name { get; set; }
        
        public readonly List<string> Comments = new List<string>();
    }
}
    