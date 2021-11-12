using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PaymentBill___Serialization
{
    class Program
    {
        static int GetIntInfo(string text)
        {
            int value;
            Console.Write($"Input {text}. Value must be > 0. : ");
            do
            {
                value = Convert.ToInt32(Console.ReadLine());
            } while (value <= 0);
            return value;
        }
        static double GetDoubleInfo(string text)
        {
            double value;
            Console.Write($"Input {text}. Value must be > 0. : ");
            do
            {
                value = Convert.ToDouble(Console.ReadLine());
            } while (value <= 0);
            return value;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("The program is about serialization an information.");
            Console.WriteLine("The process of q-ty info to serialization depends on flag.");
            Console.WriteLine("Default set of the flag is true.");
            Console.WriteLine("If you want to change flag - input or delete false in constructor of payment object.\n");
            Console.WriteLine("An information about serialization saves in file PaymentBill.bin in default project folder.\n");

            PaymentBill payment = new PaymentBill(GetDoubleInfo("each day payment"), GetIntInfo("days quantity"),
                GetDoubleInfo("fine tax"), GetIntInfo("days payment delay"), false);  //false - flag of serialization q-ty info

            BinaryFormatter formatter = new BinaryFormatter();

            string file_path = "PaymentBill.bin";

            try
            {
                using (FileStream fstream = new FileStream(file_path,
                    FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fstream, payment);
                }

                Console.WriteLine($"\nThe info is binary serialized with bool flag {payment.GetFlag()}.\n");

                PaymentBill payment1 = null;

                using (FileStream fstream = new FileStream(file_path,
                    FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    payment1 = (PaymentBill)formatter.Deserialize(fstream);
                }

                Console.WriteLine(payment1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
