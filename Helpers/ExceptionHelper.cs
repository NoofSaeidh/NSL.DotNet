using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DotNet.Helpers {
    public static class ExceptionHelper {
        public static void CheckNull(object arg, string argName) {
            if (arg == null) throw new ArgumentNullException(argName);
        }
        public static void CheckNull(object arg, string argName, string message) {
            if (arg == null) throw new ArgumentNullException(argName, message);
        }
        public static void CheckNullOrEmpty<T>(IEnumerable<T> arg, string argName) {
            CheckNull(arg, argName);

            if (!arg.Any()) throw new ArgumentException($"Collection is empty", argName);
        }
        public static void CheckNullOrEmpty<T>(IEnumerable<T> arg, string argName, string nullCheckMessage) {
            CheckNull(arg, argName, nullCheckMessage);

            if (!arg.Any()) throw new ArgumentException($"Collection is empty", argName);
        }

        public static void CheckRange(int value, string valueName, int min, int max) {
            if (value < min || value > max) throw new ArgumentOutOfRangeException(valueName);
            //TODO: messages
            //if (value > max) throw new ArgumentOutOfRangeException()
        }
    }
}
