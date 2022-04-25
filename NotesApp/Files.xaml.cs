using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace NotesApp
{
    /// <summary>
    /// Логика взаимодействия для Files.xaml
    /// </summary>
    public partial class Files
    {
        public Files() 
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
            var window = new MainWindow(list.SelectedItem);
            window.ShowDialog();
        }
    }
}
