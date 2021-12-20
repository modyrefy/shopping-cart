using Microsoft.Extensions.Localization;
using Server.BusinessValidation.Validations.RegularExpression;
using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.Brand;
using Server.Model.Dto.Category;
using Server.Model.Interfaces.Context;
using Server.Model.Models;
using Server.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.Category
{
    public class RegisterCategoryProcessor : ProcessorBase<CategoryModel, CategoryModel>
    {
        #region constructor
        public RegisterCategoryProcessor(IRequestContext context) : base(context)
        {
        }
        #endregion
        #region public
        public override ResponseBase<CategoryModel> DoProcess(CategoryModel request)
        {
            throw new NotImplementedException();
        }

        public async override Task<ResponseBase<CategoryModel>> DoProcessAsync(CategoryModel request)
        {
            List<ValidationError> errors = DoValidation(request);
            if (errors == null || errors.Count == 0)
            {
                Categories entity = RequestContext.Mapper.Map<Categories>(request);
                entity = entity.CategoryId == 0 ? await this.RequestContext.Repositories.CategoryRepository.InsertAsync(entity) : await this.RequestContext.Repositories.CategoryRepository.UpdateAsync(entity);
                await this.RequestContext.Repositories.Repository.SaveChangesAsync();
                request = this.RequestContext.Mapper.Map<CategoryModel>(entity);
            }
            return new ResponseBase<CategoryModel>()
            {
                Result = errors == null || errors.Count == 0 ? request : null,
                Errors = errors
            };
        }

        public override List<ValidationError> DoValidation(CategoryModel request)
        {
            List<ValidationError> errors = new();
            if (request == null)
            {
                errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            else
            {
                if (request.CategoryId != 0)
                {
                    if (this.RequestContext.Repositories.CategoryRepository.GetById(request.CategoryId) == null)
                    {
                        errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("category_not_exist") });
                    }
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

            }

            return errors;
        }
        #endregion
    }
}
