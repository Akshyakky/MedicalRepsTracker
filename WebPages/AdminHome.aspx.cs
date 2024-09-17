using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Default : System.Web.UI.Page
{
    string strRole = "";
    DataManager dm = new DataManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["User_MasterId"] != null)
        {
            if (Request.Cookies["User_Role"] != null)
            {
                if (Request.Cookies["User_Role"].Value == "Admin")
                {
                    strRole = Request.Cookies["User_Role"].Value;
                    // divBrnch.Visible = true;
                }
                else
                {
                    strRole = Request.Cookies["User_Role"].Value;
                    //divBrnch.Visible = false;
                }
            }
        }
        if (!IsPostBack)
        {
            lblTdyDate.Text = DateTime.Today.ToString("dd MMM yyyy");
            lblDate.Text = DateTime.Today.ToString("dd MMM yyyy");
            GetTodaysStockOutCount();
            GetCategoryWiseCount();
            GetItemReOrderAlert();
        }

    }

    private void GetItemReOrderAlert()
    {
        int UserId = 0;
        int BranchMid = 0;
        if (strRole == "Admin")
        {
            BranchMid = 0;
        }
        else if (Request.Cookies["User_MasterId"] != null)
        {
            UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
            if (Request.Cookies["User_BranchMID"] != null)
            {
                BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);
            }
        }

        string Qry = "Select count([Trans_Mid]) as cnt,[MP_Name] from [dbo].[tbl_Transaction] " +
" inner join[dbo].[tbl_MedicalRepsMaster] " +
    " on[MP_Mid]=[Trans_RepMid] " +
    " where[MP_AF]=1 and[Trans_AF]=1 and convert(varchar,Trans_VisitDateTime,103) = convert(varchar,GETDATE(),103) ";
        if (BranchMid != 0)
        {

            Qry += " and MP_BranchMid='" + BranchMid + "' ";
        }

        Qry += " group by MP_Name";

        // divReOrder.Visible = false;
        gvCatWiseStock.DataSource = null;
        gvCatWiseStock.DataBind();
        DataSet ds = dm.GetDataSet(Qry);
        if (ds != null)
        {
            //divReOrder.Visible = true;
            gvCatWiseStock.DataSource = ds;
            gvCatWiseStock.DataBind();
        }
    }


    private void GetCategoryWiseCount()
    {
        //gvCatWiseStock.DataSource = null;
        //gvCatWiseStock.DataBind();
        //DataSet ds = ObjSI.GetCatCount(5, 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, "", "", "", "", "", "", 0);
        //DataSet ds = ObjItemCat.GetDetails(5, 0, "", "", "", "", "", "", "", 0); //by dhanya

        //if (ds != null)
        //{
        //    gvCatWiseStock.DataSource = ds;
        //    gvCatWiseStock.DataBind();
        //}
    }

    private void GetTodaysStockOutCount()
    {
        int UserId = 0;
        if (Request.Cookies["User_MasterId"] != null)
        {
            UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
            if (Request.Cookies["User_LoginUserName"] != null)
            {
                lblUserName.Text = Request.Cookies["User_LoginUserName"].Value;
            }
        }
        //DataSet ds = objSO.GetStockOutCount(5);
        //if (ds != null)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //lblUserName.Text = ds.Tables[0].Rows.Count.ToString();
        //    }
        //}
    }
    protected void lnkCatWise_Click(object sender, EventArgs e)
    {

    }
    protected void gvReOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvCatWiseStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCatWiseStock.PageIndex = e.NewPageIndex;
        GetCategoryWiseCount();
    }
    protected void lnkViewMore_Click(object sender, EventArgs e)
    {

    }
}