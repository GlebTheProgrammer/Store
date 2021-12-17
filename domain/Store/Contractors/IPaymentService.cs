using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public interface IPaymentService
    {
        string Name { get; }  // на замену id

        string Title { get; }

        Form FirstForm(Order order);  // создаёт первый экран

        // Если метод возвращает форму, у которой параметр isFinal == true, то на этом показ формул будет преостановлен
        Form NextForm(int step, IReadOnlyDictionary<string, string> values);

        OrderPayment GetPayment(Form form);
    }
}
