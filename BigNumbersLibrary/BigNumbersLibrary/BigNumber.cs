using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigNumbersLibrary
{
    /// <summary>
    /// Очень длинное число
    /// </summary>
    public class BigNumber
    {
        /// <summary>
        /// Приватный список, в котором хранится все число поразрядно
        /// </summary>
        private List<sbyte> numbers;
        /// <summary>
        ///Свойство инкапсулирующее список, в котором хранится все число поразрядно
        /// </summary>
        private List<sbyte> Number
        {
            get 
            {
                //int index = numbers.FindIndex((num) => num == 1);
                //numbers = new List<sbyte>(numbers.Skip(index));
                return numbers; 
            }
            set
            {
                List<sbyte> number = new List<sbyte>();
                try
                {

                    int index = value.FindIndex((num) => num == 1);
                    value = new List<sbyte>(value.Skip(index));
                    
                }
                catch(Exception ex)
                {
                   // Number = new List<sbyte>(value);
                }
                numbers = value;

            }
        }
        /// <summary>
        /// Длина числа
        /// </summary>
        private int Length { get { return Number.Count; } }
        /// <summary>
        /// Знак числа
        /// </summary>
        private bool isPositive { get; set; }

        private static BigNumber Null { get { return new BigNumber(new List<sbyte>() { 0 }); } }

        private static BigNumber One { get { return new BigNumber(new List<sbyte>() { 1 }); } }

        private static BigNumber Two { get { return new BigNumber(new List<sbyte>() { 1, 0 }); } }
        /// <summary>
        /// Конструктор
        /// </summary>
        public BigNumber() { Number = new List<sbyte>(); isPositive = true; }
        /// <summary>
        /// Конструктор с числом на входе
        /// </summary>
        /// <param name="number"></param>
        public BigNumber(List<sbyte> number)
        {
            Number = new List<sbyte>(number);
            isPositive = true;
        }
        /// <summary>
        ///  Конструктор с числом и его знаком на входе
        /// </summary>
        /// <param name="number"></param>
        /// <param name="positive"></param>
        public BigNumber(List<sbyte> number, bool positive)
        {
            Number = new List<sbyte>(number);
            isPositive = positive;
        }
        /// <summary>
        /// Задание "длинного" числа определенной длины.
        /// </summary>
        /// <param name="length">Длина числа</param>
        public void SetRandomNumber(int length, int k)
        {
            Random randomizer = new Random((int)DateTime.Now.Ticks);

            bool newNumberFlag = true;
            bool isPrimeFlag = true;

            while (newNumberFlag)
            {
                isPrimeFlag = true;
                Number = new List<sbyte>() { 1 };
                for (int i = 1; i < length - 1; i++)
                    Number.Add((sbyte)randomizer.Next(2));
                Number.Add(1);
                //проверка
                for (int i = 0; i < k; i++)
                {
                    BigNumber A = GetRandomA(1, Length);
                    BigNumber NMinusOne = this - One;
                    if (Pow(A, NMinusOne, this) != One)
                    {
                        isPrimeFlag = false;
                        break;
                    }
                    else
                    {
                        BigNumber T = new BigNumber(NMinusOne.Number);
                        int r = 0;

                        do
                        {
                            T = T / Two;
                            r++;
                        }//2^r*t
                        while (T.Number[T.Number.Count - 1] != 1);

                        if (Pow(A, T, this) == One)
                            continue;
                        else
                        {
                            for (int j = 1; j < r; j++)
                            {
                                T = T * Two;
                                if (Pow(A, T, this) == One)
                                {
                                    isPrimeFlag = false;
                                    break;//ToNewNumber
                                }
                                else if (Pow(A, T, this) == this - One)
                                {
                                    break;//ToNewA
                                }
                            }
                            if (!isPrimeFlag)
                                break;
                        }
                    }
                }
                if (isPrimeFlag)
                    break;
            }      
        }
        public BigNumber GetRandomA(int minLength, int maxLength)
        {
            Random randomizer = new Random((int)DateTime.Now.Ticks);

            int ALength = randomizer.Next(minLength, maxLength);
            BigNumber A = new BigNumber(new List<sbyte>() { 1 });
            for (int i = 0; i < ALength - 2; i++)
                A.Number.Add((sbyte)randomizer.Next(2));
            A.Number.Add(1);

            return A;
        }

        /// <summary>
        /// Задание определенного "длинного" числа
        /// </summary>
        /// <param name="number">"Длинное" число</param>
        public void SetNumber(List<sbyte> number)
        { Number = number; }
        /// <summary>
        /// Функция перевода "длинного" числа в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder number = new StringBuilder();
            foreach (sbyte num in Number)
                number.Append(num);
            return number.ToString();
        }

        public static BigNumber ConvertToBin(long number)
        {
            BigNumber NewNumber = new BigNumber();
            if (number!=1)
            do
            {
                NewNumber.Number.Insert(0, (sbyte)(number % 2));
                number = number / 2;             
            }
            while (number != 1);

            NewNumber.Number.Insert(0, 1);
            return NewNumber;
        }
        public string ToDecString()
        {
            long numInDec = 0;
            for (int i = Number.Count - 1, j = 0; i >= 0; i--, j++)
                numInDec = numInDec + Number[i] * (long)Math.Pow(2, j);
            return numInDec.ToString();       
        }
        /// <summary>
        /// Перегруженный оператор +
        /// </summary>
        /// <param name="number1">Первое слагаемое</param>
        /// <param name="number2">Второе слагаемое</param>
        /// <returns>Сумма двух "длинных" чисел</returns>
        public static BigNumber operator +(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            int CurrentPosition1 = number1.Length - 1;
            int CurrentPosition2 = number2.Length - 1;

            sbyte AddNum = 0;
            BigNumber NewBigNumber = new BigNumber();

            while (CurrentPosition1 != -1 && CurrentPosition2 != -1)
            {
                switch (number1.Number[CurrentPosition1] + number2.Number[CurrentPosition2])
                {
                    case 0:
                        {
                            NewBigNumber.Number.Insert(0, AddNum);
                            AddNum = 0;
                            break;
                        }
                    case 1:
                        {
                            if (AddNum == 1)
                                NewBigNumber.Number.Insert(0, 0);
                            else
                                NewBigNumber.Number.Insert(0, 1);
                            break;
                        }
                    case 2:
                        {
                            if (AddNum == 1)
                                NewBigNumber.Number.Insert(0, 1);
                            else
                            {
                                NewBigNumber.Number.Insert(0, 0);
                                AddNum = 1;
                            }
                            break;
                        }
                    default: { break; }
                }
                CurrentPosition1--;
                CurrentPosition2--;
            }

            if (CurrentPosition1 == -1)
                while (CurrentPosition2 != -1)
                {
                    switch (number2.Number[CurrentPosition2])
                    {
                        case 0:
                            {
                                NewBigNumber.Number.Insert(0, AddNum);
                                AddNum = 0;
                                break;
                            }
                        case 1:
                            {
                                if (AddNum == 1)
                                    NewBigNumber.Number.Insert(0, 0);
                                else
                                    NewBigNumber.Number.Insert(0, 1);
                                break;
                            }
                        default: { break; }
                    }
                    CurrentPosition2--;
                }
            else
                while (CurrentPosition1 != -1)
                {
                    switch (number1.Number[CurrentPosition1])
                    {
                        case 0:
                            {
                                NewBigNumber.Number.Insert(0, AddNum);
                                AddNum = 0;
                                break;
                            }
                        case 1:
                            {
                                if (AddNum == 1)
                                    NewBigNumber.Number.Insert(0, 0);
                                else
                                    NewBigNumber.Number.Insert(0, 1);
                                break;
                            }
                        default: { break; }
                    }
                    CurrentPosition1--;
                }

            if (AddNum == 1)
                NewBigNumber.Number.Insert(0, 1);
            return NewBigNumber;
        }
        /// <summary>
        /// Перегруженный оператор -
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns>Разница двух "длинных" чисел</returns>
        public static BigNumber operator -(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();
            BigNumber NewBigNumber = number1 + number2.GetAdditionalCode(number1.Length);
            NewBigNumber.Number.RemoveAt(0);
            return NewBigNumber;
        }
        /// <summary>
        /// Удаление у числа лишних нулей в начале
        /// </summary>
        void RemoveZerosFromBigNumber()
        {

            int index = Number.FindIndex((num) => num == 1);
            if (index == -1&&Number.Count>0)
                Number = new List<sbyte>() { 0 };
            else
                Number = new List<sbyte>(Number.Skip(index));
        }
        /// <summary>
        /// Перегруженный оператор *
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns>Результат умножения двух "длинных" чисел</returns>
        public static BigNumber operator *(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length < number2.Length)
            {
                BigNumber tempNum = new BigNumber(number1.Number);
                number1 = number2;
                number2 = tempNum;
            }

            int count = 0;
            BigNumber num1 = new BigNumber();
            for (int i = 0; i < number1.Length; i++)
                num1.Number.Add(0);
            BigNumber num2 = new BigNumber();
            for (int i = number2.Length - 1; i >= 0; i--)
            {
                if (number2.Number[i] == 1)
                {
                    num2 = new BigNumber(number1.Number);
                    for (int j = 0; j < count; j++)
                        num2.Number.Add(0);
                    num1 = num1 + num2;
                }
                count++;
            }
            return num1;
        }
        /// <summary>
        /// Перегруженный оператор / на цело
        /// </summary>
        /// <param name="delimoe">Первое число</param>
        /// <param name="delitel">Второе число</param>
        /// <returns>Результат деления на цело двух "длинных" чисел</returns>
        public static BigNumber operator /(BigNumber delimoe, BigNumber delitel)
        {
            delimoe.RemoveZerosFromBigNumber();
            delitel.RemoveZerosFromBigNumber();

            if (delimoe >= delitel)
            {
                BigNumber tempDelimoe = new BigNumber();
                BigNumber result = new BigNumber();

                for (int i = 0; i < delimoe.Length; i++)
                {
                    tempDelimoe.RemoveZerosFromBigNumber();
                    if (tempDelimoe == Null)
                        tempDelimoe.Number = new List<sbyte>();

                    tempDelimoe.Number.Add(delimoe.Number[i]);
                    if (tempDelimoe >= delitel)
                    {
                        result.Number.Add(1);
                        tempDelimoe = tempDelimoe - delitel;
                    }
                    else
                    {
                        result.Number.Add(0);
                    }
                }

                result.RemoveZerosFromBigNumber();
                return result;
            }
            throw new Exception();
        }
        /// <summary>
        /// Перегруженный оператор % (взятие остатка от деления)
        /// </summary>
        /// <param name="delimoe">Первое число</param>
        /// <param name="delitel">Второе число</param>
        /// <returns>Остаток от деления двух "длинных" чисел</returns>
        public static BigNumber operator %(BigNumber delimoe, BigNumber delitel)
        {
            delimoe.RemoveZerosFromBigNumber();
            delitel.RemoveZerosFromBigNumber();

            if (delimoe >= delitel)
            {
                BigNumber tempDelimoe = new BigNumber();
                BigNumber result = new BigNumber();

                for (int i = 0; i < delimoe.Length; i++)
                {
                    tempDelimoe.RemoveZerosFromBigNumber();
                    if (tempDelimoe == Null)
                    {
                        tempDelimoe.Number = new List<sbyte>();
                    }

                    tempDelimoe.Number.Add(delimoe.Number[i]);
                    if (tempDelimoe >= delitel)
                    {
                        result.Number.Add(1);
                        tempDelimoe = tempDelimoe - delitel;
                    }
                    else
                    {
                        result.Number.Add(0);
                    }
                }
                tempDelimoe.RemoveZerosFromBigNumber();
                if (tempDelimoe.Number.Count == 0)
                    tempDelimoe.Number = new List<sbyte>() { delimoe.Number[delimoe.Number.Count - 1] };
                return tempDelimoe;
            }
            else
                return delimoe;
        }       



        /// <summary>
        /// Быстрое возведение в степень. (number1^number2)mod(number3)
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="number3"></param>
        /// <returns></returns>
        public static BigNumber Pow(BigNumber number1, BigNumber number2, BigNumber number3)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();
            number3.RemoveZerosFromBigNumber();

            BigNumber tempNumber = new BigNumber(number1.Number);
            for (int i = 0; i < number2.Length-1; i++)
            {
                if (number2.Number[i+1]==1)
                {
                    tempNumber = (tempNumber * tempNumber * number1) % number3;
                }
                else
                {
                    tempNumber = (tempNumber * tempNumber) % number3;
                }

            }
            tempNumber.RemoveZerosFromBigNumber();
            return tempNumber;

        }
        /// <summary>
        /// Получение обратного элемента x. (number1*x)mod(number2)=1
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="number3"></param>
        /// <returns></returns>
        public static BigNumber GetReverseElement(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();
            BigNumber tempNumber1 = new BigNumber(number1.Number);
            BigNumber tempNumber2 = new BigNumber(number2.Number);
            List<BigNumber> bigNumbersZ = new List<BigNumber>();
            if (tempNumber1 < tempNumber2)
            {
                tempNumber1 = new BigNumber(number2.Number);
                tempNumber2 = new BigNumber(number1.Number);
            }
                while ((tempNumber1 % tempNumber2) != Null)
                {
                    bigNumbersZ.Add(tempNumber1 / tempNumber2);
                    BigNumber tempNumber1Copy = new BigNumber(tempNumber1.Number);
                    tempNumber1 = new BigNumber(tempNumber2.Number);
                    tempNumber2 = (tempNumber1Copy % tempNumber2);
                }


                BigNumber XNumber = Null;
                BigNumber YNumber = One;
                for (int i = bigNumbersZ.Count - 1; i >= 0; i--)
                {
                    BigNumber XNumberTemp = new BigNumber(XNumber.Number, XNumber.isPositive);
                    XNumber = new BigNumber(YNumber.Number, YNumber.isPositive);

                    if (!YNumber.isPositive)
                    {
                        if (XNumberTemp.isPositive)
                        {
                            YNumber = XNumberTemp + YNumber * bigNumbersZ[i];
                            YNumber.isPositive = true;
                        }
                        else
                        {
                            if (YNumber * bigNumbersZ[i] < XNumberTemp)
                            {
                                YNumber = YNumber * bigNumbersZ[i] - XNumberTemp;
                                YNumber.isPositive = false;
                            }
                            else
                            {
                                YNumber = YNumber * bigNumbersZ[i] - XNumberTemp;
                                YNumber.isPositive = true;
                            }
                        }
                    }
                    else
                    {
                        if (!XNumberTemp.isPositive)
                        {
                            YNumber = XNumberTemp + YNumber * bigNumbersZ[i];
                            YNumber.isPositive = false;
                        }
                        else
                        {
                            if (YNumber * bigNumbersZ[i] < XNumberTemp)
                            {
                                YNumber = YNumber * bigNumbersZ[i] - XNumberTemp;
                                YNumber.isPositive = true;
                            }
                            else
                            {
                                YNumber = YNumber * bigNumbersZ[i] - XNumberTemp;
                                YNumber.isPositive = false;
                            }
                        }
                    }

                }


                if (number1 > number2)
                {

                    if (!XNumber.isPositive)
                    {
                        BigNumber num = number1 - XNumber;
                        num.RemoveZerosFromBigNumber();
                        return num;
                    }
                    else
                    {
                        BigNumber num = XNumber;
                        num.RemoveZerosFromBigNumber();
                        return num;
                    }
                }
                else
                {
                    if (!YNumber.isPositive)
                    {
                        BigNumber num = number2 - YNumber;
                        num.RemoveZerosFromBigNumber();
                        return num;
                    }
                    else
                    {
                        BigNumber num = YNumber;
                        num.RemoveZerosFromBigNumber();
                        return num;
                    }
                }
        }

        public static bool operator >(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length > number2.Length)
                return true;
            if (number1.Length < number2.Length)
                return false;
            for (int i = 0; i < number1.Length; i++)
                if (number1.Number[i] > number2.Number[i])
                    return true;
                else if (number1.Number[i] < number2.Number[i])
                    return false;
            return false;
        }

        public static bool operator >=(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length > number2.Length)
                return true;
            if (number1.Length < number2.Length)
                return false;
            for (int i = 0; i < number1.Length; i++)
                if (number1.Number[i] > number2.Number[i])
                    return true;
                else if (number1.Number[i] < number2.Number[i])
                    return false;
            return true;
        }

        public static bool operator <(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length > number2.Length)
                return false;
            if (number1.Length < number2.Length)
                return true;
            for (int i = 0; i < number1.Length; i++)
                if (number1.Number[i] > number2.Number[i])
                    return false;
                else if (number1.Number[i] < number2.Number[i])
                    return true;
            return false;
        }

        public static bool operator <=(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length > number2.Length)
                return false;
            if (number1.Length < number2.Length)
                return true;
            for (int i = 0; i < number1.Length; i++)
                if (number1.Number[i] > number2.Number[i])
                    return false;
                else if (number1.Number[i] < number2.Number[i])
                    return true;
            return true;
        }

        public static bool operator ==(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length > number2.Length)
                return false;
            if (number1.Length < number2.Length)
                return false;
            for (int i = 0; i < number1.Length; i++)
                if (number1.Number[i] > number2.Number[i])
                    return false;
                else if (number1.Number[i] < number2.Number[i])
                    return false;
            return true;
        }

        public static bool operator !=(BigNumber number1, BigNumber number2)
        {
            number1.RemoveZerosFromBigNumber();
            number2.RemoveZerosFromBigNumber();

            if (number1.Length > number2.Length)
                return true;
            if (number1.Length < number2.Length)
                return true;
            for (int i = 0; i < number1.Length; i++)
                if (number1.Number[i] > number2.Number[i])
                    return true;
                else if (number1.Number[i] < number2.Number[i])
                    return true;
            return false;
        }
        /// <summary>
        /// Получить дополнительный код числа
        /// </summary>
        /// <param name="length">Длина</param>
        /// <returns>Дополнительный код числа</returns>
        private BigNumber GetAdditionalCode(int length)
        {
            BigNumber NewBigNumber=new BigNumber();
            for(int i=0;i<Length;i++)
            {
                if (Number[i] == 0)
                    NewBigNumber.Number.Add(1);
                else
                    NewBigNumber.Number.Add(0);
            }
            int zeroCount = length - Length;
            for(int i=0;i<zeroCount;i++)
                NewBigNumber.Number.Insert(0, 1);
            return NewBigNumber + One;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
