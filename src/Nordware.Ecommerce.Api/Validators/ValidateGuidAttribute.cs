using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Nordware.Ecommerce.Api.Validators
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly string[] _parameterNames;

        public ValidateGuidAttribute(params string[] parameterNames)
        {
            _parameterNames = parameterNames;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var paramName in _parameterNames)
            {
                if (context.ActionArguments.ContainsKey(paramName))
                {
                    var value = context.ActionArguments[paramName]?.ToString();
                    if (!Guid.TryParse(value, out Guid guidValue) || guidValue == Guid.Empty)
                    {
                        context.Result = new BadRequestObjectResult($"O {paramName} é inválido.");
                        return;
                    }
                }
            }
        }
    }
}
