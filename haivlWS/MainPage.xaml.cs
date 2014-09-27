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
using Windows.UI.Core;
using System.Threading;

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
        private int current_index = -1;
        //current page segment
        private int current_page_index = 0;
        //
        private int current_max_reach_index = -1;
        //
        private string current_type = mHAIVL.NEW;
        public MainPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// Queue Task, only one task allow at the time
        /// </summary>
        private SemaphoreSlim taskSemaphore = new SemaphoreSlim(1);
        CancellationTokenSource taskCancelGateway = new CancellationTokenSource();

        private List<Task<List<PhotoItem>>> tsk_list = new List<Task<List<PhotoItem>>>();
        /// <summary>
        /// 
        /// </summary>
        private async void loadMoreDataSegment(int pages=5)
        {
            for (int i = 0; i < pages; i++)
            {
                
                //Đợi lượt
                await taskSemaphore.WaitAsync();

                //Đang là Task hiện hành
                try
                {
                    //do a task in bg
                    var tsk = mHAIVL.getPhoto(current_type, ++current_page_index, taskCancelGateway.Token);
                    var re = await tsk;
                    if (re.Count > 0)
                    {
                        OnTaskDone(tsk);
                    }
                    else
                    {
                        current_page_index--;
                    }
                }
                finally
                {
                    //release
                    if (taskSemaphore != null)
                    {
                        taskSemaphore.Release();
                    }
                }
            }
            
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
                            if (flipView1 != null)
                            {
                                flipView1.Items.Add(convertPhotoItem(item));
                                Debug.WriteLine("Item added: " + flipView1.Items.Count +" ["+item.root_image_url+"]");
                                //update page status
                                txtIndexStatus.Text = current_index + "/" + flipView1.Items.Count;
                            }
                        }

                        //set focus for flipview
                        if (flipView1 != null)
                        {
                            flipView1.Focus(FocusState.Programmatic);
                        }
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

        private void img_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var img = (sender as Image);
            var sv = flipView1.SelectedItem as ScrollViewer;
            var pt = img.Tag as PhotoItem;
            if (pt.isSensitive)
            {
                //load hd 
                img.Source = pt.getRootImage();
                pt.getRootImage().ImageOpened += new RoutedEventHandler(
                    (sender3, e3) => 
                        when_image_loaded(pt.getRootImage(),new RoutedEventArgs(), sv)
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
            }
            catch (Exception ex)
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
                if(index-mHAIVL._PAGE_SEGMENT >= current_max_reach_index)
                {
                    loadMoreDataSegment(3);
                    //reset max
                    current_max_reach_index = index;
                }

                
                current_index = index;

                ScrollViewer sv = fv.SelectedItem as ScrollViewer;
                Image img = sv.Content as Image;
                
                current = (img.Tag as PhotoItem);

                if ((img.Source as BitmapImage).PixelWidth > 0)
                {
                    //hinh da duoc load roi
                    //Debug.WriteLine("");
                    when_image_loaded(img.Source as BitmapImage, new RoutedEventArgs(), sv);
                }
                else
                {
                    //hinh chua duoc load
                    //Debug.WriteLine("");
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
                //update page status
                txtIndexStatus.Text = current_index + "/" + flipView1.Items.Count;
                //view fb comment
                string iframe = pt.getFbIframe(10, (int)webView_fb.ActualWidth - 20, 8000);

                //webView_fb.NavigateToString(iframe);
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
            String youtube_link = pt.getVideoUrl();
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

        private void hotCAT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), mHAIVL.HOT);
        }

        private void newCAT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), mHAIVL.NEW);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //clear backstage
            this.Frame.BackStack.Clear();
            //do not cache on current page, reserve for next navigation
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
            //prepare param
            var type = e.Parameter;
            if (type != null && !type.ToString().Equals(""))
            {
                current_type = type.ToString();
            }

            //load in backgroud
            loadMoreDataSegment();
        }

        private void voteCAT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), mHAIVL.VOTE);
        }

        private void oldCAT_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), mHAIVL.OLD);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            //Do chua release resource nen gay cham khi chuyen page
            //Khi thoat khoi page hien tai
            flipView1.Items.Clear();
            mCACHE.release();
            flipView1 = null;
            webView_fb = null;
            
            if(taskCancelGateway!=null)
            {
                taskCancelGateway.Cancel();
            }
        }
    }
}
