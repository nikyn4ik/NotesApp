using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
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

        public MainWindow()
        {
            this.Title = "Notes";
            InitializeComponent();
            //var latestModifiedFile = GetLatestModifiedFile(@"Notes");
            //Console.WriteLine(latestModifiedFile);
            //System.Diagnostics.Process.Start(latestModifiedFile);
        }
        //static string GetLatestModifiedFile(string directory) // последний измененный/созданный файл
        //{
        //    var files = Directory.GetFiles(@"Notes\");
        //    return files.OrderBy(f => File.GetLastWriteTime(f)).LastOrDefault();
        //}

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Boolean TextBox = false;
            TextBox = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e) //Сохранение уже существующего файла / изменение ранее созданного файла
        {
            if (filePath != "")
                File.WriteAllText(filePath, TextNote.Text); //проработать исключение пустого пути
            else
                SaveAs();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e) // Открытие файла из директории
        {
            Boolean textChanged = false;
            if (textChanged)
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
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextNote.Text = File.ReadAllText(openFileDialog.FileName);
                tblStatus.Text = openFileDialog.FileName;
                fileName = openFileDialog.SafeFileName;
                filePath = openFileDialog.FileName;
                name = fileName;
                textChanged = false;
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e) // Сохранение файла
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
                name = fileName;
            }
            else
            {
                System.Windows.MessageBox.Show("Error", "Notes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FontClick(object sender, RoutedEventArgs e) // Шрифт
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // Закрытие приложения и остановка программы
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Delete_Click(object sender, RoutedEventArgs e) // Удаление созданной только что заметки
        {
            //File.Delete(fileName);
            DirectoryInfo DI = new DirectoryInfo(@"Notes");
            try
            {
                DI.Delete(true);
            }
            catch (DirectoryNotFoundException)
            {
                System.Windows.MessageBox.Show("Directory not found", "Notes");
            }
        }
    }
}