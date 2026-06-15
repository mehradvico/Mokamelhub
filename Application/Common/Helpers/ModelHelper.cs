using Application.Common.Dto.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.Common.Helpers
{
    public class ModelHelper<TDto>
    {
        public static BaseResultDto<TDto> ModelErrors(TDto dto)
        {
            var errors = new List<ValidationResult>();
            var context = new ValidationContext(dto);
            bool validate = Validator.TryValidateObject(dto, context, errors);
            var messages = new List<Tuple<string, string>>();
            if (!validate)
            {
                foreach (var error in errors)
                {
                    messages.Add(new Tuple<string, string>(error.ErrorMessage, error.MemberNames.Any() ? error.MemberNames.First() : ""));
                }
            }
            return new BaseResultDto<TDto>(isSuccess: validate, messages: messages, dto);

        }
    }
}
