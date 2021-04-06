using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NLog;
using SimbirSoftGUI.Models;

namespace SimbirSoftGUI
{
    public partial class Result_window : Window
    {
        public Result_window(IOrderedEnumerable<KeyValuePair<string, int>> parseHtmlToWindow)
        {
            InitializeComponent();
            List<Words> a = ShowData(parseHtmlToWindow);
            ListViewResultWindow.ItemsSource = a;
        }

        private List<Words> ShowData(IOrderedEnumerable<KeyValuePair<string, int>> parseHtmlToWindow)
        {
            List<Words> wordsList = new List<Words>();

            // Вывод на экран
            foreach (var item in parseHtmlToWindow)
            {
                wordsList.Add(
                    new Words(
                        item.Key,
                        item.Value
                    )
                );
            }

            return wordsList;
        }
    }
}