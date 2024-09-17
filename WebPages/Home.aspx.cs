using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Home : System.Web.UI.Page
{

    //clsStockIn ObjSI = new clsStockIn();
    DataManager dm = new DataManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int UserId = 0;

            if (Request.Cookies["MP_Mid"] != null)
            {
                UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
            }
            else
            {
                Response.Redirect("AdminHome.aspx");
            }

            //lblTdyDate.Text = DateTime.Today.ToString("dd MMM yyyy");
            lblDate.Text = DateTime.Today.ToString("dd MMM yyyy");
            GetTodaysVisitCount(UserId);

            GetLocationWiseCount(UserId);
        }

    }

    private void GetLocationWiseCount(int UserId)
    {
        string TdyDate = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
        divReOrder.Visible = false;
        gvReOrder.DataSource = null;
        gvReOrder.DataBind();
        string Qry = " select count([Trans_Mid]) as cnt,Trans_Loc_Lan+','+Trans_Loc_Lat [Location] from [dbo].[tbl_Transaction] where [Trans_AF]=1 and [Trans_RepMid]='" + UserId + "' and convert(nvarchar,Trans_VisitDateTime,111)='" + TdyDate + "' group by  Trans_Loc_Lan+','+Trans_Loc_Lat";
        DataSet ds = dm.GetDataSet(Qry);
        if (ds != null)
        {
            divReOrder.Visible = true;
            gvReOrder.DataSource = ds;
            gvReOrder.DataBind();
        }
    }


  

    private void GetTodaysVisitCount( int UserId)
    {
        string TdyDate = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
        string Qry = " select count([Trans_Mid])  as cnt from [dbo].[tbl_Transaction] where [Trans_AF]=1 and [Trans_RepMid]='" + UserId +"' and convert(nvarchar,Trans_VisitDateTime,111)='" + TdyDate + "'";
        DataSet ds = dm.GetDataSet(Qry);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCount.Text = ds.Tables[0].Rows[0]["cnt"].ToString();
            }
        }
    }
   
    protected void gvReOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int UserId = 0;

        if (Request.Cookies["MP_Mid"] != null)
        {
            UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
        }
        else
        {
            Response.Redirect("../SessionExpired.aspx");
        }
        gvReOrder.PageIndex = e.NewPageIndex;
        GetLocationWiseCount(UserId);
    }
    
  
}