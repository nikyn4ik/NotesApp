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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace NotesApp
{
    /// <summary>
    /// Логика взаимодействия для Files.xaml
    /// </summary>
    public partial class Files
    {
        public Files() //Вывод файлов, которые использовались или находятся в папке ?
        {
            InitializeComponent();
            Loaded += PageLoa;
        }
        private void PageLoa(object sender, RoutedEventArgs e)
        {
            string[] fileEntries = Directory.GetFiles("Notes");
            foreach (string fileName in fileEntries)
                list.Items.Add(fileName);
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var window = new MainWindow();
            window.ShowDialog();
        }
    }
}
