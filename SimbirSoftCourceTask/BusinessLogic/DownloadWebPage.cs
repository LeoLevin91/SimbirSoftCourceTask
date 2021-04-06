using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NLog;

namespace SimbirSoftGUI.Classes
{
    public class DownloadWebPage
    {
        public string UrlAddress { get;}
        public string FileName { get; set; }

        private HttpClient httpClient= new HttpClient();

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        
        /*
         * Конструктор (string URL, string fileName)
         */
        public DownloadWebPage(string urlAddress, string fileName)
        {
            this.UrlAddress = urlAddress;
            this.FileName = fileName;
        }
        /*
         * Данный метод предназначается для установления соединения с сервером
         */
        public async Task GetHTML()
        {
            try
            {
                HttpResponseMessage result = await httpClient.GetAsync(UrlAddress);
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        await SaveHTML();
                    }
                    catch (IOException e)
                    {
                        Logger.Error($"{e.Message}.");
                        Logger.Trace(e.StackTrace);
                    }
                }
                else
                {
                    Logger.Error($"Status Code Server Ansver: {result.StatusCode}.\n " +
                                 $"Не удалось подключится к серверу");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Error($"{e.Message}.\n" +
                             $"{UrlAddress}: должен быть абсолютным URL или необходимо задать BaseAddress");
                Logger.Trace(e.StackTrace);
            }
            catch (HttpRequestException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Error($"{e.Message}.\n" +
                             $"Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети," +
                             $"ошибки DNS, проверки сертификата сервера или времени ожидания.");
                Logger.Trace(e.StackTrace);
            }
            catch (TaskCanceledException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Error($"{e.Message}.+/n" +
                             $"Не удалось выполнить запрос из-за истечения времени ожидания.");
                Logger.Trace(e.StackTrace);
            }
            finally
            {
                Console.WriteLine("Программа закончила работу");
            }
        }
        
        /*
         * Данный метод предназначается для сохраненияHTML в файл
         */
        private async Task SaveHTML()
        {
            using (StreamWriter streamWriter = new StreamWriter($"{FileName}"))
            {
                string gettedHTML = await httpClient.GetStringAsync(UrlAddress);
                streamWriter.WriteLine(gettedHTML);
            }
        }
    }
}