using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace haivlWSCORE
{
    public static class mHAIVL
    {
        private static string domain = "http://haivl.com";
        public static string NEW = "new";
        public static string VOTE = "vote";
        public static string HOT = "hot";

        private static string getPageURL(String type="new", int page = 1)
        {
            return domain + "/" + type + "/" + page;
        }
        #region Business
        public static List<PhotoItem> getPhoto(String type="new", int page = 1)
        {
            HtmlDocument doc;
            try
            {
                var Webget = new HtmlWeb();
                doc = Webget.Load(getPageURL(type, page));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Khong có ket noi!");
                return new List<PhotoItem>();
            }
            var list = doc.DocumentNode.SelectNodes("//*[contains(@class,'" + PhotoItem._CLASS_NAME + "')]");

            var re = new List<PhotoItem>();
            foreach (var item in list)
            {
                PhotoItem obj = new PhotoItem();

                var thumnail = item.ChildNodes.Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(PhotoItem._THUMNAIL_CLASS_NAME)).FirstOrDefault();
                if (thumnail != null)
                {
                    var a = thumnail.ChildNodes.Where(c => c.Name.ToLower().Equals("a")).FirstOrDefault();
                    if (a != null)
                    {
                        obj.root_image_url = domain + a.Attributes["href"].Value;
                        var img = a.ChildNodes.Where(c => c.Name.ToLower().Equals("img")).FirstOrDefault();
                        if (img != null)
                        {
                            obj.direct_thumbnail_url = img.Attributes["src"].Value;
                        }
                    }
                }
                else
                {
                    //Bỏ qua do là item không khả dụng
                    continue;
                }
                var info = item.ChildNodes.Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(PhotoItem._INFO_CLASS_NAME)).FirstOrDefault();
                if (info != null)
                {
                    var h2 = info.ChildNodes.Where(c => c.Name.ToLower().Equals("h2")).FirstOrDefault();
                    if (h2 != null)
                    {
                        obj.title = h2.InnerText.Trim();
                    }
                    var uploader = info.ChildNodes.Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(PhotoItem._UPLOADER_CLASS_NAME)).FirstOrDefault();
                    if (uploader != null)
                    {
                        obj.uploader = uploader.InnerText.Trim();
                    }
                }
                //add to re
                re.Add(obj);
            }

            return re;
        }

        /// <summary>
        /// Lấy URL hình gốc hoặc link youtube từ link haivl
        /// </summary>
        /// <param name="haivl_link"></param>
        /// <returns></returns>
        public static String getRootImageUrl(string haivl_link = "")
        {
            HtmlDocument doc;
            try
            {
                var Webget = new HtmlWeb();
                doc = Webget.Load(haivl_link);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Khong co ket noi");
                return "";
            }
            var item = doc.DocumentNode.SelectNodes("//*[contains(@class,'" + PhotoItem._ROOTMAGE_CLASS_NAME + "')]").FirstOrDefault();
            if (item != null)
            {
                var img = item.ChildNodes.Where(c => c.Name.ToLower().Equals("img") & c.Attributes.Contains("src")).FirstOrDefault();

                if (img != null)
                {
                    return img.Attributes["src"].Value;
                }

                var iframe = item.ChildNodes.Where(c => c.Name.ToLower().Equals("iframe") && c.Attributes.Contains("src")).FirstOrDefault();
                if (iframe != null)
                {
                    return iframe.Attributes["src"].Value;
                }
            }
            return "";
        }
        #endregion
    }
}
