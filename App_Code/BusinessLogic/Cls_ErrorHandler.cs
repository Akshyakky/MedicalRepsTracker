using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for Cls_ErrorHandler
/// </summary>
public class Cls_ErrorHandler
{
	public Cls_ErrorHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void SaveReportInXML(string FunctionName, string ErrorText)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            string str = DateTime.Today.ToString("ddMMyyyy");
            string filename = "FMCI_Error_" + str;
            string xmlfile = System.Web.HttpContext.Current.Server.MapPath("~/FMCILog/" + filename + ".xml");
            if (File.Exists(xmlfile))
            {
                doc.Load(xmlfile);
            }
            else
            {
                XmlNode rootNode = doc.CreateElement("FMCIERRORS");

                doc.AppendChild(rootNode);
            }

            XmlElement Report = doc.CreateElement("Error");

            XmlNode FunctionNode = doc.CreateElement("FunctionName");
            FunctionNode.InnerText = FunctionName;

            XmlNode ErrorNode = doc.CreateElement("ErrorText");
            ErrorNode.InnerText = ErrorText;

            XmlNode rootnode = doc.SelectSingleNode("FMCIERRORS");
            int index = rootnode.ChildNodes.Count;

            XmlNode SlNo = doc.CreateElement("SerialNo");
            SlNo.InnerText = (index + 1).ToString();

            Report.AppendChild(SlNo);
            Report.AppendChild(FunctionNode);
            Report.AppendChild(ErrorNode);


            rootnode.AppendChild(Report);

            doc.Save(xmlfile);
        }
        catch (Exception excen)
        {
            excen.Data.Clear();
        }

    }
}