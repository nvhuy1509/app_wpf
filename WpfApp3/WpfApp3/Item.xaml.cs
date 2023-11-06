using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static WpfApp3.Home;
using static WpfApp3.Offline;
using static WpfApp3.MainWindow;
using Path = System.IO.Path;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    /// 
    public class ItemDeletedEventArgs : EventArgs
    {
        public ListFileModel DeletedItem { get; }

        public ItemDeletedEventArgs(ListFileModel deletedItem)
        {
            DeletedItem = deletedItem;
        }
    }
    public class LogDownloadFile
    {
        public DateTime Timestamp { get; set; }
        public string FileName { get; set; }

    }
    public partial class Item : UserControl
    {
        private static string privateKey = "<RSAKeyValue><Modulus>lfH+E409Hv2Yx7ieTxOcpixdn4B8P4sFER+Y9YPJM+6poA1MQszGwXF1us07HZpZVP2nfvNfkQ6o1Y8GK59BsUrDunfGeSCMo3fNN32uPiB2s62TR1Tb0u7+c6/H0pQqOgO0RJOtVjvl3pAd3IJCTPBC71SEMI2OIe/tQlOvGRef+uj8c8VOS8s5kq925TZyr8EQnkbd43vppAvxMTWaYqJjbetRE3o+TjZJ6iaeMEb1bXUpqUgdK8v8xrVy/ou+D+sRxOrwrhwP0vIbxROebK8cQQw84bsC9ExXio4j6s0cSQ/TSAq5w9bsX/kBCiCtHlLPUDYHt/gRBhL/35dqqQ==</Modulus><Exponent>AQAB</Exponent><P>px5NLLAwqj2dDF2Ldn+zpewShGaOR2QfKrKHE9KKI79S9me4yiA2SrRvow9MJqB5QfoxiNoxNHsu7zO4z8Gg4agVfZTIrpLt794CBSRlJEb97OMmyTKqxtsBPuXwtvelcfjIrr9QLlf76S2hMOhoL9VJOIW/8Ih6AVj6EByU+BM=</P><Q>5bGFaOS/ztaxXn3yePR5LtLPNO0STqxuDPS2k4z3ORPO2LwlvgYOAol5LA1db+w0ddEJS98BF5+JUpsrz74kjVe4kU73q3iNKGcUPuO7KJ2VePdnsGQ5UP60hhsW8WBRbFGk/aKt+wzJ4YjClNhWtWbkdje+cCGSk51CtbtpodM=</Q><DP>LkT8dw/9GVVfwwmvUErBEYJQCipe4DM/UdIGBh33szgIn0JObmAGyB42/n2Axv0NNZAw4MQQdYhZkU7Pr23bvj6MXK2x9mulxxC/nG7cNfQV391wYqpNkCsYqDJ/uBjOzMRlw8QiTgJ8M643f9QI7J1v3V9iqhUPOopJCebv89M=</DP><DQ>aliJwyn94H8oee5oQDY4kURLaV7GFiThgyAeFCy1Hfkc0N4zv0pAXE5YCtxZQYOkOlRBMJ6ce9qCkvuDClknxUUzEBv2sa5L9MPc7M01jzV+yhYKLPIKU4TKAjkyoykMKdGXSVNzqAOgyMowBQgxBSYRauaeRn5UxgGOrGOMIb8=</DQ><InverseQ>gwJX7KoiXKZ0X7CW16RmzqA5YyU3f94xN9t2zrAUHYkXTT3nx9B6vuS9/EBwqYQf0zjfqK6BGK2dk3xvfCW8yBIsGYL1+pouiMtuSgxjq9/r4psyxEk6fj31a1AIpAcCeHlsYfgsWO1Set32CAK87sxpJShUAREnIYb96cE+ylc=</InverseQ><D>BfQHrGsNJC/ZYwSXdHhI3eghB3YoeLViTB2/CvBiiqdNIMemYXiT45ZHoFUJpdE8/rzba5TqFG5KkcAN9KlsCEf5oWuj+6cpLkxRX4Sj6F+NlvCrSke6OpO2ko1GF7w+OPNwstBSL3xNUhrStLCPRVK8tZfbb7oPDJXkC6Aa8ZoIxMss5mf41y0jX+6dfAVvueKEKriTXXKQqoLeN286inoGaTI8v/5GReltmL8AEKTC56/9M1MKa+0mnTQzUXM6LF+af6obNPXDHmS+GghnEkpXYZZYrJ3/05xq0P7/5QKQj42MkdGp4bxiY8NDCVTShM4IjwqvnUPeNxrATxDoKQ==</D></RSAKeyValue>"; // private key

        public static readonly DependencyProperty MyDataProperty =
        DependencyProperty.Register("MyData", typeof(ListFileModel), typeof(Item), new PropertyMetadata(null));


        public event EventHandler<ItemDeletedEventArgs> ItemDeleted;

        // Trong phương thức xóa, gọi sự kiện ItemDeleted để thông báo rằng UserControl này đã bị xóa.

        public ListFileModel MyData
        {
            get { return (ListFileModel)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        public async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(Properties.Settings.Default.ListFileDownLoaded))
            {
                List<ListFileModel> dataList = JsonConvert.DeserializeObject<List<ListFileModel>>(Properties.Settings.Default.ListFileDownLoaded);

                if(dataList.Count() > 0)
                {
                    if (MyData != null)
                    {
                        foreach (var item in dataList)
                        {
                            if (item.Id == MyData.Id)
                            {
                                btnDownload.Visibility = Visibility.Collapsed;
                                listBtn.Visibility = Visibility.Visible;
                                break;
                            }
                        }
                    }
                }
                
            }
        }

        public static string Decrypt(string encryptedText, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public Item()
        {
            InitializeComponent();
        }
        static async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        public class ItemDeletedEventArgs : EventArgs
        {
            public ListFileModel DeletedItem { get; }

            public ItemDeletedEventArgs(ListFileModel deletedItem)
            {
                DeletedItem = deletedItem;
            }
        }
        private float CalculateTotalSizeListFile(List<string> imagePaths)
        {
            long totalSizeBytes = 0;

            foreach (var imagePath in imagePaths)
            {
                var urlPath = "https://stemschool.vn" + imagePath;
                using (var client = new HttpClient())
                {
                    var headRequest = new HttpRequestMessage(HttpMethod.Head, urlPath);
                    var response = client.SendAsync(headRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        long contentLength;
                        if (response.Content.Headers.TryGetValues("Content-Length", out IEnumerable<string> values))
                        {
                            if (long.TryParse(values.First(), out contentLength))
                            {
                                totalSizeBytes += contentLength;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Lỗi khi truy cập {urlPath}: {response.StatusCode}");
                    }
                }
            }

            // Chuyển tổng kích thước thành MB
            float totalSizeMB = (float)totalSizeBytes / (1024 * 1024);
            return totalSizeBytes;
        }

         private float CalculateSizeFile(string Url)
        {
            long totalSizeBytes = 0;

            using (var client = new HttpClient())
            {
                var headRequest = new HttpRequestMessage(HttpMethod.Head, Url);
                var response = client.SendAsync(headRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    long contentLength;
                    if (response.Content.Headers.TryGetValues("Content-Length", out IEnumerable<string> values))
                    {
                        if (long.TryParse(values.First(), out contentLength))
                        {
                            totalSizeBytes = contentLength;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Lỗi khi truy cập {Url}: {response.StatusCode}");
                }
            }

            // Chuyển tổng kích thước thành MB
            float totalSizeMB = (float)totalSizeBytes / (1024 * 1024);
            return totalSizeBytes;
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnDownload.Visibility = Visibility.Collapsed;
                listLoading.Visibility = Visibility.Visible;


                string downloadFolder = "C:\\StemApp\\" + MyData.Id; // Đường dẫn thư mục theo Id
                string folderGiaoAn = downloadFolder + "\\GiaoAn"; // Đường dẫn thư mục giáo án theo Id

                //nếu tải lại thì cứ xóa thư mục cũ đi thêm lại
                if (Directory.Exists(downloadFolder))
                {
                    var dir = new DirectoryInfo(downloadFolder); // kiểm tra đã có folder chưa. chưa thì tạo
                    dir.Delete(true);
                }
                // Kiểm tra xem thư mục đã tồn tại hay chưa, nếu chưa thì tạo mới
                if (!Directory.Exists(downloadFolder))
                {
                    Directory.CreateDirectory(downloadFolder); // kiểm tra đã có folder chưa. chưa thì tạo
                }
                if (!Directory.Exists(folderGiaoAn))
                {
                    Directory.CreateDirectory(folderGiaoAn); // kiểm tra đã có folder chưa. chưa thì tạo

                }


                var nameFilePP = MyData.Name.Replace(" ", "") + ".pptx"; // tên file pptx
                var nameFileThumb = "ImageThumb" + MyData.Name.Replace(" ", "") + MyData.Icon.Substring(MyData.Icon.LastIndexOf('.')); //tên file thumb

                string tempFilePath1 = System.IO.Path.Combine(downloadFolder, nameFilePP);  //tạo đường dẫn tạm thời file pptx
                string tempFilePath2 = Path.Combine(downloadFolder, nameFileThumb); //tạo đường dẫn tạm thời file thumb

               
                float countFileDownloadedMB = 0;
                float totalSizeMB = 0;

                //dlStatus.Maximum += totalSizeMB;
                //float countFileDownload = imagePaths.Count() + 2;
                //float countFileDownloaded = 0;

                var imagePaths = JsonConvert.DeserializeObject<List<string>>(MyData.Screenshot); // lấy ra 3 ảnh mô tả

                totalSizeMB += CalculateTotalSizeListFile(imagePaths); // lấy ra tổng số bytes của các file mô tả
                dlStatus.UpdateLayout();
                var count = 0;
                //tải xuống ảnh mô tả
                foreach (string imagePath in imagePaths)
                {
                    count++;
                    var nameFileImg = "ImgDescription" + count + imagePath.Substring(imagePath.IndexOf('.'));
                    string tempFileImgDesc = Path.Combine(folderGiaoAn, nameFileImg.Replace(" ", ""));
                    using (var client = new HttpClient())
                    {
                        var UrlImgDesc = "https://stemschool.vn" + imagePath;
                        totalSizeMB += CalculateSizeFile(UrlImgDesc);
                        dlStatus.UpdateLayout();
                        var dataImgDesc = await client.GetByteArrayAsync(UrlImgDesc);
                        dlStatus.Value += (dataImgDesc.Length / totalSizeMB) * 100;
                        //countFileDownloadedMB += dataImgDesc.Length / (1024 * 1024);
                        dlStatus.UpdateLayout();
                        try
                        {
                            await WriteAllBytesAsync(tempFileImgDesc, dataImgDesc); // lưu file pptx vào folder

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error", "  Lỗi khi khi lưu file ảnh mô tả:" + ex.Message);
                        }
                    }
                }
                
                List<FileDesc> lstFileDes = JsonConvert.DeserializeObject<List<FileDesc>>(MyData.FileDescription); // lấy ra các file mô tả

                //for (float i = 33; i < 40; i++)
                //{
                //    dlStatus.Value = i;
                //}
                foreach (var itemFileDesc in lstFileDes)
                {
                    var nameFileDesc = itemFileDesc.Url.Substring(itemFileDesc.Url.IndexOf("CMS/") + 4);
                    string tempFileDesc = Path.Combine(folderGiaoAn, nameFileDesc.Replace(" ", ""));
                    using (var client = new HttpClient())
                    {
                        var UrlFileDesc = "https://stemschool.vn" + itemFileDesc.Url;
                        totalSizeMB += CalculateSizeFile(UrlFileDesc);
                        dlStatus.UpdateLayout();
                        var dataFileDesc = await client.GetByteArrayAsync(UrlFileDesc);
                        dlStatus.Value += (dataFileDesc.Length / totalSizeMB) * 100;
                        dlStatus.UpdateLayout();
                        //countFileDownloadedMB += dataFileDesc.Length / (1024 * 1024);
                        try
                        {
                            await WriteAllBytesAsync(tempFileDesc, dataFileDesc);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error", "  Lỗi khi lưu file mô tả:" + ex.Message);
                        }
                    }
                }
                MyData.IconOff = tempFilePath2;
                if (string.IsNullOrEmpty(Properties.Settings.Default.HistoryDownload))
                {

                    LogDownloadFile LogAction = new LogDownloadFile();
                    LogAction.FileName = MyData.Name;
                    LogAction.Timestamp = DateTime.Now;
                    string updatedHistory = "[" + JsonConvert.SerializeObject(LogAction, Formatting.Indented) + "]";

                    Properties.Settings.Default.HistoryDownload = updatedHistory;
                    Properties.Settings.Default.Save();
                } else
                {
                    List<LogDownloadFile> dataListHistory = JsonConvert.DeserializeObject<List<LogDownloadFile>>(Properties.Settings.Default.HistoryDownload);

                    LogDownloadFile LogAction = new LogDownloadFile();
                    LogAction.FileName = MyData.Name;
                    LogAction.Timestamp = DateTime.Now;
                    dataListHistory.Add(LogAction);
                    string updatedHistory = JsonConvert.SerializeObject(dataListHistory, Formatting.Indented) ;

                    Properties.Settings.Default.HistoryDownload = updatedHistory;
                    Properties.Settings.Default.Save();
                }
                if (string.IsNullOrEmpty(Properties.Settings.Default.ListFileDownLoaded))
                {
                    string fileDownloaded = "[" + JsonConvert.SerializeObject(MyData, Formatting.Indented) + "]";

                    Properties.Settings.Default.ListFileDownLoaded = fileDownloaded;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    List<ListFileModel> listFileDownload = JsonConvert.DeserializeObject<List<ListFileModel>>(Properties.Settings.Default.ListFileDownLoaded);

                    if(!listFileDownload.Any(x => x.Id == MyData.Id))
                    {
                        listFileDownload.Add(MyData);
                    }
                    string lstFileDownloaded = JsonConvert.SerializeObject(listFileDownload, Formatting.Indented);

                    Properties.Settings.Default.ListFileDownLoaded = lstFileDownloaded;
                    Properties.Settings.Default.Save();
                }
                // Tải xuống tệp .pptx0
                using (var client = new HttpClient())
                {
                    var UrlFilePP = "https://stemschool.vn" + MyData.Url;  //url tải file pp
                    totalSizeMB += CalculateSizeFile(UrlFilePP);
                    dlStatus.UpdateLayout();
                    var UrlThumb = MyData.Icon; //url tải file ảnh thumb
                    totalSizeMB += CalculateSizeFile(UrlThumb);
                    dlStatus.UpdateLayout();

                    //for (float i = 0; i < 5; i++)
                    //{
                    //    dlStatus.Value = i;
                    //}
                    var dataPP = await client.GetByteArrayAsync(UrlFilePP);  // tạo byte để tải file pp
                    dlStatus.Value += (dataPP.Length / totalSizeMB) * 100;
                    dlStatus.UpdateLayout();
                    //for (float i = 5; i < 16; i++)
                    //{
                    //    dlStatus.Value = i;
                    //}
                    //countFileDownloadedMB += dataPP.Length / (1024 * 1024);
                    var dataThumb = await client.GetByteArrayAsync(UrlThumb); // tạo byte để tải ảnh thumb
                    dlStatus.Value += (dataThumb.Length / totalSizeMB) * 100;
                    dlStatus.UpdateLayout();

                    var urlAPIUpdateDownload = $"{_baseUrl}{APIs.UpdateDownloadFile}"; // call api update khi tải file

                    UpdateDownloadFilePP dataBody = new UpdateDownloadFilePP();
                    dataBody.IdFilePP = MyData.Id;
                    string returnStr = string.Empty;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //for (float i = 16; i < 33; i++)
                    //{
                    //    dlStatus.Value = i;
                    //}
                    //countFileDownloadedMB += dataThumb.Length / (1024 * 1024);
                    try
                    {
                        await WriteAllBytesAsync(tempFilePath1, dataPP); // lưu file pptx vào folder
                        await WriteAllBytesAsync(tempFilePath2, dataThumb); // lưu ảnh thumb vào folder

                        var serializedStr = JsonConvert.SerializeObject(dataBody);

                        var response = await client.PostAsync(urlAPIUpdateDownload, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                        if (response.IsSuccessStatusCode)
                        {
                            returnStr = await response.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<DalResult>(returnStr);
                            if (data.IsSuccess == true)
                            {
                                Debug.WriteLine("+1 download file");
                            }
                            else
                            {
                                MessageBox.Show("Lỗi api update download file");
                            }
                        }
                        else
                        {
                            MessageBox.Show(response.StatusCode + "");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error", "  Lỗi khi khi lưu file PowerPoint:" + ex.Message);
                    }
                }

                
                //for (float i = 40; i < 67; i++)
                //{
                //    dlStatus.Value = i;
                //}

                
                //for (float i = 67; i < 100; i++)
                //{
                //    dlStatus.Value = i;
                //}

                
                listBtn.Visibility = Visibility.Visible;
                listLoading.Visibility = Visibility.Collapsed;
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show("  Lỗi hệ thống:" + ex.Message);
            }

        }

        public float GetOfficeVersion()
        {
            try
            {
                string sVersion = string.Empty;
                MessageBox.Show("function check version=====");

                Microsoft.Office.Interop.PowerPoint.Application appVersion = new Microsoft.Office.Interop.PowerPoint.Application();
                //appVersion.Visible = false;
                MessageBox.Show("appVersion.Version=====" + sVersion);
                float versionFloat = float.Parse(sVersion);
                //float versionFloat = float.Parse(appVersion.Version);
                return versionFloat;
            }catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
                return 0;
            }
            


        }

        private async void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //float version = GetOfficeVersion();
            //if (version == 0)
            //{
            //    MessageBox.Show("Bạn chưa cài powerpoint trên máy!", "Warning");
            //    return;
            //}
            //if(version <= 12)
            //{
            //    MessageBox.Show( "Bạn phải cài đặt office 2010 trở lên để mở file pptx có mật khẩu!", "Warning");
            //    return;
            //}
            //MessageBox.Show("open file 1 lấy đường dẫn");
            string downloadFolder = "C:\\StemApp\\" + MyData.Id; //Đường dẫn đến file pptx

            var password = Decrypt(MyData.Password, privateKey);
            //MessageBox.Show("open file 2 decrypt password");
            // Kiểm tra xem thư mục đã tồn tại hay chưa, nếu chưa thì tạo mới
            var nameFilePP = MyData.Name.Replace(" ", "") + ".pptx";
            // Đường dẫn đầy đủ tới tệp .pptx
            string tempFilePath = Path.Combine(downloadFolder, nameFilePP);
            //MessageBox.Show("open file 3 lấy đường dẫn");
            try
            {
                //MessageBox.Show("open file 4");

                Microsoft.Office.Interop.PowerPoint.Application powerPoint = new Microsoft.Office.Interop.PowerPoint.Application();
               //MessageBox.Show("open file 5");
                Presentations presentations = powerPoint.Presentations;
                //MessageBox.Show("open file 3");
                Presentation ps = null;
               //MessageBox.Show("open file 6");
                //Thread openPPT = new Thread(delegate ()
                //{
                //  MessageBox.Show("open file 5");
                //    presentations.Open(tempFilePath, MsoTriState.msoTrue, MsoTriState.msoFalse);

                //});
               //MessageBox.Show("open file 6");
                //Presentation pptPresentation = powerPoint
                ps = powerPoint.ProtectedViewWindows.Open(tempFilePath, password, Microsoft.Office.Core.MsoTriState.msoFalse).Presentation;
              //MessageBox.Show("open file 7");
            }
            catch (Exception ex)
            {
                MessageBox.Show("  Lỗi hệ thống:" + ex.HResult +"==========="+ex.Message, "Error" );
                listBtn.Visibility = Visibility.Collapsed;
                btnDownload.Visibility = Visibility.Visible;
            }
        }

        private async void btnOpenGiaoAn_Click(object sender, RoutedEventArgs e)
        {
            string downloadFolder = "C:\\StemApp\\" + MyData.Id; // Đường dẫn thư mục theo Id
            string folderGiaoAn = downloadFolder + "\\GiaoAn";

            Process.Start("explorer.exe", folderGiaoAn);
        }

        private async void btnDeleteFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có chắc chắn muốn xoá file" + MyData.Name, "Xoá File", MessageBoxButton.YesNo);

            ItemDeleted?.Invoke(this, new ItemDeletedEventArgs(MyData));
            string downloadFolder = "C:\\StemApp\\" + MyData.Id;
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                List<ListFileModel> dataList = JsonConvert.DeserializeObject<List<ListFileModel>>(Properties.Settings.Default.ListFileDownLoaded);

                bool alreadyExist = dataList.Any(x => x.Id == MyData.Id);
                if (alreadyExist)
                {
                    dataList.RemoveAll(x => x.Id == MyData.Id);
                    string updatedData = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                    // Write the updated JSON data back to the file
                    Properties.Settings.Default.ListFileDownLoaded = updatedData;
                    Properties.Settings.Default.Save();
                }

                //if()
                if(TeachOff == true)
                {
                    UpdateLayout();
                } else
                {
                    listBtn.Visibility = Visibility.Collapsed;
                    btnDownload.Visibility = Visibility.Visible;
                }
                //Directory.Delete(downloadFolder, true);  
                MessageBox.Show("Xoá thành công");

            }
        }
    }
}
