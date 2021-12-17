using System.Collections.Generic;

namespace Store.Contractors
{
    public interface IDeliveryService
    {
        string Name { get; }  // на замену id

        string Title { get; }

        Form FirstForm(Order order);  // создаёт первый экран

        // Если метод возвращает форму, у которой параметр isFinal == true, то на этом показ формул будет преостановлен
        Form NextForm(int step, IReadOnlyDictionary<string, string> values);

        OrderDelivery GetDelivery(Form form);
    }
}
