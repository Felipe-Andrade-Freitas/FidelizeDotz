using System;
using System.Globalization;

namespace FidelizeDotz.Services.Api.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static bool ToBoolean(this string source)
        {
            return Convert.ToBoolean(source ?? bool.FalseString);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string FormatString(this decimal texto, int maxLength)
        {
            return texto.FormatValor().FormatString(maxLength);
        }

        public static string FormatString(this DateTime texto, int maxLength)
        {
            return $"{texto:yyyy-MM-dd}".FormatString(maxLength);
        }

        public static string FormatString(this DateTime? texto, int maxLength)
        {
            return $"{texto:yyyy-MM-dd}".FormatString(maxLength);
        }

        public static string FormatString(this object texto, int maxLength)
        {
            var str = texto.ToString();
            var maxSubstring = str.Length <= maxLength ? str.Length : maxLength;
            return str?.Substring(0, maxSubstring).PadRight(maxLength, ' ');
        }

        public static string FormatValor(this decimal valor)
        {
            return valor.ToString("F", CultureInfo.InvariantCulture).Replace(".", "");
        }

        public static string FormatLeitura(this string texto, int initial, int final = 0)
        {
            return final == 0 ? texto.Substring(initial).TrimEnd() : texto.Substring(initial, final).TrimEnd();
        }
    }
}