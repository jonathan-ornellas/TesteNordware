using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace Nordware.Ecommerce.Api.Helps
{
    public class DisplayNameEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumText = reader.GetString();

            foreach (var field in typeof(TEnum).GetFields())
            {
                if (field.GetCustomAttribute<DisplayAttribute>()?.Name == enumText)
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            if (Enum.TryParse<TEnum>(enumText, out var value))
            {
                return value;
            }

            throw new JsonException($"Não é possível converter \"{enumText}\"para enum \"{typeof(TEnum)}\"");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            var field = typeof(TEnum).GetField(value.ToString());
            var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
            var name = displayAttribute?.Name ?? value.ToString();
            writer.WriteStringValue(name);
        }
    }
}
