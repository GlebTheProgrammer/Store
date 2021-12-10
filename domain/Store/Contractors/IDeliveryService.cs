using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public interface IDeliveryService
    {
        string UniqueCode { get; }  // на замену id

        string Title { get; }

        Form CreateForm(Order order);  // создаёт первый экран

        // Если метод возвращает форму, у которой параметр isFinal == true, то на этом показ формул будет преостановлен
        Form MoveNext(int orderId, int step, IReadOnlyDictionary<string, string> values);


    }
}
