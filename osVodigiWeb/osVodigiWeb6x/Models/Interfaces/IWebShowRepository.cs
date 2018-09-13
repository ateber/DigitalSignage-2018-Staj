using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osVodigiWeb6x.Models 
{
    public interface IWebShowRepository  //added
    {
        void CreateWebShow(WebShow webshow);
        void UpdateWebShow(WebShow webshow);
        WebShow GetWebShow(int webshowid);
        IEnumerable<WebShow> GetAllWebShows(int accountid);
        IEnumerable<WebShow> GetActiveWebShows(int accountid);
        IEnumerable<WebShow> GetWebShowPage(int accountid, string webshowname, string tag, bool includeinactive, string sortby, bool isdescending, int pagenumber, int pagecount);
        int GetWebShowRecordCount(int accountid, string webshowname, string tag, bool includeinactive);
        int SaveChanges();
    }
}
