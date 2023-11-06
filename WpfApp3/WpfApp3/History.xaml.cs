using Newtonsoft.Json;
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
using static WpfApp3.Home;
using static WpfApp3.MainWindow;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();
        }
        public async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string jsonData = Properties.Settings.Default.HistoryDownload;
            List<LogDownloadFile> dataList = JsonConvert.DeserializeObject<List<LogDownloadFile>>(jsonData);
            DataHistory.ItemsSource = dataList;
        }
    }
}
