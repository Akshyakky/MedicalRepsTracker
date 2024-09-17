<%@ WebHandler Language="C#" Class="Items" %>

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;

public class Items : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
  {
        string prefix = context.Request.QueryString["q"] ?? "";

        cls_ProductMaster objProd= new cls_ProductMaster();
        List<string> ListItemName = objProd.getProductName(prefix);

        JavaScriptSerializer js = new JavaScriptSerializer();
        context.Response.Write(js.Serialize(ListItemName));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}