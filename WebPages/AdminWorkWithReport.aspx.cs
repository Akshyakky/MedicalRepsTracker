using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_StockOutReport : System.Web.UI.Page
{
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    //clsStockOutSales objStock = new clsStockOutSales();
    //clsLog objLog = new clsLog();
    DataManager dm = new DataManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int UserId = 0;

            if (Request.Cookies["User_MasterId"] != null)
            {
                UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
            }
            else
            {
                Response.Redirect("../SessionExpired.aspx");
            }
            txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            //txtItem.Focus();
            BindVisitReport(UserId);
            // BindDispensary(ddlSrchDispensary);
            //BindSource(ddlSrchSource);
            //BindCategory();
        }
    }
    //private void BindCategory()
    //{
    //    dm.BindDropDown("SELECT * FROM (select 0 as ICat_MID,'--SELECT--' as  ICat_Name union all SELECT ICat_MID,ICat_Name    FROM dbo.tbl_ItemCategory WHERE ICat_AF=1)a  order by  ICat_Name", ddlSrchICategry);

    //}
    //private void BindSource(DropDownList ddlSrchSource)
    //{
    //    dm.BindDropDown("SELECT * FROM (select 0 as FundSrc_MID,'--SELECT--' FundSrc_Name union all SELECT FundSrc_MID,FundSrc_Name FROM dbo.tbl_FundingSource WHERE FundSrc_AF=1)a  order by  FundSrc_Name", ddlSrchSource);
    //}

    //private void BindDispensary(DropDownList ddlDispensary)
    //{

    //        dm.BindDropDown("SELECT * FROM (select 0 as Disp_MID,'--SELECT--' as Disp_Name union all SELECT Disp_MID,Disp_Name FROM dbo.tbl_Dispensary WHERE Disp_AF=1)a order by Disp_Name", ddlDispensary);

    //}

    private void BindVisitReport(int UserId)
    {
        try
        {
            lblRecordsCount.Visible = false;
            gvStockOutReport.DataSource = null;
            gvStockOutReport.DataBind();


            string BMNAme = txtSrchBMName.Text;
            string MedicalRepName = txtmedrepName.Text;
            string RSMName = txtSrchRSMName.Text;


            string qry = "select   " +
 " (select MP_Name from tbl_MedicalRepsMaster where MP_Mid = Trans_RepMid  and MP_AF = 1) RepName,    " +
 " convert(nvarchar, Trans_VisitDateTime, 103)visitDate,CAST(Trans_VisitTime as nvarchar)Visittime, " +
 " Trans_Loc_Lan + ',' + Trans_Loc_Lat LOC , Trans_Status,Trans_Mid,Trans_Worked, " +
 " (case Trans_Worked " +
 " when 'No' then '-' " +
 " when 'Branch Manager' then '-' " +
 " when 'RSM' then(select RSM_Name from[dbo].[tbl_RSMMaster] where[RSM_Mid] =[Trans_RSMMid]) " +
 " when 'Both'  then(select RSM_Name from[dbo].[tbl_RSMMaster] where[RSM_Mid] =[Trans_RSMMid]) End) As RSMName, " +
 " (case Trans_Worked " +
 " when 'No' then '-' " +
 " when 'RSM' then '-' " +
 " when 'Branch Manager' then(select User_Name from[dbo].tbl_UserMaster where[User_Id] = Trans_BMMid) " +
 " when 'Both' then " +
 " (select User_Name from[dbo].tbl_UserMaster where[User_Id] = Trans_BMMid)   End) As BMName,[Trans_Extra1],Trans_RepMid " +
 " FROM tbl_Transaction where[Trans_AF] = 1 and [Trans_Worked]<>'No'  ";

            if (txtSrchBMName.Text != "")
            {
                qry += " and  ((select User_Name from tbl_UserMaster where User_Id = Trans_BMMid  and User_AF = 1) like '%" +
                       BMNAme.Trim() + "%')";

            }
            if (txtSrchRSMName.Text != "")
            {
                qry += " and ((select RSM_Name from tbl_RSMMaster where RSM_Mid = Trans_RSMMid  and RSM_AF = 1) like '%" +
                      RSMName.Trim() + "%')";

            }
            if(txtmedrepName.Text!="")
            {
                qry += "and ((select MP_Name from tbl_MedicalRepsMaster where MP_Mid = Trans_RepMid  and MP_AF = 1) like '%" +
                      MedicalRepName.Trim() + "%')";
            }

            if (rbdWorkWith.SelectedIndex > 0)
            {
                qry += " and  [Trans_Worked]='" + rbdWorkWith.SelectedItem.Text.Trim() + "'";
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
            qry += "  order by Trans_Mid desc";


            DataSet dsd = dm.GetDataSet(qry);
            gvStockOutReport.DataSource = dsd;
            gvStockOutReport.DataBind();

        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("BindStockReport", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int UserId = 0;

        if (Request.Cookies["User_MasterId"] != null)
        {
            UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
        }
        else
        {
            Response.Redirect("../SessionExpired.aspx");
        }
        BindVisitReport(UserId);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        divSearch.Visible = false;
        lblHeading.Visible = true;
        ExportToExcel();
    }

    private void ExportToExcel()
    {
        string Today = DateTime.Now.ToString("dd/MM/yyyy");
        lblHeading.Text = "RSM or BM Work With Medical Rep Report " + Today;
        Response.Clear();
        long RanNo = DateTime.Today.Ticks;
        string dtToday = DateTime.Now.ToString("dd_MM_yyyy");
        Response.AddHeader("content-disposition", "attachment;filename=MedicalRepsWorkWIth_" + dtToday + RanNo + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dvview.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminWorkWithReport.aspx");
    }

    private void Refresh()
    {
        //ddlSrchDispensary.SelectedIndex = 0;
        //ddlSrchSource.SelectedIndex = 0;

        //txtInvNo.Text = "";
        txtSrchBMName.Text = "";
        txtSrchRSMName.Text = "";
        txtFrom.Text = "";
        //txtItem.Text = "";
        txtTo.Text = "";
        ddlShowTop.SelectedIndex = 0;
        int UserId = 0;

        if (Request.Cookies["User_MasterId"] != null)
        {
            UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
        }
        else
        {
            Response.Redirect("../SessionExpired.aspx");
        }
        BindVisitReport(UserId);
    }

    //protected void ddlSrchSource_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSrchSource.SelectedIndex > 0)
    //    {
    //        string Qry = "select '--SELECT--' union all select distinct (select  ICat_Name from dbo.tbl_ItemCategory where ICat_MID=Item_CategoryMID)  from dbo.tbl_Item,dbo.tbl_StockOutSales where Sales_MID=Item_MID and Sales_FundingSourceMID='" + ddlSrchSource.SelectedValue + "'";
    //        DataSet ds = dm.GetDataSet(Qry);
    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlSrchICategry.DataTextField = ds.Tables[0].Columns[0].ToString();
    //                ddlSrchICategry.DataValueField = "";
    //                ddlSrchICategry.DataSource = ds.Tables[0];
    //                ddlSrchICategry.DataBind();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        BindCategory();
    //    }
    //}



    protected void gvStockOutReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text == "Pending")
            {
                lblStatus.ForeColor = System.Drawing.Color.Gray;
            }
            else if (lblStatus.Text == "Approved")
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (lblStatus.Text == "Rejected")
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}