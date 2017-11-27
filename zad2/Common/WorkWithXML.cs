using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class WorkWithXML
    {
        public static string[] ReadXml(string role)
        {
            XmlDocument xd = new XmlDocument();

            xd.Load("..//..//document.xml");
            List<string> prm = new List<string>();
            XmlNode nodelist = xd.SelectSingleNode("/Roles");
            XmlNodeList childnodelist = nodelist.ChildNodes;

            string[] parts = new string[] { };

            foreach (XmlNode node in childnodelist)
            {
                if (node.Name.ToString() == role)
                {
                    if (node.InnerText.Contains(","))
                    {
                        parts = node.InnerText.Split(',');
                        foreach (string p in parts)
                        {
                            prm.Add(p);
                        }
                    }
                    else
                    {
                        prm.Add(node.InnerText);
                    }
                }
            }

            return prm.ToArray(); ;
        }
    }
}
