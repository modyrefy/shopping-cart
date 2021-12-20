using Server.Model.Dto.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Interfaces.Repositories
{
    public interface ILookupRepository
    {
        List<LookupItem> GetBrandList();
        List<LookupItem> GetCategoryList();
        List<LookupItem> GetParentCategoryList();
        List<LookupItem> GetCountryList();
        List<LookupItem> GetUserRoles();

    }
}
