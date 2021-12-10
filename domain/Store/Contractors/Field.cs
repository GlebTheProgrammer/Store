using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    // Объект этого класса невозможно создать
    public abstract class Field
    {
        public string Label { get; }

        public string Name { get; }

        public string Value { get; }

        protected Field(string label, string name, string value)
        {
            Label = label;
            Name = name;
            Value = value;
        }
    }

    public class HiddenField : Field
    {
        public HiddenField(string label, string name, string value)
            : base(label,name,value)
        {

        }
    }

    public class SelectionField : Field
    {
        public IReadOnlyDictionary<string,string> Items { get; }

        public SelectionField(string label, string name, string value, IReadOnlyDictionary<string,string> items)
            : base(label,name,value)
        {
            Items = items;
        }
    }
}
