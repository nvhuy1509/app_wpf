using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string _baseUrl = "https://stemschool.vn";
        public static bool TeachOff = false;
        public static string token = "";
        public class APIs
        {
            public const string Login = "/Api/Login";
            public const string UpdateDownloadFile = "/Api/AddDataDownload";
            public const string GetFilePublished = "/Api/GetFilePublished";
            public const string GetListPackage = "/Api/GetListPackagePP";
            public const string GetListSubject = "/Api/GetListSubjectPP";

        }

        public class LoginModel
        {
            //.[Required]
            public string Username { get; set; }
           // [Required]
            public string Password { get; set; }
        }

        public class UpdateDownloadFilePP
        {
            public Guid IdFilePP { get; set; }
        }
        public class DalResult
        {
            public bool IsSuccess { get; set; }
            public int EffectRow { get; set; }
            public int ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
            public long NewRowId { get; set; }
            public object Data { get; set; }
        }
        public class Root
        {
            public object message { get; set; }
            public bool success { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string id { get; set; }
            public string phonenumber { get; set; }
            public object email { get; set; }
            public string token { get; set; }
            public int lifetimetoken { get; set; }
        }
        public MainWindow()
        {
            System.Net.ServicePointManager.SecurityProtocol =
          SecurityProtocolType.Tls12 |
          SecurityProtocolType.Tls11 |
          SecurityProtocolType.Tls;
            InitializeComponent();
        }
    }
}
