using Server.Model.Dto.Lookup;
using Server.Model.Enums;
using Server.Model.Interfaces.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Core.Manager
{
    public class LookupManager
    {
        #region instance
        private LookupManager() { }
        private static LookupManager instance = null;
        public static LookupManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LookupManager();
                }
                return instance;
            }
        }
        #endregion
        #region public
        public async Task<List<LookupItem>> GetAsync(LookupRequest request, IRequestContext context)
        {
            List<LookupItem> result = new();
            switch (request.LookupEnum)
            {
                case LookupsEnum.Brand:
                    result = context.MemoryCacheManager.GetOrSet<List<LookupItem>>(request.LookupEnum.ToString(), () => context.Repositories.LookupRepository.GetBrandList());
                    break;
                case LookupsEnum.Category:
                    result = context.MemoryCacheManager.GetOrSet<List<LookupItem>>(request.LookupEnum.ToString(), () => context.Repositories.LookupRepository.GetCategoryList());
                    break;
                case LookupsEnum.Country:
                    result = context.MemoryCacheManager.GetOrSet<List<LookupItem>>(request.LookupEnum.ToString(), () => context.Repositories.LookupRepository.GetCountryList());
                    break;
                case LookupsEnum.UserRole:
                    result = context.MemoryCacheManager.GetOrSet<List<LookupItem>>(request.LookupEnum.ToString(), () => context.Repositories.LookupRepository.GetUserRoles());
                    break;
                case LookupsEnum.ParentCategory:
                    result = context.MemoryCacheManager.GetOrSet<List<LookupItem>>(request.LookupEnum.ToString(), () => context.Repositories.LookupRepository.GetParentCategoryList());
                    break;

            }

            result = result != null && result.Count != 0 && request.ParentId.GetValueOrDefault() != 0 ? result.Where(p => p.ParentId == request.ParentId).ToList() : result;

            if (request.IdList != null && request.IdList.Count != 0 && result != null && result.Count != 0)
            {
                List<LookupItem> _list = result.Where(t => request.IdList.Contains(t.Value)).ToList();
                result = request.IsRemovable ? result.Except(_list).ToList() : _list;

            }
            return result != null && result.Count != 0 ? result : null;
        }
        #endregion
        
    }
}
