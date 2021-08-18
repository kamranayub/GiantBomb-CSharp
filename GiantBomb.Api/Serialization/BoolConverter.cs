using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GiantBomb.Api.Serialization
{
    internal class BoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // return literal boolean
            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
            {
                return reader.GetBoolean();
            }

            // string encoded booleans
            if (reader.TokenType == JsonTokenType.String)
            {
                string boolString = reader.GetString();
                if (bool.TryParse(boolString, out bool result))
                {
                    return result;
                }

                return Convert.ToInt32(boolString) != 0;
            }

            // final fallback to number, expected to throw if this doesn't work.
            return Convert.ToInt32(reader.GetInt32()) != 0;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            // we don't need to write anything.
            throw new NotSupportedException();
        }
    }

}
