using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPI_1.Helpers
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            string value = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName)
                .ToString();

            if (string.IsNullOrEmpty(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var elementtype = bindingContext.ModelType.GenericTypeArguments[0];
            var converter= TypeDescriptor.GetConverter(elementtype);
            var values = value
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();
            var typedvalues = Array.CreateInstance(elementtype, values.Length);
            values.CopyTo(typedvalues, 0);
            bindingContext.Model = typedvalues;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;

        }
    }
}
