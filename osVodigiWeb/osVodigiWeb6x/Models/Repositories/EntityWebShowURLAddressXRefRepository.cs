using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace osVodigiWeb6x.Models 
{
    public class EntityWebShowURLAddressXRefRepository : IWebShowURLAddressXRefRepository
    {
        VodigiContext db = new VodigiContext();

        public void CreateWebShowURLAddressXRef(WebShowURLAddressXRef xref)
        {
            db.WebShowURLAddressXRefs.Add(xref);
            db.SaveChanges();
        }

        public void DeleteWebShowURLAddressXRefs(int webshowid)
        {
            var query = from xref in db.WebShowURLAddressXRefs
                        where xref.WebShowID == webshowid 
                        select xref;

            List<WebShowURLAddressXRef> xrefs = query.ToList();

            foreach (WebShowURLAddressXRef xref in xrefs)
            {
                db.WebShowURLAddressXRefs.Remove(xref);
            }
            db.SaveChanges();
        }

        public IEnumerable<WebShowURLAddressXRef> GetWebShowURLAddressXRefs(int webshowid)
        {
            var query = from xref in db.WebShowURLAddressXRefs
                        where xref.WebShowID == webshowid
                        orderby xref.PlayOrder
                        select xref;
            List<WebShowURLAddressXRef> xrefs = query.ToList();
            return xrefs;
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}