using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.Product;
using Server.Model.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.Product
{
    public class SearchProductDashboardProcessor : ProcessorBase<ProductDashboardModelRequest, List<ProductDashboardModelResponse>>
    {
        #region constructor
        public SearchProductDashboardProcessor(IRequestContext context) : base(context)
        {
        }
        #endregion
        #region public
        public override ResponseBase<List<ProductDashboardModelResponse>> DoProcess(ProductDashboardModelRequest request)
        {
            throw new NotImplementedException();
        }

        public override async Task<ResponseBase<List<ProductDashboardModelResponse>>> DoProcessAsync(ProductDashboardModelRequest request)
        {
            request ??= new ProductDashboardModelRequest();
            List<ValidationError> errors = DoValidation(request);
            
            List<ProductDashboardModelResponse> response = null;
            int recordCount = 0;
            if (errors == null || errors.Count == 0)
            {
                var productsQuery = RequestContext.Repositories.ProductRepository.GetAllQueryable();
                var productImages = RequestContext.Repositories.ProductImageRepository.GetAllQueryable().Where(p => p.IsPrimary == true && p.IsActive == true);
                productsQuery = productsQuery.Where(p => p.IsActive == true);
                if (!string.IsNullOrEmpty(request.Name))
                {
                    request.Name = request.Name.Trim().ToLower();
                    productsQuery = productsQuery.Where(p =>
                    p.ProductNameAr.ToLower().Trim().Contains(request.Name)
                      || p.ProductNameAr.ToLower().Trim().Contains(request.Name)
                      );
                }
                productsQuery = request.BrandId != 0 ? productsQuery.Where(p => p.BrandId == request.BrandId) : productsQuery;
                productsQuery = request.CategoryId != 0 ? productsQuery.Where(p => p.CategoryId == request.CategoryId) : productsQuery;
                //productsQuery = productsQuery.Skip(request.PageIndex * request.PageSize).Take(request.PageSize);
                response = (from product in productsQuery
                            from img in productImages.Where(p=>p.ProductId==product.ProductId).Take(1).DefaultIfEmpty()
                            //join img in productImages on product.ProductId equals img.ProductId into details from data in details.DefaultIfEmpty()
                            select new ProductDashboardModelResponse() {
                             ProductId=product.ProductId,
                             ProductKey=product.ProductKey,
                             ProductNameAr=product.ProductNameAr,
                             ProductNameEn=product.ProductNameEn,
                             StockQuantiy=product.StockQuantiy,
                             Price=product.Price,
                             ProductImage=img!=null && !string.IsNullOrEmpty(img.ImageUrl )? img.ImageUrl:"default-image"
                            }).ToList();
                if (response != null && response.Count() != 0)
                {
                    recordCount = response.Count();
                    response = response.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                }

            }
            return new ResponseBase<List<ProductDashboardModelResponse>>()
            {
                Errors=errors,
                Result=response,
                RecordCount=recordCount
            };
        }

        public override List<ValidationError> DoValidation(ProductDashboardModelRequest request)
        {
            return new List<ValidationError>();
        }
        #endregion
    }
}
