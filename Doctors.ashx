<%@ WebHandler Language="C#" Class="Doctors" %>

using System;
using System.Web;

public class Doctors : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string prefix = context.Request.QueryString["q"] ?? "";

        cls_DoctorMaster objDoctor = new cls_DoctorMaster();
        System.Collections.Generic.List<string> ListItemName = objDoctor.GetDoctorsName(prefix);
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        context.Response.Write(js.Serialize(ListItemName));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}