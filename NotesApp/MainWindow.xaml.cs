using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media;

namespace NotesApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileName;
        public string filePath = "";

        public MainWindow(object selectedItem)
        {
            filePath = selectedItem.ToString();
            this.Title = "Notes";
            InitializeComponent();
            TextNote.Text = File.ReadAllText(filePath);
        }

        public MainWindow()
        {
            this.Title = "Notes";
            InitializeComponent();

        }

        public string name
        {
            get
            {
                return (String)GetValue(MainWindow.TitleProperty);
            }
            set
            {
                SetValue(MainWindow.TitleProperty, fileName);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e) 
        {
            if (filePath != "")
                File.WriteAllText(filePath, TextNote.Text);
            else
                SaveAs();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e) 
        {
            if (TextNote.Text != "")
            {
                var result = System.Windows.MessageBox.Show("Do you want to save the changes?", "Notes", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (File.Exists(filePath))
                        {
                            Save_Click(sender, e);
                        }
                        else
                        {
                            SaveFile_Click(sender, e);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextNote.Text = File.ReadAllText(openFileDialog.FileName);
                tblStatus.Text = openFileDialog.FileName;
                fileName = openFileDialog.SafeFileName;
                filePath = openFileDialog.FileName;
                name = fileName;
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();

        }

        private void SaveAs()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save the all the text";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, TextNote.Text);
                filePath = saveFileDialog1.FileName;
                fileName = saveFileDialog1.FileName;
                name = fileName;
            }
            else
            {
                System.Windows.MessageBox.Show("Error", "Notes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FontClick(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK) 
            {
                TextNote.FontFamily = new FontFamily(fd.Font.Name);
                TextNote.FontSize = fd.Font.Size * 96.0 / 72.0;
                TextNote.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                TextNote.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

                TextDecorationCollection tdc = new TextDecorationCollection();
                if (fd.Font.Underline) tdc.Add(TextDecorations.Underline);
                if (fd.Font.Strikeout) tdc.Add(TextDecorations.Strikethrough);
                TextNote.TextDecorations = tdc;
            }
        
        }

        private void Delete_Click(object sender, RoutedEventArgs e) 
        {
            if (filePath != "")
            {
                try
                {
                    File.Delete(filePath);
                    this.Close();
                }
                catch (DirectoryNotFoundException)
                {
                    System.Windows.MessageBox.Show("Directory not found", "Notes");
                }
            }
            else
                System.Windows.MessageBox.Show("File not saved", "Notes");
        }
    }
}