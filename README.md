# SimbirSoftCourceTask

## 1. Содержимое проекта
Данный проект содержит следующие папки:
1. <b>BusinessLogic</b> - в данной папке находятся три класса:
    1. <b>CheckURL</b> - проверка URL адреса с помощью регулярного выражения;
    2. <b>CounterWord</b> - производит подсчет уникальных слов;
    3. <b>DownloadWebPage</b> - загружает HTML страницу по указанному URL;
2) <b>Models</b> - в данной папке находятся следующие классы:
    1. <b>Words</b> - класс, в котором определенны две переменные класса Word и Count
3) <b>obj</b> - в данной папке находятся объектные или промежуточные файлы, которые представляют собой скомпилированные двоичные файлы, которые еще не были связаны.
4) <b>View</b> - в данной папке находятся следующие классы:
    1. <b>MainWindow</b> - класс, содержащий дизайн (.xaml) и логику (.xaml.cs) в нем происходит связь GUI с основными функциями приложения, для загрузки HTML страницы и для подсчета количества уникальных слов
    1. <b>ResultWindow</b> - класс содержащий дизайн (.xaml) и логику (.xaml.cs) в нем происходит вывод результатов подсчета в новое окно, при условии выбора в MainWindow "Результат в новом окне"
5) <b>bin</b> - содержит двоичные файлы, которые являются фактическим исполняемым кодом для приложения

## 2. Инструкция по запуску
<b>1 Способ</b>
1) Выгрузить код из репозитория
2) В Visual studio или в Rider создать новый проект типа Desktop Application
3) Скопировать папки BusinessLogic, Models, View в свой проект
4) Удалить созданные автоматически файлы App.xaml, MainWindow.xaml
4) Подгрузить через NuGet следующие библиотеки (если потребуется)
    * HtmlAgilityPack;
    * NLog;
    * NLog Config.
5) Задать в NLog.config следующие параметры:
  ```
  <targets>
    <!-- fileName - куда будет сохраняться log -->
    <target name="LogFile" xsi:type="File" fileName="./Logs/LogFile.txt"/>
    <target name="logconsole" xsi:type="Console" />
  </targets>
  <rules>
    <logger name="*" minlevel="Trace" writeTo="LogFile"/>
    <logger name="*" minlevel="Trace" writeTo="logconsole" />
  </rules>
  ```
6) Запустить

<b>2 Способ</b>
1) Скачать репозиторий в виде архива
2) По следующему пути <i>SimbirSoftCourceTask/bin/Debug/net5.0-windows/win-x64</i> в папке <b>win-x64</b> найти файл <b>SimbirSoftCourceTask.exe</b>
3) Запустить файл <b>SimbirSoftCourceTask.exe</b>

## 3. Инструкция по использованию
1) После открытия окна MainWindow ввести в первый TextBox URL адрес в следующем формате "https//apple.com". При некорректном вводе будет появляться соответствующее окно с сообщением о введении корректного URL
2) Далее необходимо указать путь до выгружаемого HTML файла. Нажимаем кнопку <i>Choise a path</i> и выбираем путь.
3) Далее в выпадающем списке выбираем куда мы хотим вывести результат. По умолчанию стоит </i>"Результат в консоль"</i>, но данный вариант сработает при запуске из IDE.
Так же на выбор есть еще два варианта: <i>"Результат в файл"</i> и <i>"Результат в новом окне"</i>. При выборе <i>"Результат в файл"</i> вам будет предложено выбрать имя выходного файла,
после чего результат будет сохранен в него. При выборе <i>"Результат в новом окне"</i> откроется новое окно, в котором отобразиться результат выполнения программы.
4) Нажать <b>Give HTML</b>

## 4. Дополнительная информация
Программа имеет:
  1) Выгрузку HTML документа с указанного по URL ресурса;
  2) Подсчет уникальных слов и вывод результата либо в новое окно, либо запись результата в файл;
  3) Графический интерфейс пользователя;
  4) Реализована обработка исключений;
  5) Логгирование с помощью NLog.
Логгирование производится в .txt файл по следующему пути: <i>SimbirSoftCourceTask/bin/Debug/net5.0-windows/win-x64/Logs</i>

