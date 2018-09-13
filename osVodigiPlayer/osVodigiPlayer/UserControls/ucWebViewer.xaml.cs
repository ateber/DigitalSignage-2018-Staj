
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents; 
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace osVodigiPlayer.UserControls
{
     
    public partial class ucWebViewer : UserControl      //added
    {
        // Complete Event
        public static readonly RoutedEvent WebShowCompleteEvent = EventManager.RegisterRoutedEvent(
            "WebShowComplete", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucWebViewer));

        public event RoutedEventHandler WebShowComplete
        {
            add { AddHandler(WebShowCompleteEvent, value); }
            remove { RemoveHandler(WebShowCompleteEvent, value); }
        }

         
        public ucWebViewer()
        { 
            InitializeComponent(); 
             
        }
        // Public properties
        public Color dsBackgroundColor { get; set; }
        public int dsWebDurationInSeconds { get; set; }
        public List<string> WebsiteUrl { get; set; } 
        public bool dsFireCompleteEvent { get; set; }
 
        // Local variables
        DispatcherTimer timer;
        int URLIndex = -1; // Zero-based index
         

        public void Pause()
        {
            try
            {
                timer.Stop();
            }
            catch { }
        }

        public void Resume()
        {
            try
            {
                timer.Start();
            }
            catch { }
        }

        public void ResetControl()
        {
            try
            {
                  
                gridMain.Background = new SolidColorBrush(dsBackgroundColor);

                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = TimeSpan.FromSeconds(dsWebDurationInSeconds);

                this.Unloaded += ucWebShowFader_Unloaded;
                webBrowser.FrameLoadEnd += (sender, args) =>
                {
                    if (args.Frame.IsMain)
                    {
                         
                        Dispatcher.BeginInvoke(new Action(delegate
                        {
                            System.Diagnostics.Debug.WriteLine(this.Width+ " " + webBrowser.ActualWidth + " ");
                            //string script = "var rate=" + webBrowser.ActualWidth + "/" + "document.body.scrollWidth;" + "document.body.style.zoom=rate.toString();alert(document.body.offsetWidth+\" -   \"+document.body.scrollWidth+\"    ------   \"+document.rootElement.width);";
                            string script = "document.body.style.zoom=0.7;";
                            CefSharp.WebBrowserExtensions.ExecuteScriptAsync(webBrowser, script);
                        }));   
                    } 
                };
                
                ShowNextURL();        
                timer.Start();
            }
            catch { }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowNextURL(); 
        }

        public void StopTimer()
        {
            try
            {
                timer.Stop(); 
            }
            catch { }
        }

        private void ShowNextURL()
        {
            try
            {
                if (WebsiteUrl != null && WebsiteUrl.Count > 0)
                {
                    if (URLIndex + 1 < WebsiteUrl.Count)
                        URLIndex = URLIndex + 1;
                    else
                    {

                        if (dsFireCompleteEvent)
                        {
                            RaiseEvent(new RoutedEventArgs(WebShowCompleteEvent)); 
                        }

                        URLIndex = 0;
                    }
                    webBrowser.Address=(WebsiteUrl[URLIndex]);
                    //System.Diagnostics.Debug.WriteLine(" --------------------------------------------------------- "+ WebsiteUrl[URLIndex]);
 
                    
                   
                     
                }
            }
            catch { }
        }


        void ucWebShowFader_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                timer.Stop();
            }
            catch { }
        }

        void ucWebShowFader_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("----------------------------------------------------------------------");
            }
            catch { }
        }

    }


}


//<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID = ""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID = ""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID = ""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID = ""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID = ""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID = ""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID = ""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID = ""1000000"" AccountID=""1000000"" ScreenName=""web sites"" WebShowId=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""true"" ButtonImageID=""1000000""  /></Screens><ScreenScreenContentXrefs><ScreenScreenContentXref ScreenScreenContentXrefID = ""1000032"" ScreenID=""1000000"" ScreenContentID=""1000000"" DisplayOrder=""1""  /><ScreenScreenContentXref ScreenScreenContentXrefID = ""1000033"" ScreenID=""1000000"" ScreenContentID=""1000001"" DisplayOrder=""2""  /><ScreenScreenContentXref ScreenScreenContentXrefID = ""1000034"" ScreenID=""1000000"" ScreenContentID=""1000002"" DisplayOrder=""3""  /><ScreenScreenContentXref ScreenScreenContentXrefID = ""1000035"" ScreenID=""1000000"" ScreenContentID=""1000003"" DisplayOrder=""4""  /><ScreenScreenContentXref ScreenScreenContentXrefID = ""1000036"" ScreenID=""1000000"" ScreenContentID=""1000020"" DisplayOrder=""5""  /></ScreenScreenContentXrefs><ScreenContents><ScreenContent ScreenContentID = ""1000000"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 01 Image"" ScreenContentTitle=""Las Vegas Is badly!"" ThumbnailImageID=""1000001"" CustomField1=""1000001"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID = ""1000001"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 02 Image"" ScreenContentTitle=""Visit Las Vegas!"" ThumbnailImageID=""1000002"" CustomField1=""1000002"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID = ""1000002"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 03 Image"" ScreenContentTitle=""There&apos;s so much to do in Vegas!"" ThumbnailImageID=""1000003"" CustomField1=""1000003"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID = ""1000003"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 04 Image"" ScreenContentTitle=""Good times, day or night!"" ThumbnailImageID=""1000004"" CustomField1=""1000004"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID = ""1000020"" ScreenContentTypeID=""1000004"" ScreenContentTypeName=""Web Site"" ScreenContentName=""website"" ScreenContentTitle=""web"" ThumbnailImageID=""1000000"" CustomField1=""https://www.google.com/"" CustomField2="""" CustomField3="""" CustomField4=""""  /></ScreenContents><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><SlideShows><SlideShow SlideShowID = ""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""  /></SlideShows><WebShows><WebShow WebShowId = ""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID = ""1000000"" WebShowId=""1000000"" Name=""google"" Source=""www.google.com"" PlayOrder=""1""  /><WebShowSource WebShowUrlID = ""1000001"" WebShowId=""1000000"" Name=""google"" Source=""www.facebook.com"" PlayOrder=""2""  /></WebShowSources><SlideShowImageXrefs><SlideShowImageXref SlideShowImageXrefID = ""1000029"" SlideShowID=""1000000"" ImageID=""1000001"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID = ""1000030"" SlideShowID=""1000000"" ImageID=""1000002"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID = ""1000031"" SlideShowID=""1000000"" ImageID=""1000003"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID = ""1000032"" SlideShowID=""1000000"" ImageID=""1000004"" PlayOrder=""4""  /></SlideShowImageXrefs><SlideShowMusicXrefs></SlideShowMusicXrefs><Timelines></Timelines><TimelineImageXrefs></TimelineImageXrefs><TimelineMusicXrefs></TimelineMusicXrefs><TimelineVideoXrefs></TimelineVideoXrefs><Images><Image ImageID = ""1000000"" StoredFilename=""6f5e187f-52a2-4799-bdac-2e9199580b98.png"" ImageName=""Visit Las Vegas Button""  /><Image ImageID = ""1000001"" StoredFilename=""60255096-6a72-409e-b905-4d98ee717bb0.jpg"" ImageName=""Vegas 01""  /><Image ImageID = ""1000002"" StoredFilename=""612bb76c-e16e-4fe8-87f2-bddc7eb59300.jpg"" ImageName=""Vegas 02""  /><Image ImageID = ""1000003"" StoredFilename=""69f99c47-d1b0-4123-b62b-8f18bdc5702f.jpg"" ImageName=""Vegas 03""  /><Image ImageID = ""1000004"" StoredFilename=""626c6a35-4523-46aa-9d0a-c2687b581e27.jpg"" ImageName=""Vegas 04""  /></Images><PlayLists></PlayLists><PlayListVideoXrefs></PlayListVideoXrefs><Videos></Videos><Musics></Musics><PlayerSettings><PlayerSetting PlayerSettingName = ""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName = ""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName = ""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName = ""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName = ""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>


/*
 * 
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace osVodigiPlayer.UserControls
{
     
    public partial class ucWebViewer : UserControl      //added
    {
        // Complete Event
        public static readonly RoutedEvent WebShowCompleteEvent = EventManager.RegisterRoutedEvent(
            "WebShowComplete", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucWebViewer));

        public event RoutedEventHandler WebShowComplete
        {
            add { AddHandler(WebShowCompleteEvent, value); }
            remove { RemoveHandler(WebShowCompleteEvent, value); }
        }


        Storyboard sbFadeOutWebViewerOne;
        Storyboard sbFadeInWebViewerOne;
        Storyboard sbFadeOutWebViewerTwo;
        Storyboard sbFadeInWebViewerTwo;
        public ucWebViewer()
        { 
            InitializeComponent();
            sbFadeOutWebViewerOne = (Storyboard)FindResource("sbFadeOutWebViewerOne");
            sbFadeInWebViewerOne = (Storyboard)FindResource("sbFadeInWebViewerOne");
            sbFadeOutWebViewerTwo = (Storyboard)FindResource("sbFadeOutWebViewerTwo");
            sbFadeInWebViewerTwo = (Storyboard)FindResource("sbFadeInWebViewerTwo");
        }
        // Public properties
        public Color dsBackgroundColor { get; set; }
        public int dsWebDurationInSeconds { get; set; }
        public List<string> WebsiteUrl { get; set; } 
        public bool dsFireCompleteEvent { get; set; }
 
        // Local variables
        DispatcherTimer timer;
        int URLIndex = -1; // Zero-based index
        int URLToDisplay = 1; // 1 or 2 to indicate which Image control is currently visible

        public void Pause()
        {
            try
            {
                timer.Stop();
            }
            catch { }
        }

        public void Resume()
        {
            try
            {
                timer.Start();
            }
            catch { }
        }

        public void ResetControl()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(WebsiteUrl[0]);
                webBrowser1.Width = this.Width;
                webBrowser1.Height = this.Height;
                 
                webBrowser2.Width = this.Width;
                webBrowser2.Height = this.Height;

                gridMain.Background = new SolidColorBrush(dsBackgroundColor);

                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = TimeSpan.FromSeconds(dsWebDurationInSeconds);

                this.Unloaded += ucWebShowFader_Unloaded;

                ShowNextURL();
                
                timer.Start();
 
            }
            catch { }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowNextURL(); 
        }

        public void StopTimer()
        {
            try
            {
                timer.Stop(); 
            }
            catch { }
        }

        private void ShowNextURL()
        {
            try
            {
                if (WebsiteUrl != null && WebsiteUrl.Count > 0)
                {
                    if (URLIndex + 1 < WebsiteUrl.Count)
                        URLIndex = URLIndex + 1;
                    else
                    {

                        if (dsFireCompleteEvent)
                        {
                            RaiseEvent(new RoutedEventArgs(WebShowCompleteEvent)); 
                        }

                        URLIndex = 0;
                    }

                    if (URLToDisplay == 1)
                    {
                         
                        webBrowser1.Navigate(WebsiteUrl[URLIndex]);                         
                        sbFadeInWebViewerOne.Begin();                        
                        sbFadeOutWebViewerTwo.Begin();
                        URLToDisplay = 2;
                         
                    }
                    else
                    {
                         
                        webBrowser2.Navigate(WebsiteUrl[URLIndex]);
                        sbFadeInWebViewerTwo.Begin();
                        sbFadeOutWebViewerOne.Begin(); 
                        URLToDisplay = 1;                       
                    }
                }
            }
            catch { }
        }
        void ucWebShowFader_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                timer.Stop();
            }
            catch { }
        }
    }


}
*/