using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr;

            try
            {
                sr = new StreamReader(args[0]);
            }
            catch (Exception e){
                Console.WriteLine("Ошибка! {0}", e.Message);
                return;
            }

            string[] str;
            int phoneErr = 0, phoneCorr = 0, emailErr = 0, emailCorr = 0, employees = 0;
            Regex nameReg = new Regex(@"\w+(\u0020\w+)?");
            Regex emailReg = new Regex(@"^[^\s@]+@[a-z]+\.[a-z]{2,64}$");
            Regex phoneReg = new Regex(@"^(\+7|8)[-\(\s]?\d{3}[-\)\s]?\d{3}([-\s]?\d{2}){2}$");

            using (sr) //@"C:\Users\79166\source\repos\TextAnalysis\Book.csv"
            {
                string[] fields = sr.ReadLine().Trim().Split('\t');

                while (!sr.EndOfStream) {
                    str = sr.ReadLine().Trim().Trim('"').Split('\t');
                    if (!nameReg.IsMatch(str[0])) break;

                    employees++;

                    if (emailReg.IsMatch(str[1]))
                        emailCorr++;
                    else
                        emailErr++;

                    if (phoneReg.IsMatch(str[2]))
                        phoneCorr++;
                    else
                        phoneErr++;
                }

                Console.WriteLine("Количество сотрудников: {0}", employees);
                Console.WriteLine("Корректных почтовых адресов: {0}, некорректных: {1}", emailCorr, emailErr);
                Console.WriteLine("Корректных телефонных номеров: {0}, некорректных: {1}", phoneCorr, phoneErr);
            }
        }
    }
}
