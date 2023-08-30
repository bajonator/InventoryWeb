using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InventoryWeb.ModelBinders 
{
    public class InvariantDecimalModelBinderProvider : IModelBinderProvider
    {
        private readonly CultureInfo _culture;

        public InvariantDecimalModelBinderProvider(CultureInfo culture)
        {
            _culture = culture;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
            {
                return new InvariantDecimalModelBinder(_culture);
            }

            return null;
        }
    }
}
