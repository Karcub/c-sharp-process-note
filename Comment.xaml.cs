using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using c_sharp_process_note;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class commentsDialog : Window
    {
        public commentsDialog()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            var window2 = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;

            DataGridRow dgr = window2.ProcessInfo.ItemContainerGenerator.ContainerFromItem(window2.ProcessInfo.SelectedItem) as DataGridRow;
            ListedProcess process = dgr.Item as ListedProcess;

            process.Comments.Add(commentsBox.Text);
            window2.commentsList.ItemsSource = " ";
            window2.commentsList.ItemsSource = process.Comments;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}