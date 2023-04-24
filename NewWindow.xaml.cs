using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfApp
{
    /// <summary>
    /// Logika interakcji dla klasy NewWindow.xaml
    /// </summary>
    public partial class NewWindow : Window
    {
        string photoPath;
        public NewWindow()
        {
            InitializeComponent();
        }

        private void btnUpload_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                photoPath = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                image.Source = new BitmapImage(fileUri);
            }
        }

        private void btnSave_click(object sender, RoutedEventArgs e)
        {
            ZwierzetaModel Zwierzeta = new ZwierzetaModel();
            Zwierzeta.Name = Nazwa.Text;
            Zwierzeta.Species = Gatunek.Text;
            Zwierzeta.Description = txtDescription.Text;
            if (string.IsNullOrEmpty(photoPath) == false)
            {
                Zwierzeta.PhotoName = Zwierzeta.Name + ".bmp";
                string destFileName = SQLiteAccess.dbFileDirectory + "\\" + Zwierzeta.PhotoName;
                File.Copy(photoPath, destFileName);
            }
            if (SQLiteAccess.Create(Zwierzeta) == false)
            {
                MessageBox.Show("Wystąpił błąd", "Error", MessageBoxButton.OK);
            }
            else
            {
                this.Close();
            }
        }

        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
