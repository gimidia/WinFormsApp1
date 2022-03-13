using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinFormsApp1
{
    public class Product
    {
        public string Product_id { get; set; }
        public string Product_name { get; set; }
        public string Product_price { get; set; }
    }

    [XmlRoot("Table")]
    public class XmlData
    {
        [XmlElement("Product")]
        public Product[] Table { get; set; }
    }

}
