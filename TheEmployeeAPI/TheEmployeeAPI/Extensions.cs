using Microsoft.AspNetCore.Mvc.ModelBinding;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace TheEmployeeAPI;

public static class Extensions
{
    public static ModelStateDictionary ToModelStateDictionary(this ValidationResult validationResult)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in validationResult.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        return modelState;
    }
}