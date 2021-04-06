using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SimbirSoftGUI.Classes;

namespace SimbirSoftGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private string FilePath { get; set; }
        private string CurrentUrl { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        // Обработка события "Нажатие на кнопку"
        private async void GH_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentUrl = InputURL.Text;
            if (InputURL.Text == "")
            {
                MessageBox.Show("Введите URL адресс", "Word Processor", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                // Проверка URL адреса
                if (CheckURL.CheckInputURL(CurrentUrl))
                {
                    if (InputPath.Text == "")
                    {
                        MessageBox.Show("Введите имя сохраняемого файла", "Word Processor", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    else
                    {
                        try
                        {
                            DownloadWebPage downloadWebPage = new DownloadWebPage(CurrentUrl, FilePath);
                            await downloadWebPage.GetHTML();

                            CounterWord counterWord = new CounterWord(@$"{FilePath}");

                            switch (ComboBox.Text)
                            {
                                case "Результат в консоль":
                                    counterWord.ParseHTMLToConsole();
                                    SuccessfulFinishWork();
                                    break;
                                case "Результат в новом окне":
                                    Result_window resultWindow = new Result_window(counterWord.ParseHTMLToWindow());
                                    resultWindow.Show();
                                    SuccessfulFinishWork();
                                    break;
                                case "Результат в файл":
                                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                                    if (saveFileDialog.ShowDialog() == true)
                                    {
                                        String LocalFilePath = saveFileDialog.FileName;
                                        // Сохранить файл
                                        counterWord.ParseHTMLToFile(LocalFilePath);
                                        SuccessfulFinishWork();
                                    }
                                    break;
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show($"{exception.Message}", "Word Processor", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Введите корректный URL адресс", "Word Processor", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void CP_Button_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалоговый файл
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            // Запись только .txt
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Стандартный путь
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveFileDialog.ShowDialog() == true)
            {
                // Вернуть путь до файла
                FilePath = saveFileDialog.FileName;
                // Вывести в текстовое поле путь до файла
                InputPath.Text = FilePath;
            }
        }

        private void SuccessfulFinishWork()
        {
            MessageBox.Show($"Программа успешно завершила работу", "Word Processor", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void InputPath_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (InputURL.Text == "Введите URL")
            {
                InputURL.Text = "";
            }
        }
        private void InputPath_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (InputURL.Text == "")
            {
                InputURL.Text = "Введите URL";
            }
        }

        
    }
}