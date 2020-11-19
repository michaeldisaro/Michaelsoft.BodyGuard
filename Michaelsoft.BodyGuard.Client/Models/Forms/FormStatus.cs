using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class FormStatus
    {

        public Dictionary<string, string> Values = new Dictionary<string, string>();

        public Dictionary<string, string> Errors = new Dictionary<string, string>();

        public FormStatus()
        {

        }

        public FormStatus(ModelStateDictionary modelState)
        {
            var count = modelState.Count;
            var keys = modelState.Keys.ToArray();
            var values = modelState.Values.ToArray();
            for (var i = 0; i < count; i++)
            {
                Values.Add(keys[i], values[i].AttemptedValue);
                if (values[i].Errors.Any())
                    Errors.Add(keys[i], values[i].Errors.First().ErrorMessage);
            }
        }

        public string GetAttemptedValue(string key)
        {
            Values.TryGetValue(key, out var o);
            return o;
        }

        public string GetError(string key)
        {
            Errors.TryGetValue(key, out var o);
            return o;
        }

    }
}