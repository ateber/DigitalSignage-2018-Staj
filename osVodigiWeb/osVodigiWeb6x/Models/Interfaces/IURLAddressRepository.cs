using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osVodigiWeb6x.Models 
{
    public interface IURLAddressRepository
    {
        void DeleteURLAddress(URLAddress urladdress);
        void DeleteURLAddress(int URLAddressID);
        void CreateURLAddress(URLAddress urladdress);
        void UpdateURLAddress(URLAddress urladdress);
        URLAddress GetURLAddress(int urladdressid);  
        IEnumerable<URLAddress> GetActiveURLAddresses(int accountid);
        IEnumerable<URLAddress> GetAllURLAddresses(int accountid);
        IEnumerable<URLAddress> GetURLAddressPage(int accountid, string urladdressname, string tag, bool includeinactive, string sortby, bool isdescending, int pagenumber, int pagecount);
        int GetURLAddressRecordCount(int accountid, string urladdressname, string tag, bool includeinactive);
        int SaveChanges();
    }
}
