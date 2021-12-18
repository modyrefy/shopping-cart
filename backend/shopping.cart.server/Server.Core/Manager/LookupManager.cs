using Server.Model.Dto.Lookup;
using Server.Model.Enums;
using Server.Model.Interfaces.Context;
using System.Collections.Generic;

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
        public  List<LookupItem> Get(IRequestContext RequestContext,LookupsEnum enumValue)
        {
            List < LookupItem > items = new();
            switch (enumValue)
            {
                case LookupsEnum.Brand:

                    break;
            }
            return items != null && items.Count != 0 ? items : null;
        }
        #endregion
        #region private


        #endregion
    }
}
