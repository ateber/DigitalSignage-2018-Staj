using osVodigiWeb6x.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace osVodigiWeb6x.Controllers
{
    public class WebShowController : Controller
    {


        IWebShowRepository repository;  
        string selectedURLAddress = String.Empty;

        public WebShowController()
            : this(new EntityWebShowRepository())
        { }

        public WebShowController(IWebShowRepository paramrepository)
        {
            repository = paramrepository;
        } 

        //
        // GET: /WebShow/

        public ActionResult Index()
        {
            try
            {
                if (Session["UserAccountID"] == null)
                    return RedirectToAction("Validate", "Login");
                User user = (User)Session["User"];
                ViewData["LoginInfo"] = Utility.BuildUserAccountString(user.Username, Convert.ToString(Session["UserAccountName"]));
                if (user.IsAdmin)
                    ViewData["txtIsAdmin"] = "true";
                else
                    ViewData["txtIsAdmin"] = "false";

                // Initialize or get the page state using session
                WebShowPageState pagestate = GetPageState();

                // Get the account id
                int accountid = 0;
                if (Session["UserAccountID"] != null)
                    accountid = Convert.ToInt32(Session["UserAccountID"]);

                // Set and save the page state to the submitted form values if any values are passed
                if (Request.Form["lstAscDesc"] != null)
                {
                    pagestate.AccountID = accountid;
                    pagestate.WebShowName = Request.Form["txtWebShowName"].ToString().Trim(); //  txtWebShowName
                    pagestate.Tag = Request.Form["txtTag"].ToString().Trim();
                    if (Request.Form["chkIncludeInactive"].ToLower().StartsWith("true"))
                        pagestate.IncludeInactive = true;
                    else
                        pagestate.IncludeInactive = false;
                    pagestate.SortBy = Request.Form["lstSortBy"].ToString().Trim();
                    pagestate.AscDesc = Request.Form["lstAscDesc"].ToString().Trim();
                    pagestate.PageNumber = Convert.ToInt32(Request.Form["txtPageNumber"].ToString().Trim());
                    SavePageState(pagestate);
                }

                // Add the session values to the view data so they can be populated in the form
                ViewData["AccountID"] = pagestate.AccountID;
                ViewData["WebShowName"] = pagestate.WebShowName;
                ViewData["Tag"] = pagestate.Tag;
                ViewData["IncludeInactive"] = pagestate.IncludeInactive;
                ViewData["SortBy"] = pagestate.SortBy;
                ViewData["SortByList"] = new SelectList(BuildSortByList(), "Value", "Text", pagestate.SortBy);
                ViewData["AscDescList"] = new SelectList(BuildAscDescList(), "Value", "Text", pagestate.AscDesc);

                // Determine asc/desc
                bool isdescending = false;
                if (pagestate.AscDesc.ToLower().StartsWith("d"))
                    isdescending = true;

                // Get a Count of all filtered records
                int recordcount = repository.GetWebShowRecordCount(pagestate.AccountID, pagestate.WebShowName, pagestate.Tag, pagestate.IncludeInactive);

                // Determine the page count
                int pagecount = 1;
                if (recordcount > 0)
                {
                    pagecount = recordcount / Constants.PageSize;
                    if (recordcount % Constants.PageSize != 0) // Add a page if there are more records
                    {
                        pagecount = pagecount + 1;
                    }
                }

                // Make sure the current page is not greater than the page count
                if (pagestate.PageNumber > pagecount)
                {
                    pagestate.PageNumber = pagecount;
                    SavePageState(pagestate);
                }

                // Set the page number and account in viewdata
                ViewData["PageNumber"] = Convert.ToString(pagestate.PageNumber);
                ViewData["PageCount"] = Convert.ToString(pagecount);
                ViewData["RecordCount"] = Convert.ToString(recordcount);

                ViewResult result = View(repository.GetWebShowPage(pagestate.AccountID, pagestate.WebShowName, pagestate.Tag, pagestate.IncludeInactive, pagestate.SortBy, isdescending, pagestate.PageNumber, pagecount));
                result.ViewName = "Index";
                return result;
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("WebShow", "Index", ex.Message);
                return RedirectToAction("Index", "ApplicationError"); 
            }
             
        }

        public ActionResult Create()
        {
            try
            {
                if (Session["UserAccountID"] == null)
                    return RedirectToAction("Validate", "Login");
                User user = (User)Session["User"];
                ViewData["LoginInfo"] = Utility.BuildUserAccountString(user.Username, Convert.ToString(Session["UserAccountName"]));
                if (user.IsAdmin)
                    ViewData["txtIsAdmin"] = "true";
                else
                    ViewData["txtIsAdmin"] = "false";

                ViewData["ValidationMessage"] = String.Empty; 
                ViewData["URLAddressList"] =  new SelectList(BuildURLAddressList(), "Value", "Text", "");  
                ViewData["WebShowURLAddresses"] = String.Empty; 
                ViewData["WebShowURLAddressList"] = new SelectList(BuildWebShowURLAddressList(""), "Value", "Text", ""); 
                // Get the account id
                int accountid = 0;
                if (Session["UserAccountID"] != null)
                    accountid = Convert.ToInt32(Session["UserAccountID"]);
 
                return View(CreateNewWebShow());
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("PlayList", "Create", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        [HttpPost]
        public ActionResult Create(WebShow webshow)
        {
            try
            {
                if (Session["UserAccountID"] == null)
                    return RedirectToAction("Validate", "Login");
                User user = (User)Session["User"];
                ViewData["LoginInfo"] = Utility.BuildUserAccountString(user.Username, Convert.ToString(Session["UserAccountName"]));
                if (user.IsAdmin)
                    ViewData["txtIsAdmin"] = "true";
                else
                    ViewData["txtIsAdmin"] = "false";

                if (ModelState.IsValid)
                {
                    // Set NULLs to Empty Strings
                    webshow = FillNulls(webshow);
                    webshow.AccountID = Convert.ToInt32(Session["UserAccountID"]);

                    string validation = ValidateInput(webshow, Request.Form["txtWebShowURLAddresses"].ToString());
                    if (!String.IsNullOrEmpty(validation))
                    {
                        ViewData["ValidationMessage"] = validation;
                        ViewData["URLAddressList"] = new SelectList(BuildURLAddressList(), "Value", "Text", ""); 
                        ViewData["WebShowURLAddresses"] = Request.Form["txtWebShowURLAddresses"].ToString();
                        ViewData["WebShowURLAddressZooms"] = Request.Form["txtWebShowURLAddressZooms"].ToString();
                        ViewData["WebShowURLAddressList"] = new SelectList(BuildWebShowURLAddressList(Request.Form["txtWebShowURLAddresses"].ToString()), "Value", "Text", "");
                 
                        return View(webshow);
                    }
                    else
                    {
                        // Create the webshow
                         
                        repository.CreateWebShow(webshow); 
                        CommonMethods.CreateActivityLog((User)Session["User"], "Web Show", "Add",
                            "Added web show '" + webshow.WebShowName + "' - ID: " + webshow.WebShowID.ToString());

                        IWebShowURLAddressXRefRepository xrefrep = new EntityWebShowURLAddressXRefRepository(); 
                        IURLAddressRepository urlrep = new EntityURLAddressRepository();

                        char[] separator = { '{', '|', '}' };
                        // Create a xref for each web address in the webshow
                        string[] ids = Request.Form["txtWebShowURLAddresses"].ToString().Split(separator);
                        string[] zooms = Request.Form["txtWebShowURLAddressZooms"].ToString().Split(separator); 
                        int playOrderNumber = 1; 
                        for(int i=0;i< ids.Length; i++)
                        {
                            if (!String.IsNullOrEmpty(ids[i].Trim()))
                            {
                                URLAddress urlAddress = urlrep.GetURLAddress(Convert.ToInt32(ids[i]));
                                if (urlAddress != null)
                                {
                                    WebShowURLAddressXRef xref = new WebShowURLAddressXRef();
                                    xref.PlayOrder = playOrderNumber;
                                    xref.WebShowID = webshow.WebShowID;
                                    xref.URLAddressID = urlAddress.URLAddressID;
                                    xref.Zoom = Convert.ToInt32(zooms[i]);
                                    xrefrep.CreateWebShowURLAddressXRef(xref);
                                    playOrderNumber += 1;
                                }
                            }
                        }
                         
                        return RedirectToAction("Index");
                    }
                }

                return View(webshow);
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("WebShow", "Create POST", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (Session["UserAccountID"] == null)
                    return RedirectToAction("Validate", "Login");
                User user = (User)Session["User"];
                ViewData["LoginInfo"] = Utility.BuildUserAccountString(user.Username, Convert.ToString(Session["UserAccountName"]));
                if (user.IsAdmin)
                    ViewData["txtIsAdmin"] = "true";
                else
                    ViewData["txtIsAdmin"] = "false";
                
                WebShow webShow = repository.GetWebShow(id);
                ViewData["ValidationMessage"] = String.Empty;
                ViewData["URLAddressList"] = new SelectList(BuildURLAddressList(), "Value", "Text", "");
               

                // Get the img ids for the websohw
                string ids = String.Empty;
                string zooms = String.Empty;
                IWebShowURLAddressXRefRepository xrefrep = new EntityWebShowURLAddressXRefRepository(); 
                IEnumerable<WebShowURLAddressXRef> xrefs = xrefrep.GetWebShowURLAddressXRefs(id);
                foreach (WebShowURLAddressXRef xref in xrefs)
                {
                    ids += "{|}" + xref.URLAddressID;
                    zooms += "{|}" + xref.Zoom;
                }
                ViewData["WebShowURLAddresses"] = ids;
                ViewData["WebShowURLAddressZooms"] = zooms;
                ViewData["WebShowURLAddressList"] = new SelectList(BuildWebShowURLAddressList(ids), "Value", "Text", "");
                 
                return View(webShow);
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("WebShow", "Edit", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        [HttpPost]
        public ActionResult Edit(WebShow webshow)
        {
            try
            {
                if (Session["UserAccountID"] == null)
                    return RedirectToAction("Validate", "Login");
                User user = (User)Session["User"];
                ViewData["LoginInfo"] = Utility.BuildUserAccountString(user.Username, Convert.ToString(Session["UserAccountName"]));
                if (user.IsAdmin)
                    ViewData["txtIsAdmin"] = "true";
                else
                    ViewData["txtIsAdmin"] = "false";

                if (ModelState.IsValid)
                {
                    // Set NULLs to Empty Strings
                    webshow = FillNulls(webshow);
                    webshow.AccountID = Convert.ToInt32(Session["UserAccountID"]);
                    string validation = ValidateInput(webshow, Request.Form["txtWebShowURLAddresses"].ToString());

                    if (!String.IsNullOrEmpty(validation))
                    {
                        ViewData["ValidationMessage"] = validation;
                        ViewData["URLAddressList"] = new SelectList(BuildURLAddressList(), "Value", "Text", "");
                        ViewData["WebShowURLAddresses"] = Request.Form["txtWebShowURLAddresses"].ToString();
                        ViewData["WebShowURLAddressZooms"] = Request.Form["txtWebShowURLAddressZooms"].ToString();
                        ViewData["WebShowURLAddressList"] = new SelectList(BuildWebShowURLAddressList(Request.Form["txtWebShowURLAddresses"].ToString()), "Value", "Text", "");
 
                        return View(webshow);
                    }
                    else
                    {
                        // Create the webshow

                        repository.UpdateWebShow(webshow);
                        CommonMethods.CreateActivityLog((User)Session["User"], "Web Show", "Edit",
                            "Edited web show '" + webshow.WebShowName + "' - ID: " + webshow.WebShowID.ToString());

                        IWebShowURLAddressXRefRepository xrefrep = new EntityWebShowURLAddressXRefRepository();
                        IURLAddressRepository urlrep = new EntityURLAddressRepository();

                        // Delete existing xrefs for the playlist
                        xrefrep.DeleteWebShowURLAddressXRefs(webshow.WebShowID);

                        char[] separator = { '{', '|', '}' };
                        // Create a xref for each web address in the webshow
                        string[] ids = Request.Form["txtWebShowURLAddresses"].ToString().Split(separator);
                        string[] zooms = Request.Form["txtWebShowURLAddressZooms"].ToString().Split(separator);
                        int playOrderNumber = 1;
                        for (int i = 0; i < ids.Length; i++)
                        {
                            if (!String.IsNullOrEmpty(ids[i].Trim()))
                            {
                                URLAddress urlAddress = urlrep.GetURLAddress(Convert.ToInt32(ids[i]));
                                if (urlAddress != null)
                                {
                                    WebShowURLAddressXRef xref = new WebShowURLAddressXRef();
                                    xref.PlayOrder = playOrderNumber;
                                    xref.WebShowID = webshow.WebShowID;
                                    xref.URLAddressID = urlAddress.URLAddressID;
                                    xref.Zoom = Convert.ToInt32(zooms[i]);
                                    xrefrep.CreateWebShowURLAddressXRef(xref);
                                    playOrderNumber += 1;
                                }
                            }
                        }

                        return RedirectToAction("Index");
                    }
                }

                return View(webshow);
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("WebShow", "Create POST", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (Session["UserAccountID"] == null)
                    return RedirectToAction("Validate", "Login");
                User user = (User)Session["User"];
                ViewData["LoginInfo"] = Utility.BuildUserAccountString(user.Username, Convert.ToString(Session["UserAccountName"]));
                if (user.IsAdmin)
                    ViewData["txtIsAdmin"] = "true";
                else
                    ViewData["txtIsAdmin"] = "false";
                WebShow webShow = repository.GetWebShow(id);
                if (webShow.AccountID.Equals(user.AccountID))
                    repository.DeleteWebShow(id);
                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("webShow", "Delete Get", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        private string ValidateInput(WebShow webshow, string urlAddresses)
        {
            if (webshow.AccountID == 0)
                return "Account ID is not valid.";

            if (String.IsNullOrEmpty(webshow.WebShowName))
                return "Web Show Name is required.";

            if (String.IsNullOrEmpty(urlAddresses.Replace("{|}", "")))
                return "You must select at least one url address for this web show.";

            return String.Empty;
        }

        private WebShow FillNulls(WebShow webshow)
        {
            if (webshow.Tags == null) webshow.Tags = String.Empty;

            return webshow;
        }

        private WebShow CreateNewWebShow()
        {
            WebShow webshow = new WebShow();
            webshow.WebShowID = 0;
            webshow.AccountID = 0;
            webshow.WebShowName = String.Empty;
            webshow.Tags = String.Empty;
            webshow.IsActive = true;
            webshow.IntervalInSecs = 10;

            return webshow;
        }

        private List<SelectListItem> BuildURLAddressList()
        {
            // Get the account id
            int accountid = 0;
            if (Session["UserAccountID"] != null)
                accountid = Convert.ToInt32(Session["UserAccountID"]);

            // Get the active URLAddress
            IURLAddressRepository urlrep = new EntityURLAddressRepository();
            IEnumerable<URLAddress> urls = urlrep.GetActiveURLAddresses(accountid);

            List<SelectListItem> items = new List<SelectListItem>();
            
            StringBuilder URLAddressSources=new StringBuilder();
            foreach (URLAddress url in urls)
            {
                SelectListItem item = new SelectListItem();
                item.Text = url.URLAddressName;
                item.Value = ""+url.URLAddressID;
                URLAddressSources.Append("{|}"+url.URLAddressSource ); 
                items.Add(item);
            }
            ViewData["URLAddressSources"]= URLAddressSources.ToString();

            return items;
        }

        private List<SelectListItem> BuildWebShowURLAddressList(string urlIDs)
        {
            IURLAddressRepository urlrep = new EntityURLAddressRepository();
            List<SelectListItem> items = new List<SelectListItem>();
            char[] separator = {'{','|','}' };
            string[] parseUrlIDs = urlIDs.Split(separator);
            StringBuilder URLAddressSources = new StringBuilder();
            foreach (string parseUrlID in parseUrlIDs)
            {
                
                if (!String.IsNullOrEmpty(parseUrlID.Trim()))
                {
                    URLAddress urlAddress = urlrep.GetURLAddress(Convert.ToInt32(parseUrlID));
                    if (urlAddress != null)
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = urlAddress.URLAddressName;
                        item.Value = ""+ urlAddress.URLAddressID;
                        URLAddressSources.Append("{|}" + urlAddress.URLAddressSource  );
                       
                        items.Add(item); 
                    } 
                }
            }
            ViewData["WebShowURLAddressSources"] = URLAddressSources.ToString();
            return items;
        }

        private WebShowPageState GetPageState()
        {
            try
            {
                WebShowPageState pagestate = new WebShowPageState(); 

                // Initialize the session values if they don't exist - need to do this the first time controller is hit
                if (Session["WebShowPageState"] == null)
                {
                    int accountid = 0;
                    if (Session["UserAccountID"] != null)
                        accountid = Convert.ToInt32(Session["UserAccountID"]);

                    pagestate.AccountID = accountid;
                    pagestate.WebShowName = String.Empty;
                    pagestate.Tag = String.Empty;
                    pagestate.IncludeInactive = false;
                    pagestate.SortBy = "WebShowName";
                    pagestate.AscDesc = "Ascending";
                    pagestate.PageNumber = 1;
                    Session["WebShowPageState"] = pagestate;
                }
                else
                {
                    pagestate = (WebShowPageState)Session["WebShowPageState"];
                }
                return pagestate;
            }
            catch { return new WebShowPageState(); }
        }

        private List<SelectListItem> BuildSortByList()
        {
            // Build the sort by list
            List<SelectListItem> sortitems = new List<SelectListItem>();

            SelectListItem sortitem1 = new SelectListItem();
            sortitem1.Text = "Web Show Name";
            sortitem1.Value = "WebShowName";

            SelectListItem sortitem2 = new SelectListItem();
            sortitem2.Text = "Tags";
            sortitem2.Value = "Tags";

            SelectListItem sortitem3 = new SelectListItem();
            sortitem3.Text = "Is Active";
            sortitem3.Value = "IsActive";

            sortitems.Add(sortitem1);
            sortitems.Add(sortitem2);
            sortitems.Add(sortitem3);

            return sortitems;
        }

        private List<SelectListItem> BuildAscDescList()
        {
            // Build the asc desc list
            List<SelectListItem> ascdescitems = new List<SelectListItem>();

            SelectListItem ascdescitem1 = new SelectListItem();
            ascdescitem1.Text = "Asc";
            ascdescitem1.Value = "Asc";

            SelectListItem ascdescitem2 = new SelectListItem();
            ascdescitem2.Text = "Desc";
            ascdescitem2.Value = "Desc";

            ascdescitems.Add(ascdescitem1);
            ascdescitems.Add(ascdescitem2);

            return ascdescitems;
        }

        private void SavePageState(WebShowPageState pagestate)
        {
            Session["WebShowPageState"] = pagestate;
        }

       

    }
}
