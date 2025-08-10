using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookStore.MVC.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value) =>
            session.SetString(key, JsonConvert.SerializeObject(value));

        public static T? GetObject<T>(this ISession session, string key)
        {
            var str = session.GetString(key);
            return string.IsNullOrEmpty(str) ? default : JsonConvert.DeserializeObject<T>(str);
        }
    }
}