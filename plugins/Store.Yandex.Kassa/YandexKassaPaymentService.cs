﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Contractors;
using Store.Web.Contractors;

namespace Store.Yandex.Kassa
{
    public class YandexKassaPaymentService : IPaymentService, IWebContractorService
    {
        public string UniqueCode => "YandexKassa";

        public string Title => "Оплата банковской картой";

        public string GetUri => "/YandexKassa/";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "Оплата картой", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}
