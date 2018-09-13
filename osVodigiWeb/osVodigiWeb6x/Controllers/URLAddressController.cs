using osVodigiWeb6x.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace osVodigiWeb6x.Controllers
{
    public class URLAddressController : Controller
    {
        IURLAddressRepository repository;
        string firstfile = String.Empty;
        string selectedfile = String.Empty;

        public URLAddressController()
            : this(new EntityURLAddressRepository())
        { }

        public URLAddressController(IURLAddressRepository paramrepository)
        {
            repository = paramrepository;
        }

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
                URLAddressPageState pagestate = GetURLAddressPageState();

                // Get the account id
                int accountid = 0;
                if (Session["UserAccountID"] != null)
                    accountid = Convert.ToInt32(Session["UserAccountID"]);

                // Set and save the page state to the submitted form values if any values are passed
                if (Request.Form["lstAscDesc"] != null)
                {
                    pagestate.AccountID = accountid;
                    pagestate.UrlAddressName = Request.Form["txtUrlAddressName"].ToString().Trim();
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
                ViewData["UrlAddressName"] = pagestate.UrlAddressName;
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
                int recordcount = repository.GetURLAddressRecordCount(pagestate.AccountID, pagestate.UrlAddressName, pagestate.Tag, pagestate.IncludeInactive);

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

                // Set the Video folder 
                //ViewData["VideoFolder"] = @"~/Media/" + Convert.ToString(Session["UserAccountID"]) + @"/Videos/";

                ViewResult result = View(repository.GetURLAddressPage(pagestate.AccountID, pagestate.UrlAddressName, pagestate.Tag, pagestate.IncludeInactive, pagestate.SortBy, isdescending, pagestate.PageNumber, pagecount));
                result.ViewName = "Index";
                return result;
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("URLAddress", "Index", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        private URLAddressPageState GetURLAddressPageState()
        {
            try
            {
                URLAddressPageState pagestate = new URLAddressPageState();


                // Initialize the session values if they don't exist - need to do this the first time controller is hit
                if (Session["URLAddressPageState"] == null)
                {
                    int accountid = 0;
                    if (Session["UserAccountID"] != null)
                        accountid = Convert.ToInt32(Session["UserAccountID"]);

                    pagestate.AccountID = accountid;
                    pagestate.UrlAddressName = String.Empty;
                    pagestate.Tag = String.Empty;
                    pagestate.IncludeInactive = false;
                    pagestate.SortBy = "UrlAddressName";
                    pagestate.AscDesc = "Ascending";
                    pagestate.PageNumber = 1;
                    Session["URLAddressPageState"] = pagestate;
                }
                else
                {
                    pagestate = (URLAddressPageState)Session["URLAddressPageState"];
                }
                return pagestate;
            }
            catch { return new URLAddressPageState(); }
        }

        private void SavePageState(URLAddressPageState pagestate)
        {
            Session["URLAddressPageState"] = pagestate;
        }

        private List<SelectListItem> BuildSortByList()
        {
            // Build the sort by list
            List<SelectListItem> sortitems = new List<SelectListItem>();

            SelectListItem sortitem1 = new SelectListItem();
            sortitem1.Text = "URL Address Name";
            sortitem1.Value = "UrlAddressName"; 

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

        //
        // GET: /URLAddress/Upload
        [HttpGet]
        public ActionResult Upload()
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

                return View(CreateNewURLAddress());
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("URLAddress", "Upload", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        //
        // POST: /URLAddress/Upload
        [HttpPost]
        public ActionResult Upload(URLAddress urlAddress)
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

                string validation = String.Empty;
                if (ModelState.IsValid)
                {
                    // Set NULLs to Empty Strings
                    urlAddress = FillNulls(urlAddress);
                    urlAddress.AccountID = Convert.ToInt32(Session["UserAccountID"]); 
                     
                    if (!String.IsNullOrEmpty(validation))
                    {
                        ViewData["ValidationMessage"] = validation;
                        return View(urlAddress);
                    }
                    else
                    { 
                        repository.CreateURLAddress(urlAddress);

                        CommonMethods.CreateActivityLog((User)Session["User"], "URLAddress", "Upload",
                                "Added URLAddress '" + urlAddress.UrlAddressName + "' - ID: " + urlAddress.UrlAddressID.ToString()); 
                        return RedirectToAction("Index");
                    }
                }

                return View(urlAddress);
            }
            catch (Exception ex)
            {
                Helpers.SetupApplicationError("URLAddress", "Upload POST", ex.Message);
                return RedirectToAction("Index", "ApplicationError");
            }
        }

        private URLAddress CreateNewURLAddress()
        {
            URLAddress UrlAddress = new URLAddress() {
                UrlAddressID = 0,
                AccountID = 0, 
                UrlAddressName = String.Empty,
                Tags = String.Empty,
                UrlAddressSource = String.Empty,
                IsActive = true
            };
            return UrlAddress;
        }

        private URLAddress FillNulls(URLAddress urlAddress)
        {
            if (urlAddress.Tags == null) urlAddress.Tags = String.Empty; 
            return urlAddress;
        }

    }
}
