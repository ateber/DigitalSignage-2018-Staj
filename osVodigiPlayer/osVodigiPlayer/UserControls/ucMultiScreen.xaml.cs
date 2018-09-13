using CefSharp;
using CefSharp.Wpf;
using osVodigiPlayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace osVodigiPlayer.UserControls
{
    /// <summary>
    /// Interaction logic for MultiScreen.xaml
    /// </summary>
    public partial class ucMultiScreen : UserControl
    {
        // Complete Event
        public static readonly RoutedEvent MultiScreenCompleteEvent = EventManager.RegisterRoutedEvent(
            "MultiScreenComplete", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucMultiScreen));

        public event RoutedEventHandler MultiScreenComplete 
        {
            add { AddHandler(MultiScreenCompleteEvent, value); }
            remove { RemoveHandler(MultiScreenCompleteEvent, value); }
        }

        private List<ucSlideShowDropFromTop> ucSlideShowDropFromTops;
        private List<ucSlideShowFader> ucSlideShowFaders;
        private List<ucSlideShowPanZoom> ucSlideShowPanZooms;
        private List<ucSlideShowSlideFromRight> ucSlideShowSlideFromRights;
        private List<ucSlideShowZoomIn> ucSlideShowZoomIns;
        private List<ucPlayList> ucPlayLists;
        private List<ucTimeline> ucTimelines;
        private List<ucWebViewer> ucWebViewers;

        private string imagefillmode = "Fill"; 
        public double ScreenHeight { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenHeightRate { get; set; }
        public double ScreenWidthRate { get; set; }
        public double ScreenPositionXRate { get; set; }
        public double ScreenPositionYRate { get; set; }
        public List<MultiScreenXref> MultiScreenXrefs { get; set; }

        private void Init()
        {
            ucSlideShowDropFromTops = new List<ucSlideShowDropFromTop>();
            ucSlideShowFaders = new List<ucSlideShowFader>();
            ucSlideShowPanZooms = new List<ucSlideShowPanZoom>();
            ucSlideShowSlideFromRights = new List<ucSlideShowSlideFromRight>();
            ucSlideShowZoomIns = new List<ucSlideShowZoomIn>();
            ucPlayLists = new List<ucPlayList>();
            ucTimelines = new List<ucTimeline>();
            ucWebViewers = new List<ucWebViewer>();
        }

        public void Pause()
        {
            try
            {
                foreach (ucSlideShowDropFromTop ucSlideShowDropFromTop in ucSlideShowDropFromTops)
                {
                    ucSlideShowDropFromTop.Pause();
                }
                foreach (ucSlideShowFader ucSlideShowFader in ucSlideShowFaders)
                {
                    ucSlideShowFader.Pause();
                }
                foreach (ucSlideShowPanZoom ucSlideShowPanZoom in ucSlideShowPanZooms)
                {
                    ucSlideShowPanZoom.Pause();
                }
                foreach (ucSlideShowSlideFromRight ucSlideShowSlideFromRight in ucSlideShowSlideFromRights)
                {
                    ucSlideShowSlideFromRight.Pause();
                }
                foreach (ucSlideShowZoomIn ucSlideShowZoomIn in ucSlideShowZoomIns)
                {
                    ucSlideShowZoomIn.Pause();
                }
                foreach (ucPlayList ucPlayList in ucPlayLists)
                {
                    ucPlayList.Pause();
                }
                foreach (ucTimeline ucTimeline in ucTimelines)
                {
                    ucTimeline.Pause();
                }
                foreach (ucWebViewer ucWebViewer in ucWebViewers)
                {
                    ucWebViewer.Pause();
                }
            }
            catch { }
        }

        public void Resume()
        {
            try
            {
                foreach (ucSlideShowDropFromTop ucSlideShowDropFromTop in ucSlideShowDropFromTops)
                {
                    ucSlideShowDropFromTop.Resume();
                }
                foreach (ucSlideShowFader ucSlideShowFader in ucSlideShowFaders)
                {
                    ucSlideShowFader.Resume();
                }
                foreach (ucSlideShowPanZoom ucSlideShowPanZoom in ucSlideShowPanZooms)
                {
                    ucSlideShowPanZoom.Resume();
                }
                foreach (ucSlideShowSlideFromRight ucSlideShowSlideFromRight in ucSlideShowSlideFromRights)
                {
                    ucSlideShowSlideFromRight.Resume();
                }
                foreach (ucSlideShowZoomIn ucSlideShowZoomIn in ucSlideShowZoomIns)
                {
                    ucSlideShowZoomIn.Resume();
                }
                foreach (ucPlayList ucPlayList in ucPlayLists)
                {
                    ucPlayList.Resume();
                }
                foreach (ucTimeline ucTimeline in ucTimelines)
                {
                    ucTimeline.Resume();
                }
                foreach (ucWebViewer ucWebViewer in ucWebViewers)
                {
                    ucWebViewer.Resume();
                }
            }
            catch { }
        }

         

        public void ResetControl()
        {
             
            foreach (MultiScreenXref msxref in MultiScreenXrefs)
            {
                foreach (Screen screen in CurrentSchedule.Screens)
                {
                    if (screen.ScreenID == msxref.ScreenID)
                    {
                        try
                        {
                            ScreenHeightRate = (msxref.Height / ScreenHeight);
                            ScreenWidthRate = (msxref.Width / ScreenWidth);
                            ScreenPositionXRate = (msxref.PosX / ScreenWidth);
                            ScreenPositionYRate = (msxref.PosY / ScreenHeight);
                            if (screen.PlayListID != 0)
                            {

                                // Get the videos to display
                                List<string> videos = new List<string>();
                                foreach (PlayListVideoXref xref in CurrentScreen.PlayListVideoXrefs)
                                {
                                    if (screen.PlayListID == xref.PlayListID)
                                    {
                                        foreach (Video video in CurrentScreen.Videos)
                                        {
                                            if (xref.VideoID == video.VideoID)
                                            {
                                                videos.Add(DownloadManager.DownloadFolder + @"Videos\" + video.StoredFilename);
                                                break;
                                            }
                                        }
                                    }
                                }

                                UserControls.ucPlayList ucplaylist = new UserControls.ucPlayList();
                                ucPlayLists.Add(ucplaylist);
                                ucplaylist.Height = ScreenHeightRate * this.Height;
                                ucplaylist.Width = ScreenWidthRate * this.Width; 
                                double posX = ScreenPositionXRate * this.Width;
                                double posY = ScreenPositionYRate * this.Height;
                                ucplaylist.HorizontalAlignment = HorizontalAlignment.Left;
                                ucplaylist.VerticalAlignment = VerticalAlignment.Top;
                                ucplaylist.Margin = new Thickness(posX, posY, 0, 0);
                                ucplaylist.dsVideoURLs = videos;
                                ucplaylist.Visibility = Visibility.Visible;
                                gridMain.Children.Add(ucplaylist);
                                ucplaylist.ResetControl();
                            }

                            // Add the WebShow if appropriate
                            if (screen.WebShowID != 0) //added
                            {

 

                                WebShow webShow = null;
                                foreach (WebShow ws in CurrentScreen.WebShows)
                                {
                                    if (CurrentScreen.ScreenInfo.WebShowID == ws.WebShowID)
                                    {
                                        webShow = ws;
                                        break;
                                    }
                                }

                                List<WebShowURLAddressXRef> webShowUrlAddressXRefs = new List<WebShowURLAddressXRef>();

                                foreach (WebShowURLAddressXRef wsuaxref in CurrentScreen.WebShowUrlAddressXRefs)
                                {
                                    if (wsuaxref.WebShowID == webShow.WebShowID)
                                    {
                                        webShowUrlAddressXRefs.Add(wsuaxref);
                                    }
                                }

                                //sorting
                                webShowUrlAddressXRefs.Sort((t1, t2) => { return t1.PlayOrder.CompareTo(t2.PlayOrder); }); //added

                                List<string> urlAddresses = new List<string>();
                                foreach (WebShowURLAddressXRef wsuaxref in webShowUrlAddressXRefs)
                                {
                                    foreach (URLAddress urlAddress in CurrentScreen.URLAddresses)
                                    {
                                        if (wsuaxref.URLAddressID == urlAddress.URLAddressID)
                                        {
                                            urlAddresses.Add(urlAddress.URLAddressSource);
                                            break;
                                        }
                                    }
                                } 

                                UserControls.ucWebViewer ucWebViewer = new UserControls.ucWebViewer();
                                ucWebViewers.Add(ucWebViewer);
                                ucWebViewer.Height = ScreenHeightRate * this.Height;
                                ucWebViewer.Width = ScreenWidthRate * this.Width;
                                ucWebViewer.webBrowser.Height = ScreenHeightRate * this.Height;
                                ucWebViewer.webBrowser.Width = ScreenWidthRate * this.Width;
                                ucWebViewer.WebsiteUrl = urlAddresses;
                                ucWebViewer.dsWebDurationInSeconds = webShow.IntervalInSecs;
                                double posX = ScreenPositionXRate * this.Width;
                                double posY = ScreenPositionYRate * this.Height;
                                ucWebViewer.HorizontalAlignment = HorizontalAlignment.Left;
                                ucWebViewer.VerticalAlignment = VerticalAlignment.Top;
                                ucWebViewer.Margin = new Thickness(posX, posY, 0, 0);
                                ucWebViewer.Visibility = Visibility.Visible;
                                gridMain.Children.Add(ucWebViewer);
                                ucWebViewer.webBrowser.Visibility = Visibility.Visible;
                                ucWebViewer.ResetControl();

                                /*ucWebViewer.webBrowser.LoadCompleted += delegate (object sender, NavigationEventArgs e)
                                {
                                 
                                    // doc.parentWindow.execScript(@"body.style.-webkit-column-width=""200*""; body.style.column-width = ""800px""; ");
                                    double Zoom = ((ucWebViewer.Width / ucWebViewer.Width) * 100);
                                    //doc.body.style.height =(this.Height/ msxref.Height) *ucWebViewer.Height;
                                    //doc.body.style.width = (this.Width / ScreenWidth) * ucWebViewer.Width;
                                    doc.parentWindow.scroll(0, 0);
                                    //doc.parentWindow.execScript("document.body.style.zoom=" + "\""+Zoom.ToString().Replace(",", ".")+"%\"" + ";");
                                    //doc.parentWindow.execScript("document.body.style.transform= \"scaleY("+0.5+")\";  document.body.style.transform= \"scaleX("+0.5+")\"; ");
                                    //doc.parentWindow.execScript("function addCss(rule) {"
                                    //    +"var css = document.createElement(\"style\");"
                                    //    +"css.type = \"text/css\";"
                                    //    +"if (css.styleSheet) css.styleSheet.cssText = rule;  "
                                    //    +"else css.appendChild(document.createTextNode(rule)); "
                                    //    +"document.getElementsByTagName(\"head\")[0].appendChild(css);"
                                    //    +"}"
                                    //    + "addCss(\"" + "body{transform: scaleX(0.5);transform: scaleY(0.5);}" + "\");" 

                                    //    );

                                    System.Diagnostics.Debug.WriteLine(doc.parentWindow.screen.width + "  " + doc.parentWindow.screen.availWidth+" ZOoM"+Zoom);


                                };*/


                            }
                            

                                // Add the slideshow if appropriate
                            if (screen.SlideShowID != 0)
                            {
                                // Get the images to display
                                List<string> images = new List<string>();
                                foreach (SlideShowImageXref xref in CurrentScreen.SlideShowImageXrefs)
                                {
                                    if (screen.SlideShowID == xref.SlideShowID)
                                    {
                                        foreach (Image image in CurrentScreen.Images)
                                        {
                                            if (xref.ImageID == image.ImageID)
                                            {
                                                images.Add(DownloadManager.DownloadFolder + @"Images\" + image.StoredFilename);
                                                break;
                                            }
                                        }
                                    }
                                }

                                // Get the music to play
                                List<string> musics = new List<string>();
                                foreach (SlideShowMusicXref xref in CurrentScreen.SlideShowMusicXrefs)
                                {
                                    if (screen.SlideShowID == xref.SlideShowID)
                                    {
                                        foreach (Music music in CurrentScreen.Musics)
                                        {
                                            if (xref.MusicID == music.MusicID)
                                            {
                                                musics.Add(DownloadManager.DownloadFolder + @"Music\" + music.StoredFilename);
                                                break;
                                            }
                                        }
                                    }
                                }

                                // Determine the type of slideshow to create - "Fade", "Drop From Top", "Slide From Right", "Pan Zoom", "Zoom In"
                                SlideShow slideshow = new SlideShow();
                                foreach (SlideShow ss in CurrentScreen.SlideShows)
                                {
                                    if (screen.SlideShowID == ss.SlideShowID)
                                        slideshow = ss;
                                }

                                if (slideshow.TransitionType == "Drop From Top")
                                {
                                    UserControls.ucSlideShowDropFromTop drop = new UserControls.ucSlideShowDropFromTop();
                                    ucSlideShowDropFromTops.Add(drop);
                                    drop.Height = ScreenHeightRate * this.Height;
                                    drop.Width = ScreenWidthRate * this.Width;
                                    double posX = ScreenPositionXRate * this.Width;
                                    double posY = ScreenPositionYRate * this.Height;
                                    drop.HorizontalAlignment = HorizontalAlignment.Left;
                                    drop.VerticalAlignment = VerticalAlignment.Top;
                                    drop.Margin = new Thickness(posX, posY, 0, 0);
                                    drop.dsSlideDurationInSeconds = slideshow.IntervalInSecs;
                                    drop.dsBackgroundColor = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                                    drop.dsImageURLs = images;
                                    drop.dsMusicURLs = musics;
                                    drop.dsImageFillMode = imagefillmode;
                                    drop.Visibility = Visibility.Visible;
                                    gridMain.Children.Add(drop);
                                    drop.ResetControl();
                                }
                                else if (slideshow.TransitionType == "Slide From Right")
                                {
                                    UserControls.ucSlideShowSlideFromRight slide = new UserControls.ucSlideShowSlideFromRight();
                                    ucSlideShowSlideFromRights.Add(slide);
                                    slide.Height = ScreenHeightRate * this.Height;
                                    slide.Width = ScreenWidthRate * this.Width;
                                    double posX = ScreenPositionXRate * this.Width;
                                    double posY = ScreenPositionYRate * this.Height;
                                    slide.HorizontalAlignment = HorizontalAlignment.Left;
                                    slide.VerticalAlignment = VerticalAlignment.Top;
                                    slide.Margin = new Thickness(posX, posY, 0, 0);
                                    slide.dsSlideDurationInSeconds = slideshow.IntervalInSecs;
                                    slide.dsBackgroundColor = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                                    slide.dsImageURLs = images;
                                    slide.dsMusicURLs = musics;
                                    slide.dsImageFillMode = imagefillmode;
                                    slide.Visibility = Visibility.Visible;
                                    gridMain.Children.Add(slide);
                                    slide.ResetControl();
                                }
                                else if (slideshow.TransitionType == "Pan Zoom")
                                {
                                    UserControls.ucSlideShowPanZoom panzoom = new UserControls.ucSlideShowPanZoom();
                                    ucSlideShowPanZooms.Add(panzoom);
                                    panzoom.Height = ScreenHeightRate * this.Height;
                                    panzoom.Width = ScreenWidthRate * this.Width;
                                    double posX = ScreenPositionXRate * this.Width;
                                    double posY = ScreenPositionYRate * this.Height;
                                    panzoom.HorizontalAlignment = HorizontalAlignment.Left;
                                    panzoom.VerticalAlignment = VerticalAlignment.Top;
                                    panzoom.Margin = new Thickness(posX, posY, 0, 0);
                                    panzoom.dsSlideDurationInSeconds = slideshow.IntervalInSecs;
                                    panzoom.dsBackgroundColor = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                                    panzoom.dsImageURLs = images;
                                    panzoom.dsMusicURLs = musics;
                                    panzoom.dsImageFillMode = imagefillmode;
                                    panzoom.Visibility = Visibility.Visible;
                                    gridMain.Children.Add(panzoom);
                                    panzoom.ResetControl();
                                }
                                else if (slideshow.TransitionType == "Zoom In")
                                {
                                    UserControls.ucSlideShowZoomIn zoomin = new UserControls.ucSlideShowZoomIn();
                                    ucSlideShowZoomIns.Add(zoomin);
                                    zoomin.Height = ScreenHeightRate * this.Height;
                                    zoomin.Width = ScreenWidthRate * this.Width;
                                    double posX = ScreenPositionXRate * this.Width;
                                    double posY = ScreenPositionYRate * this.Height;
                                    zoomin.HorizontalAlignment = HorizontalAlignment.Left;
                                    zoomin.VerticalAlignment = VerticalAlignment.Top;
                                    zoomin.Margin = new Thickness(posX, posY, 0, 0);
                                    zoomin.dsSlideDurationInSeconds = slideshow.IntervalInSecs;
                                    zoomin.dsBackgroundColor = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                                    zoomin.dsImageURLs = images;
                                    zoomin.dsMusicURLs = musics;
                                    zoomin.dsImageFillMode = imagefillmode;
                                    zoomin.Visibility = Visibility.Visible;
                                    gridMain.Children.Add(zoomin);
                                    zoomin.ResetControl();
                                }
                                else // Fade
                                {
                                    UserControls.ucSlideShowFader fader = new UserControls.ucSlideShowFader();
                                    ucSlideShowFaders.Add(fader);
                                    fader.Height = ScreenHeightRate * this.Height;
                                    fader.Width = ScreenWidthRate * this.Width;
                                    double posX = ScreenPositionXRate * this.Width;
                                    double posY = ScreenPositionYRate * this.Height;
                                    fader.HorizontalAlignment = HorizontalAlignment.Left;
                                    fader.VerticalAlignment = VerticalAlignment.Top;
                                    fader.Margin = new Thickness(posX, posY, 0, 0);
                                    fader.dsSlideDurationInSeconds = slideshow.IntervalInSecs;
                                    fader.dsBackgroundColor = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                                    fader.dsImageURLs = images;
                                    fader.dsMusicURLs = musics;
                                    fader.dsImageFillMode = imagefillmode;
                                    fader.Visibility = Visibility.Visible;
                                    gridMain.Children.Add(fader);
                                    fader.ResetControl();
                                }
                            }

                                // Add the timeline if appropriate
                            if (screen.TimelineID != 0)
                            {
                                // Get the timeline to display
                                Timeline timeline = new Timeline();
                                foreach (Timeline tl in CurrentScreen.Timelines)
                                {
                                    if (screen.TimelineID == tl.TimelineID)
                                        timeline = tl;
                                }

                                List<TimelineMedia> timelinemedia = new List<TimelineMedia>();

                                // Get the videos to display
                                foreach (TimelineVideoXref xref in CurrentScreen.TimelineVideoXrefs)
                                {
                                    if (screen.TimelineID == xref.TimelineID)
                                    {
                                        foreach (Video video in CurrentScreen.Videos)
                                        {
                                            if (xref.VideoID == video.VideoID)
                                            {
                                                TimelineMedia tlm = new TimelineMedia();
                                                tlm.StoredFilename = DownloadManager.DownloadFolder + @"Videos\" + video.StoredFilename;
                                                tlm.DisplayOrder = xref.DisplayOrder;
                                                timelinemedia.Add(tlm);
                                                break;
                                            }
                                        }
                                    }
                                }

                                // Get the images to display
                                List<string> images = new List<string>();
                                foreach (TimelineImageXref xref in CurrentScreen.TimelineImageXrefs)
                                {
                                    if (screen.TimelineID == xref.TimelineID)
                                    {
                                        foreach (Image image in CurrentScreen.Images)
                                        {
                                            if (xref.ImageID == image.ImageID)
                                            {
                                                TimelineMedia tlm = new TimelineMedia();
                                                tlm.StoredFilename = DownloadManager.DownloadFolder + @"Images\" + image.StoredFilename;
                                                tlm.DisplayOrder = xref.DisplayOrder;
                                                timelinemedia.Add(tlm);
                                                break;
                                            }
                                        }
                                    }
                                }

                                timelinemedia.Sort(delegate (TimelineMedia tlm1, TimelineMedia tlm2)
                                { return tlm1.DisplayOrder.CompareTo(tlm2.DisplayOrder); });

                                List<string> tlmedia = new List<string>();
                                foreach (TimelineMedia tlm in timelinemedia)
                                {
                                    tlmedia.Add(tlm.StoredFilename);
                                }

                                // Get the music to play
                                List<string> musics = new List<string>();
                                foreach (TimelineMusicXref xref in CurrentScreen.TimelineMusicXrefs)
                                {
                                    if (screen.TimelineID == xref.TimelineID)
                                    {
                                        foreach (Music music in CurrentScreen.Musics)
                                        {
                                            if (xref.MusicID == music.MusicID)
                                            {
                                                musics.Add(DownloadManager.DownloadFolder + @"Music\" + music.StoredFilename);
                                                break;
                                            }
                                        }
                                    }
                                }


                                UserControls.ucTimeline uctimeline = new UserControls.ucTimeline();
                                ucTimelines.Add(uctimeline);
                                uctimeline.Height = ScreenHeightRate * this.Height;
                                uctimeline.Width = ScreenWidthRate * this.Width;
                                double posX = ScreenPositionXRate * this.Width;
                                double posY = ScreenPositionYRate * this.Height;
                                uctimeline.HorizontalAlignment = HorizontalAlignment.Left;
                                uctimeline.VerticalAlignment = VerticalAlignment.Top;
                                uctimeline.Margin = new Thickness(posX, posY, 0, 0);
                                uctimeline.dsBackgroundColor = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                                uctimeline.dsMediaURLs = tlmedia;
                                uctimeline.dsMusicURLs = musics;
                                uctimeline.dsFireCompleteEvent = false;
                                uctimeline.dsMuteMusicOnPlayback = timeline.MuteMusicOnPlayback;
                                uctimeline.dsSlideDurationInSeconds = timeline.DurationInSecs;
                                uctimeline.Visibility = Visibility.Visible;
                                gridMain.Children.Add(uctimeline);
                                uctimeline.ResetControl();
                            } 
                        }
                        catch { }
                        break;
                    }
                }
            }
        }

        void ucMultiScreen_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Pause(); 
            }
            catch { }
        }

        public ucMultiScreen()
        { 
            InitializeComponent();
            Init();
            this.Unloaded += ucMultiScreen_Unloaded;
        }
        
    }
}
