using System;
using System.Collections.Generic;
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
using System.IO;
using Microsoft.Win32;

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

        private void OpenF(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.DefaultExt = ".txt";
            //openFileDialog.Filter = "Text files (*.txt)|*.txt|All files(*.*)|*.*";
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    TextBox.Text = File.ReadAllText(openFileDialog.FileName);
            //    tblStatus.Text = openFileDialog.FileName;
            //    fileName = openFileDialog.SafeFileName;
            //    filePath = openFileDialog.FileName;
            //    name = fileName;
            //}
        }

        static void CheckDirectory()
        {
            if (Directory.Exists("Notes") == false)
            {
                Directory.CreateDirectory("Notes");
            }

            if (Directory.GetFiles("Notes").Length == 0)
            {
                File.WriteAllText(@"Notes\0.txt", "My first notes");
            }
        }
    }
}
