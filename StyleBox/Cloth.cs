using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleBox
{
    public class GlobalClass
    {
        public struct Cloth
        {
            public string cloth_article;
            public string cloth_name;
            public string cloth_type;
            public double cloth_price;
            public int cloth_number;

            public Cloth(string article, string name, string type, double price, int number)
            {
                this.cloth_article = article;
                cloth_name = name;
                cloth_type = type;
                cloth_price = price;
                cloth_number = number;
            }
        }


    }
}
