using Server.Model.Enums;
using System.Collections.Generic;

namespace Server.Model.Dto.Lookup
{
    public class LookupItem
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int ParentId { get; set; }
        public string OtherValue { get; set; }
    }
    public class LookupRequest
    {
        public LookupsEnum LookupEnum { get; set; }
        public int? ParentId { get; set; } = 0;
        public bool IsRemovable { get; set; } = false;
        public List<string> IdList { get; set; }
    }
}
