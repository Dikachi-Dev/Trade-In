using System.Text;

namespace TradeIn.Models
{
    public class Number
    {
        private readonly Random _random = new Random();

        public string InitialNumber()
        {
            var idBuilder = new StringBuilder();

            int init = 200;
            // contant account number initial
            idBuilder.Append(init);

            // 8-Digits between 1000 and 9999
            idBuilder.Append(RandomNumber(10000000, 99999999));

            return idBuilder.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
