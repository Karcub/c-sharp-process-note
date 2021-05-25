using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace c_sharp_process_note
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Process[] processes;
        public string[] processesss;
        public MainWindow()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            List<processlist> processlist = new List<processlist>();
            foreach (Process item in processes)
            {
                processlist.Add(new processlist() { id = item.Id, name = item.ProcessName });
            }
            ProcessInfo.ItemsSource = processlist;
        }
    }
    public class processlist
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}