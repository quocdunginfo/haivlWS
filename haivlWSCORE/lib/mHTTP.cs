using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace haivlWSCORE
{
    public class mHTTP
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Đường dẫn URL của hình</param>
        /// <param name="force">Bỏ qua cached fail url (luôn lấy về từ Internet)</param>
        /// <returns>null (Mạng lỗi, URL not found, image corrupted or unsupported)</returns>
        public static BitmapImage getImage(String url, Boolean force = false)
        {
            try
            {
                BitmapImage re = new BitmapImage(new Uri(url, UriKind.Absolute));
                return re;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        /// <summary>
        /// ConfigureAwait(false), cho phep goi Result ngay sau do,
        /// cho pheo cancel task giua chung
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> getHTML(string url = "", CancellationToken cancelGateway = new CancellationToken())
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(new Uri(url), cancelGateway);
                var html = await response.Content.ReadAsStringAsync();

                return html;
            }catch(OperationCanceledException)
            {
                Debug.WriteLine("Task canceled: " + url);
                return "";
            }
            catch(Exception)
            {
                Debug.WriteLine("Network fail: " + url);
                return "";
            }
        }
        /// <summary>
        /// ConfigureAwait(false)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> getHTML(string url = "")
        {
            try
            {
                var httpClient = new HttpClient();
                return await httpClient.GetStringAsync(new Uri(url)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                Debug.WriteLine("Network fail: " + url);
                return "";
            }
        }
    }
}
