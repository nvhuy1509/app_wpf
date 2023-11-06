using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
using static WpfApp3.Home;
using static WpfApp3.MainWindow;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Frame MainFrame { get; set; }
        public Login()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            string pathJson = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
            if (System.IO.File.Exists(pathJson))
            {
                string dataJson = File.ReadAllText(pathJson);
                string dataLocal = Properties.Settings.Default.ListFileDownLoaded;

                if(dataLocal.Length < dataJson.Length)
                {
                    if (dataJson != "[]")
                    {
                        Properties.Settings.Default.ListFileDownLoaded = dataJson;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            InitializeComponent();

            if (Properties.Settings.Default.Username != string.Empty && Properties.Settings.Default.Password != string.Empty)
            {

                txtUsername.Text = Properties.Settings.Default.Username;
                passwordBox.Password = Properties.Settings.Default.Password;
                IsRemember.IsChecked = true;
            }
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string returnStr = string.Empty;
            string txtPassword = passwordBox.Password;
            if (txtUsername.Text != "" && txtPassword != "")
            {
               
                LoginModel loginModel = new LoginModel();
                loginModel.Username = txtUsername.Text;
                loginModel.Password = txtPassword;

                using (var client = new HttpClient())
                {
                    var url = $"{_baseUrl}{APIs.Login}";

                    var serializedStr = JsonConvert.SerializeObject(loginModel);
                    try
                    {
                        var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                        if (response.IsSuccessStatusCode)
                        {
                            returnStr = await response.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<Root>(returnStr);
                            if (data.success == true)
                            {
                                token = data.data.token;
                                if (IsRemember.IsChecked == true)
                                {
                                    Properties.Settings.Default.Username = txtUsername.Text;
                                    Properties.Settings.Default.Password = txtPassword;
                                    Properties.Settings.Default.Save();
                                }
                                if (IsRemember.IsChecked == false)
                                {
                                    Properties.Settings.Default.Username = "";
                                    Properties.Settings.Default.Password = "";
                                    Properties.Settings.Default.Save();
                                }

                                Frame mainFrame = ((MainWindow)Application.Current.MainWindow).mainFrame;

                                Application.Current.Properties["Username"] = txtUsername.Text;

                                mainFrame.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
                                //mainFrame.Source = new Uri("Home.xaml", UriKind.Relative);

                            }
                            else
                            {
                                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                            }
                        }
                        else
                        {
                            MessageBox.Show(response.StatusCode+"");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }

                    // return returnStr;
                }
            }
            else
            {
                MessageBox.Show("Bạn cần nhập đầy đủ cả Username và Password");
            }

        }

        private async void btnOffline_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.ListFileDownLoaded != "")
            {
                List<ListFileModel> listFileDownload = JsonConvert.DeserializeObject<List<ListFileModel>>(Properties.Settings.Default.ListFileDownLoaded);

                if (listFileDownload != null && listFileDownload.Count() > 0)
                {
                    Frame mainFrame = ((MainWindow)Application.Current.MainWindow).mainFrame;
                    mainFrame.Source = new Uri("Offline.xaml", UriKind.Relative);
                }
                else
                {
                    MessageBox.Show("Chưa có file tải về để dạy offline", "Warning");
                }
            } else
            {
                MessageBox.Show("Chưa có file tải về để dạy offline", "Warning");
            }
        }
    }
}
