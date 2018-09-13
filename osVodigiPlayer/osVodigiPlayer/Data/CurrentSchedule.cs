using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using osVodigiPlayer.Data;

/* ----------------------------------------------------------------------------------------
    Vodigi - Open Source Interactive Digital Signage
    Copyright (C) 2005-2013  JMC Publications, LLC

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
---------------------------------------------------------------------------------------- */

namespace osVodigiPlayer
{
    class CurrentSchedule
    {
        public static List<PlayerGroupSchedule> PlayerGroupSchedules; // Should only be one
        public static List<Screen> Screens;
        public static List<PlayList> PlayLists;
        public static List<PlayListVideoXref> PlayListVideoXrefs;
        public static List<WebShow> WebShows; //added
        public static List<WebShowURLAddressXRef> WebShowUrlAddressXRefs; //added //updated
        public static List<URLAddress> URLAddresses; //new added
        public static List<MultiScreenXref> MultiScreenXrefs; //added
        public static List<MultiScreen> MultiScreens;   //added
        public static List<SlideShow> SlideShows;
        public static List<SlideShowImageXref> SlideShowImageXrefs;
        public static List<SlideShowMusicXref> SlideShowMusicXrefs;
        public static List<ScreenScreenContentXref> ScreenScreenContentXrefs;
        public static List<ScreenContent> ScreenContents;
        public static List<Image> Images;
        public static List<Video> Videos;
        public static List<Timeline> Timelines;
        public static List<TimelineImageXref> TimelineImageXrefs;
        public static List<TimelineVideoXref> TimelineVideoXrefs;
        public static List<TimelineMusicXref> TimelineMusicXrefs;
        public static List<Music> Musics;
        public static List<Survey> Surveys;
        public static List<SurveyQuestion> SurveyQuestions;
        public static List<SurveyQuestionOption> SurveyQuestionOptions;
        public static List<PlayerSetting> PlayerSettings;
        public static string LastScheduleXML;

        public static void ParseScheduleXml(string xml)
        {
            try
            {
                //xml = @"<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID=""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000000"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID=""1000000"" AccountID=""1000000"" ScreenName=""web sites"" MultiScreenID=""0"" WebShowID=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /></Screens><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><SlideShows></SlideShows><WebShows><WebShow WebShowID=""1000000"" IntervalInSecs=""5"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID=""1000000"" WebShowID=""1000000"" Name=""google"" Source=""http://www.google.com"" PlayOrder=""1""  /><WebShowSource WebShowUrlID=""1000001"" WebShowID=""1000000"" Name=""face"" Source=""http://www.facebook.com"" PlayOrder=""2""  /></WebShowSources><SlideShowMusicXrefs></SlideShowMusicXrefs><Timelines></Timelines><TimelineImageXrefs></TimelineImageXrefs><TimelineMusicXrefs></TimelineMusicXrefs><TimelineVideoXrefs></TimelineVideoXrefs><Images></Images><PlayLists></PlayLists><PlayListVideoXrefs></PlayListVideoXrefs><Videos></Videos><Musics></Musics><PlayerSettings><PlayerSetting PlayerSettingName=""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName=""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName=""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName=""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName=""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>";
                //xml = @"<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID=""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID=""1000010"" AccountID=""1000000"" ScreenName=""multi screen"" MultiScreenID=""1000000"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000000"" AccountID=""1000000"" ScreenName=""web sites"" MultiScreenID=""0"" WebShowID=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /></Screens><MultiScreenXrefs><MultiScreenXref MultiScreenXrefID=""1000000"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""0"" PosY=""0"" Height=""300"" Width=""700"" /><MultiScreenXref MultiScreenXrefID=""1000001"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""0"" PosY=""300""	Height=""300"" Width=""700"" /><MultiScreenXref MultiScreenXrefID=""1000002"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""700"" PosY=""0""	Height=""300"" Width=""700"" /></MultiScreenXrefs><MultiScreens><MultiScreen MultiScreenID=""1000000"" ScreenHeight=""1366"" ScreenWidth=""768""  /></MultiScreens><ScreenScreenContentXrefs><ScreenScreenContentXref ScreenScreenContentXrefID=""1000032"" ScreenID=""1000000"" ScreenContentID=""1000000"" DisplayOrder=""1""	/><ScreenScreenContentXref ScreenScreenContentXrefID=""1000033"" ScreenID=""1000000"" ScreenContentID=""1000001"" DisplayOrder=""2""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000034"" ScreenID=""1000000"" ScreenContentID=""1000002"" DisplayOrder=""3""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000035"" ScreenID=""1000000"" ScreenContentID=""1000003"" DisplayOrder=""4""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000036"" ScreenID=""1000000"" ScreenContentID=""1000020"" DisplayOrder=""5""  /></ScreenScreenContentXrefs><ScreenContents><ScreenContent ScreenContentID=""1000000"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 01 Image"" ScreenContentTitle=""Las Vegas Is badly!"" ThumbnailImageID=""1000001"" CustomField1=""1000001"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000001"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 02 Image"" ScreenContentTitle=""Visit Las Vegas!"" ThumbnailImageID=""1000002"" CustomField1=""1000002"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000002"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 03 Image"" ScreenContentTitle=""There&apos;s so much to do in Vegas!"" ThumbnailImageID=""1000003"" CustomField1=""1000003"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000003"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 04 Image"" ScreenContentTitle=""Good times, day or night!"" ThumbnailImageID=""1000004"" CustomField1=""1000004"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000020"" ScreenContentTypeID=""1000004"" ScreenContentTypeName=""Web Site"" ScreenContentName=""website"" ScreenContentTitle=""web"" ThumbnailImageID=""1000000"" CustomField1=""https://www.google.com/"" CustomField2="""" CustomField3="""" CustomField4=""""  /></ScreenContents><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><SlideShows><SlideShow SlideShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""  /></SlideShows><WebShows><WebShow WebShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID=""1000000"" WebShowID=""1000000"" Name=""google"" Source=""http://www.google.com"" PlayOrder=""1""  /><WebShowSource WebShowUrlID=""1000001"" WebShowID=""1000000"" Name=""google"" Source=""http://www.facebook.com"" PlayOrder=""2""  /></WebShowSources><SlideShowImageXrefs><SlideShowImageXref SlideShowImageXrefID=""1000029"" SlideShowID=""1000000"" ImageID=""1000001"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000030"" SlideShowID=""1000000"" ImageID=""1000002"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000031"" SlideShowID=""1000000"" ImageID=""1000003"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000032"" SlideShowID=""1000000"" ImageID=""1000004"" PlayOrder=""4""  /></SlideShowImageXrefs><SlideShowMusicXrefs></SlideShowMusicXrefs><Timelines></Timelines><TimelineImageXrefs></TimelineImageXrefs><TimelineMusicXrefs></TimelineMusicXrefs><TimelineVideoXrefs></TimelineVideoXrefs><Images><Image ImageID=""1000000"" StoredFilename=""6f5e187f-52a2-4799-bdac-2e9199580b98.png"" ImageName=""Visit Las Vegas Button""  /><Image ImageID=""1000001"" StoredFilename=""60255096-6a72-409e-b905-4d98ee717bb0.jpg"" ImageName=""Vegas 01""  /><Image ImageID=""1000002"" StoredFilename=""612bb76c-e16e-4fe8-87f2-bddc7eb59300.jpg"" ImageName=""Vegas 02""  /><Image ImageID=""1000003"" StoredFilename=""69f99c47-d1b0-4123-b62b-8f18bdc5702f.jpg"" ImageName=""Vegas 03""  /><Image ImageID=""1000004"" StoredFilename=""626c6a35-4523-46aa-9d0a-c2687b581e27.jpg"" ImageName=""Vegas 04""  /></Images><PlayLists></PlayLists><PlayListVideoXrefs></PlayListVideoXrefs><Videos></Videos><Musics></Musics><PlayerSettings><PlayerSetting PlayerSettingName=""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName=""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName=""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName=""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName=""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>";
                xml = @"<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID=""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID=""1000001"" AccountID=""1000000"" ScreenName=""Visit Las Vegas"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""1000000"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000010"" AccountID=""1000000"" ScreenName=""multi screen"" MultiScreenID=""1000000"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000000"" AccountID=""1000000"" ScreenName=""web sites"" MultiScreenID=""0"" WebShowID=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /></Screens><MultiScreenXrefs><MultiScreenXref MultiScreenXrefID=""1000000"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""0"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000001"" MultiScreenID=""1000000"" ScreenID=""1000001"" PosX=""0"" PosY=""350"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000002"" MultiScreenID=""1000000"" ScreenID=""1000001"" PosX=""650"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000003"" MultiScreenID=""1000000"" ScreenID=""1000001"" PosX=""650"" PosY=""350"" Height=""350"" Width=""650"" /></MultiScreenXrefs><MultiScreens><MultiScreen MultiScreenID=""1000000"" ScreenHeight=""768"" ScreenWidth=""1366""  /></MultiScreens><ScreenScreenContentXrefs><ScreenScreenContentXref ScreenScreenContentXrefID=""1000032"" ScreenID=""1000001"" ScreenContentID=""1000000"" DisplayOrder=""1""	/><ScreenScreenContentXref ScreenScreenContentXrefID=""1000033"" ScreenID=""1000001"" ScreenContentID=""1000001"" DisplayOrder=""2""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000034"" ScreenID=""1000001"" ScreenContentID=""1000002"" DisplayOrder=""3""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000035"" ScreenID=""1000001"" ScreenContentID=""1000003"" DisplayOrder=""4""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000036"" ScreenID=""1000001"" ScreenContentID=""1000020"" DisplayOrder=""5""  /></ScreenScreenContentXrefs><ScreenContents><ScreenContent ScreenContentID=""1000000"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 01 Image"" ScreenContentTitle=""Las Vegas Is badly!"" ThumbnailImageID=""1000001"" CustomField1=""1000001"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000001"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 02 Image"" ScreenContentTitle=""Visit Las Vegas!"" ThumbnailImageID=""1000002"" CustomField1=""1000002"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000002"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 03 Image"" ScreenContentTitle=""There&apos;s so much to do in Vegas!"" ThumbnailImageID=""1000003"" CustomField1=""1000003"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000003"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 04 Image"" ScreenContentTitle=""Good times, day or night!"" ThumbnailImageID=""1000004"" CustomField1=""1000004"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000020"" ScreenContentTypeID=""1000004"" ScreenContentTypeName=""Web Site"" ScreenContentName=""website"" ScreenContentTitle=""web"" ThumbnailImageID=""1000000"" CustomField1=""https://www.google.com/"" CustomField2="""" CustomField3="""" CustomField4=""""  /></ScreenContents><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><SlideShows><SlideShow SlideShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""  /></SlideShows><WebShows><WebShow WebShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID=""1000000"" WebShowID=""1000000"" Name=""google"" Source=""http://www.google.com"" PlayOrder=""1""  /><WebShowSource WebShowUrlID=""1000001"" WebShowID=""1000000"" Name=""google"" Source=""http://www.facebook.com"" PlayOrder=""2""  /></WebShowSources><SlideShowImageXrefs><SlideShowImageXref SlideShowImageXrefID=""1000029"" SlideShowID=""1000000"" ImageID=""1000001"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000030"" SlideShowID=""1000000"" ImageID=""1000002"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000031"" SlideShowID=""1000000"" ImageID=""1000003"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000032"" SlideShowID=""1000000"" ImageID=""1000004"" PlayOrder=""4""  /></SlideShowImageXrefs><SlideShowMusicXrefs></SlideShowMusicXrefs><Timelines></Timelines><TimelineImageXrefs></TimelineImageXrefs><TimelineMusicXrefs></TimelineMusicXrefs><TimelineVideoXrefs></TimelineVideoXrefs><Images><Image ImageID=""1000000"" StoredFilename=""6f5e187f-52a2-4799-bdac-2e9199580b98.png"" ImageName=""Visit Las Vegas Button""  /><Image ImageID=""1000001"" StoredFilename=""60255096-6a72-409e-b905-4d98ee717bb0.jpg"" ImageName=""Vegas 01""  /><Image ImageID=""1000002"" StoredFilename=""612bb76c-e16e-4fe8-87f2-bddc7eb59300.jpg"" ImageName=""Vegas 02""  /><Image ImageID=""1000003"" StoredFilename=""69f99c47-d1b0-4123-b62b-8f18bdc5702f.jpg"" ImageName=""Vegas 03""  /><Image ImageID=""1000004"" StoredFilename=""626c6a35-4523-46aa-9d0a-c2687b581e27.jpg"" ImageName=""Vegas 04""  /></Images><PlayLists></PlayLists><PlayListVideoXrefs></PlayListVideoXrefs><Videos></Videos><Musics></Musics><PlayerSettings><PlayerSetting PlayerSettingName=""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName=""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName=""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName=""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName=""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>";
                //xml= @"<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID=""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID=""1000008"" AccountID=""1000000"" ScreenName=""Example PlayList"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""1000000"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000007"" AccountID=""1000000"" ScreenName=""Example Timeline"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""1000000"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000001"" AccountID=""1000000"" ScreenName=""Visit Las Vegas"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""1000000"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""true"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000010"" AccountID=""1000000"" ScreenName=""multi screen"" MultiScreenID=""1000000"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000000"" AccountID=""1000000"" ScreenName=""web sites"" MultiScreenID=""0"" WebShowID=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /></Screens><MultiScreenXrefs><MultiScreenXref MultiScreenXrefID=""1000000"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""0"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000001"" MultiScreenID=""1000000"" ScreenID=""1000001"" PosX=""0"" PosY=""350"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000002"" MultiScreenID=""1000000"" ScreenID=""1000008"" PosX=""650"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000003"" MultiScreenID=""1000000"" ScreenID=""1000007"" PosX=""650"" PosY=""350"" Height=""350"" Width=""650"" /></MultiScreenXrefs><MultiScreens><MultiScreen MultiScreenID=""1000000"" ScreenHeight=""768"" ScreenWidth=""1366""  /></MultiScreens><ScreenScreenContentXrefs><ScreenScreenContentXref ScreenScreenContentXrefID=""1000032"" ScreenID=""1000001"" ScreenContentID=""1000000"" DisplayOrder=""1""	/><ScreenScreenContentXref ScreenScreenContentXrefID=""1000033"" ScreenID=""1000001"" ScreenContentID=""1000001"" DisplayOrder=""2""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000034"" ScreenID=""1000001"" ScreenContentID=""1000002"" DisplayOrder=""3""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000035"" ScreenID=""1000001"" ScreenContentID=""1000003"" DisplayOrder=""4""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000036"" ScreenID=""1000001"" ScreenContentID=""1000020"" DisplayOrder=""5""  /></ScreenScreenContentXrefs><ScreenContents><ScreenContent ScreenContentID=""1000000"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 01 Image"" ScreenContentTitle=""Las Vegas Is badly!"" ThumbnailImageID=""1000001"" CustomField1=""1000001"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000001"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 02 Image"" ScreenContentTitle=""Visit Las Vegas!"" ThumbnailImageID=""1000002"" CustomField1=""1000002"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000002"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 03 Image"" ScreenContentTitle=""There&apos;s so much to do in Vegas!"" ThumbnailImageID=""1000003"" CustomField1=""1000003"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000003"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 04 Image"" ScreenContentTitle=""Good times, day or night!"" ThumbnailImageID=""1000004"" CustomField1=""1000004"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000020"" ScreenContentTypeID=""1000004"" ScreenContentTypeName=""Web Site"" ScreenContentName=""website"" ScreenContentTitle=""web"" ThumbnailImageID=""1000000"" CustomField1=""https://www.google.com/"" CustomField2="""" CustomField3="""" CustomField4=""""  /></ScreenContents><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><SlideShows><SlideShow SlideShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""  /></SlideShows><WebShows><WebShow WebShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID=""1000000"" WebShowID=""1000000"" Name=""google"" Source=""https://demos.devexpress.com/Dashboard/?mode=viewer&amp;dashboardId=EnergyConsumption&amp;dashboardState=%7B%22Parameters%22%3A%7B%22Year%22%3A2009%7D%2C%22Items%22%3A%7B%22mapProduction%22%3A%7B%22MasterFilterValues%22%3A%5B%5B39%2C22%5D%5D%7D%7D%7D"" PlayOrder=""1""  /><WebShowSource WebShowUrlID=""1000001"" WebShowID=""1000000"" Name=""google"" Source=""https://demos.devexpress.com/Dashboard//?mode=viewer\&amp;dashboardId=SalesOverview"" PlayOrder=""2""  /></WebShowSources><SlideShowImageXrefs><SlideShowImageXref SlideShowImageXrefID=""1000029"" SlideShowID=""1000000"" ImageID=""1000001"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000030"" SlideShowID=""1000000"" ImageID=""1000002"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000031"" SlideShowID=""1000000"" ImageID=""1000003"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000032"" SlideShowID=""1000000"" ImageID=""1000004"" PlayOrder=""4""  /></SlideShowImageXrefs><SlideShowMusicXrefs></SlideShowMusicXrefs><Timelines><Timeline TimelineID=""1000000"" DurationInSecs=""5"" MuteMusicOnPlayback=""true""  /></Timelines><TimelineImageXrefs><TimelineImageXref TimelineImageXrefID=""1000025"" TimelineID=""1000000"" ImageID=""1000001"" DisplayOrder=""1""  /><TimelineImageXref TimelineImageXrefID=""1000026"" TimelineID=""1000000"" ImageID=""1000002"" DisplayOrder=""2""  /><TimelineImageXref TimelineImageXrefID=""1000027"" TimelineID=""1000000"" ImageID=""1000003"" DisplayOrder=""3""  /><TimelineImageXref TimelineImageXrefID=""1000028"" TimelineID=""1000000"" ImageID=""1000004"" DisplayOrder=""4""  /></TimelineImageXrefs><TimelineMusicXrefs><TimelineMusicXref TimelineMusicXrefID=""1000018"" TimelineID=""1000000"" MusicID=""1000000"" PlayOrder=""1""  /><TimelineMusicXref TimelineMusicXrefID=""1000019"" TimelineID=""1000000"" MusicID=""1000001"" PlayOrder=""2""  /><TimelineMusicXref TimelineMusicXrefID=""1000020"" TimelineID=""1000000"" MusicID=""1000002"" PlayOrder=""3""  /></TimelineMusicXrefs><TimelineVideoXrefs><TimelineVideoXref TimelineVideoXrefID=""1000001"" TimelineID=""1000000"" VideoID=""1000000"" DisplayOrder=""5""  /></TimelineVideoXrefs><Images><Image ImageID=""1000000"" StoredFilename=""6f5e187f-52a2-4799-bdac-2e9199580b98.png"" ImageName=""Visit Las Vegas Button""  /><Image ImageID=""1000001"" StoredFilename=""60255096-6a72-409e-b905-4d98ee717bb0.jpg"" ImageName=""Vegas 01""  /><Image ImageID=""1000002"" StoredFilename=""612bb76c-e16e-4fe8-87f2-bddc7eb59300.jpg"" ImageName=""Vegas 02""  /><Image ImageID=""1000003"" StoredFilename=""69f99c47-d1b0-4123-b62b-8f18bdc5702f.jpg"" ImageName=""Vegas 03""  /><Image ImageID=""1000004"" StoredFilename=""626c6a35-4523-46aa-9d0a-c2687b581e27.jpg"" ImageName=""Vegas 04""  /></Images><PlayLists><PlayList PlayListID=""1000000""  /></PlayLists><PlayListVideoXrefs><PlayListVideoXref PlayListVideoXrefID=""1000000"" PlayListID=""1000000"" VideoID=""1000000"" PlayOrder=""1""  /></PlayListVideoXrefs><Videos><Video VideoID=""1000000"" StoredFilename=""0EBC6160-CA2C-4497-960C-0A2C2DE7B380.mp4"" VideoName=""Countdown Video""  /></Videos><Musics><Music MusicID=""1000000"" StoredFilename=""1B36982F-4101-4D38-AF20-FAD88A0FA9B5.mp3"" MusicName=""Music Example 1""  /><Music MusicID=""1000001"" StoredFilename=""ADA2DBFA-D8D9-49A8-8370-8329A830667E.mp3"" MusicName=""Music Example 2""  /><Music MusicID=""1000002"" StoredFilename=""E4B660F0-ACD3-44F1-92EE-FA23110BE5C6.mp3"" MusicName=""Music Example 3""  /></Musics><PlayerSettings><PlayerSetting PlayerSettingName=""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName=""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName=""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName=""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName=""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>";
                //xml = @"<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID=""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID=""1000008"" AccountID=""1000000"" ScreenName=""Example PlayList"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""1000000"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000101"" AccountID=""1000000"" ScreenName=""Omer abi Video"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""1000001"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000007"" AccountID=""1000000"" ScreenName=""Example Timeline"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""1000000"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000100"" AccountID=""1000000"" ScreenName=""Omer Abi resim"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""1000001"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000001"" AccountID=""1000000"" ScreenName=""Visit Las Vegas"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""1000000"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000010"" AccountID=""1000000"" ScreenName=""multi screen"" MultiScreenID=""1000000"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000000"" AccountID=""1000000"" ScreenName=""web sites"" MultiScreenID=""0"" WebShowID=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /></Screens><MultiScreenXrefs><MultiScreenXref MultiScreenXrefID=""1000000"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""0"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000001"" MultiScreenID=""1000000"" ScreenID=""1000100"" PosX=""0"" PosY=""350"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000002"" MultiScreenID=""1000000"" ScreenID=""1000101"" PosX=""650"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000003"" MultiScreenID=""1000000"" ScreenID=""1000007"" PosX=""650"" PosY=""350"" Height=""350"" Width=""650"" /></MultiScreenXrefs><MultiScreens><MultiScreen MultiScreenID=""1000000"" ScreenHeight=""768"" ScreenWidth=""1366""  /></MultiScreens><ScreenScreenContentXrefs><ScreenScreenContentXref ScreenScreenContentXrefID=""1000032"" ScreenID=""1000001"" ScreenContentID=""1000000"" DisplayOrder=""1""	/><ScreenScreenContentXref ScreenScreenContentXrefID=""1000033"" ScreenID=""1000001"" ScreenContentID=""1000001"" DisplayOrder=""2""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000034"" ScreenID=""1000001"" ScreenContentID=""1000002"" DisplayOrder=""3""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000035"" ScreenID=""1000001"" ScreenContentID=""1000003"" DisplayOrder=""4""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000036"" ScreenID=""1000001"" ScreenContentID=""1000020"" DisplayOrder=""5""  /></ScreenScreenContentXrefs><ScreenContents><ScreenContent ScreenContentID=""1000000"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 01 Image"" ScreenContentTitle=""Las Vegas Is badly!"" ThumbnailImageID=""1000001"" CustomField1=""1000001"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000001"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 02 Image"" ScreenContentTitle=""Visit Las Vegas!"" ThumbnailImageID=""1000002"" CustomField1=""1000002"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000002"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 03 Image"" ScreenContentTitle=""There&apos;s so much to do in Vegas!"" ThumbnailImageID=""1000003"" CustomField1=""1000003"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000003"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 04 Image"" ScreenContentTitle=""Good times, day or night!"" ThumbnailImageID=""1000004"" CustomField1=""1000004"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000020"" ScreenContentTypeID=""1000004"" ScreenContentTypeName=""Web Site"" ScreenContentName=""website"" ScreenContentTitle=""web"" ThumbnailImageID=""1000000"" CustomField1=""https://www.google.com/"" CustomField2="""" CustomField3="""" CustomField4=""""  /></ScreenContents><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><WebShows><WebShow WebShowID=""1000000"" IntervalInSecs=""40"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID=""1000000"" WebShowID=""1000000"" Name=""google"" Source=""https://demos.devexpress.com/Dashboard/?mode=viewer&amp;dashboardId=EnergyConsumption&amp;dashboardState=%7B%22Parameters%22%3A%7B%22Year%22%3A2009%7D%2C%22Items%22%3A%7B%22mapProduction%22%3A%7B%22MasterFilterValues%22%3A%5B%5B39%2C22%5D%5D%7D%7D%7D"" PlayOrder=""1""  /><WebShowSource WebShowUrlID=""1000001"" WebShowID=""1000000"" Name=""google"" Source=""https://demos.devexpress.com/Dashboard/?mode=viewer&amp;dashboardId=SalesOverview"" PlayOrder=""2""  /></WebShowSources><Timelines><Timeline TimelineID=""1000000"" DurationInSecs=""5"" MuteMusicOnPlayback=""true""  /></Timelines><TimelineImageXrefs><TimelineImageXref TimelineImageXrefID=""1000025"" TimelineID=""1000000"" ImageID=""1000001"" DisplayOrder=""1""  /><TimelineImageXref TimelineImageXrefID=""1000026"" TimelineID=""1000000"" ImageID=""1000002"" DisplayOrder=""2""  /><TimelineImageXref TimelineImageXrefID=""1000027"" TimelineID=""1000000"" ImageID=""1000003"" DisplayOrder=""3""  /><TimelineImageXref TimelineImageXrefID=""1000028"" TimelineID=""1000000"" ImageID=""1000004"" DisplayOrder=""4""  /></TimelineImageXrefs><TimelineMusicXrefs><TimelineMusicXref TimelineMusicXrefID=""1000018"" TimelineID=""1000000"" MusicID=""1000000"" PlayOrder=""1""  /><TimelineMusicXref TimelineMusicXrefID=""1000019"" TimelineID=""1000000"" MusicID=""1000001"" PlayOrder=""2""  /><TimelineMusicXref TimelineMusicXrefID=""1000020"" TimelineID=""1000000"" MusicID=""1000002"" PlayOrder=""3""  /></TimelineMusicXrefs><TimelineVideoXrefs><TimelineVideoXref TimelineVideoXrefID=""1000001"" TimelineID=""1000000"" VideoID=""1000000"" DisplayOrder=""5""  /></TimelineVideoXrefs><SlideShows><SlideShow SlideShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""  /><SlideShow SlideShowID=""1000001"" IntervalInSecs=""10"" TransitionType=""Fade""  /></SlideShows><SlideShowImageXrefs><SlideShowImageXref SlideShowImageXrefID=""1000029"" SlideShowID=""1000000"" ImageID=""1000001"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000030"" SlideShowID=""1000000"" ImageID=""1000002"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000031"" SlideShowID=""1000000"" ImageID=""1000003"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000032"" SlideShowID=""1000000"" ImageID=""1000004"" PlayOrder=""4""  /><SlideShowImageXref SlideShowImageXrefID=""1000100"" SlideShowID=""1000001"" ImageID=""1000010"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000101"" SlideShowID=""1000001"" ImageID=""1000011"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000102"" SlideShowID=""1000001"" ImageID=""1000012"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000103"" SlideShowID=""1000001"" ImageID=""1000013"" PlayOrder=""4""  /><SlideShowImageXref SlideShowImageXrefID=""1000104"" SlideShowID=""1000001"" ImageID=""1000014"" PlayOrder=""5""  /><SlideShowImageXref SlideShowImageXrefID=""1000105"" SlideShowID=""1000001"" ImageID=""1000015"" PlayOrder=""6""  /><SlideShowImageXref SlideShowImageXrefID=""1000106"" SlideShowID=""1000001"" ImageID=""1000016"" PlayOrder=""7""  /><SlideShowImageXref SlideShowImageXrefID=""1000107"" SlideShowID=""1000001"" ImageID=""1000018"" PlayOrder=""8""  /><SlideShowImageXref SlideShowImageXrefID=""1000108"" SlideShowID=""1000001"" ImageID=""1000019"" PlayOrder=""9""  /><SlideShowImageXref SlideShowImageXrefID=""1000109"" SlideShowID=""1000001"" ImageID=""1000020"" PlayOrder=""10""  /><SlideShowImageXref SlideShowImageXrefID=""1000110"" SlideShowID=""1000001"" ImageID=""1000021"" PlayOrder=""11""  /><SlideShowImageXref SlideShowImageXrefID=""1000111"" SlideShowID=""1000001"" ImageID=""1000022"" PlayOrder=""12""  /><SlideShowImageXref SlideShowImageXrefID=""1000112"" SlideShowID=""1000001"" ImageID=""1000023"" PlayOrder=""13""  /><SlideShowImageXref SlideShowImageXrefID=""1000113"" SlideShowID=""1000001"" ImageID=""1000024"" PlayOrder=""14""  /><SlideShowImageXref SlideShowImageXrefID=""1000114"" SlideShowID=""1000001"" ImageID=""1000025"" PlayOrder=""15""  /><SlideShowImageXref SlideShowImageXrefID=""1000115"" SlideShowID=""1000001"" ImageID=""1000026"" PlayOrder=""16""  /></SlideShowImageXrefs><SlideShowMusicXrefs></SlideShowMusicXrefs><Images><Image ImageID=""1000000"" StoredFilename=""6f5e187f-52a2-4799-bdac-2e9199580b98.png"" ImageName=""Visit Las Vegas Button""  /><Image ImageID=""1000001"" StoredFilename=""60255096-6a72-409e-b905-4d98ee717bb0.jpg"" ImageName=""Vegas 01""  /><Image ImageID=""1000002"" StoredFilename=""612bb76c-e16e-4fe8-87f2-bddc7eb59300.jpg"" ImageName=""Vegas 02""  /><Image ImageID=""1000003"" StoredFilename=""69f99c47-d1b0-4123-b62b-8f18bdc5702f.jpg"" ImageName=""Vegas 03""  /><Image ImageID=""1000004"" StoredFilename=""626c6a35-4523-46aa-9d0a-c2687b581e27.jpg"" ImageName=""Vegas 04""  /><Image ImageID=""1000010"" StoredFilename=""1.png"" ImageName=""1""  /><Image ImageID=""1000011"" StoredFilename=""2.png"" ImageName=""2""  /><Image ImageID=""1000012"" StoredFilename=""3.png"" ImageName=""3""  /><Image ImageID=""1000013"" StoredFilename=""4.png"" ImageName=""4""  /><Image ImageID=""1000014"" StoredFilename=""5.png"" ImageName=""5""  /><Image ImageID=""1000015"" StoredFilename=""6.png"" ImageName=""6""  /><Image ImageID=""1000016"" StoredFilename=""7.png"" ImageName=""7""  /><Image ImageID=""1000017"" StoredFilename=""8.png"" ImageName=""8""  /><Image ImageID=""1000018"" StoredFilename=""9.png"" ImageName=""9""  /><Image ImageID=""1000019"" StoredFilename=""10.png"" ImageName=""10""  /><Image ImageID=""1000020"" StoredFilename=""bi1.gif"" ImageName=""bi1.gif""  /><Image ImageID=""1000021"" StoredFilename=""bi1.png"" ImageName=""bi1.png""  /><Image ImageID=""1000022"" StoredFilename=""bi2.gif"" ImageName=""bi2.gif""  /><Image ImageID=""1000023"" StoredFilename=""bi2.png"" ImageName=""bi2.png""  /><Image ImageID=""1000024"" StoredFilename=""bi3.gif"" ImageName=""bi3.gif""  /><Image ImageID=""1000025"" StoredFilename=""bi3.png"" ImageName=""bi3.png""  /><Image ImageID=""1000026"" StoredFilename=""bi4.gif"" ImageName=""bi4.gif""  /></Images><PlayLists><PlayList PlayListID=""1000000""  /><PlayList PlayListID=""1000001""  /></PlayLists><PlayListVideoXrefs><PlayListVideoXref PlayListVideoXrefID=""1000000"" PlayListID=""1000000"" VideoID=""1000000"" PlayOrder=""1""  /><PlayListVideoXref PlayListVideoXrefID=""1000001"" PlayListID=""1000001"" VideoID=""1000001"" PlayOrder=""1""  /></PlayListVideoXrefs><Videos><Video VideoID=""1000000"" StoredFilename=""0EBC6160-CA2C-4497-960C-0A2C2DE7B380.mp4"" VideoName=""Countdown Video""  /><Video VideoID=""1000001"" StoredFilename=""Kurumsal Zeka Tanıtım Videosu.wmv"" VideoName=""Omer abi video""  /></Videos><Musics><Music MusicID=""1000000"" StoredFilename=""1B36982F-4101-4D38-AF20-FAD88A0FA9B5.mp3"" MusicName=""Music Example 1""  /><Music MusicID=""1000001"" StoredFilename=""ADA2DBFA-D8D9-49A8-8370-8329A830667E.mp3"" MusicName=""Music Example 2""  /><Music MusicID=""1000002"" StoredFilename=""E4B660F0-ACD3-44F1-92EE-FA23110BE5C6.mp3"" MusicName=""Music Example 3""  /></Musics><PlayerSettings><PlayerSetting PlayerSettingName=""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName=""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName=""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName=""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName=""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>";
                //xml = @"<xml><PlayerGroupSchedules><PlayerGroupSchedule PlayerGroupScheduleID=""1000077"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""0"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000078"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""1"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000079"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""2"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000080"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""3"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000081"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""4"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000082"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""5"" Hour=""0"" Minute=""0""  /><PlayerGroupSchedule PlayerGroupScheduleID=""1000083"" PlayerGroupID=""1000000"" ScreenID=""1000010"" Day=""6"" Hour=""0"" Minute=""0""  /></PlayerGroupSchedules><Screens><Screen ScreenID=""1000008"" AccountID=""1000000"" ScreenName=""Example PlayList"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""1000000"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000101"" AccountID=""1000000"" ScreenName=""Omer abi Video"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""1000001"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000007"" AccountID=""1000000"" ScreenName=""Example Timeline"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""1000000"" IsInteractive=""false"" ButtonImageID=""0""  /><Screen ScreenID=""1000100"" AccountID=""1000000"" ScreenName=""Omer Abi resim"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""1000001"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000001"" AccountID=""1000000"" ScreenName=""Visit Las Vegas"" MultiScreenID=""0"" WebShowID=""0"" SlideShowID=""1000000"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000010"" AccountID=""1000000"" ScreenName=""multi screen"" MultiScreenID=""1000000"" WebShowID=""0"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /><Screen ScreenID=""1000000"" AccountID=""1000000"" ScreenName=""web sites"" MultiScreenID=""0"" WebShowID=""1000000"" SlideShowID=""0"" PlayListID=""0"" TimelineID=""0"" IsInteractive=""false"" ButtonImageID=""1000000""  /></Screens><MultiScreenXrefs><MultiScreenXref MultiScreenXrefID=""1000000"" MultiScreenID=""1000000"" ScreenID=""1000000"" PosX=""0"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000001"" MultiScreenID=""1000000"" ScreenID=""1000100"" PosX=""0"" PosY=""350"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000002"" MultiScreenID=""1000000"" ScreenID=""1000101"" PosX=""650"" PosY=""0"" Height=""350"" Width=""650"" /><MultiScreenXref MultiScreenXrefID=""1000003"" MultiScreenID=""1000000"" ScreenID=""1000007"" PosX=""650"" PosY=""350"" Height=""350"" Width=""650"" /></MultiScreenXrefs><MultiScreens><MultiScreen MultiScreenID=""1000000"" ScreenHeight=""768"" ScreenWidth=""1366""  /></MultiScreens><ScreenScreenContentXrefs><ScreenScreenContentXref ScreenScreenContentXrefID=""1000032"" ScreenID=""1000001"" ScreenContentID=""1000000"" DisplayOrder=""1""	/><ScreenScreenContentXref ScreenScreenContentXrefID=""1000033"" ScreenID=""1000001"" ScreenContentID=""1000001"" DisplayOrder=""2""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000034"" ScreenID=""1000001"" ScreenContentID=""1000002"" DisplayOrder=""3""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000035"" ScreenID=""1000001"" ScreenContentID=""1000003"" DisplayOrder=""4""  /><ScreenScreenContentXref ScreenScreenContentXrefID=""1000036"" ScreenID=""1000001"" ScreenContentID=""1000020"" DisplayOrder=""5""  /></ScreenScreenContentXrefs><ScreenContents><ScreenContent ScreenContentID=""1000000"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 01 Image"" ScreenContentTitle=""Las Vegas Is badly!"" ThumbnailImageID=""1000001"" CustomField1=""1000001"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000001"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 02 Image"" ScreenContentTitle=""Visit Las Vegas!"" ThumbnailImageID=""1000002"" CustomField1=""1000002"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000002"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 03 Image"" ScreenContentTitle=""There&apos;s so much to do in Vegas!"" ThumbnailImageID=""1000003"" CustomField1=""1000003"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000003"" ScreenContentTypeID=""1000000"" ScreenContentTypeName=""Image"" ScreenContentName=""Vegas 04 Image"" ScreenContentTitle=""Good times, day or night!"" ThumbnailImageID=""1000004"" CustomField1=""1000004"" CustomField2="""" CustomField3="""" CustomField4=""""  /><ScreenContent ScreenContentID=""1000020"" ScreenContentTypeID=""1000004"" ScreenContentTypeName=""Web Site"" ScreenContentName=""website"" ScreenContentTitle=""web"" ThumbnailImageID=""1000000"" CustomField1=""https://www.google.com/"" CustomField2="""" CustomField3="""" CustomField4=""""  /></ScreenContents><Surveys></Surveys><SurveyQuestions></SurveyQuestions><SurveyQuestionOptions></SurveyQuestionOptions><WebShows><WebShow WebShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""   /></WebShows><WebShowSources><WebShowSource WebShowUrlID=""1000000"" WebShowID=""1000000"" Name=""google"" Source=""www.facebook.com.tr"" PlayOrder=""1""  /><WebShowSource WebShowUrlID=""1000001"" WebShowID=""1000000"" Name=""google"" Source=""www.google.com"" PlayOrder=""2""  /></WebShowSources><Timelines><Timeline TimelineID=""1000000"" DurationInSecs=""5"" MuteMusicOnPlayback=""true""  /></Timelines><TimelineImageXrefs><TimelineImageXref TimelineImageXrefID=""1000025"" TimelineID=""1000000"" ImageID=""1000001"" DisplayOrder=""1""  /><TimelineImageXref TimelineImageXrefID=""1000026"" TimelineID=""1000000"" ImageID=""1000002"" DisplayOrder=""2""  /><TimelineImageXref TimelineImageXrefID=""1000027"" TimelineID=""1000000"" ImageID=""1000003"" DisplayOrder=""3""  /><TimelineImageXref TimelineImageXrefID=""1000028"" TimelineID=""1000000"" ImageID=""1000004"" DisplayOrder=""4""  /></TimelineImageXrefs><TimelineMusicXrefs><TimelineMusicXref TimelineMusicXrefID=""1000018"" TimelineID=""1000000"" MusicID=""1000000"" PlayOrder=""1""  /><TimelineMusicXref TimelineMusicXrefID=""1000019"" TimelineID=""1000000"" MusicID=""1000001"" PlayOrder=""2""  /><TimelineMusicXref TimelineMusicXrefID=""1000020"" TimelineID=""1000000"" MusicID=""1000002"" PlayOrder=""3""  /></TimelineMusicXrefs><TimelineVideoXrefs><TimelineVideoXref TimelineVideoXrefID=""1000001"" TimelineID=""1000000"" VideoID=""1000000"" DisplayOrder=""5""  /></TimelineVideoXrefs><SlideShows><SlideShow SlideShowID=""1000000"" IntervalInSecs=""10"" TransitionType=""Fade""  /><SlideShow SlideShowID=""1000001"" IntervalInSecs=""10"" TransitionType=""Fade""  /></SlideShows><SlideShowImageXrefs><SlideShowImageXref SlideShowImageXrefID=""1000029"" SlideShowID=""1000000"" ImageID=""1000001"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000030"" SlideShowID=""1000000"" ImageID=""1000002"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000031"" SlideShowID=""1000000"" ImageID=""1000003"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000032"" SlideShowID=""1000000"" ImageID=""1000004"" PlayOrder=""4""  /><SlideShowImageXref SlideShowImageXrefID=""1000100"" SlideShowID=""1000001"" ImageID=""1000010"" PlayOrder=""1""  /><SlideShowImageXref SlideShowImageXrefID=""1000101"" SlideShowID=""1000001"" ImageID=""1000011"" PlayOrder=""2""  /><SlideShowImageXref SlideShowImageXrefID=""1000102"" SlideShowID=""1000001"" ImageID=""1000012"" PlayOrder=""3""  /><SlideShowImageXref SlideShowImageXrefID=""1000103"" SlideShowID=""1000001"" ImageID=""1000013"" PlayOrder=""4""  /><SlideShowImageXref SlideShowImageXrefID=""1000104"" SlideShowID=""1000001"" ImageID=""1000014"" PlayOrder=""5""  /><SlideShowImageXref SlideShowImageXrefID=""1000105"" SlideShowID=""1000001"" ImageID=""1000015"" PlayOrder=""6""  /><SlideShowImageXref SlideShowImageXrefID=""1000106"" SlideShowID=""1000001"" ImageID=""1000016"" PlayOrder=""7""  /><SlideShowImageXref SlideShowImageXrefID=""1000107"" SlideShowID=""1000001"" ImageID=""1000018"" PlayOrder=""8""  /><SlideShowImageXref SlideShowImageXrefID=""1000108"" SlideShowID=""1000001"" ImageID=""1000019"" PlayOrder=""9""  /><SlideShowImageXref SlideShowImageXrefID=""1000109"" SlideShowID=""1000001"" ImageID=""1000020"" PlayOrder=""10""  /><SlideShowImageXref SlideShowImageXrefID=""1000110"" SlideShowID=""1000001"" ImageID=""1000021"" PlayOrder=""11""  /><SlideShowImageXref SlideShowImageXrefID=""1000111"" SlideShowID=""1000001"" ImageID=""1000022"" PlayOrder=""12""  /><SlideShowImageXref SlideShowImageXrefID=""1000112"" SlideShowID=""1000001"" ImageID=""1000023"" PlayOrder=""13""  /><SlideShowImageXref SlideShowImageXrefID=""1000113"" SlideShowID=""1000001"" ImageID=""1000024"" PlayOrder=""14""  /><SlideShowImageXref SlideShowImageXrefID=""1000114"" SlideShowID=""1000001"" ImageID=""1000025"" PlayOrder=""15""  /><SlideShowImageXref SlideShowImageXrefID=""1000115"" SlideShowID=""1000001"" ImageID=""1000026"" PlayOrder=""16""  /></SlideShowImageXrefs><SlideShowMusicXrefs></SlideShowMusicXrefs><Images><Image ImageID=""1000000"" StoredFilename=""6f5e187f-52a2-4799-bdac-2e9199580b98.png"" ImageName=""Visit Las Vegas Button""  /><Image ImageID=""1000001"" StoredFilename=""60255096-6a72-409e-b905-4d98ee717bb0.jpg"" ImageName=""Vegas 01""  /><Image ImageID=""1000002"" StoredFilename=""612bb76c-e16e-4fe8-87f2-bddc7eb59300.jpg"" ImageName=""Vegas 02""  /><Image ImageID=""1000003"" StoredFilename=""69f99c47-d1b0-4123-b62b-8f18bdc5702f.jpg"" ImageName=""Vegas 03""  /><Image ImageID=""1000004"" StoredFilename=""626c6a35-4523-46aa-9d0a-c2687b581e27.jpg"" ImageName=""Vegas 04""  /><Image ImageID=""1000010"" StoredFilename=""1.png"" ImageName=""1""  /><Image ImageID=""1000011"" StoredFilename=""2.png"" ImageName=""2""  /><Image ImageID=""1000012"" StoredFilename=""3.png"" ImageName=""3""  /><Image ImageID=""1000013"" StoredFilename=""4.png"" ImageName=""4""  /><Image ImageID=""1000014"" StoredFilename=""5.png"" ImageName=""5""  /><Image ImageID=""1000015"" StoredFilename=""6.png"" ImageName=""6""  /><Image ImageID=""1000016"" StoredFilename=""7.png"" ImageName=""7""  /><Image ImageID=""1000017"" StoredFilename=""8.png"" ImageName=""8""  /><Image ImageID=""1000018"" StoredFilename=""9.png"" ImageName=""9""  /><Image ImageID=""1000019"" StoredFilename=""10.png"" ImageName=""10""  /><Image ImageID=""1000020"" StoredFilename=""bi1.gif"" ImageName=""bi1.gif""  /><Image ImageID=""1000021"" StoredFilename=""bi1.png"" ImageName=""bi1.png""  /><Image ImageID=""1000022"" StoredFilename=""bi2.gif"" ImageName=""bi2.gif""  /><Image ImageID=""1000023"" StoredFilename=""bi2.png"" ImageName=""bi2.png""  /><Image ImageID=""1000024"" StoredFilename=""bi3.gif"" ImageName=""bi3.gif""  /><Image ImageID=""1000025"" StoredFilename=""bi3.png"" ImageName=""bi3.png""  /><Image ImageID=""1000026"" StoredFilename=""bi4.gif"" ImageName=""bi4.gif""  /></Images><PlayLists><PlayList PlayListID=""1000000""  /><PlayList PlayListID=""1000001""  /></PlayLists><PlayListVideoXrefs><PlayListVideoXref PlayListVideoXrefID=""1000000"" PlayListID=""1000000"" VideoID=""1000000"" PlayOrder=""1""  /><PlayListVideoXref PlayListVideoXrefID=""1000001"" PlayListID=""1000001"" VideoID=""1000001"" PlayOrder=""1""  /></PlayListVideoXrefs><Videos><Video VideoID=""1000000"" StoredFilename=""0EBC6160-CA2C-4497-960C-0A2C2DE7B380.mp4"" VideoName=""Countdown Video""  /><Video VideoID=""1000001"" StoredFilename=""Kurumsal Zeka Tanıtım Videosu.wmv"" VideoName=""Omer abi video""  /></Videos><Musics><Music MusicID=""1000000"" StoredFilename=""1B36982F-4101-4D38-AF20-FAD88A0FA9B5.mp3"" MusicName=""Music Example 1""  /><Music MusicID=""1000001"" StoredFilename=""ADA2DBFA-D8D9-49A8-8370-8329A830667E.mp3"" MusicName=""Music Example 2""  /><Music MusicID=""1000002"" StoredFilename=""E4B660F0-ACD3-44F1-92EE-FA23110BE5C6.mp3"" MusicName=""Music Example 3""  /></Musics><PlayerSettings><PlayerSetting PlayerSettingName=""ButtonTextBack"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Back""  /><PlayerSetting PlayerSettingName=""ButtonTextClose"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Close""  /><PlayerSetting PlayerSettingName=""ButtonTextNext"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Next""  /><PlayerSetting PlayerSettingName=""ButtonTextOpen"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""Open""  /><PlayerSetting PlayerSettingName=""DownloadFolder"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""C:\osVodigi\""  /><PlayerSetting PlayerSettingName=""MediaSourceUrl"" PlayerSettingTypeID=""1000001"" PlayerSettingValue=""http://localhost:52713/Media/""  /><PlayerSetting PlayerSettingName=""ShowCursor"" PlayerSettingTypeID=""1000003"" PlayerSettingValue=""True""  /></PlayerSettings></xml>";

                // Get the PlayerGroupSchedule(s)
                try
                {
                    List<PlayerGroupSchedule> pgs = new List<PlayerGroupSchedule>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    pgs = (from PlayerGroupSchedule in xmldoc.Descendants("PlayerGroupSchedule")
                           select new PlayerGroupSchedule
                           {
                               PlayerGroupScheduleID = Convert.ToInt32(PlayerGroupSchedule.Attribute("PlayerGroupScheduleID").Value),
                               PlayerGroupID = Convert.ToInt32(PlayerGroupSchedule.Attribute("PlayerGroupID").Value),
                               ScreenID = Convert.ToInt32(PlayerGroupSchedule.Attribute("ScreenID").Value),
                               Day = Convert.ToInt32(PlayerGroupSchedule.Attribute("Day").Value),
                               Hour = Convert.ToInt32(PlayerGroupSchedule.Attribute("Hour").Value),
                               Minute = Convert.ToInt32(PlayerGroupSchedule.Attribute("Minute").Value),
                           }
                    ).ToList();
                   
                    PlayerGroupSchedules = pgs;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 1 ---------------------"); }

                // Parse out the Screens
                try
                {
                    List<Screen> ss = new List<Screen>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    ss = (from Screen in xmldoc.Descendants("Screen")
                          select new Screen
                          {
                              ScreenID = Convert.ToInt32(Screen.Attribute("ScreenID").Value),
                              AccountID = Convert.ToInt32(Screen.Attribute("AccountID").Value),
                              ScreenName = Utility.DecodeXMLString(Convert.ToString(Screen.Attribute("ScreenName").Value)),
                              PlayListID = Convert.ToInt32(Screen.Attribute("PlayListID").Value),
                              WebShowID= Convert.ToInt32(Screen.Attribute("WebShowID").Value),          //added
                              MultiScreenID = Convert.ToInt32(Screen.Attribute("MultiScreenID").Value),          //added
                              SlideShowID = Convert.ToInt32(Screen.Attribute("SlideShowID").Value),
                              TimelineID = Convert.ToInt32(Screen.Attribute("TimelineID").Value),
                              ButtonImageID = Convert.ToInt32(Screen.Attribute("ButtonImageID").Value),
                              IsInteractive = Convert.ToBoolean(Screen.Attribute("IsInteractive").Value),
                          }
                    ).ToList(); 

                     Screens = ss;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 2 ---------------------"); }

                // Parse out the ScreenScreenContentXrefs
                try
                {
                    List<ScreenScreenContentXref> sscxrefs = new List<ScreenScreenContentXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    sscxrefs = (from ScreenScreenContentXref in xmldoc.Descendants("ScreenScreenContentXref")
                                select new ScreenScreenContentXref
                                {
                                    ScreenScreenContentXrefID = Convert.ToInt32(ScreenScreenContentXref.Attribute("ScreenScreenContentXrefID").Value),
                                    ScreenID = Convert.ToInt32(ScreenScreenContentXref.Attribute("ScreenID").Value),
                                    ScreenContentID = Convert.ToInt32(ScreenScreenContentXref.Attribute("ScreenContentID").Value),
                                    DisplayOrder = Convert.ToInt32(ScreenScreenContentXref.Attribute("ScreenID").Value),
                                }
                    ).ToList();

                    ScreenScreenContentXrefs = sscxrefs;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 3 ---------------------"); }

                // Parse out the ScreenContents
                try
                {
                    List<ScreenContent> scs = new List<ScreenContent>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    scs = (from ScreenContent in xmldoc.Descendants("ScreenContent")
                           select new ScreenContent
                           {
                               ScreenContentID = Convert.ToInt32(ScreenContent.Attribute("ScreenContentID").Value),
                               ScreenContentTypeID = Convert.ToInt32(ScreenContent.Attribute("ScreenContentTypeID").Value),
                               ScreenContentTypeName = Utility.DecodeXMLString(Convert.ToString(ScreenContent.Attribute("ScreenContentTypeName").Value)),
                               ScreenContentName = Utility.DecodeXMLString(Convert.ToString(ScreenContent.Attribute("ScreenContentName").Value)),
                               ScreenContentTitle = Convert.ToString(ScreenContent.Attribute("ScreenContentTitle").Value),
                               ThumbnailImageID = Convert.ToInt32(ScreenContent.Attribute("ThumbnailImageID").Value),
                               CustomField1 = Utility.DecodeXMLString(Convert.ToString(ScreenContent.Attribute("CustomField1").Value)),
                               CustomField2 = Utility.DecodeXMLString(Convert.ToString(ScreenContent.Attribute("CustomField2").Value)),
                               CustomField3 = Utility.DecodeXMLString(Convert.ToString(ScreenContent.Attribute("CustomField3").Value)),
                               CustomField4 = Utility.DecodeXMLString(Convert.ToString(ScreenContent.Attribute("CustomField4").Value)),
                           }
                    ).ToList();

                    ScreenContents = scs;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 4 ---------------------"); }

                // Parse out the SlideShows
                try
                {
                    List<SlideShow> sss = new List<SlideShow>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    sss = (from SlideShow in xmldoc.Descendants("SlideShow")
                           select new SlideShow
                           {
                               SlideShowID = Convert.ToInt32(SlideShow.Attribute("SlideShowID").Value),
                               IntervalInSecs = Convert.ToInt32(SlideShow.Attribute("IntervalInSecs").Value),
                               TransitionType = Utility.DecodeXMLString(Convert.ToString(SlideShow.Attribute("TransitionType").Value)),
                           }
                    ).ToList();

                    SlideShows = sss;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 5 ---------------------"); }

                // Parse out the WebShows
                try //added
                { 
                    List<WebShow> ws = new List<WebShow>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    ws = (from WebShow in xmldoc.Descendants("WebShow")
                           select new WebShow
                           {
                               WebShowID = Convert.ToInt32(WebShow.Attribute("WebShowID").Value),
                               IntervalInSecs = Convert.ToInt32(WebShow.Attribute("IntervalInSecs").Value),
                               TransitionType = Utility.DecodeXMLString(Convert.ToString(WebShow.Attribute("TransitionType").Value))
                           }
                    ).ToList();
                    
                    WebShows = ws;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 6 ---------------------"); }

                // Parse out the WebShowUrlAddressXRefs
                try //added
                {
                    List<WebShowURLAddressXRef> wss = new List<WebShowURLAddressXRef>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    wss = (from WebShowUrlAddressXRef in xmldoc.Descendants("WebShowURLAddressXRef")
                          select new WebShowURLAddressXRef
                          {
                              WebShowID = Convert.ToInt32(WebShowUrlAddressXRef.Attribute("WebShowID").Value),
                              PlayOrder = Convert.ToInt32(WebShowUrlAddressXRef.Attribute("PlayOrder").Value),
                              URLAddressID= Convert.ToInt32(WebShowUrlAddressXRef.Attribute("URLAddressID").Value),
                              WebShowURLAddressXRefID =   Convert.ToInt32(WebShowUrlAddressXRef.Attribute("WebShowURLAddressXRefID").Value)
                          }
                    ).ToList();
                    WebShowUrlAddressXRefs = wss;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 7 ---------------------"); }

                // Parse out the URLAddresses
                try //added
                {
                    List<URLAddress> urlAddress = new List<URLAddress>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    urlAddress = (from URLAddress in xmldoc.Descendants("URLAddress")
                           select new URLAddress
                           {
                               URLAddressID = Convert.ToInt32(URLAddress.Attribute("URLAddressID").Value),
                               URLAddressSource = Utility.DecodeXMLString(Convert.ToString(URLAddress.Attribute("URLAddressSource").Value)),
                               Zoom = Convert.ToInt32(URLAddress.Attribute("Zoom").Value),
                               URLAddressName = Utility.DecodeXMLString(Convert.ToString(URLAddress.Attribute("URLAddressName").Value)),
                               AccountID= Convert.ToInt32(URLAddress.Attribute("AccountID").Value),
                               IsActive= Convert.ToBoolean(URLAddress.Attribute("IsActive").Value),
                               Tags= Utility.DecodeXMLString(Convert.ToString(URLAddress.Attribute("Tags").Value))
                           }
                    ).ToList();
                    URLAddresses = urlAddress;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 30 ---------------------"); }

                // Parse out the MultiScreens
                try //added
                {

                    List<MultiScreen> ms = new List<MultiScreen>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    ms = (from MultiScreen in xmldoc.Descendants("MultiScreen")
                          select new MultiScreen
                          {
                              MultiScreenID = Convert.ToInt32(MultiScreen.Attribute("MultiScreenID").Value),
                              ScreenHeight = Convert.ToDouble(MultiScreen.Attribute("ScreenHeight").Value),
                              ScreenWidth = Convert.ToDouble(MultiScreen.Attribute("ScreenWidth").Value),
                          }
                    ).ToList();
                    
                    MultiScreens = ms;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 8 ---------------------"); }

                // Parse out the MultiScreenXrefs
                try //added
                {
                    List<MultiScreenXref> msxref = new List<MultiScreenXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    msxref = (from multiScreenXref in xmldoc.Descendants("MultiScreenXref")
                           select new MultiScreenXref
                           {
                               MultiScreenXrefID = Convert.ToInt32(multiScreenXref.Attribute("MultiScreenXrefID").Value),
                               ScreenID = Convert.ToInt32(multiScreenXref.Attribute("ScreenID").Value),
                               MultiScreenID = Convert.ToInt32(multiScreenXref.Attribute("MultiScreenID").Value),
                               Height = Convert.ToDouble(multiScreenXref.Attribute("Height").Value),
                               Width = Convert.ToDouble(multiScreenXref.Attribute("Width").Value),
                               PosX = Convert.ToDouble(multiScreenXref.Attribute("PosX").Value),
                               PosY = Convert.ToDouble(multiScreenXref.Attribute("PosY").Value) 
                           }
                    ).ToList();
                    MultiScreenXrefs = msxref;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 9 ---------------------"); }

                // Parse out the SlideShowImageXrefs
                try
                {
                    List<SlideShowImageXref> ssis = new List<SlideShowImageXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    ssis = (from SlideShowImageXref in xmldoc.Descendants("SlideShowImageXref")
                            select new SlideShowImageXref
                            {
                                SlideShowImageXrefID = Convert.ToInt32(SlideShowImageXref.Attribute("SlideShowImageXrefID").Value),
                                SlideShowID = Convert.ToInt32(SlideShowImageXref.Attribute("SlideShowID").Value),
                                ImageID = Convert.ToInt32(SlideShowImageXref.Attribute("ImageID").Value),
                                PlayOrder = Convert.ToInt32(SlideShowImageXref.Attribute("PlayOrder").Value),
                            }
                    ).ToList();

                    SlideShowImageXrefs = ssis;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 10 ---------------------"); }

                // Parse out the SlideShowMusicXrefs
                try
                {
                    List<SlideShowMusicXref> ssms = new List<SlideShowMusicXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    ssms = (from SlideShowMusicXref in xmldoc.Descendants("SlideShowMusicXref")
                            select new SlideShowMusicXref
                            {
                                SlideShowMusicXrefID = Convert.ToInt32(SlideShowMusicXref.Attribute("SlideShowMusicXrefID").Value),
                                SlideShowID = Convert.ToInt32(SlideShowMusicXref.Attribute("SlideShowID").Value),
                                MusicID = Convert.ToInt32(SlideShowMusicXref.Attribute("MusicID").Value),
                                PlayOrder = Convert.ToInt32(SlideShowMusicXref.Attribute("PlayOrder").Value),
                            }
                    ).ToList();

                    SlideShowMusicXrefs = ssms;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 11 ---------------------"); }

                // Parse out the Timelines
                try
                {
                    List<Timeline> tls = new List<Timeline>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    tls = (from Timeline in xmldoc.Descendants("Timeline")
                           select new Timeline
                           {
                               TimelineID = Convert.ToInt32(Timeline.Attribute("TimelineID").Value),
                               DurationInSecs = Convert.ToInt32(Timeline.Attribute("DurationInSecs").Value),
                               MuteMusicOnPlayback = Convert.ToBoolean(Timeline.Attribute("MuteMusicOnPlayback").Value),
                           }
                    ).ToList();

                    Timelines = tls;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 12 ---------------------"); }

                // Parse out the TimelineImageXrefs
                try
                {
                    List<TimelineImageXref> tlis = new List<TimelineImageXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    tlis = (from TimelineImageXref in xmldoc.Descendants("TimelineImageXref")
                            select new TimelineImageXref
                            {
                                TimelineImageXrefID = Convert.ToInt32(TimelineImageXref.Attribute("TimelineImageXrefID").Value),
                                TimelineID = Convert.ToInt32(TimelineImageXref.Attribute("TimelineID").Value),
                                ImageID = Convert.ToInt32(TimelineImageXref.Attribute("ImageID").Value),
                                DisplayOrder = Convert.ToInt32(TimelineImageXref.Attribute("DisplayOrder").Value),
                            }
                    ).ToList();

                    TimelineImageXrefs = tlis;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 13 ---------------------"); }

                // Parse out the TimelineMusicXrefs
                try
                {
                    List<TimelineMusicXref> tlms = new List<TimelineMusicXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    tlms = (from TimelineMusicXref in xmldoc.Descendants("TimelineMusicXref")
                            select new TimelineMusicXref
                            {
                                TimelineMusicXrefID = Convert.ToInt32(TimelineMusicXref.Attribute("TimelineMusicXrefID").Value),
                                TimelineID = Convert.ToInt32(TimelineMusicXref.Attribute("TimelineID").Value),
                                MusicID = Convert.ToInt32(TimelineMusicXref.Attribute("MusicID").Value),
                                PlayOrder = Convert.ToInt32(TimelineMusicXref.Attribute("PlayOrder").Value),
                            }
                    ).ToList();

                    TimelineMusicXrefs = tlms;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 14 ---------------------"); }

                // Parse out the TimelineVideoXrefs
                try
                {
                    List<TimelineVideoXref> tlvs = new List<TimelineVideoXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    tlvs = (from TimelineVideoXref in xmldoc.Descendants("TimelineVideoXref")
                            select new TimelineVideoXref
                            {
                                TimelineVideoXrefID = Convert.ToInt32(TimelineVideoXref.Attribute("TimelineVideoXrefID").Value),
                                TimelineID = Convert.ToInt32(TimelineVideoXref.Attribute("TimelineID").Value),
                                VideoID = Convert.ToInt32(TimelineVideoXref.Attribute("VideoID").Value),
                                DisplayOrder = Convert.ToInt32(TimelineVideoXref.Attribute("DisplayOrder").Value),
                            }
                    ).ToList();

                    TimelineVideoXrefs = tlvs;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 15 ---------------------"); }

                // Parse out the Images
                try
                {
                    List<Image> images = new List<Image>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    images = (from Image in xmldoc.Descendants("Image")
                              select new Image
                              {
                                  ImageID = Convert.ToInt32(Image.Attribute("ImageID").Value),
                                  StoredFilename = Convert.ToString(Image.Attribute("StoredFilename").Value),
                                  ImageName = Utility.DecodeXMLString(Image.Attribute("ImageName").Value),
                              }
                    ).ToList();

                    Images = images;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 16 ---------------------"); }

                // Parse out the Musics
                try
                {
                    List<Music> musics = new List<Music>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    musics = (from Music in xmldoc.Descendants("Music")
                              select new Music
                              {
                                  MusicID = Convert.ToInt32(Music.Attribute("MusicID").Value),
                                  StoredFilename = Convert.ToString(Music.Attribute("StoredFilename").Value),
                                  MusicName = Utility.DecodeXMLString(Music.Attribute("MusicName").Value),
                              }
                    ).ToList();

                    Musics = musics;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 17 ---------------------"); }

                // Parse out the PlayLists
                try
                {
                    List<PlayList> pls = new List<PlayList>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    pls = (from PlayList in xmldoc.Descendants("PlayList")
                           select new PlayList
                           {
                               PlayListID = Convert.ToInt32(PlayList.Attribute("PlayListID").Value),
                           }
                    ).ToList();

                    PlayLists = pls;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 18 ---------------------"); }

                // Parse out the PlayListVideoXrefs
                try
                {
                    List<PlayListVideoXref> plvs = new List<PlayListVideoXref>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    plvs = (from PlayListVideoXref in xmldoc.Descendants("PlayListVideoXref")
                            select new PlayListVideoXref
                            {
                                PlayListVideoXrefID = Convert.ToInt32(PlayListVideoXref.Attribute("PlayListVideoXrefID").Value),
                                PlayListID = Convert.ToInt32(PlayListVideoXref.Attribute("PlayListID").Value),
                                VideoID = Convert.ToInt32(PlayListVideoXref.Attribute("VideoID").Value),
                                PlayOrder = Convert.ToInt32(PlayListVideoXref.Attribute("PlayOrder").Value),
                            }
                    ).ToList();

                    PlayListVideoXrefs = plvs;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 19 ---------------------"); }

                // Parse out the Videos
                try
                {
                    List<Video> videos = new List<Video>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    videos = (from Video in xmldoc.Descendants("Video")
                              select new Video
                              {
                                  VideoID = Convert.ToInt32(Video.Attribute("VideoID").Value),
                                  StoredFilename = Convert.ToString(Video.Attribute("StoredFilename").Value),
                                  VideoName = Utility.DecodeXMLString(Convert.ToString(Video.Attribute("VideoName").Value)),
                              }
                    ).ToList();

                    Videos = videos;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 20 ---------------------"); }

                // Parse out the Surveys
                try
                {
                    List<Survey> surveys = new List<Survey>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    surveys = (from Survey in xmldoc.Descendants("Survey")
                               select new Survey
                               {
                                   SurveyID = Convert.ToInt32(Survey.Attribute("SurveyID").Value),
                                   SurveyName = Utility.DecodeXMLString(Convert.ToString(Survey.Attribute("SurveyName").Value)),
                                   SurveyImageID = Convert.ToInt32(Survey.Attribute("SurveyImageID").Value),
                               }
                    ).ToList();

                    Surveys = surveys;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 21 ---------------------"); }

                // Parse out the SurveyQuestions
                try
                {
                    List<SurveyQuestion> questions = new List<SurveyQuestion>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    questions = (from SurveyQuestion in xmldoc.Descendants("SurveyQuestion")
                                 select new SurveyQuestion
                                 {
                                     SurveyQuestionID = Convert.ToInt32(SurveyQuestion.Attribute("SurveyQuestionID").Value),
                                     SurveyID = Convert.ToInt32(SurveyQuestion.Attribute("SurveyID").Value),
                                     SurveyQuestionText = Utility.DecodeXMLString(Convert.ToString(SurveyQuestion.Attribute("SurveyQuestionText").Value)),
                                     AllowMultiselect = Convert.ToBoolean(SurveyQuestion.Attribute("AllowMultiselect").Value),
                                     SortOrder = Convert.ToInt32(SurveyQuestion.Attribute("SortOrder").Value),
                                 }
                    ).ToList();

                    SurveyQuestions = questions;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 22 ---------------------"); }

                // Parse out the SurveyQuestionOptions
                try
                {
                    List<SurveyQuestionOption> options = new List<SurveyQuestionOption>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    options = (from SurveyQuestionOption in xmldoc.Descendants("SurveyQuestionOption")
                               select new SurveyQuestionOption
                               {

                                   SurveyQuestionOptionID = Convert.ToInt32(SurveyQuestionOption.Attribute("SurveyQuestionOptionID").Value),
                                   SurveyQuestionID = Convert.ToInt32(SurveyQuestionOption.Attribute("SurveyQuestionID").Value),
                                   SurveyQuestionOptionText = Utility.DecodeXMLString(Convert.ToString(SurveyQuestionOption.Attribute("SurveyQuestionOptionText").Value)),
                                   SortOrder = Convert.ToInt32(SurveyQuestionOption.Attribute("SortOrder").Value),
                               }
                    ).ToList();

                    SurveyQuestionOptions = options;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 23 ---------------------"); }

                // Parse out the PlayerSettings
                try
                {
                    List<PlayerSetting> settings = new List<PlayerSetting>();
                    XDocument xmldoc = XDocument.Parse(xml);
                    settings = (from PlayerSetting in xmldoc.Descendants("PlayerSetting")
                               select new PlayerSetting
                               {
                                   PlayerSettingName = Utility.DecodeXMLString(Convert.ToString(PlayerSetting.Attribute("PlayerSettingName").Value)),
                                   PlayerSettingTypeID = Convert.ToInt32(PlayerSetting.Attribute("PlayerSettingTypeID").Value),
                                   PlayerSettingValue = Utility.DecodeXMLString(Convert.ToString(PlayerSetting.Attribute("PlayerSettingValue").Value)),
                               }
                    ).ToList();

                    PlayerSettings = settings;
                    osVodigiPlayer.Helpers.PlayerSettings.AllPlayerSettings = settings;
                }
                catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 24 ---------------------"); }

            }

            catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI 25 ---------------------"); }
        }

        public static void ClearSchedule()
        {
            try
            {
                // Clear the global data used to store schedule data
                PlayerGroupSchedules = new List<PlayerGroupSchedule>();
                Screens = new List<Screen>();
                PlayListVideoXrefs = new List<PlayListVideoXref>();
                SlideShowImageXrefs = new List<SlideShowImageXref>();
                ScreenScreenContentXrefs = new List<ScreenScreenContentXref>();
                ScreenContents = new List<ScreenContent>();
                Images = new List<Image>();
                Videos = new List<Video>();
                Timelines = new List<Timeline>();
                TimelineImageXrefs = new List<TimelineImageXref>();
                TimelineVideoXrefs = new List<TimelineVideoXref>();
                TimelineMusicXrefs = new List<TimelineMusicXref>();
                Surveys = new List<Survey>();
                SurveyQuestions = new List<SurveyQuestion>();
                SurveyQuestionOptions = new List<SurveyQuestionOption>();
            }
            catch { System.Diagnostics.Debug.WriteLine("----------------------- CurrentSchedule HATASI ---------------------"); }
        }
    }
}
