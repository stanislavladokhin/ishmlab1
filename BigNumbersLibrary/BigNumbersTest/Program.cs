using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigNumbersLibrary;

namespace BigNumbersTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //BigNumber a = new BigNumber(new List<sbyte>() { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1 });
            BigNumber a = new BigNumber(new List<sbyte>() { 1,0 , 1, 1 });
            BigNumber b = new BigNumber(new List<sbyte>() { 1, 0, 1, 1, 0, 1, 1 });
            BigNumber n = new BigNumber(new List<sbyte>() { 1, 1, 0, 1, 1, 0, 1, 1, 0, 1 });
            //BigNumber p = new BigNumber(new List<sbyte>() { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            BigNumber p = new BigNumber(new List<sbyte>() { 1, 0, 1 });

           // p.SetRandomNumber(1024,1);
            long number=10;
            Console.WriteLine(BigNumber.ConvertToBin(number));
            
            //number2.SetRandomNumber(16);
            //System.Threading.Thread.Sleep(DateTime.Now.Millisecond);
            //number3.SetRandomNumber(2048);
            Console.WriteLine(p);
            Console.WriteLine(p.ToDecString());
           // Console.WriteLine((a*BigNumber.Pow(number1,number2,number3))%p);// / number2) + number1 % number2);
            //Console.WriteLine(number2);
            //var a = number1 - number2;
            //Console.WriteLine(BigNumber.Pow(a, b, n));
            //Console.WriteLine((a * b) % n);
            //Console.WriteLine((BigNumber.GetReverseElement(a, p)));
            
            //Console.WriteLine(BigNumber.Mod(number1, number2));
        }
    }
}
