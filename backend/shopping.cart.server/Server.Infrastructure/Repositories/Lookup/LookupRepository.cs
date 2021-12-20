using Server.Infrastructure.Data;
using Server.Infrastructure.Repositories.EFCore;
using Server.Model.Dto.Lookup;
using Server.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories.Lookup
{
    public class LookupRepository : EfCoreRepository<Object>, ILookupRepository
    {
        #region constructor
        public LookupRepository(DefaultDBContext dBContext) : base(dBContext)
        {
        }
        #endregion

        public List<LookupItem> GetBrandList()
        {
            return (from p in Context.Brands
                    select new LookupItem()
                    {
                        Value = p.BrandId.ToString(),
                        Name = p.NameEn,
                        NameAr = p.NameAr,
                        ParentId = (int)p.BrandId,
                    }).ToList();
        }

        public List<LookupItem> GetCategoryList()
        {
            return (from p in Context.Categories
                    select new LookupItem()
                    {
                        Value = p.CategoryId.ToString(),
                        Name = p.NameEn,
                        NameAr = p.NameAr,
                        ParentId = (int)p.CategoryId,
                    }).ToList();
        }
        public List<LookupItem> GetParentCategoryList()
        {
            return (from p in Context.Categories
                    where p.ParentCategoryId.GetValueOrDefault()==0
                    select new LookupItem()
                    {
                        Value = p.CategoryId.ToString(),
                        Name = p.NameEn,
                        NameAr = p.NameAr,
                        ParentId = (int)p.CategoryId,
                    }).ToList();
        }
        public List<LookupItem> GetCountryList()
        {
            return (from p in Context.Countries
                    select new LookupItem()
                    {
                        Value = p.CountryId.ToString(),
                        Name = p.NameEn,
                        NameAr = p.NameAr,
                        ParentId = (int)p.CountryId,
                    }).ToList();
        }

        public List<LookupItem> GetUserRoles()
        {
            return (from p in Context.UserRoles
                    select new LookupItem()
                    {
                        Value = p.UserRoleId.ToString(),
                        Name = p.UserRole,
                        NameAr = p.UserRole,
                        ParentId = (int)p.UserRoleId,
                    }).ToList();
        }
    }
}
