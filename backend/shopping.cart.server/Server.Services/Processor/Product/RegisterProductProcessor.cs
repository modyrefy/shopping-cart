using Microsoft.Extensions.Localization;
using Server.BusinessValidation.Validations.RegularExpression;
using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.Product;
using Server.Model.Interfaces.Context;
using Server.Model.Models;
using Server.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.Product
{
    public class RegisterProductProcessor : ProcessorBase<ProductModel, ProductModel>
    {
        #region constructor
        public RegisterProductProcessor(IRequestContext context) : base(context)
        {
        }
        #endregion
        #region public
        public override ResponseBase<ProductModel> DoProcess(ProductModel request)
        {
            throw new NotImplementedException();
        }

        public override async Task<ResponseBase<ProductModel>> DoProcessAsync(ProductModel request)
        {
            List<ValidationError> errors = DoValidation(request);
            if (errors == null || errors.Count == 0)
            {
                Products entity = RequestContext.Mapper.Map<Products>(request);
                //entity.ProductImages = null;
              await using var transaction = await this.RequestContext.Repositories.Repository.BeginTransactionAsync();
                try
                {
                    entity = entity.ProductId == 0 ? 
                        await this.RequestContext.Repositories.ProductRepository.InsertAsync(entity) : 
                        await this.RequestContext.Repositories.ProductRepository.UpdateAsync(entity);
                    //if (request.Images != null && request.Images.Count != 0)
                    //{
                    //    request.Images.ForEach(async row =>
                    //    {
                    //        ProductImages iamgeEntity = RequestContext.Mapper.Map<ProductImages>(row);
                    //        iamgeEntity = iamgeEntity.ProductImageId == 0 ?
                    //        await this.RequestContext.Repositories.ProductImageRepository.InsertAsync(iamgeEntity) :
                    //        await this.RequestContext.Repositories.ProductImageRepository.UpdateAsync(iamgeEntity);
                    //       // await this.RequestContext.Repositories.Repository.SaveChangesAsync();
                    //    });
                    //}
                    //if (request.Tags != null && request.Tags.Count != 0)
                    //{
                    //    request.Tags.ForEach(async row =>
                    //    {
                    //        ProductTags tagEntity = RequestContext.Mapper.Map<ProductTags>(row);
                    //        tagEntity = tagEntity.ProductTagId == 0 ?
                    //        await this.RequestContext.Repositories.ProductTagRepository.InsertAsync(tagEntity) :
                    //        await this.RequestContext.Repositories.ProductTagRepository.UpdateAsync(tagEntity);
                    //     //   await this.RequestContext.Repositories.Repository.SaveChangesAsync();
                    //    });
                    //}
                    await this.RequestContext.Repositories.ProductRepository.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
               // request = this.RequestContext.Mapper.Map<BrandModel>(entity);
            }
            return new ResponseBase<ProductModel>()
            {
                Result = errors == null || errors.Count == 0 ? request : null,
                Errors = errors
            };
        }

        public override List<ValidationError> DoValidation(ProductModel request)
        {
            List<ValidationError> errors = new();
            if (request == null)
            {
                errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            else
            {
                if (request.ProductId != 0)
                {
                    if (this.RequestContext.Repositories.ProductRepository.GetById(request.ProductId) == null)
                    {
                        errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("brand_not_exist") });
                    }
                    if (RegularExpressionValidation.Instance.Validate(request.NameAr, RegExResource.NameArRegEx, true) == false)
                    {
                        errors.Add(new ValidationError()
                        {
                            ErrorMessage = this.ValidationMessages.GetString("arabic_name_missing_or_not_valid"),
                        });
                    }
                    if (RegularExpressionValidation.Instance.Validate(request.NameEn, RegExResource.NameEnRegEx, true) == false)
                    {
                        errors.Add(new ValidationError()
                        {
                            ErrorMessage = this.ValidationMessages.GetString("english_name_missing_or_not_valid"),
                        });
                    }
                    if (request.BrandId == 0 
                        || 
                        this.RequestContext.Repositories.BrandRepository.GetById(request.BrandId) == null)
                    {
                        errors.Add(new ValidationError()
                        {
                            ErrorMessage = this.ValidationMessages.GetString("brand_not_exist"),
                        });
                    }
                    if (request.CategoryId == 0
                        ||
                        this.RequestContext.Repositories.CategoryRepository.GetById(request.CategoryId) == null)
                    {
                        errors.Add(new ValidationError()
                        {
                            ErrorMessage = this.ValidationMessages.GetString("category_not_exist"),
                        });
                    }
                }
            }

            return errors;
        }
        #endregion
    }
}
