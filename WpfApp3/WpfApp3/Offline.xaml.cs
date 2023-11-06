using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using static WpfApp3.Home;
using static WpfApp3.MainWindow;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Offline.xaml
    /// </summary>
    public partial class Offline : Page
    {
        public ObservableCollection<ListFileModel> DataList { get; private set; }
        public ListFileModel DataElement { get; private set; }
        public Offline()
        {
            InitializeComponent();
            TeachOff = true;

        }

        private void ItemControl_ItemDeleted(object sender, ItemDeletedEventArgs e)
        {
            // Xóa phần tử khỏi danh sách DataList
            DataList.Remove(e.DeletedItem);
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<ListFileModel> listFileDownload = JsonConvert.DeserializeObject<List<ListFileModel>>(Properties.Settings.Default.ListFileDownLoaded);

            listFilePPOffline.ItemsSource = listFileDownload;
        }

        private async void btnSearchOffline_Click(object sender, RoutedEventArgs e)
        {
            GifLoadingOffline.Visibility = Visibility.Visible;
            List<ListFileModel> listFileDownload = JsonConvert.DeserializeObject<List<ListFileModel>>(Properties.Settings.Default.ListFileDownLoaded);

            string selectedPackage = (SelectPackageOffline.SelectedItem as ComboBoxItem)?.Tag as string;
            string selectedSubject = (SelectSubjectOffline.SelectedItem as ComboBoxItem)?.Tag as string;
            //string textSearch
            if (!string.IsNullOrEmpty(selectedPackage))
            {
                listFileDownload = listFileDownload.Where(x => x.Package == int.Parse(selectedPackage)).ToList();
                // Ở đây, selectedTag chứa giá trị của Tag của mục được chọn.
            }
            if (!string.IsNullOrEmpty(selectedSubject))
            {
                listFileDownload = listFileDownload.Where(x => x.Subject == Guid.Parse(selectedSubject)).ToList();
                // Ở đây, selectedTag chứa giá trị của Tag của mục được chọn.
            }
            if (!string.IsNullOrEmpty(txtSearchOffline.Text))
            {
                listFileDownload = listFileDownload.Where(x => x.Name.Contains(txtSearchOffline.Text)).ToList();
                // Ở đây, selectedTag chứa giá trị của Tag của mục được chọn.
            }
            listFilePPOffline.ItemsSource = listFileDownload;
            UpdateLayout();
            GifLoadingOffline.Visibility = Visibility.Collapsed;
        }
    }
}
