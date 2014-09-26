using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace haivlWSCORE
{
    /// <summary>
    /// Hỗ trợ cache
    /// </summary>
    public static class mCACHE
    {
        private static Dictionary<String, Bitmap> collection = new Dictionary<string, Bitmap>();
        private static HashSet<String> fail_url = new HashSet<string>();

        public static Bitmap get(String url)
        {
            if (url == null)
            {
                return null;
            }
            //Nếu nằm trong fail list thì return ảnh mặc định
            if (fail_url.Contains(url))
            {
                return null;
            }
            //tìm kiếm trong cache list
            if (collection.ContainsKey(url))
            {
                return collection[url];
            }
            return null;
        }
        /// <summary>
        /// Đánh dấu url fail, lần get kế tiếp từ CACHE sẽ trả về hình mặc định
        /// </summary>
        /// <param name="url"></param>
        public static void mark_url_fail(String url)
        {
            fail_url.Add(url);
        }
        /// <summary>
        /// Đăng ký ảnh vào cache list, OVERRIDE MODE ON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="image"></param>
        public static void register(String url, Bitmap image)
        {
            if (url == null)
            {
                return;
            }
            if (collection.ContainsKey(url))
            {
                collection.Remove(url);
            }
            collection.Add(url, image);
        }
        /// <summary>
        /// clear memory
        /// </summary>
        public static void release()
        {
            if (collection != null)
            {
                collection.Clear();
            }
            if (fail_url != null)
            {
                fail_url.Clear();
            }
        }
    }
}
