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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ZwierzetaModel> listZwierzeta = new List<ZwierzetaModel>();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEdit_click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.ShowDialog();
        }
        private void btnNew_click(object sender, RoutedEventArgs e)
        {
            NewWindow newWindow = new NewWindow();
            newWindow.ShowDialog();
        }
        private void cmbListView_DropDownOpened(object sender, EventArgs e)
        {
            listZwierzeta = SQLiteAccess.Read();
            cmbListView.ItemsSource = listZwierzeta.Select(n => n.Name);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (cmbListView.SelectedItem != null)
            {
                if (MessageBox.Show("Jesteś pewien?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (SQLiteAccess.Delete(listZwierzeta[cmbListView.SelectedIndex]) == false)
                    {
                        MessageBox.Show("Wystąpił błąd", "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        cmbListView.SelectedIndex--;
                    }
                }
            }
        }
        private void cmbListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbListView.SelectedItem != null)
            {
                Nazwa.Text = listZwierzeta[cmbListView.SelectedIndex].Name;
                Gatunek.Text = listZwierzeta[cmbListView.SelectedIndex].Species;
                txtDescription.Text = listZwierzeta[cmbListView.SelectedIndex].Description;
                if (listZwierzeta[cmbListView.SelectedIndex].PhotoName != string.Empty)
                {
                    string photoPath = System.IO.Path.Combine(SQLiteAccess.dbFileDirectory, listZwierzeta[cmbListView.SelectedIndex].PhotoName);
                    Uri fileUri = new Uri(photoPath);
                    image.Source = new BitmapImage(fileUri);
                }
            }
        }
    }
}

