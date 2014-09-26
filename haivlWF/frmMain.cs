using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace haivlWSCORE
{
    public partial class frmMain : Form
    {
        List<PhotoItem> list = new List<PhotoItem>();
        PhotoItem current = new PhotoItem();
        /// <summary>
        /// 5 item per segment
        /// </summary>
        int current_page_segment = 1;
        int current_index = 0;
        private string current_type = mHAIVL.NEW;
        public frmMain()
        {
            InitializeComponent();

            //set full screen
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //simulate
            //clear all
            init_new_session(mHAIVL.NEW);
            //Populate ImageSlider with images
            List<PhotoItem> re = mHAIVL.getPhoto(current_type, current_page_segment);
            list.AddRange(re);
            current = list.FirstOrDefault();
            setInfo(current);
        }

        private void btnNext_Click()
        {
            if(current_index+1 >=list.Count )
            {
                //Vuot gioi han
                //call_thread();
                return;
            }
            current = list[++current_index];
            setInfo(current);
            //prepare for next display
            if (current_index >= list.Count - 4)
            {
                //load next data segment in new thread
                call_thread();
            }
        }
        private void call_thread()
        {
            Thread oThread = new Thread(new ThreadStart(load_data_background));
            oThread.Start();
        }
        private void load_data_background()
        {
            try
            {
                try
                {
                    //trigger last item in list
                    for (int i = current_index + 1; i < list.Count; i++)
                    {
                        Bitmap tmp = list[i].DIRECT_THUMNAIL;
                    }
                }
                catch (Exception r)
                {
                    Debug.WriteLine("Nhanh qua!");
                }
                //load next segment
                List<PhotoItem> re = mHAIVL.getPhoto(current_type, ++current_page_segment);
                list.AddRange(re);
                
            }
            catch (Exception e)
            {
                Debug.WriteLine("Nhanh qua!");
            }
        }

        private void btnPrev_Click()
        {
            if(--current_index<0)
            {
                current_index = 0;
            }
            current = list[current_index];
            setInfo(current);

            this.Focus();
        }
        private void setInfo(PhotoItem item)
        {
            if (item != null)
            {
                if (item.DIRECT_THUMNAIL != null)
                {
                    //calculate size
                    int width = item.DIRECT_THUMNAIL.Width;
                    int height = item.DIRECT_THUMNAIL.Height;

                    float rate = (float)height / width;

                    picture_main.Width = this.Width;
                    picture_main.Height = (int)((rate * picture_main.Width) / 2.0);
                    //Hình quá dài cần đưa vào Scroll
                    if (rate >= 2.5)
                    {
                        picture_main.Dock = DockStyle.Top;
                    }
                    else
                    {
                        picture_main.Dock = DockStyle.Fill;
                    }
                    //set image
                    picture_main.Image = item.DIRECT_THUMNAIL;
                }
                label_title.Text = mSTRING.fromHTML( item.title );
                label_uploader.Text = mSTRING.fromHTML( item.uploader );
                label_isvideo.Visible = item.isvideo;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        btnPrev_Click();
                        break;
                    case Keys.Right:
                        btnNext_Click();
                        break;
                    case Keys.Up:
                        
                        break;
                    case Keys.Down:
                        
                        break;


                    case Keys.D1:
                        btnNew_Click();
                        break;
                    case Keys.D2:
                        btnVote_Click();
                        break;
                    case Keys.D3:
                        btnHot_Click();
                        break;
                    case Keys.Escape:
                        btnExitFullscreen_Click();
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void init_new_session(String type="")
        {
            list = new List<PhotoItem>();
            current = new PhotoItem();
            current_index = 0;
            current_page_segment = 1;
            current_type = type;
            label_title.Text = label_uploader.Text = "";
        }
        private void btnVote_Click()
        {
            //clear all
            init_new_session(mHAIVL.VOTE);
            //Populate ImageSlider with images
            List<PhotoItem> re = mHAIVL.getPhoto(current_type, current_page_segment);
            list.AddRange(re);
            current = list.FirstOrDefault();
            setInfo(current);
        }

        private void btnHot_Click()
        {
            //clear all
            init_new_session(mHAIVL.HOT);
            //Populate ImageSlider with images
            List<PhotoItem> re = mHAIVL.getPhoto(current_type, current_page_segment);
            list.AddRange(re);
            current = list.FirstOrDefault();
            setInfo(current);
        }

        private void btnNew_Click()
        {
            //clear all
            init_new_session(mHAIVL.NEW);
            //Populate ImageSlider with images
            List<PhotoItem> re = mHAIVL.getPhoto(current_type, current_page_segment);
            list.AddRange(re);
            current = list.FirstOrDefault();
            setInfo(current);
        }

        private void btnExitFullscreen_Click()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
        }
        private void playVideo(String youtube_url = "")
        {
            MessageBox.Show(youtube_url);
        }

        private void picture_main_Click(object sender, EventArgs e)
        {
            if (current.isvideo)
            {
                playVideo(current.VIDEO_URL);
            }
        }
    }
}
