using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public enum CreditCardOwnerType
    {
        Corporate = 1,
        Consumer = 2
    }

    class Program
    {
        static void Main(string[] args)
        {
            //StringBuilder sb = new StringBuilder();

            //AppendAlignedContent(sb, string.Empty, 35);

            //Console.WriteLine(sb.ToString());

            //string CustomerNumber = null;

            //Console.WriteLine(default(string));

            Console.WriteLine((CreditCardOwnerType)2);
        }

        static void AppendAlignedContent(StringBuilder sb, string content, int maxLength, bool addSeparator = true)
        {
            if (content.Length > maxLength)
                content = content.Remove(maxLength);

            content = content.Replace(",", " ");
            sb.Append(content);

            if (addSeparator)
                sb.Append(",");
        }
    }
}
