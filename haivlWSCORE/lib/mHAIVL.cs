using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace haivlWSCORE
{
    public static class mHAIVL
    {
        private static string domain = "http://haivl.com";
        public static string NEW = "new";
        public static string VOTE = "vote";
        public static string HOT = "hot";
        public static string OLD = "old";
        /// <summary>
        /// Số item trên mỗi trang
        /// </summary>
        public static int _PAGE_SEGMENT = 5;
        private static string getPageURL(String type="new", int page = 1)
        {
            return domain + "/" + type + "/" + page;
        }
        #region Business
        /// <summary>
        /// ConfigureAwait(false)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task<List<PhotoItem>> getPhoto(String type="new", int page = 1, CancellationToken cancelGateway=new CancellationToken())
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                string html = await mHTTP.getHTML(getPageURL(type, page), cancelGateway).ConfigureAwait(false);
                //get html fail
                if(html==null || html.Equals(""))
                {
                    return new List<PhotoItem>();
                }
                //parse html
                doc.LoadHtml(html);
                //query
                var list = doc.DocumentNode.Descendants("div")
                    .Where(
                    c =>
                        c.Attributes.Contains("class")
                        &&
                        c.Attributes["class"].Value.Equals(PhotoItem._CLASS_NAME)
                    ).ToList();

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
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return new List<PhotoItem>();
            }
        }

        /// <summary>
        /// Lấy URL hình gốc hoặc link youtube từ link haivl,
        /// ConfigureAwait(false)
        /// </summary>
        /// <param name="haivl_link"></param>
        /// <returns></returns>
        public static async Task<string> getRootImageUrl(string haivl_link = "", CancellationToken cancelGateway=new CancellationToken())
        {
            HtmlDocument doc = new HtmlDocument();
            string html = await mHTTP.getHTML(haivl_link,cancelGateway).ConfigureAwait(false);
            //get html fail
            if (html == null || html.Equals(""))
            {
                return "";
            }
            //parse html
            doc.LoadHtml( await mHTTP.getHTML(haivl_link, cancelGateway).ConfigureAwait(false));
            //query
            var item = doc.DocumentNode.Descendants("div").Where(
                c=>
                    c.Attributes.Contains("class")
                    &&
                    c.Attributes["class"].Value.Equals(PhotoItem._ROOTMAGE_CLASS_NAME)
            ).FirstOrDefault();
            if (item != null)
            {
                var img = item. ChildNodes.Where(c => c.Name.ToLower().Equals("img") & c.Attributes.Contains("src")).FirstOrDefault();

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
        private static string haivl_fb_id = "378808135489760";
        public static string getFbIframe(string haivl_link="", int numposts = 10, int width = 600, int height = 800)
        {
            string re =
              "<!DOCTYPE html>"
            + "</html xmlns=\"http://www.w3.org/1999/xhtml\"><head></head><body>"
            + "<iframe id=\"f9c791a5\" name=\"fc58fad2\" scrolling=\"no\" title=\"Facebook Social Plugin\" class=\"fb_ltr\" src=\"https://www.facebook.com/plugins/comments.php?api_key="
            + haivl_fb_id
            + "&amp;channel_url=http%3A%2F%2Fstatic.ak.facebook.com%2Fconnect%2Fxd_arbiter%2FZEbdHPQfV3x.js%3Fversion%3D41%23cb%3Dfb3e5de8c"
            + ""//"%26domain%3Dwww.haivl.com%26origin%3Dhttp%253A%252F%252Fwww.haivl.com%252Ff28d8c7e84"
            + "%26relation%3Dparent.parent&amp;href="
            + WebUtility.UrlEncode(haivl_link)
            + "&amp;locale=en_US&amp;numposts="
            + numposts
            + "&amp;sdk=joey&amp;width="
            + width
            + "\" style=\"border: none; overflow: hidden; height: "
            + height
            + "px; width: "
            + width
            + "px;\"></iframe>"
            + "</body></html>";

            return re;
        }
        #endregion
    }
}
