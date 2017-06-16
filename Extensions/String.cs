using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DotNet.Extensions {
    public static class StringExtensions {
        public static string ToFormat(this string value, params object[] args) => string.Format(value, args);
        public static string ToFormat(this string value, object arg0) => string.Format(value, arg0);
    }
}
