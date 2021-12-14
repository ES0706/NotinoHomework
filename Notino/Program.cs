using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Notino
{
    //class Program
    //{
    //    static async Task Main(string[] args)
    //    {
    //        var sourcePath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\Document1.xml");
    //        var targetPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\Document1.json");

    //        var converterProvider = new XmlToJsonConverterProvider();
    //        var writerProvider = new FileWriterProvider();
    //        var readerProvider = new FileReaderProvider();
    //        var converterService = new ConverterService(converterProvider, writerProvider, readerProvider);

    //        var convertResult = await converterService.ConvertSourceToTarget(sourcePath, targetPath);
    //        if (convertResult.Failed)
    //            Console.WriteLine(convertResult.Errors);
    //    }
    //}


    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
