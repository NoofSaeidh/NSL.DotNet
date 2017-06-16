using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DotNet.Attributes {

    public interface IAttributeValue<T> {
        T Value { get; }
    }

    [AttributeUsage(AttributeTargets.Field 
        | AttributeTargets.Property 
        | AttributeTargets.Class 
        | AttributeTargets.Struct 
        | AttributeTargets.Enum
        , Inherited = false, AllowMultiple = false)]
    sealed public class StringValueAttribute : Attribute, IAttributeValue<string> {

        public string Value { get; }

        public StringValueAttribute(string value) {
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.Field
        | AttributeTargets.Property
        | AttributeTargets.Class
        | AttributeTargets.Struct
        | AttributeTargets.Enum
        , Inherited = false, AllowMultiple = false)]
    sealed public class IntValueAttribute : Attribute, IAttributeValue<int> {

        public int Value { get; }

        public IntValueAttribute(int value) {
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.Field
        | AttributeTargets.Property
        | AttributeTargets.Class
        | AttributeTargets.Struct
        | AttributeTargets.Enum
        , Inherited = false, AllowMultiple = false)]
    sealed public class ObjectValueAttribute : Attribute, IAttributeValue<object> {

        public object Value { get; }

        public Type ValueType { get; }

        public ObjectValueAttribute(object value) {
            Value = value;
            ValueType = value.GetType();
        }
    }
}
