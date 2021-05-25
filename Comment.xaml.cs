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
    public partial class CommentsDialog : Window
    {
        public CommentsDialog()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {

            var mainWindow = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;

            DataGridRow dgr = mainWindow.ProcessInfo.ItemContainerGenerator.ContainerFromItem(mainWindow.ProcessInfo.SelectedItem) as DataGridRow;
            ListedProcess SelectedProcess = dgr.Item as ListedProcess;

            SelectedProcess.Comments.Add(commentsBox.Text);
            mainWindow.CommentsList.ItemsSource = SelectedProcess.Comments;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}