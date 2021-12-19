using Server.Model.Dto.Lookup;
using Server.Model.Enums;
using Server.Model.Interfaces.Context;
using System.Collections.Generic;
using System.Linq;

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
        public  List<LookupItem> Get(IRequestContext requestContext,LookupsEnum enumValue)
        {
            List < LookupItem > items = new();
            var px = GetCurrentLanguage(requestContext);
            switch (enumValue)
            {
                case LookupsEnum.Brand:

                    break;
            }
            return items != null && items.Count != 0 ? items : null;
        }
        #endregion
        #region private
        private LanguagesEnum GetCurrentLanguage(IRequestContext requestContext)
        {
            LanguagesEnum languagesEnum = LanguagesEnum.English;
            string language = requestContext.HttpContextAccessor.HttpContext.Request.Headers["accept-language"].FirstOrDefault().Split(',').FirstOrDefault().Split('-').FirstOrDefault().ToLower();
            switch (language)
            {
                case "ar":
                    languagesEnum = LanguagesEnum.Arabic;
                    break;
                case "fr":
                    languagesEnum = LanguagesEnum.French;
                    break;
                case "en":
                default:
                    languagesEnum = LanguagesEnum.English;
                    break;
            }
            return languagesEnum;
        }

        #endregion
    }
}
