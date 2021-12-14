using Microsoft.Extensions.Localization;
using Server.BusinessValidation.Validations.RegularExpression;
using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.Brand;
using Server.Model.Interfaces.Context;
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

        public override Task<ResponseBase<BrandModel>> DoProcessAsync(BrandModel request)
        {
            throw new NotImplementedException();
        }

        public override List<ValidationError> DoValidation(BrandModel request)
        {
            List<ValidationError> validationList = new();
            if (request == null)
            {
                validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            else
            {
                if (request.BrandId != 0)
                {
                    if (this.RequestContext.Repositories.BrandRepository.GetById(request.BrandId) == null)
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("brand_not_exist") });
                    }
                }
                if (this.RequestContext.Repositories.CategoryRepository.GetById(request.CategoryId) == null)
                {
                    validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("category_not_exist") });
                }
                if (RegularExpressionValidation.Instance.Validate(request.BrandNameAr, RegExResource.PersonNameArRegEx, true) == false)
                {
                    validationList.Add(new ValidationError()
                    {
                        ErrorMessage = this.ValidationMessages.GetString(""),
                    });
                }
                if (RegularExpressionValidation.Instance.Validate(request.BrandNameEn, RegExResource.PersonNameEnRegEx, true) == false)
                {
                    validationList.Add(new ValidationError()
                    {
                        ErrorMessage = this.ValidationMessages.GetString(""),
                    });
                }
            }
            return validationList;
        }
        #endregion
    }
}
