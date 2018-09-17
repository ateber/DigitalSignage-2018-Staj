using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace osVodigiWeb6x.Models 
{
    public class EntityURLAddressRepository : IURLAddressRepository
    {
        VodigiContext db = new VodigiContext();

        public void CreateURLAddress(URLAddress urladdress)
        {
            db.URLAddresses.Add(urladdress); 
            db.SaveChanges();
        }

        public void DeleteURLAddress(int urlAddressID)
        {
            URLAddress customer = new URLAddress() { URLAddressID = urlAddressID };
            db.URLAddresses.Attach(customer);
            db.URLAddresses.Remove(customer);
            db.SaveChanges();
        }

        public void DeleteURLAddress(URLAddress urladdress)
        {
            db.URLAddresses.Remove(urladdress);
            db.SaveChanges();
        }

        public IEnumerable<URLAddress> GetActiveURLAddresses(int accountid)
        {
            return db.URLAddresses.Where(url => url.AccountID == accountid).Where(url => url.IsActive==true).OrderBy("URLAddressName", false).ToList(); ; 
        }

        public IEnumerable<URLAddress> GetAllURLAddresses(int accountid)
        {
            return db.URLAddresses.Where(url => url.AccountID == accountid).OrderBy("URLAddressName",false).ToList(); ;
        }

        public URLAddress GetURLAddress(int urladdressid)
        {
            return db.URLAddresses.AsNoTracking().Where(urlADdress => urlADdress.URLAddressID==urladdressid).SingleOrDefault();
        } 
    
        public IEnumerable<URLAddress> GetURLAddressPage(int accountid, string urlname, string tag, bool includeinactive, string sortby, bool isdescending, int pagenumber, int pagecount)
        {
             //Get a single page from the filtered records
            var query = db.URLAddresses.Where(urls => urls.AccountID.Equals(accountid));
            if (!String.IsNullOrEmpty(urlname))
                query = query.Where(urls => urls.URLAddressName.StartsWith(urlname));
            if (!String.IsNullOrEmpty(tag))
                query = query.Where(urls => urls.Tags.Contains(tag));
            if (!includeinactive)
                query = query.Where(urls => urls.IsActive == true);
            if (!String.IsNullOrEmpty(sortby))
                query = query.OrderBy(sortby, isdescending);

            // Get a single page from the filtered records
            int iSkip = (pagenumber * Constants.PageSize) - Constants.PageSize;

            List<URLAddress> urlAddress = query.Skip(iSkip).Take(Constants.PageSize).ToList();

            return urlAddress;
        }

        public int GetURLAddressRecordCount(int accountid, string urlname, string tag, bool includeinactive)
        {
            var query = from urls in db.URLAddresses
                        select urls;
            query = query.Where(urls => urls.AccountID.Equals(accountid));
            if (!String.IsNullOrEmpty(urlname))
                query = query.Where(urls => urls.URLAddressName.StartsWith(urlname));
            if (!String.IsNullOrEmpty(tag))
                query = query.Where(urls => urls.Tags.Contains(tag));
            if (!includeinactive)
                query = query.Where(urls => urls.IsActive == true);

            // Get a Count of all filtered records
            return query.Count();
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void UpdateURLAddress(URLAddress urladdress)
        {
            db.Entry(urladdress).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}