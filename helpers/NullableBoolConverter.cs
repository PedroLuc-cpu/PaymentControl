using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace PaymentControl.helpers
{
    public class NullableBoolConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text = text.Trim().ToLower();

            if (text == "t" || text == "true" || text == "1")
            {
                return true;
            }

            if (text == "f" || text == "false" || text == "0")
            {
                return false;
            }

            throw new TypeConverterException(this, memberMapData, text, row.Context, "Cannot convert value to nullable bool.");
        }
    }
}