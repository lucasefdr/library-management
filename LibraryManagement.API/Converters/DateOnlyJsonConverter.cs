using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagement.API.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "dd/MM/yyyy";

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        // Converte a string para DateOnly
        var dateString = reader.GetString();
        return DateOnly.ParseExact(dateString!, DateFormat, CultureInfo.InvariantCulture);
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly value,
        JsonSerializerOptions options)
    {
        // Formata a saída para "dd/MM/yyyy"
        writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
    }
}