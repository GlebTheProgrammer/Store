using System;
using Xunit;

namespace Store.Tests
{
    public class BookTests
    {
        [Fact]
        public void IsIsbn_WithNull_ReturnFalse()
        {
            bool actual = Book.IsIsbn(null);  // Если ф-ия IsIsbn запускается с параметром null, то она должна вернуть false

            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithBlankString_ReturnFalse()  // С пустой строкой
        {
            bool actual = Book.IsIsbn("   "); 

            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithInvalidIsbn_ReturnFalse()  // С неверным isbn
        {
            bool actual = Book.IsIsbn("ISBN 123");

            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithIsbn10_ReturnTrue()  // С 10 цифрами в Isbn, с неограниченным кол-вом дефисов и регистром
        {
            bool actual = Book.IsIsbn("IsBn 123-456-789 0");

            Assert.True(actual);
        }

        [Fact]
        public void IsIsbn_WithIsbn13_ReturnTrue()  // Если isbn содержит больше цифр (13) - всё ок
        {
            bool actual = Book.IsIsbn("IsBn 123-456-789 0123"); 

            Assert.True(actual);
        }

        [Fact]
        public void IsIsbn_WithTrashStart_ReturnFalse()
        {
            bool actual = Book.IsIsbn("xxx IsBn 123-456-789 0123 yyy");

            Assert.False(actual);
        }
    }
}
