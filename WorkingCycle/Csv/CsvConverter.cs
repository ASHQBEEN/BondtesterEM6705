using CsvHelper;
using CsvHelper.Configuration;
using DutyCycle.Models.BondTest;
using System.Globalization;

namespace DutyCycle.Csv
{
    public static class CsvConverter
    {
        public static void SaveToCsv(List<BondTest> tests)
        {
            try
            {
                if (tests.Count == 0) return;
                var firstTest = tests.First();
                string filePath = $"{DateTime.Now:dd-MM-yy HH.mm.ss} {firstTest.Name}.csv";
                using StreamWriter writer = new(filePath, false, System.Text.Encoding.UTF8);
                var culture = new CultureInfo("en-US");
                var config = new CsvConfiguration(culture)
                {
                    Delimiter = "; "
                };
                using var csv = new CsvWriter(writer, config);
                if (firstTest is BreakTest)
                {
                    csv.Context.RegisterClassMap<BreakTestMap>();
                    csv.WriteHeader<BreakTest>();
                    csv.NextRecord();
                    foreach (var test in tests)
                    {
                        csv.WriteRecord(test as BreakTest);
                        csv.NextRecord();
                    }
                    MessageBox.Show("Тесты на Разрыв успешно сохранены!");
                }
                else if (firstTest is StretchTest)
                {
                    csv.Context.RegisterClassMap<StretchTestMap>();
                    csv.WriteHeader<StretchTest>();
                    csv.NextRecord();
                    foreach (var test in tests)
                    {
                        csv.WriteRecord(test as StretchTest);
                        csv.NextRecord();
                    }
                    MessageBox.Show("Тесты на Растяжение успешно сохранены!");
                }
                else if (firstTest is ShearTest)
                {
                    csv.Context.RegisterClassMap<ShearTestMap>();
                    csv.WriteHeader<ShearTest>();
                    csv.NextRecord();
                    foreach (var test in tests)
                    {
                        csv.WriteRecord(test as ShearTest);
                        csv.NextRecord();
                    }
                    MessageBox.Show("Тесты на Сдвиг успешно сохранены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         }
        public static List<BondTest> LoadFromCsv(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, // Установите в true, если ваш файл CSV содержит заголовок
                Delimiter = "; "
            };

            using var reader = new StreamReader(filePath);
            using var csvCheck = new CsvReader(reader, config);

            csvCheck.Read();
            csvCheck.Read();
            var firstTestName = csvCheck.GetField(1);
            reader.Close();

            using var fieldsReader = new StreamReader(filePath);
            using var csv = new CsvReader(fieldsReader, config);

            List<BondTest> result;
            if (firstTestName == "Разрыв")
            {
                csv.Context.RegisterClassMap<BreakTestMap>();
                var list = csv.GetRecords<BreakTest>().ToList();
                result = list.Select(x => (BondTest)x).ToList();
            }
            else if (firstTestName == "Растяжение")
            {
                csv.Context.RegisterClassMap<StretchTestMap>();
                var list = csv.GetRecords<StretchTest>().ToList();
                result = list.Select(x => (BondTest)x).ToList();
            }
            else if (firstTestName == "Сдвиг")
            {
                csv.Context.RegisterClassMap<ShearTestMap>();
                var list = csv.GetRecords<ShearTest>().ToList();
                result = list.Select(x => (BondTest)x).ToList();
            }
            else
                throw new Exception("Тип теста из загружаемого файла не распознан.");

            MessageBox.Show($"Тесты на {firstTestName} успешно загружены!");
            return result;
        }
    }
}
