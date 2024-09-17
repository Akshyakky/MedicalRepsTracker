using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_VisitsChart : System.Web.UI.Page
{
    DataManager dm = new DataManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranch(ddlBranch);
        }
    }

    private void BindBranch(DropDownList ddl)

    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT Branch_Mid,Branch_Name FROM dbo.tbl_BranchMaster WHERE Branch_AF=1", ddl);
    }
    public string FnGetChartStringMAs(DataTable DT)
    {
        string strReturn = "[";
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            if (strReturn == "[")
            {
                strReturn += "['" + DT.Rows[i][0].ToString() + "'," + DT.Rows[i][1].ToString() + "]";
            }
            else
            {
                strReturn += "," + "['" + DT.Rows[i][0].ToString() + "'," + DT.Rows[i][1].ToString() + "]";
            }
        }
        strReturn += "]";
        strReturn = strReturn.Replace(".", "").Replace("@", "").Replace("\n", "");
        return strReturn;
    }

    private void FnUGCourseSGoogleCHartBind(DataTable DT)
    {
        string Script = "<script type=\"text/javascript\" src=\"https://www.google.com/jsapi\"></script>";
        Script += "<script type=\"text/javascript\">";
        // Load the Visualization API and the piechart package.
        Script += " google.load('visualization', '1.0', { 'packages': ['corechart'] });";

        // Set a callback to run when the Google Visualization API is loaded.
        Script += " google.setOnLoadCallback(drawChart);";
        // Callback that creates and populates a data table, 
        // instantiates the pie chart, passes in the data and
        // draws it.
        Script += " function drawChart() {";
        // Create the data table.
        Script += " var data = new google.visualization.DataTable();";
        Script += " data.addColumn('string', 'Topping');";
        Script += " data.addColumn('number', 'Quantity');";
        Script += "data.addRows(" + FnGetChartStringMAs(DT) + ");";
        //Script += " data.addRows([['Mushrooms', 5], ['Onions', 1], ['Olives', 1], ['Zucchini', 1], ['Pepperoni', 2], ['Madhu', 2], ['Mushrooms', 5], ['Onions', 1], ['Olives', 1], ['Zucchini', 1], ['Pepperoni', 2], ['Madhu', 2]]);";

        // Set chart options
        Script += " var options = { 'title': 'Sample Consumed Wise Visits'};";
        // Instantiate and draw our chart, passing in some options.// ,pieHole: 0.2,'width': 570,'height': 470

        Script += " var chart = new google.visualization.ColumnChart(document.getElementById('div_LocationWiseChart'));";

        //     Script += " var chart = new google.visualization.PieChart(document.getElementById('chart_div'));";

        Script += " chart.draw(data, options);";
        Script += " }";
        Script += " </script>";
        //  Script+="  }";

        Page.RegisterClientScriptBlock("ClientScript1", Script);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string qry = "   select [MedTrans_ProdName],sum([MedTrans_Qty]) as cnt   from[dbo].[tbl_MedicineTransaction] "+
 " inner join[dbo].[tbl_Transaction] "+
  "  on[Trans_Mid]=[MedTrans_TransMid] "+

   " where[MedTrans_AF]=1";

        if(ddlBranch.SelectedIndex>0)
        {
            qry += " and [Trans_RepMid]='" +ddlBranch.SelectedValue + "'";

        }
        if (txtFrom.Text != "")
        {
            string strfrm = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd").Replace("-", "/");
            qry += " and  convert(nvarchar,Trans_VisitDateTime,111)>='" + strfrm + "'";
        }
        if (txtTo.Text != "")
        {
            string strfrm = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd").Replace("-", "/");
            qry += " and  convert(nvarchar,Trans_VisitDateTime,111)<='" + strfrm + "'";
        }
        qry += " group by [MedTrans_ProdName]";
        DisplayChartUGCourse(qry);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("VisitsChart.aspx");
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    private void DisplayChartUGCourse(string strCourseQry)
    {
        dm.OpenSQLConnection();
        SqlDataReader dr = dm.GetSQLDataReader(strCourseQry);
        DataTable dt = new DataTable();
        dt.Load(dr);
        dr.Dispose();
        //Bind to script
        FnUGCourseSGoogleCHartBind(dt);
        dm.CloseConnection();

    }


}