using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InventoryWeb.ModelBinders
{
    public class InvariantDecimalModelBinder : IModelBinder
    {
        private readonly CultureInfo _culture;

        public InvariantDecimalModelBinder(CultureInfo culture)
        {
            _culture = culture;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var valueAsString = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(valueAsString))
            {
                return Task.CompletedTask;
            }

            var cultureInfo = new CultureInfo("pl-Pl");
            try
            {
                var result = Convert.ToDecimal(valueAsString, cultureInfo);
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (FormatException)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid number format.");
            }

            return Task.CompletedTask;
        }
    }
}
