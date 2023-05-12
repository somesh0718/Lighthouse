using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Igmite.Lighthouse.Platform
{
    public static class HttpHelperExtentions
    {
        public static IDictionary<string, string> GetModelErrors(this ModelStateDictionary modelStates)
        {
            IDictionary<string, string> errors = new Dictionary<string, string>();

            foreach (string stateKey in modelStates.Keys)
            {
                if (modelStates[stateKey].Errors.Count > 0)
                {
                    foreach (ModelError modelError in modelStates[stateKey].Errors)
                    {
                        errors.Add(stateKey, modelError.ErrorMessage);
                    }
                }
            }

            return errors;
        }
    }
}