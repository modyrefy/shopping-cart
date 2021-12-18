using Server.Model.Enums;

namespace Server.Model.Dto.Lookup
{
    public class LookupItem
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }
    public class LookupRequest
    {
        public LookupsEnum LookupEnum { get; set; }
    }
}
