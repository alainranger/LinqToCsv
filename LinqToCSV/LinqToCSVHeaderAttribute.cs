using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LinqToCSV
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LinqToCSVHeaderAttribute : Attribute
    {
        public int Order { get; set; }
        public string HeaderName { get; set; }

        public static IComparer<int?> SortOrder()
        {
            return new SortOrderAscendingHelper();
        }

        public class SortOrderAscendingHelper : IComparer<int?>
        {
            public int Compare([AllowNull] int? x, [AllowNull] int? y)
            {
                //if (x == null)
                //    return 1;

                if (x > y || x == null)
                    return 1;

                if (x < y || y == null)
                    return -1;
                else
                    return 0;
            }
        }
    }
}
