using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Superheroes.Exceptions;

namespace Superheroes.Infrastructure.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = GetActionResult((dynamic)context.Exception);
        }

        private IActionResult GetActionResult(ValidationException validationException)
        {
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            modelStateDictionary.AddModelError(validationException.MemberName, validationException.ErrorMessage);

            return new BadRequestObjectResult(modelStateDictionary);
        }

        private IActionResult GetActionResult(Exception exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}