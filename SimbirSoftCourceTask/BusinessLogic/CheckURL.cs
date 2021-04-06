using System;
using System.Text.RegularExpressions;

namespace SimbirSoftGUI.Classes
{
    public static class CheckURL
    {
        /*
         * Данный класс производит проверку введеного URL 
         */

        public static bool CheckInputURL(string inputURL)
        {
            bool resultCheck = Regex.IsMatch(inputURL,
                @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");
            return resultCheck;
        }
    }
}