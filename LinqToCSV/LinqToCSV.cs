using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToCSV
{
    public static class LinqToCSV
    {
        public static string ToCsv<T>(this IEnumerable<T> items, bool withHeader = false)
            where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();

            IOrderedEnumerable<System.Reflection.PropertyInfo> sortedProperties;

            if (properties.Any(p=>p.GetCustomAttributes(typeof(LinqToCSVHeaderAttribute), false).Any()))
            {
                sortedProperties = properties.OrderBy(p => ((LinqToCSVHeaderAttribute[])p.GetCustomAttributes(typeof(LinqToCSVHeaderAttribute), false)).FirstOrDefault()?.Order, LinqToCSVHeaderAttribute.SortOrder());
            }
            else
            {
                sortedProperties = properties.OrderBy(p => ((LinqToCSVHeaderAttribute[])p.GetCustomAttributes(typeof(LinqToCSVHeaderAttribute), false)).FirstOrDefault()?.Order);
            }             

            if (withHeader)
            {
                string header = string.Join(",", sortedProperties.Select(p => $"\"{((LinqToCSVHeaderAttribute[])p.GetCustomAttributes(typeof(LinqToCSVHeaderAttribute), false)).FirstOrDefault()?.HeaderName ?? p.Name}\"").ToArray());

                csvBuilder.AppendLine(header);
            }

            foreach (T item in items)
            {
                string line = string.Join(",", sortedProperties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());

                csvBuilder.AppendLine(line);
            }

            return csvBuilder.ToString();
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item == null)
                return "\"\"";

            if (item is string)
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));

            if (double.TryParse(item.ToString(), out _))
                return string.Format("{0}", item);

            return string.Format("\"{0}\"", item);
        }
    }
}
