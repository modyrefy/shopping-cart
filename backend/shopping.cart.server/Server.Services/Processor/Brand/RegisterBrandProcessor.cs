using Microsoft.Extensions.Localization;
using Server.BusinessValidation.Validations.RegularExpression;
using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.Brand;
using Server.Model.Interfaces.Context;
using Server.Model.Models;
using Server.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.Brand
{
    public class RegisterBrandProcessor : ProcessorBase<BrandModel, BrandModel>
    {
        #region constructor
        public RegisterBrandProcessor(IRequestContext context) : base(context)
        {
        }
        #endregion
        #region public
        public override ResponseBase<BrandModel> DoProcess(BrandModel request)
        {
            throw new NotImplementedException();
        }

        public override async Task<ResponseBase<BrandModel>> DoProcessAsync(BrandModel request)
        {
            List<ValidationError> errors = DoValidation(request);
            if (errors == null || errors.Count == 0)
            {
                Brands entity = RequestContext.Mapper.Map<Brands>(request);

                entity =  entity.BrandId==-0? await this.RequestContext.Repositories.BrandRepository.InsertAsync(entity): await this.RequestContext.Repositories.BrandRepository.UpdateAsync(entity);
                await this.RequestContext.Repositories.Repository.SaveChangesAsync();
                request = this.RequestContext.Mapper.Map<BrandModel>(entity);
            }
            return new ResponseBase<BrandModel>()
            {
                Result =errors==null|| errors.Count==0? request:null,
                Errors = errors
            };
        }

        public override List<ValidationError> DoValidation(BrandModel request)
        {
            List<ValidationError> errors = new();
            if (request == null)
            {
                errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            else
            {
                if (request.BrandId != 0)
                {
                    if (this.RequestContext.Repositories.BrandRepository.GetById(request.BrandId) == null)
                    {
                        errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("brand_not_exist") });
                    }
                }
                if (this.RequestContext.Repositories.CategoryRepository.GetById(request.CategoryId) == null)
                {
                    errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("category_not_exist") });
                }
                if (RegularExpressionValidation.Instance.Validate(request.NameAr, RegExResource.PersonNameArRegEx, true) == false)
                {
                    errors.Add(new ValidationError()
                    {
                        ErrorMessage = this.ValidationMessages.GetString("arabic_name_missing_or_not_valid"),
                    });
                }
                if (RegularExpressionValidation.Instance.Validate(request.NameEn, RegExResource.PersonNameEnRegEx, true) == false)
                {
                    errors.Add(new ValidationError()
                    {
                        ErrorMessage = this.ValidationMessages.GetString("english_name_missing_or_not_valid"),
                    });
                }
            }
            return errors;
        }
        #endregion
    }
}
