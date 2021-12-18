using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Store.Tests
{
    public class OrderTests
    {

        [Fact]
        public void TotalCount_WithEmptyItems_ReturnZero()
        {
            var order = CreateEmptyTestOrder();

            Assert.Equal(0, order.TotalCount);
        }

        private static Order CreateEmptyTestOrder()
        {
            return new Order(new OrderDto
            {
                Id = 1,
                Items = new OrderItemDto[0]
            });
        }

        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnZero()
        {
            var order = CreateEmptyTestOrder();

            Assert.Equal(0m, order.TotalCount);
        }

        private static Order CreateTestOrder()
        {
            return new Order(new OrderDto
            {
                Id = 1,
                Items = new[]
                {
                    new OrderItemDto { BookId = 1, Price = 10m, Count = 3},
                    new OrderItemDto { BookId = 2, Price = 100m, Count = 5},
                }
            });
        }

        [Fact]
        public void TotalCount_WithNonEmptyItems_CalculatesTotalCount()
        {
            var order = CreateTestOrder();

            Assert.Equal(3 + 5, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithNonEmptyItems_CalculatesTotalPrice()
        {
            var order = CreateTestOrder();

            Assert.Equal(3 * 10m + 5 * 100m, order.TotalPrice);
        }
    }
}
