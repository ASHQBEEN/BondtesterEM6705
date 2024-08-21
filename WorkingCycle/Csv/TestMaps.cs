using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using DutyCycle.Models.BondTest;
using System.Globalization;

namespace DutyCycle.Csv
{
    public class BreakTestMap : ClassMap<BreakTest>
    {
        public BreakTestMap()
        {
            Map(m => m.Id).Name("№");
            Map(m => m.Name).Name("Тип");
            Map(m => m.Date).Name("Дата");
            Map(m => m.Result).Name("Результат");
            Map(m => m.Values).Name("Показания").TypeConverter<ListConverter>();
            Map(m => m.Terminated).Name("Прерван");
        }
    }

    public class StretchTestMap : ClassMap<StretchTest>
    {
        public StretchTestMap()
        {
            Map(m => m.Id).Name("№");
            Map(m => m.Name).Name("Тип");
            Map(m => m.Date).Name("Дата");
            Map(m => m.Result).Name("Результат");
            Map(m => m.Values).Name("Показания").TypeConverter<ListConverter>();
            Map(m => m.Terminated).Name("Прерван");
            Map(m => m.DelayTimeInSeconds).Name("Задержка до остановки, с");
            Map(m => m.StartPosition).Name("Начало отсчёта, мк");
            Map(m => m.EndPosition).Name("Конец отсчёта, мк");
        }
    }

    public class ShearTestMap : ClassMap<BreakTest>
    {
        public ShearTestMap()
        {
            Map(m => m.Id).Name("№");
            Map(m => m.Name).Name("Тип");
            Map(m => m.Date).Name("Дата");
            Map(m => m.Result).Name("Результат");
            Map(m => m.Values).Name("Показания").TypeConverter<ListConverter>();
            Map(m => m.Terminated).Name("Прерван");
        }
    }


    public class ListConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is List<double> list)
            {
                return string.Join(", ", list.Select(d => d.ToString(CultureInfo.InvariantCulture)));
            }
            return base.ConvertToString(value, row, memberMapData);
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            return text.Split(", ").Select(s => double.Parse(s, culture)).ToList();
        }
    }
}