using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_SampleConsumedReport : System.Web.UI.Page
{
    string strRole = "";
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    //clsStockOutSales objStock = new clsStockOutSales();
    //clsLog objLog = new clsLog();
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
          
            BindVisitReport();
           
        }
    }
  

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
            gvStockOutReport.DataSource = null;
            gvStockOutReport.DataBind();


            string Product = txtProductName.Text;
            string MedicalRepName = txtmedrepName.Text;
            string location = txtSrchLocation.Text;


            string qry = "select [MedTrans_ProdName],[MedTrans_Qty],[Trans_Location],[Trans_Loc_Lan],[Trans_Loc_Lat],[MP_Name],CAST(Trans_VisitTime as nvarchar)Visittime, convert(nvarchar,Trans_VisitDateTime,103)visitDate, Trans_Loc_Lan+','+Trans_Loc_Lat LOC   " +
 " from[dbo].[tbl_MedicineTransaction] "+
 " inner join[dbo].[tbl_Transaction] "+
 "   on[MedTrans_TransMid]=[Trans_Mid] "+
 "   inner join[dbo].[tbl_MedicalRepsMaster] "+
 "   on[MP_Mid]=[Trans_RepMid] "+
 "   where[MedTrans_AF]=1  and[Trans_AF]=1 and [MP_AF]=1 ";



            if (BranchMid != 0)
            {
                qry += " and [MP_BranchMid]='" + BranchMid + "'";
            }

            if (MedicalRepName != "")
            {
                qry += " and [MP_Name] like '%" + MedicalRepName + "%' ";
            }
            if (location != "")
            {
                qry += " and Trans_Location like '%" + location + "%' ";
            }
            if(Product!="")
            {


                qry += " and MedTrans_ProdName like '%" + Product + "%' ";
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
            qry += " order by MedTrans_Mid desc";
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
        lblHeading.Text = "Sample Consumed Report " + Today;
        Response.Clear();
        long RanNo = DateTime.Today.Ticks;
        string dtToday = DateTime.Now.ToString("dd_MM_yyyy");
        Response.AddHeader("content-disposition", "attachment;filename=SampleConsumedReport_" + dtToday + RanNo + ".xls");
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
        txtProductName.Text = "";
        txtSrchLocation.Text = "";
        txtFrom.Text = "";
        //txtItem.Text = "";
        txtTo.Text = "";
        ddlShowTop.SelectedIndex = 0;
        int UserId = 0;

      
        BindVisitReport();
    }

    

}