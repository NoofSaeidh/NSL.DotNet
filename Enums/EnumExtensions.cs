using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSL.DotNet.Attributes;

namespace NSL.DotNet.Enums {
    public static class EnumExtensions {

        private static void CheckIsEnum<T>(bool withFlags) {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeof(T).FullName));
            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute", typeof(T).FullName));
        }

        public static bool IsFlagSet<T>(this T value, T flag) where T : struct, IConvertible {
            CheckIsEnum<T>(true);
            var lValue = Convert.ToInt64(value);
            var lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

        public static IEnumerable<T> GetFlags<T>(this T value) where T : struct, IConvertible {
            CheckIsEnum<T>(true);
            foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>()) {
                if (value.IsFlagSet(flag))
                    yield return flag;
            }
        }

        public static T SetFlags<T>(this T value, T flags, bool on) where T : struct, IConvertible {
            CheckIsEnum<T>(true);
            var lValue = Convert.ToInt64(value);
            var lFlag = Convert.ToInt64(flags);
            if (on) {
                lValue |= lFlag;
            }
            else {
                lValue &= (~lFlag);
            }
            return (T)Enum.ToObject(typeof(T), lValue);
        }

        public static T SetFlags<T>(this T value, T flags) where T : struct, IConvertible {
            return value.SetFlags(flags, true);
        }

        public static T ClearFlags<T>(this T value, T flags) where T : struct, IConvertible {
            return value.SetFlags(flags, false);
        }

        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct, IConvertible {
            CheckIsEnum<T>(true);
            long lValue = 0;
            foreach (T flag in flags) {
                var lFlag = Convert.ToInt64(flag);
                lValue |= lFlag;
            }
            return (T)Enum.ToObject(typeof(T), lValue);
        }

        public static string GetStringValue<T>(this T value) where T : struct, IConvertible {
            CheckIsEnum<T>(false);
            var name = Enum.GetName(typeof(T), value);
            if (name != null) {
                var field = typeof(T).GetField(name);
                if (field != null) {
                    if (Attribute.GetCustomAttribute(field, typeof(StringValueAttribute)) is StringValueAttribute attr) {
                        return attr.Value;
                    }
                }
            }
            return null;
        }

        public static int? GetIntValue<T>(this T value) where T : struct, IConvertible {
            CheckIsEnum<T>(false);
            var name = Enum.GetName(typeof(T), value);
            if (name != null) {
                var field = typeof(T).GetField(name);
                if (field != null) {
                    if (Attribute.GetCustomAttribute(field, typeof(IntValueAttribute)) is IntValueAttribute attr) {
                        return attr.Value;
                    }
                }
            }
            return null;
        }
    }
}