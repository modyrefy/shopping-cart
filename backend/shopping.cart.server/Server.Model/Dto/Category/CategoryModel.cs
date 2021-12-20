namespace Server.Model.Dto.Category
{
    public class CategoryModel
    {
        public int CategoryId { get; set; } = 0;
        public int? ParentCategoryId { get; set; } 
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
