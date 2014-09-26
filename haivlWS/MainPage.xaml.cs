using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using haivlWSCORE;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace haivlWS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //current PhotoItem
        private PhotoItem current = new PhotoItem();
        //current photo index
        private int current_index = 0;
        //current page segment
        private int current_page_index = 0;
       //
        private string current_type = mHAIVL.NEW;
        public MainPage()
        {
            this.InitializeComponent();
            //load in backgroud
            loadMoreDataSegment();
        }
        private void initNewSession(String type="new")
        {
            current = new PhotoItem();
            current_index = 0;
            current_page_index = 0;
            current_type = type;
            //clear flip view
            flipView1.Items.Clear();
        }


        private async void loadMoreDataSegment()
        {
            //do a task in bg
            var tsk = mHAIVL.getPhoto(current_type, ++current_page_index);
            await tsk.ContinueWith(
                c =>
                OnTaskDone(c));
        }
        private async void OnTaskDone(Task<List<PhotoItem>> caller)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    if (caller != null)
                    {
                        //add to flip view
                        foreach (var item in caller.Result)
                        {
                            flipView1.Items.Add(convertPhotoItem(item));
                            Debug.WriteLine("Item added" + flipView1.Items.Count);
                        }
                        //force refresh flipview                        
                    }
                }
            );
        }
        private ScrollViewer convertPhotoItem(PhotoItem pt)
        {
            ScrollViewer sv = new ScrollViewer();
            Image img = new Image();
            if (pt != null)
            {
                img.Source = pt.DIRECT_THUMNAIL;
                img.Tag = pt;
                
                img.Tapped += new TappedEventHandler(img_Tapped);

                pt.DIRECT_THUMNAIL.ImageOpened += new RoutedEventHandler(
                    (sender3, e3) => 
                        when_image_loaded(pt.DIRECT_THUMNAIL, new RoutedEventArgs(), sv)
                );
                
            }
            sv.Content = img;
            sv.Tag = pt;
            return sv;
        }
        private class mContainer
        {
            private BitmapImage bi { get; set; }
            private ScrollViewer sv { get; set; }
            private Image img { get; set; }
            public mContainer(BitmapImage bi, ScrollViewer sv, Image img)
            {
                this.bi = bi;
                this.sv = sv;
                this.img = img;
            }
        }

        private void img_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var img = (sender as Image);
            var sv = flipView1.SelectedItem as ScrollViewer;
            var pt = img.Tag as PhotoItem;
            if (pt.isSensitive)
            {
                //load hd 
                img.Source = pt.ROOT_IMAGE;
                pt.ROOT_IMAGE.ImageOpened += new RoutedEventHandler(
                    (sender3, e3) => 
                        when_image_loaded(pt.ROOT_IMAGE,new RoutedEventArgs(), sv)
                    );
            }
        }
        private class mRoutedEventArgs: RoutedEventArgs
        {
            public Object extra { get; set; }
            public mRoutedEventArgs(Object extra)
            {
                this.extra = extra;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">BitmapImage</param>
        /// <param name="e"></param>
        /// <param name="extra">ScrollViewer</param>
        private void when_image_loaded(object sender, RoutedEventArgs e, Object extra)
        {
            try
            {
                //size loaded here
                var tmp = sender as BitmapImage;
                var height = tmp.PixelHeight;
                var width = tmp.PixelWidth;
                ScrollViewer sv = extra as ScrollViewer;
                sv.HorizontalAlignment = HorizontalAlignment.Stretch;
                sv.VerticalAlignment = VerticalAlignment.Stretch;

                Image img = sv.Content as Image;

                double rate = height * 1.0 / width;
                if (rate >= 2.0)
                {
                    img.Width = flipView1.ActualWidth / 2;
                }
                else
                {
                    img.MaxWidth = flipView1.ActualWidth - 10;
                    img.MaxHeight = flipView1.ActualHeight - 10;

                    sv.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        /// <summary>
        /// Canh chinh width height cho img
        /// </summary>
        /// <param name="img"></param>
        private void formatImage(Image img)
        {

        }
        private void flipView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fv = sender as FlipView;
            if (fv == null || fv.Items == null || fv.Items.Count <= 0)
            {
                return;
            }
            var index = fv.SelectedIndex;
            if(index>=0)
            {
                if (index > current_index)
                {
                    //Slide to next
                    //Needed to load more
                    if (index >= fv.Items.Count - 3)
                    {
                        //load in bg
                        loadMoreDataSegment();
                    }
                }
                else
                {
                    //Slide to prev
                }
                current_index = index;

                ScrollViewer sv = fv.SelectedItem as ScrollViewer;
                Image img = sv.Content as Image;
                
                current = (sv.Tag as PhotoItem);
                
                if((img.Source as BitmapImage).PixelWidth>0)
                {
                    //hinh da duoc load roi
                    Debug.WriteLine("");
                    when_image_loaded(img.Source as BitmapImage, new RoutedEventArgs(), sv);
                }
                else
                {
                    //hinh chua duoc load
                    Debug.WriteLine("");
                }
                //display more infor about current item
                setInfo(current);
            }
        }
        /// <summary>
        /// Call when flip image
        /// </summary>
        /// <param name="pt"></param>
        private void setInfo(PhotoItem pt)
        {
            try
            {
                txtTitle.Text = pt.title;
                //consider display play button
                
                if(pt.isVideo)
                {
                    btnPlay.Visibility = Visibility.Visible;
                    btnPlay.Tag = btnStopVideo.Tag = pt;

                    if (mainView.Children.Where(c => c is WebView).Count() > 0)
                    {
                        //đang chơi video
                        btnStopVideo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnStopVideo.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    btnPlay.Visibility = Visibility.Collapsed;
                }

                //view fb comment
                string iframe = pt.getFbIframe(10, (int)webView_fb.ActualWidth - 20, 8000);

                webView_fb.NavigateToString(iframe);
            }catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        /// <summary>
        /// sender có chứa ref đến PhoToItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Tapped(object sender, TappedRoutedEventArgs e)
        {
            btnPlay.Visibility = Visibility.Collapsed;
            var img = sender as Image;
            PhotoItem pt = img.Tag as PhotoItem;
            String youtube_link = pt.VIDEO_URL;
            //
            WebView tmp = new WebView();
            tmp.HorizontalAlignment = HorizontalAlignment.Stretch;
            tmp.VerticalAlignment = VerticalAlignment.Stretch;
            tmp.Source = new Uri(youtube_link);
            //show video on top most
            var grid = mainView as Grid;
            grid.Children.Add(tmp);
            //hide btn playpt
            
            btnStopVideo.Visibility = Visibility.Visible;
        }

        private void btnStopVideo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            btnStopVideo.Visibility = Visibility.Collapsed;
            btnPlay.Visibility = Visibility.Visible;
            //Stop and remove web from flipview
            var webv = mainView.Children.Where(c=>c is WebView).FirstOrDefault() as WebView;
            if(webv!=null)
            {
                webv.Stop();
                webv.NavigateToString("");
                mainView.Children.Remove(webv);
                webv = null;
            }
        }

        private void flipView1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void hotCAT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //init new session
            initNewSession(mHAIVL.HOT);
            //Loi bat dong bo
            loadMoreDataSegment();
        }

        private void newCAT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //init new session
            initNewSession(mHAIVL.NEW);
            
            loadMoreDataSegment();
        }
    }
}
