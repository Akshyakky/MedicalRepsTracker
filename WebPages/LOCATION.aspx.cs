using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class LOCATION_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        //string latitude = hdlatitude.Value;
        //string longitude = hdlongitude.Value;
        //string url = "https://maps.google.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyCEgJGyx_d3M0cSXcSMrwEQP4AHS2nC8zA";

        /////System.Diagnostics.Process.Start(url);

        //XmlDocument myXmlDocument = new XmlDocument();
        //myXmlDocument.Load(url);
        //DataSet theDataSet = new DataSet();
        //StringReader theReader = new StringReader(myXmlDocument.InnerXml);
        //theDataSet.ReadXml(theReader);
        //string strPlace = theDataSet.Tables["result"].Rows[0][1].ToString();
        //lblLocation.Text = strPlace;
    }
    private void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", Message, true);

    }
    protected void Unnamed2_Click1(object sender, EventArgs e)
    {
        try
        {
 string str = "insert into tbl_testlocations (Latitude,Longitude) values('" + hdlatitude.Value + "','" + hdlongitude.Value + "')";
        GetDataSet(str);

        ShowMessage("Done");
        }
        catch (Exception)
        {
            ShowMessage("error");
            throw;
        }
       
    }

    #region Routine to Get DataSet for Passed SQL Command
    /*-----------------------------------------------
        ' Routine to Get DataSet for Passed SQL Command
        '-----------------------------------------------*/
    public DataSet GetDataSet(string a_strDataSource)
    {
        System.Data.SqlClient.SqlDataAdapter myAdapter;
        DataSet DS;
        try
        {
            DS = new DataSet();
            myAdapter = new System.Data.SqlClient.SqlDataAdapter(a_strDataSource, GetConnectionString());
            myAdapter.SelectCommand.CommandTimeout = ConnectionTimeOut;
            myAdapter.Fill(DS);
            myAdapter.Dispose();
            myAdapter = null;
            return DS;
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            return null;
        }
        finally
        {
            // DS.Dispose();
        }
    }
    public int ConnectionTimeOut;
    #endregion

    public string GetConnectionString()
    {
        string strConnectionString = "Data Source=108.178.0.18;Initial Catalog=db_rTracker;Trusted_Connection=false;User ID =sa_rTracker; Password=rTracker@123;";
        return strConnectionString;
    }
}