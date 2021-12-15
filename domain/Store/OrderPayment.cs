using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Реализация паттерна ValueObject - Подход, который не дайт изменять значения объекта после его создания
namespace Store
{
    public class OrderPayment
    {
        public string UniqueCode { get; }

        public string Description { get; }

        public IReadOnlyDictionary<string, string> Parameters { get; }

        public OrderPayment(string uniqueCode, string description,IReadOnlyDictionary<string,string> parameters)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
                throw new ArgumentException(nameof(uniqueCode));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(nameof(description));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            UniqueCode = uniqueCode;
            Description = description;
            Parameters = parameters;
        }
    }
}
