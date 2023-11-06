using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using GifImage = WpfAnimatedGif.ImageBehavior;
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
using static WpfApp3.MainWindow;
using System.Reflection;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            TeachOff = false;
            if (Application.Current.Properties.Contains("Username"))
            {
                string username = Application.Current.Properties["Username"].ToString();
                NameUser.Text = username;
            }
            
        }
        public List<ListFileModel> listAllFilePP = new List<ListFileModel>();
        public List<ListPackage> listPackage = new List<ListPackage>();
        public List<ListSubject> listSubject = new List<ListSubject>();

        public static string UserName { get; set; }
        public class ListFileModel
        {
            public string IconOff { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Package { get; set; }
            public string Url { get; set; }
            public string Password { get; set; }
            public string Description { get; set; }
            public int Download { get; set; }
            public int Status { get; set; }
            public int? Id_Approved { get; set; }
            public int? UserId_Created { get; set; }

            private string _icon;
            public string Icon
            {
                get { return _icon; }
                set
                {
                    if (!TeachOff)
                        _icon = _baseUrl + value;
                    else
                        _icon = IconOff;
                }

            }

            public string Screenshot { get; set; }
            public Guid? Subject { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdateDate { get; set; }
            public int Ver { get; set; }
            public string FileDescription { get; set; }
        }

        public class FileDesc
        {
            public int Id { get; set; }
            public string Url { get; set; }
        }

        public class ListPackage
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Thumb { get; set; }
            public int Status { get; set; }

        }
        public class ListSubject
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Back to home");
        }
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            Frame mainFrame = ((MainWindow)Application.Current.MainWindow).mainFrame;
            mainFrame.Source = new Uri("History.xaml", UriKind.Relative);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            using (var client = new HttpClient())
            {
                var urlgetFile = $"{_baseUrl}{APIs.GetFilePublished}";
                var urlgetPackage = $"{_baseUrl}{APIs.GetListPackage}";
                var urlgetSubject = $"{_baseUrl}{APIs.GetListSubject}";


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var responseGetFile = await client.GetAsync(urlgetFile);
                    var responseGetPackage = await client.GetAsync(urlgetPackage);
                    var responseGetSubject = await client.GetAsync(urlgetSubject);

                    if (responseGetFile.IsSuccessStatusCode && responseGetPackage.IsSuccessStatusCode && responseGetSubject.IsSuccessStatusCode)
                    {
                        var content = await responseGetFile.Content.ReadAsStringAsync();
                        listAllFilePP = JsonConvert.DeserializeObject<List<ListFileModel>>(content);

                        listFilePP.ItemsSource = listAllFilePP; // list file PP hiển thị
                        
                        var contentPackage = await responseGetPackage.Content.ReadAsStringAsync();
                        listPackage = JsonConvert.DeserializeObject<List<ListPackage>>(contentPackage);

                        foreach( var item in listPackage )
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = item.Name; // Content là nội dung bạn muốn hiển thị
                            comboBoxItem.Tag = item.Id.ToString();

                            SelectPackage.Items.Add(comboBoxItem); //add item vào select box khối
                        }

                        var contentSubject = await responseGetSubject.Content.ReadAsStringAsync();
                        listSubject = JsonConvert.DeserializeObject<List<ListSubject>>(contentSubject);

                        foreach (var item in listSubject)
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = item.Name; // Content là nội dung bạn muốn hiển thị
                            comboBoxItem.Tag = item.Id.ToString();

                            SelectSubject.Items.Add(comboBoxItem); //add item vào select box môn
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error", "Lỗi hệ thống. Vui lòng thử lại");
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error", "  Lỗi khi load dữ liệu: " + ex.Message);
                }
                
            }
        }
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            // Kết hợp đường dẫn thư mục với tên tệp GIF
            //string gifPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Spin.gif");
            ////string gifPath = "D:\\Project\\test\\WpfApp3\\WpfApp3\\Spin.gif"; // Thay "path_to_your_gif.gif" bằng đường dẫn thực sự của tệp GIF.

            //// Tạo một BitmapImage để hiển thị hình ảnh GIF
            //BitmapImage gifBitmap = new BitmapImage(new Uri(gifPath));

            //// Đặt hình ảnh GIF cho Image Control
            //GifImage.SetAnimatedSource(GifLoading, gifBitmap);
            //GifLoading.Visibility = Visibility.Visible;
            GifLoading.Visibility = Visibility.Visible;
            using (var client = new HttpClient())
            {
                var urlgetFile = $"{_baseUrl}{APIs.GetFilePublished}";


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var responseGetFile = await client.GetAsync(urlgetFile);

                    if (responseGetFile.IsSuccessStatusCode)
                    {
                        var content = await responseGetFile.Content.ReadAsStringAsync();
                        listAllFilePP = JsonConvert.DeserializeObject<List<ListFileModel>>(content);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Lỗi hệ thống. Vui lòng thử lại");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error", "  Lỗi khi load dữ liệu: " + ex.Message);
                }

            }

            string selectedPackage = (SelectPackage.SelectedItem as ComboBoxItem)?.Tag as string;
            string selectedSubject = (SelectSubject.SelectedItem as ComboBoxItem)?.Tag as string;
            //string textSearch
            if (!string.IsNullOrEmpty(selectedPackage))
            {
                listAllFilePP = listAllFilePP.Where(x => x.Package == int.Parse(selectedPackage)).ToList();
                // Ở đây, selectedTag chứa giá trị của Tag của mục được chọn.
            }
            if (!string.IsNullOrEmpty(selectedSubject))
            {
                listAllFilePP = listAllFilePP.Where(x => x.Subject == Guid.Parse(selectedSubject)).ToList();
                // Ở đây, selectedTag chứa giá trị của Tag của mục được chọn.
            }
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                listAllFilePP = listAllFilePP.Where(x => x.Name.Contains(txtSearch.Text)).ToList();
                // Ở đây, selectedTag chứa giá trị của Tag của mục được chọn.
            }
            listFilePP.ItemsSource = listAllFilePP;
            UpdateLayout();
            GifLoading.Visibility = Visibility.Collapsed;
        }
    }
}

