using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
        public bool isvideo
        {
            get
            {
                return direct_thumbnail_url != null && direct_thumbnail_url.ToLower().Contains("youtube.com");
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
        /// url dan toi hinh goc
        /// </summary>
        public string root_image_url { get; set; }
        /// <summary>
        /// Thong tin nguoi dang
        /// </summary>
        public string uploader { get; set; }

        public Bitmap DIRECT_THUMNAIL
        {
            get
            {
                if (mCACHE.get(direct_thumbnail_url) == null)
                {
                    //get and then register to Cache
                    mCACHE.register(direct_thumbnail_url, mHTTP.getImage(direct_thumbnail_url));
                }
                return mCACHE.get(direct_thumbnail_url);
            }
        }

        public Bitmap ROOT_IMAGE
        {
            get
            {
                if (!isvideo)
                {
                    if (mCACHE.get(root_image_url) == null)
                    {
                        string tmp = mHAIVL.getRootImageUrl(root_image_url);
                        //register to Cache
                        mCACHE.register(root_image_url, mHTTP.getImage(tmp));
                    }
                }
                return mCACHE.get(root_image_url);
            }
        }

        private String cache_VIDEO_URL = null;
        public String VIDEO_URL
        {
            get
            {
                if (cache_VIDEO_URL==null)
                {
                    if (isvideo)
                    {
                        cache_VIDEO_URL = mHAIVL.getRootImageUrl(root_image_url);
                    }
                }
                return cache_VIDEO_URL;
            }
        }
    }
}
