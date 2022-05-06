using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Common.Abstract.Helpers
{
    public class NodaTimeAppJsonConverters
    {
        public static JsonConverter<Instant> InstantConverter { get; } = new InstantConverter();
        public static JsonConverter<LocalDateTime> LocalDateTimeConverter { get; } = new LocalDateTimeConverter();
    }

    class InstantConverter : JsonConverter<Instant>
    {
        static readonly JsonConverter<Instant> _defaultInstantConverter = NodaConverters.InstantConverter;
        static readonly JsonConverter<LocalDateTime> _defaulLocalDateTimeConverter = NodaConverters.LocalDateTimeConverter;
        static readonly JsonConverter<OffsetDateTime> _defaultOffsetConverter = NodaConverters.OffsetDateTimeConverter;

        public override Instant Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (str == null) throw new ApplicationException();

            if (str.Contains("+"))
            {
                // Todo: Check offset ok or adjust
                var odt = _defaultOffsetConverter.Read(ref reader, typeToConvert, options);
                return odt.ToInstant();
            }

            if (!str.EndsWith("z", StringComparison.OrdinalIgnoreCase))
            {
                var ldt = _defaulLocalDateTimeConverter.Read(ref reader, typeToConvert, options);
                return ldt.InZoneLeniently(NodaTimeHelper.ApplicationTimeZone).ToInstant();
            }

            return _defaultInstantConverter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, Instant value, JsonSerializerOptions options)
        {
            _defaultInstantConverter.Write(writer, value, options);
        }
    }

    class LocalDateTimeConverter : JsonConverter<LocalDateTime>
    {
        static readonly JsonConverter<Instant> _defaultInstantConverter = NodaConverters.InstantConverter;
        static readonly JsonConverter<OffsetDateTime> _defaultOffsetConverter = NodaConverters.OffsetDateTimeConverter;
        static readonly JsonConverter<LocalDateTime> _defaulLocalDateTimeConverter = NodaConverters.LocalDateTimeConverter;

        public override LocalDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (str == null) throw new ApplicationException();

            if (str.Contains("+"))
            {
                // Todo: Check offset ok or adjust
                var odt = _defaultOffsetConverter.Read(ref reader, typeToConvert, options);
                return odt.LocalDateTime;
            }

            if (str.EndsWith("z", StringComparison.OrdinalIgnoreCase))
            {
                var instant = _defaultInstantConverter.Read(ref reader, typeToConvert, options);
                return instant.InZone(NodaTimeHelper.ApplicationTimeZone).LocalDateTime;
            }

            return _defaulLocalDateTimeConverter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, LocalDateTime value, JsonSerializerOptions options)
        {
            _defaulLocalDateTimeConverter.Write(writer, value, options);
        }
    }
}
