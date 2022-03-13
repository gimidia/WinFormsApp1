using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    var caminho = @"C:\Users\gimid\OneDrive\Documents\Csharp\product.xml";
            //    ObterXML(caminho);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Erro", ex);

            //}

            //Table table = DeserializeTheObject();
            //MessageBox.Show("Id:" + table.Product_id +", Product: " + table.Product_name +" , Price" + table.Product_price );

            //var localArquivo = @"C:\Users\gimid\OneDrive\Documents\Csharp\product.xml";

            //var mySerializer = new XmlSerializer(typeof(Table));
            //// To read the file, create a FileStream.
            //using var myFileStream = new FileStream(localArquivo, FileMode.Open);
            //// Call the Deserialize method and cast to the object type.
            //var myObject = (Table)mySerializer.Deserialize(myFileStream);
            //var id = myObject.Product_id;
            //var name = myObject.Product_name;
            //var price = myObject.Product_price;

            //textBox1.Text = myObject.Product_id;
            //textBox2.Text = myObject.Product_name;
            //textBox3.Text = myObject.Product_price;

            // load xml file
            var xml = XDocument.Load(@"C:\Users\gimid\OneDrive\Documents\Csharp\product.xml");
            // load xml content from a string
            //var xml = XDocument.Parse("<xml>your xml content</xml>");

            // get all sub element from node "Book"
            var bookDetails = from b in xml.Root.Descendants("Product")
                              select b.Element("Product_id").Value + "-" +
                                      b.Element("Product_name").Value + "-" +
                                      b.Element("Product_price").Value;

            foreach (string detail in bookDetails)
            {
                Console.WriteLine("Book details: {0}", detail);
                
            }

            var localArquivo = @"C:\Users\gimid\OneDrive\Documents\Csharp\product.xml";

            DeserializeXML(localArquivo);



        }

        public XmlData DeserializeXML(string xmlFilename)
        {
            using (StreamReader reader = new StreamReader(xmlFilename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlData));
                return (XmlData)serializer.Deserialize(reader);
            }
        }

        public void ObterXML(string xml)
        {

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Id", 100);
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("Price", 100);

            ListViewItem item;

            var xmlContent = File.ReadAllText(xml);
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(xml.Replace("&", "&amp;"));
            //xmlDoc.LoadXml(xmlContent.Replace("&", "&amp;"));
            xmlDoc.LoadXml(ConversorXML(xmlContent));
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Table/Product");
            string proID = "", proName = "", price = "";

            foreach (XmlNode node in nodeList)
            {
                proID = node.SelectSingleNode("Product_id").InnerText;
                proName = node.SelectSingleNode(SecurityElement.Escape("Product_name")).InnerText;
                price = node.SelectSingleNode("Product_price").InnerText;
                //MessageBox.Show(proID + " " + proName + " " + price);

                item = new ListViewItem(new string[] { proID, proName, price });
                listView1.Items.Add(item);

            }             

            button1.Enabled = false;

        }

        internal Table DeserializeTheObject()
        {
            var localArquivo = @"C:\Users\gimid\OneDrive\Documents\Csharp\product.xml";
            Table objectToDeserialize = new Table();
            XmlSerializer xmlserializer = new System.Xml.Serialization.XmlSerializer(objectToDeserialize.GetType());
            using (StreamReader streamreader = new StreamReader(localArquivo))
            {
                return (Table)xmlserializer.Deserialize(streamreader);
            }
        }

        public string ConversorXML(string converter)
        {
            return converter.Replace("&", "&amp;");

        }

    }
}
