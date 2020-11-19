using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Extensions
{
    public static class TempDataExtension
    {

        /// <summary>
        /// Sets an object into the TempData by first serializing it to JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this ITempDataDictionary tempData,
                                  string key,
                                  T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Gets an object from the TempData by deserializing it from JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this ITempDataDictionary tempData,
                               string key) where T : class, new()
        {
            tempData.TryGetValue(key, out var o);
            return o == null ? new T() : JsonConvert.DeserializeObject<T>((string) o);
        }

    }
}