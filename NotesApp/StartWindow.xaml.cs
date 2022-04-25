using System.Windows;
using System.IO;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public string fileName;
        public string filePath = "";

        public StartWindow()
        {
            InitializeComponent();
            CheckDirectory();
        }

        private void Files(object sender, RoutedEventArgs e)
        {
            var window = new Files();
            window.ShowDialog();
        }

        private void CreateN(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.ShowDialog();
        }

        static void CheckDirectory()
        {
            if (Directory.Exists("Notes") == false)
            {
                Directory.CreateDirectory("Notes");
                if (Directory.GetFiles("Notes").Length == 0)
                {
                    File.WriteAllText(@"Notes\0.txt", "My first notes");
                }
            }

            
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) 
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
