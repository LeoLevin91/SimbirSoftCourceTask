using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using HtmlAgilityPack;
using NLog;

namespace SimbirSoftGUI.Classes
{
    public class CounterWord
    {
        public string Filepath { get; set; }
        private readonly char[] _splitSymbols = {'\t', '\r', '\n', ' ', ',', '.', '!', '?', '\"',';',':', '[', ']', '(', ')'};
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CounterWord(string filepath)
        {
            Filepath = filepath;
        }

        private IOrderedEnumerable<KeyValuePair<string, int>> ParseHTML()
        {
            try
            {
                // Создадим объект документа
                var htmlDoc = new HtmlDocument();
                // загрузим документ для работы
                htmlDoc.Load(Filepath);
                // Получим сожержимое тегов внутри body
                var body = htmlDoc.DocumentNode.SelectSingleNode("//body").InnerText;

                // разбить строку по заданным разделителям
                string[] splitBodyContent = body.Split(_splitSymbols, StringSplitOptions.RemoveEmptyEntries);
                // Удалить из массива пустые строки и числа с помощью LINQ
                splitBodyContent = splitBodyContent.Where(x => !string.IsNullOrEmpty(x) && !int.TryParse(x, out _))
                    .ToArray();
                // Установим все буквы в каждом слове в верхний регистр с помощью LINQ
                splitBodyContent = splitBodyContent.Select(item => item.ToUpper()).ToArray();

                // Подсчет уникальных слов
                Dictionary<string, int> counts = splitBodyContent.GroupBy(x => x).ToDictionary(g => g.Key,
                    g => g.Count()
                );

                // Отсортируем словарь
                var cc = counts.OrderByDescending(pair => pair.Value);

                // Возвращаем значение
                return cc;
            }
            catch (Exception e)
            {
                // Логирование
                Logger.Error($"{e.Message}.");
                Logger.Trace(e.StackTrace);
                MessageBox.Show($"{e.Message}", "Word Processor", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                Logger.Info("Завершение работы программы");
            }
            return null;
        }

        public IOrderedEnumerable<KeyValuePair<string, int>> ParseHTMLToWindow()
        {
            try
            {
                var dict = ParseHTML();
                return dict;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                Logger.Trace(e.StackTrace);
            }

            return null;
        }

        public void ParseHTMLToConsole()
        {
            try
            {
                var dict = ParseHTML();
                foreach (var item in dict)
                {
                    Console.WriteLine($"{item.Key} = {item.Value}");
                }
            } catch (Exception e)
            {
                Logger.Error(e.Message);
                Logger.Trace(e.StackTrace);
            }
            
        }

        public void ParseHTMLToFile(string filePath)
        {
            try
            {
                //Сохранение в файл
                using (StreamWriter fstream = new StreamWriter(filePath))
                {
                    var dict = ParseHTML();
                    foreach (var entry in dict)
                    {
                        fstream.Write($"{entry.Key} = {entry.Value}\n");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                Logger.Trace(e.StackTrace);
            }
            
        }
    }
}