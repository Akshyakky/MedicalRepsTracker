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

            if (Request.Cookies["MP_Mid"] != null)
            {
                UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
            }
            else
            {
                Response.Redirect("../webpages/SessionExpired.aspx");
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


            string Doctor = txtDoctor.Text;
            string MedicalRepName = txtmedrepName.Text;
            string location = txtSrchLocation.Text;
          

            string qry = "select " +
              "  (select MP_Name from tbl_MedicalRepsMaster where MP_Mid=Trans_RepMid  and MP_AF=1) RepName," +
          " Trans_Type+'-'+" +
              "  (case Trans_Type when ' Doctor' then" +
              "  (select Doc_Name  from tbl_DoctorMaster  where Doc_Mid=Trans_Doc_Chem_Mid)" +
            " when 'Chemist' then" +
               " (select Che_Name from tbl_ChemistMaster where  Che_Mid=Trans_Doc_Chem_Mid)" +
            " when 'Stockiest' then" +
               " (select  ST_Name from tbl_StockiestMaster where  ST_Mid=Trans_Doc_Chem_Mid)" +
           " END) AS DOCCHESTNAME," +
            "    convert(nvarchar,Trans_VisitDateTime,103)visitDate,CAST(Trans_VisitTime as nvarchar)Visittime,Trans_ChemistMeet,Trans_Status,Trans_Loc_Lan+','+Trans_Loc_Lat LOC ,(Case Trans_ChemistMeet when 'Yes' then  (Select [Che_Name]  from tbl_ChemistMaster "+
 " where [Che_Mid]=[Trans_ChemMid] and [Che_AF]=1 )  when 'No' then '-' END) as ChemName, Trans_Extra1" +
            "    FROM tbl_Transaction where  [Trans_AF]=1 ";

            if(ddlShowTop.SelectedIndex>0)
            {
                qry= "select top("+ ddlShowTop.SelectedItem.Text +") " +
              "  (select MP_Name from tbl_MedicalRepsMaster where MP_Mid=Trans_RepMid  and MP_AF=1) RepName," +
          " Trans_Type+'-'+" +
              "  (case Trans_Type when ' Doctor' then" +
              "  (select Doc_Name  from tbl_DoctorMaster  where Doc_Mid=Trans_Doc_Chem_Mid)" +
            " when 'Chemist' then" +
               " (select Che_Name from tbl_ChemistMaster where  Che_Mid=Trans_Doc_Chem_Mid)" +
            " when 'Stockiest' then" +
               " (select  ST_Name from tbl_StockiestMaster where  ST_Mid=Trans_Doc_Chem_Mid)" +
           " END) AS DOCCHESTNAME," +
            "    convert(nvarchar,Trans_VisitDateTime,103)visitDate,CAST(Trans_VisitTime as nvarchar)Visittime,Trans_ChemistMeet,Trans_Status,Trans_Loc_Lan+','+Trans_Loc_Lat LOC ,(Case Trans_ChemistMeet when 'Yes' then  (Select [Che_Name]  from tbl_ChemistMaster " +
 " where [Che_Mid]=[Trans_ChemMid] and [Che_AF]=1 )  when 'No' then '-' END) as ChemName, Trans_Extra1" +
            "    FROM tbl_Transaction where  [Trans_AF]=1 ";

            }


            if (Doctor.Trim() != "")
            {
                qry += " and ((select Doc_Name from tbl_DoctorMaster where Doc_Mid=Trans_Doc_Chem_Mid  and Doc_AF=1) like '%" +
                       Doctor.Trim() +
                       "%' or (select Che_Name from tbl_ChemistMaster where  Che_Mid=Trans_Doc_Chem_Mid  and CHE_AF=1) like '%" +
                       Doctor.Trim() +
                       "%' or (select  ST_Name from tbl_StockiestMaster where  ST_Mid=Trans_Doc_Chem_Mid  and ST_AF=1) like '%" +
                       Doctor.Trim() + "%') ";
            }

            if (MedicalRepName != "")
            {
                qry += " and (select MP_Name from tbl_MedicalRepsMaster where MP_Mid=Trans_RepMid) like '%" + MedicalRepName + "%' ";
            } if (location != "")
            {
                qry += " and Trans_Location like '%" + location + "%' ";
            }

            if (txtFrom.Text != "")
            {
                string strfrm = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd").Replace("-","/");
                qry += " and  convert(nvarchar,Trans_VisitDateTime,111)>='" + strfrm + "'";
            }
            if (txtTo.Text != "")
            {
                string strfrm = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd").Replace("-", "/");
                qry += " and  convert(nvarchar,Trans_VisitDateTime,111)<='" + strfrm + "'";
            }
            qry += " and  [Trans_RepMid]='" + UserId + "' order by Trans_Mid desc";
            DataSet dsd = dm.GetDataSet(qry);
            gvStockOutReport.DataSource = dsd;
            gvStockOutReport.DataBind();
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
        int UserId = 0;

        if (Request.Cookies["MP_Mid"] != null)
        {
            UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
        }
        else
        {
            Response.Redirect("../webpages/SessionExpired.aspx");
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
        lblHeading.Text = "Stock Out Report " + Today;
        Response.Clear();
        long RanNo = DateTime.Today.Ticks;
        string dtToday = DateTime.Now.ToString("dd_MM_yyyy");
        Response.AddHeader("content-disposition", "attachment;filename=MedicalRepsVisitReport_" + dtToday + RanNo + ".xls");
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
        Refresh();
    }

    private void Refresh()
    {
        //ddlSrchDispensary.SelectedIndex = 0;
        //ddlSrchSource.SelectedIndex = 0;

        //txtInvNo.Text = "";
        txtDoctor.Text = "";
        txtSrchLocation.Text = "";
        txtFrom.Text = "";
        //txtItem.Text = "";
        txtTo.Text = "";
        ddlShowTop.SelectedIndex = 0;
        int UserId = 0;

        if (Request.Cookies["MP_Mid"] != null)
        {
            UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
        }
        else
        {
            Response.Redirect("../webpages/SessionExpired.aspx");
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
            if(lblStatus.Text=="Pending")
            {
                lblStatus.ForeColor = System.Drawing.Color.Gray;
            }
            else if(lblStatus.Text=="Approved")
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