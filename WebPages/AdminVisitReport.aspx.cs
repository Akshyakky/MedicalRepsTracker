using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_AdminVisitReport : System.Web.UI.Page
{
    string strRole = "";
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    //clsStockOutSales objStock = new clsStockOutSales();
    //clsLog objLog = new clsLog();
    DataManager dm = new DataManager();
    cls_Transaction objTrans = new cls_Transaction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["User_MasterId"] != null)
        {
            if (Request.Cookies["User_Role"] != null)
            {
                if (Request.Cookies["User_Role"].Value == "Admin")
                {
                    strRole = Request.Cookies["User_Role"].Value;
                    //divBrnch.Visible = true;
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

            txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            //txtItem.Focus();
            BindVisitReport();
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

    private void BindVisitReport()
    {
        try
        {

            int BranchMid = 0;
            if (strRole == "Admin")
            {
                BranchMid = 0;
            }
            else if (Request.Cookies["User_MasterId"] != null)
            {
                if (Request.Cookies["User_BranchMID"] != null)
                {
                    BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);
                }
            }

            lblRecordsCount.Visible = false;
            gvAdminVisitReport.DataSource = null;
            gvAdminVisitReport.DataBind();


            string Doctor = txtDoctor.Text;
            string MedicalRepName = txtmedrepName.Text;
            string location = txtSrchLocation.Text;
            string Source = "";

            string qry = "select " +
              "  (select MP_Name from tbl_MedicalRepsMaster where MP_Mid=Trans_RepMid  and MP_AF=1) RepName," +
          " Trans_Type+'-'+" +
              "  (case Trans_Type when ' Doctor' then" +
              "  (select Doc_Name from tbl_DoctorMaster where Doc_Mid=Trans_Doc_Chem_Mid)" +
            " when 'Chemist' then" +
               " (select Che_Name from tbl_ChemistMaster where  Che_Mid=Trans_Doc_Chem_Mid)" +
            " when 'Stockiest' then" +
               " (select  ST_Name from tbl_StockiestMaster where  ST_Mid=Trans_Doc_Chem_Mid)" +
           " END) AS DOCCHESTNAME," +
            "    convert(nvarchar,Trans_VisitDateTime,103)visitDate,CAST(Trans_VisitTime as nvarchar)Visittime,Trans_ChemistMeet,Trans_Location,Trans_Loc_Lan+','+Trans_Loc_Lat LOC , Trans_Status,Trans_Mid" +
            " ,(Case Trans_ChemistMeet when 'Yes' then(Select[Che_Name]  from tbl_ChemistMaster " +
 " where [Che_Mid]=[Trans_ChemMid] and [Che_AF]=1 )  when 'No' then '-' END) as ChemName,Trans_Extra1" +
            "    FROM tbl_Transaction ";

            if (BranchMid != 0)
            {
                qry += " inner join [dbo].[tbl_MedicalRepsMaster] on [Trans_RepMid]=[MP_Mid] ";
            }


            qry += "where  [Trans_AF]=1 ";

            if (BranchMid != 0)
            {
                qry += " and [MP_BranchMid]='" + BranchMid + "'";
            }

            if (Doctor.Trim() != "")
            {
                qry += " and ((select MP_Name from tbl_MedicalRepsMaster where MP_Mid=Trans_Doc_Chem_Mid  and MP_AF=1) like '%" +
                       Doctor.Trim() +
                       "%' or (select Che_Name from tbl_ChemistMaster where  Che_Mid=Trans_Doc_Chem_Mid and CHE_AF=1 )  like '%" +
                       Doctor.Trim() +
                       "%' or (select  ST_Name from tbl_StockiestMaster where  ST_Mid=Trans_Doc_Chem_Mid  and ST_AF=1) like '%" +
                       Doctor.Trim() + "%') ";
            }

            if (MedicalRepName != "")
            {
                qry += " and (select MP_Name from tbl_MedicalRepsMaster where MP_Mid=Trans_RepMid) like '%" + MedicalRepName + "%' ";
            }
            if (location != "")
            {
                qry += " and Trans_Location like '%" + location + "%' ";
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
            qry += " order by Trans_Mid desc";
            DataSet dsd = dm.GetDataSet(qry);
            gvAdminVisitReport.DataSource = dsd;
            gvAdminVisitReport.DataBind();
            /*
    
            string count = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecordsCount.Visible = true;
                count = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
                lblRecordsCount.Text = "0 Records Found of " + count;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblRecordsCount.Visible = true;
                string SCount = ds.Tables[1].Rows.Count.ToString();
                lblRecordsCount.Text = SCount + " Records Found of " + count;
                gvStockOutReport.DataSource = ds.Tables[1];
                gvStockOutReport.DataBind();
            }
            else
            {
                gvStockOutReport.EmptyDataText = "No records found";
                gvStockOutReport.DataBind();
            }
             * */
            //  }
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
        BindVisitReport();
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
        lblHeading.Text = "Medical Reps Visit " + Today;
        Response.Clear();
        long RanNo = DateTime.Today.Ticks;
        string dtToday = DateTime.Now.ToString("dd_MM_yyyy");
        Response.AddHeader("content-disposition", "attachment;filename=MedicalRepsVisit_" + dtToday + RanNo + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        View.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        //ddlSrchDispensary.SelectedIndex = 0;
        //ddlSrchSource.SelectedIndex = 0;

        //txtInvNo.Text = "";
        txtDoctor.Text = "";
        txtSrchLocation.Text = "";
        txtmedrepName.Text = "";
        txtFrom.Text = "";
        //txtItem.Text = "";
        txtTo.Text = "";
        ddlShowTop.SelectedIndex = 0;
        BindVisitReport();
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



    protected void gvAdminVisitReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UserId = 0;
        if (Request.Cookies["User_MasterId"] != null)
        {
            UserId = int.Parse(Request.Cookies["User_MasterId"].Value.ToString());
        }
        int TransMid = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "btnApprv")
        {
            string Result = objTrans.ApproveReject(5, TransMid, UserId);
            if (Result == "1")
            {
                ShowMessage("Transaction Approved");
                BindVisitReport();
            }
        }
        if (e.CommandName == "btnReject")
        {
            string Result = objTrans.ApproveReject(6, TransMid, UserId);
            if (Result == "1")
            {
                ShowMessage("Transaction Rejected");
                BindVisitReport();
            }
        }
    }
    public void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", Message, true);
    }

    protected void gvAdminVisitReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatusApr = (Label)e.Row.FindControl("lblStatusApr");
            Label lblStatusRej = (Label)e.Row.FindControl("lblStatusRej");
            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
            if (lblStatusApr.Text == "Approved")
            {
                lnkApprove.Visible = false;
                lblStatusApr.Visible = true;
                lnkReject.Visible = false;
                lblStatusRej.Visible = false;
            }
            else if (lblStatusRej.Text == "Rejected")
            {
                lnkApprove.Visible = false;
                lblStatusApr.Visible = false;
                lnkReject.Visible = false;
                lblStatusRej.Visible = true;
            }
            else if (lblStatusApr.Text == "Pending")
            {
                lblStatusApr.Visible = false;
                lblStatusRej.Visible = false;
                lnkApprove.Visible = true;
                lnkReject.Visible = true;
            }
        }
    }
}