using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
 

namespace osVodigiWeb6x.Models 
{
    public class EntityWebShowRepository : IWebShowRepository //added
    {
        private VodigiContext db = new VodigiContext();

        public void CreateWebShow(WebShow webshow)
        {
            db.WebShows.Add(webshow);
            db.SaveChanges();
        }

        public int DeleteWebShow(WebShow webshow)
        {
            db.WebShows.Remove(webshow);
            return db.SaveChanges();
        }

        public int DeleteWebShow(int webshowid)
        {
            WebShow webShow = new WebShow() { WebShowID = webshowid };
            db.WebShows.Attach(webShow);
            db.WebShows.Remove(webShow);
            return db.SaveChanges();
        }

        public IEnumerable<WebShow> GetActiveWebShows(int accountid)
        {
            var query = from webshow in db.WebShows
                        select webshow;
            query = query.Where(sss => sss.AccountID.Equals(accountid));
            query = query.Where(sss => sss.IsActive == true);
            query = query.OrderBy("WebShowName", false);

            List<WebShow> webshows = query.ToList();

            return webshows;
        }

        public IEnumerable<WebShow> GetAllWebShows(int accountid)
        {
            var query = from webshow in db.WebShows
                        select webshow;
            query = query.Where(sss => sss.AccountID.Equals(accountid));
            query = query.OrderBy("WebShowName", false);

            List<WebShow> webshows = query.ToList();

            return webshows;
        }

        public WebShow GetWebShow(int webshowid)
        {
            WebShow webshow = db.WebShows.AsNoTracking().SingleOrDefault(ws=>ws.WebShowID== webshowid); 
            return webshow;
        }

        public IEnumerable<WebShow> GetWebShowPage(int accountid, string webshowname, string tag, bool includeinactive, string sortby, bool isdescending, int pagenumber, int pagecount)
        {
            var query = from webshow in db.WebShows
                        select webshow;
            query = query.Where(sss => sss.AccountID.Equals(accountid));
            if (!String.IsNullOrEmpty(webshowname))
                query = query.Where(sss => sss.WebShowName.StartsWith(webshowname));
            if (!String.IsNullOrEmpty(tag))
                query = query.Where(sss => sss.Tags.Contains(tag));
            if (!includeinactive)
                query = query.Where(sss => sss.IsActive == true);
            if (!String.IsNullOrEmpty(sortby))
                query = query.OrderBy(sortby, isdescending);

            // Get a single page from the filtered records
            int iSkip = (pagenumber * Constants.PageSize) - Constants.PageSize;

            List<WebShow> webshows = query.Skip(iSkip).Take(Constants.PageSize).ToList();

            return webshows;
        }

        public int GetWebShowRecordCount(int accountid, string webshowname, string tag, bool includeinactive)
        {
            var query = from webshow in db.WebShows
                        select webshow;
            query = query.Where(sss => sss.AccountID.Equals(accountid));
            if (!String.IsNullOrEmpty(webshowname))
                query = query.Where(sss => sss.WebShowName.StartsWith(webshowname));
            if (!String.IsNullOrEmpty(tag))
                query = query.Where(sss => sss.Tags.Contains(tag));
            if (!includeinactive)
                query = query.Where(sss => sss.IsActive == true);

            // Get a Count of all filtered records
            return query.Count();
        }

        public int SaveChanges()
        {
            return db.SaveChanges(); 
        }

        public void UpdateWebShow(WebShow webshow)
        {
            db.Entry(webshow).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}