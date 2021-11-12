using System;
using System.Runtime.Serialization;

namespace PaymentBill___Serialization
{
    [Serializable]
    class PaymentBill : ISerializable
    {
        double each_day_payment;
        int days_quantity;
        double fine_tax;
        int days_payment_delay;
        double direct_payment;
        double fine;
        double total_payment;
        static bool flag;
        static bool Flag
        {
            get => flag;
            set => flag = value;
        }

        public PaymentBill() { }

        public PaymentBill(double edp, int dq, double ft, int dpd, bool flag = true)
        {
            each_day_payment = edp;
            days_quantity = dq;
            fine_tax = ft;
            days_payment_delay = dpd;
            direct_payment = 0;
            fine = 0;
            total_payment = 0;
            Flag = flag;
        }

        private PaymentBill(SerializationInfo info, StreamingContext context)
        {
            each_day_payment = info.GetDouble("Each day payment");
            days_quantity = info.GetInt32("Days quantity");
            fine_tax = info.GetDouble("Fine tax");
            days_payment_delay = info.GetInt32("Days payment delay");
            if (Flag)
            {
                direct_payment = info.GetDouble("Direct payment");
                fine = info.GetDouble("Fine");
                total_payment = info.GetDouble("Total payment");
            }
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Each day payment", each_day_payment);
            info.AddValue("Days quantity", days_quantity);
            info.AddValue("Fine tax", fine_tax);
            info.AddValue("Days payment delay", days_payment_delay);
            if (Flag)
            {
                direct_payment = each_day_payment * days_quantity;
                fine = fine_tax * days_payment_delay;
                total_payment = direct_payment + fine;
                info.AddValue("Direct payment", direct_payment);
                info.AddValue("Fine", fine);
                info.AddValue("Total payment", total_payment);
            }
        }

        public bool GetFlag() => Flag;

        public override string ToString()
        {
            return $"Each day payment: {each_day_payment}\nDays quantity: {days_quantity}\n" +
                $"Fine tax: {fine_tax}\nDays payment delay: {days_payment_delay}\n" +
                $"Direct payment: {direct_payment}\nFine: {fine}\nTotal payment: {total_payment}\n";
        }
    }
}
