using Application.Common.Enumerable;

namespace Application.Common.Dto.Input
{
    public class BaseInputDto
    {
        public BaseInputDto()
        {
            PageIndex = 1;
            PageSize = 20;
            SortBy = SortEnum.New;
        }
        /// <summary>
        /// شماره صفحه
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// تعداد در صفحه
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// متن قابل جستجو
        /// </summary>
        public string Q { get; set; }
        /// <summary>
        /// مرتب سازی بر اساس 
        /// </summary>
        public SortEnum SortBy { get; set; }
        public bool? Available { get; set; }
    }
}
