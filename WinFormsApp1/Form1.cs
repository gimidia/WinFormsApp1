using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security;

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

            try
            {
                var caminho = @"C:\Users\gimid\OneDrive\Documents\Csharp\product.xml";
                ObterXML(caminho);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro", ex);

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

        public string ConversorXML(string converter)
        {
            return converter.Replace("&", "&amp;");

        }

    }
}
