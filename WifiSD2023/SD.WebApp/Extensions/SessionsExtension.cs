

using System.Text.Json;

namespace SD.WebApp.Extensions
{
    public static class SessionsExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            session.SetString(key, serializedValue);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var serializedValue = session.GetString(key);
            return serializedValue == null ? default : JsonSerializer.Deserialize<T>(serializedValue);
        }
    }
}
