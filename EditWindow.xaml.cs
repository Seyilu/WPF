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
using Microsoft.Win32;
using System.IO;

namespace WpfApp
{
    /// <summary>
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        string photoPath;
        public EditWindow()
        {
            InitializeComponent();
        }


        private void btnSave_click(object sender, RoutedEventArgs e)
        {
            ZwierzetaModel Zwierzeta = new ZwierzetaModel();
            Zwierzeta.Name = Nazwa.Text;
            //Zwierzeta.Name = "adam";
            Zwierzeta.Species = Gatunek.Text;
            Zwierzeta.Description = txtDescription.Text;

            if (string.IsNullOrEmpty(photoPath) == false)
            {
                Zwierzeta.PhotoName = Zwierzeta.Name + ".bmp";
                string destFileName = SQLiteAccess.dbFileDirectory + "\\" + Zwierzeta.PhotoName;
                File.Copy(photoPath, destFileName);
            }
            /*            if (SQLiteAccess.Update(Zwierzeta) == false)
                        {
                            MessageBox.Show("Wystąpił błąd", "Error", MessageBoxButton.OK);
                        }
                        else
                        {
                            this.Close();
                        }*/
            SQLiteAccess.Update(Zwierzeta);
        }


        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Close();
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
    }

}
