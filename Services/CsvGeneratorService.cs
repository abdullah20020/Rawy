//using CsvHelper;
//using System;
//using System.Collections.Generic;
//using System.Formats.Asn1;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services
//{
//    public class CsvGeneratorService
//    {
//        public MemoryStream GenerateCsvInMemory<T>(IEnumerable<T> records)
//        {
//            var memoryStream = new MemoryStream();
//            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
//            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
//            {
//                csvWriter.WriteRecords(records);
//            }
//            memoryStream.Position = 0;
//            return memoryStream;
//        }

//        public async Task SendCsvToPythonServerAsync(MemoryStream csvStream)
//        {
//            using var client = new HttpClient();

//            var content = new MultipartFormDataContent();
//            csvStream.Position = 0;
//            var fileContent = new StreamContent(csvStream);
//            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");

//            content.Add(fileContent, "file", "user_interests.csv");
//            var response = await client.PostAsync("/upload", content);


//            if (response.IsSuccessStatusCode)
//            {
//                Console.WriteLine("CSV uploaded successfully to Python server!");
//            }
//            else
//            {
//                Console.WriteLine($"Failed to upload CSV. Status: {response.StatusCode}");
//            }
//        }
//    }
//}
