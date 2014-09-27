using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace haivlWSCORE
{
    public class PhotoItem
    {
        #region Constant
        public static string _CLASS_NAME
        {
            get
            {
                return "photoListItem";
            }
        }
        public static string _THUMNAIL_CLASS_NAME
        {
            get
            {
                return "thumbnail";
            }
        }
        public static string _INFO_CLASS_NAME
        {
            get
            {
                return "info";
            }
        }
        public static string _UPLOADER_CLASS_NAME
        {
            get
            {
                return "uploader";
            }
        }
        public static string _ROOTMAGE_CLASS_NAME
        {
            get
            {
                return "photoImg";
            }
        }
        #endregion
        public bool isVideo
        {
            get
            {
                return direct_thumbnail_url != null && direct_thumbnail_url.ToLower().Contains("youtube.com");
            }
        }
        public bool isSensitive
        {
            get
            {
                return direct_thumbnail_url != null && direct_thumbnail_url.ToLower().Contains("nsfw_");
            }
        }
        /// <summary>
        /// tieu de
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// thumnail url
        /// </summary>
        public string direct_thumbnail_url { get; set; }
        /// <summary>
        /// url dan toi hinh goc, dung de lay comment fb luon,
        /// haivl.com/photo/373657
        /// </summary>
        public string root_image_url { get; set; }
        /// <summary>
        /// Thong tin nguoi dang
        /// </summary>
        public string uploader { get; set; }

        public BitmapImage DIRECT_THUMNAIL
        {
            get
            {
                //if (!isSensitive)
                if (true)
                {
                    if (mCACHE.get(direct_thumbnail_url) == null)
                    {
                        //get and then register to Cache
                        mCACHE.register(direct_thumbnail_url, mHTTP.getImage(direct_thumbnail_url));
                    }
                    return mCACHE.get(direct_thumbnail_url);
                }
                //else
                //{
                //    return ROOT_IMAGE;
                //}
            }
        }

        public BitmapImage getRootImage()
        {
            //return null;
                if (!isVideo)
                {
                    if (mCACHE.get(root_image_url) == null)
                    {
                        string tmp = mHAIVL.getRootImageUrl(root_image_url).Result;
                        //register to Cache
                        mCACHE.register(root_image_url, mHTTP.getImage(tmp));
                    }
                }
                return mCACHE.get(root_image_url);
        }

        private String cache_VIDEO_URL = null;
        public String getVideoUrl()
        {
            if (cache_VIDEO_URL == null)
            {
                if (isVideo)
                {
                    cache_VIDEO_URL = mHAIVL.getRootImageUrl(root_image_url).Result + "&autoplay=1";
                }
            }
            return cache_VIDEO_URL;
        }
        public String getFbIframe(int numposts = 10, int width = 600, int height = 800)
        {
            return mHAIVL.getFbIframe(root_image_url, numposts, width, height);
        }
    }
}
