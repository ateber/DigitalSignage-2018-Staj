using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osVodigiWeb6x.Models 
{
    public interface IWebShowURLAddressXRefRepository
    {
        void CreateWebShowURLAddressXRef(WebShowURLAddressXRef xref);
        void DeleteWebShowURLAddressXRefs(int webshowid);
        IEnumerable<WebShowURLAddressXRef> GetWebShowURLAddressXRefs(int webshowid);
        int SaveChanges();
    }
}
