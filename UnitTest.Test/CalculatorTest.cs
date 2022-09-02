using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.App;

namespace UnitTest.Test
{
    public class CalculatorTest
    {
        public Calculator Calculator { get; set; }
        public Mock<ICalculatorService> MyMock { get; set; }
        public CalculatorTest()
        {
            MyMock = new Mock<ICalculatorService>(); //ICalculatorService'nin impl. ettiği calculatorService'i taklit edicek

            Calculator = new Calculator(MyMock.Object);  //Burada moq üzerinden calculatorService'i taklit edicek bir servis vererek calculatorService hiç çalışmayacak.

            //Calculator = new Calculator(new CalculatorService()); taklitsiz olarak yapıldığında test metodu calculatorService'deki ana metoda giricek ve test sürecini uzatıcak
        }
        [Fact] //test edilecek metodun işaretlenmesi gerekir. Test Explorer'da test edilecek metod eklenir
        public void AddTest()
        {
            #region Assert Methods
            #region Add Metodu için Unit Test
            //Arrange - Değişkenlerin Initialize edilen yerdir
            //int a = 5;
            //int b = 20;

            //Act - Initialize dilen metodu çalıştırdığımız yerdir
            //var total = Calculator.Add(a, b);

            //Assert - Doğrulama aşamasıdır, çıkan sonucu kontrolü sonucu doğrumu? değilmi?
            //Assert.Equal<int>(25, total);

            #endregion
            #region Contain - Does Not Contain Metodu
            //Assert.Contains("Emre", "Emre Gundogdu"); //true
            //Assert.Contains("Emre2", "Emre Gundogdu"); //emre2 "emre gundogdu" içerisinde olmadığından false hata döner
            //Assert.DoesNotContain("Emre", "Emre");  //hata döner çnk: emre değerl emre içerisinde olmaması lazımdı

            //var names = new List<string>()
            //{
            //    "Emre","Hasan","Fatih"
            //};
            //Assert.Contains(names, x => x == "Emre");emre names içerisinde varsa test başarılı


            #endregion
            #region True/False metod
            //Assert.True(7 > 5);
            //Assert.False(7 > 5);
            //Assert.True("".GetType()==typeof(string));
            #endregion
            #region Regex Kodu kontrolü
            //var regEx = "^dog"; dog ile başlayan regex kodu
            //Assert.Matches(regEx, "emre dog"); test başarısız dog ile başlamıyor
            //Assert.DoesNotMatch(regEx, "emre dog"); test başarılı dog ile başlamıyor
            #endregion
            #region StartsWith/EndsWith
            //Assert.StartsWith("Bir", "Bir hikaye daha...");
            //Assert.EndsWith(".", "Hallo.");
            #endregion
            #region Empty/NotEmpty
            //Assert.Empty(new List<string>()); test başarılı çünkü listin içi boş
            //Assert.NotEmpty(new List<string>() { "Emre"}); Test başarılı çünkü listin içi boş değil
            #endregion
            #region InRange/NotInRange
            //Assert.InRange(10, 2, 20); 10 değeri 2 ile 20 arasında mı? Evet arasında ve test başarılı
            //Assert.NotInRange(50, 2, 20); 50 değeri 2 ile 20 arasında mı? Hayır arasında ve test başarısız
            #endregion
            #region Single
            //Assert.Single(new List<string>() { "Emre" }); bu listte sadece bir eleman(single = tekk) olduğu için test başarılıdır
            //Assert.Single<int>(new List<int> { 1, 2, 3 }); Test başarısız single= tek bir değer yok
            #endregion
            #region IsType/IsNotType
            //Assert.IsType<string>("Emre"); test başarılı beklenen değer stringdir
            //Assert.IsNotType<int>("Emre"); test başarısız beklenen değer int değildir
            #endregion
            #region IsAssignableFrom
            //Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>()); //list<string> IEnumerable'den referans aldığı için true
            //Assert.IsAssignableFrom<object>("Emre"); //string değer objeden referans alabildiği için true
            //Assert.IsAssignableFrom<object>(2); //int değer objeden referans alabildiği için true
            #endregion
            #region Nul/NutNull
            //string deger = null;
            //Assert.Null(deger); test başarılı
            //Assert.NotNull(deger); test başarısız
            #endregion
            #region Equal/NotEqual
            //Assert.Equal("Emre", "Emreee");
            //Assert.NotEqual("Emre", "Emreee");
            #endregion
            #endregion
            //Fact: Bu metodun test metodu olduğu ve parametre almadığını belirtmiş oluruz
        }

        //WITHOUT MOCK/MOQ
        [Theory]
        [InlineData(5, 8, 13)]
        public void AddTest2(int a, int b, int expectedTotal)
        {
            //Burada mocklama olmadığı için hata veriyor add metodu | Mocklama Taklit etmeden bu şekilde test edebiliriz tabi constructorda Calculator = new Calculator(new CalculatorService()); bu şekilde olduğu sürece
            var actualTotal = Calculator.Add(a, b);
            Assert.Equal(expectedTotal, actualTotal);
        }
        [Theory]
        [InlineData(5, 8, 13)]
        [InlineData(10, 2, 12)]
        public void Add_simpleValues_ReturnTotalValue(int a, int b, int expectedTotal)
        {
            MyMock.Setup(x => x.Add(a, b)).Returns(expectedTotal);

            var actualTotal = Calculator.Add(a, b);
            Assert.Equal(expectedTotal, actualTotal);
        }
        [Theory]
        [InlineData(0, 0, 0)] //test başarılı çnk: biz burda a değerine 0 b değerine 0 ve sonucu 0 bekledik
        public void Add_zeroValues_ReturnTotalValue(int a, int b, int expectedTotal)
        {
            var actualTotal = Calculator.Add(a, b);
            Assert.Equal(expectedTotal, actualTotal);
            MyMock.Verify(x => x.Add(a, b), Times.Once); //add metodunun 1 kere çalışırsa test başarılı
        }
        [Theory]
        [InlineData(3, 3, 9)]
        public void Multip_SimpleValue_ReturnsMultipValue(int a, int b, int expectedValue)
        {
            //MyMock.Setup(x => x.Multip(a, b)).Returns(expectedValue);
            //MyMock.Setup(x => x.Multip(a, b)).Returns(9); Bunu böyle direkt beklediğimiz sonucu da verebiliriz


            int actualMultip = 0;
            MyMock.Setup(x => x.Multip(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((x, y) => actualMultip = x * y);
            //burada actualMultip =9 oldu a=3,b=3 ile çarpıldı
            Assert.Equal(expectedValue, actualMultip);  //Buradaki kontrol aşamasında expected değeri inlineData'da 9 olarak vermiştik ve actualMultip yukarıdaki callback metodundan 9 olduğu için test başarılı

            //KENDI TEST ORNEGIMIZ SAYILARIMIZ ILE
            //Calculator.Multip(5, 20);
            //Assert.Equal(100, actualMultip);

        }
        [Theory]
        [InlineData(3, 3)]
        public void Multip_ZeroValue_ReturnsException(int a, int b)
        {
            MyMock.Setup(x => x.Multip(a, b)).Throws(new Exception("a=0 olamaz"));
            Exception exception = Assert.Throws<Exception>(() => Calculator.Multip(a, b));
            Assert.Equal("a=0 olamaz", exception.Message);
        }
    }
}
