using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_ItemCategory : System.Web.UI.Page
{
    string strRole = "";
    DataManager dm = new DataManager();
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    cls_DoctorMaster objDocMaster = new cls_DoctorMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["User_MasterId"] != null)
            {
                if (Request.Cookies["User_Role"] != null)
                {
                    if (Request.Cookies["User_Role"].Value == "Admin")
                    {
                        strRole = Request.Cookies["User_Role"].Value;
                        divBrnch.Visible = true;
                    }
                    else
                    {
                        strRole = Request.Cookies["User_Role"].Value;
                        divBrnch.Visible = false;
                    }
                }
            }
            if (!IsPostBack)
            {

                BindDoctorDetails();
                BindBranch(ddlBranch);

            }
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("Page_Load", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }
    private void BindBranch(DropDownList ddl)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT Branch_Mid,Branch_Name FROM dbo.tbl_BranchMaster WHERE Branch_AF=1", ddl);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int BranchMid = 0;
            if (strRole == "Admin")
            {
                BranchMid = int.Parse(ddlBranch.SelectedValue);
            }
            else if (Request.Cookies["User_MasterId"] != null)
            {
                if (Request.Cookies["User_BranchMID"] != null)
                {
                    BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);
                }
            }
            string DoctorName = txtDoctor.Text;
            string DoctorNumber = txtNumber.Text;
            string DoctorAddress = txtCAddress.Text;
            string DoctorContactNo = txtMobile.Text;
            string DoctorLocation = txtLocation.Text;
            if (btnSubmit.Text == "Submit")
            {
                string result = objDocMaster.DoctorMasterTransaction(2, 0, DoctorName, DoctorNumber, DoctorAddress, DoctorContactNo, DoctorLocation, BranchMid, "Unblocked", "", "", "", 0);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Doctor Information Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage(" Doctor Information Added Successfully");

                           BindDoctorDetails();
                           ClearAdd();
                        }
                    }


                }
                else
                {
                    ShowMessage("Unsuccessfull try again");
                }
            }
            else if (btnSubmit.Text == "Update")
            {

                if (txtDoctor.Text.Trim() != Session["txtDoctor"].ToString().Trim() || txtNumber.Text.Trim() != Session["txtNumber"].ToString().Trim() || txtCAddress.Text.Trim() != Session["txtCAddress"].ToString().Trim() || txtMobile.Text.Trim() != Session["txtMobile"].ToString().Trim() || txtLocation.Text.Trim() != Session["txtLocation"].ToString().Trim() || ddlBranch.SelectedValue != Session["ddlBranch"].ToString().Trim())
                {
                    string result = objDocMaster.DoctorMasterTransaction(3, int.Parse(Session["DocMid"].ToString()), DoctorName, DoctorNumber, DoctorAddress, DoctorContactNo, DoctorLocation, BranchMid, "", "", "", "", 0);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("Doctor Information Updated Successfully");
                            UpReset();
                            BindDoctorDetails();


                        }

                    }
                    else
                    {
                        ShowMessage("Unsuccessfull try again");

                    }
                }
                else
                {
                    ShowMessage("No Modifications Done");

                    UpReset();

                }
            }

        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("btnSubmit_Click", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void ClearAdd()
    {
        txtDoctor.Text = "";
        txtNumber.Text = "";
        txtCAddress.Text = "";
        txtMobile.Text = "";
        txtLocation.Text = "";
        ddlBranch.SelectedIndex = 0;
    }

    private void UpReset()
    {
        txtDoctor.Text = "";
        txtNumber.Text = "";
        txtCAddress.Text = "";
        txtMobile.Text = "";
        txtLocation.Text = "";
        ddlBranch.ClearSelection();
        btnReset.Text = "Reset";
        btnSubmit.Text = "Submit";
        divViewItem.Visible = true;
        divAddItems.Visible = false;
    }
    public void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", Message, true);
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        divViewItem.Visible = false;
        divAddItems.Visible = true;
        ClearAdd();
        btnSubmit.Text = "Submit";
        btnReset.Text = "Reset";


    }
    protected void btnAddView_Click(object sender, EventArgs e)
    {
        divViewItem.Visible = true;
        divAddItems.Visible = false;
        ClearView();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Cancel")
        {
            btnSubmit.Text = "Submit";
            btnReset.Text = "Reset";
            divViewItem.Visible = true;
            divAddItems.Visible = false;
        }
        ClearAdd();
    }

    private void ClearView()
    {
        txtSrchAddress.Text = "";
        txtSrchDoctorName.Text = "";
        txtSrchLocation.Text = "";
        txtSrchNumber.Text = "";
        txtContactNumber.Text = "";
        ddlShowTop.SelectedIndex = 1;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDoctorDetails();
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("btnSearch_Click", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void BindDoctorDetails()
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
        string DName = txtSrchDoctorName.Text;
        string DNumber = txtSrchNumber.Text;
        string DAddress = txtSrchAddress.Text;
        string DContactNumber = txtContactNumber.Text;
        string DLocation = txtSrchLocation.Text;
        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objDocMaster.GetDoctorDetails(GetRecords, DName, DNumber, DAddress, DContactNumber, DLocation, BranchMid);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvDoctorDetail.DataSource = ds.Tables[1];
                gvDoctorDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
            else
            {
                gvDoctorDetail.EmptyDataText = "No records found";
                gvDoctorDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();
                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
        }
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ClearView();
            BindDoctorDetails();
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("btnRefresh_Click", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int DocMid = 0;
            int UserId = 0;
            if (Request.Cookies["User_MasterId"] != null)
            {
                UserId = int.Parse(Request.Cookies["User_MasterId"].Value.ToString());
            }
            string[] str = e.CommandArgument.ToString().Split(',');
            string ItemCategoryName = str[1].ToString();

            DocMid = int.Parse(str[0].ToString());
            Session["DocMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objDocMaster.GetDoctorMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtDoctor.Text = ds.Tables[0].Rows[0]["Doc_Name"].ToString();
                        txtLocation.Text = ds.Tables[0].Rows[0]["Doc_Location"].ToString();
                        txtNumber.Text = ds.Tables[0].Rows[0]["Doc_Number"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["Doc_ContactNumber"].ToString();
                        txtCAddress.Text = ds.Tables[0].Rows[0]["Doc_Address"].ToString();
                        txtLocation.Text = ds.Tables[0].Rows[0]["Doc_Location"].ToString();
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["Doc_BranchMid"].ToString();
                        Session["txtNumber"] = txtNumber.Text;
                        Session["txtMobile"] = txtMobile.Text;
                        Session["ddlBranch"] = ddlBranch.SelectedValue;
                        Session["txtCAddress"] = txtCAddress.Text;
                        Session["txtDoctor"] = txtDoctor.Text;
                        Session["txtLocation"] = txtLocation.Text;
                    }
                }
                divAddItems.Visible = true;
                divViewItem.Visible = false;
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
            }

            if (e.CommandName == "btnDelete")
            {
                string result = objDocMaster.DoctorMasterTransaction(4, DocMid, "", "", "", "", "", 0, "", "", "", "", 0);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("Doctor Information Deleted Successfully");
                    BindDoctorDetails();
                }
                else
                {
                    ShowMessage("Unsuccessfull try again");
                }
            }

            if (e.CommandName == "btnBlock")
            {
                string result = objDocMaster.DoctorMasterTransaction(5, DocMid, "", "", "", "", "", 0, "Blocked", "", "", "", 0);
                if (result == "1")
                {
                    ShowMessage("Successfully blocked");
                    BindDoctorDetails();
                }
            }
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("gvItems_RowCommand", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }
    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            LinkButton lnkBlock = (LinkButton)e.Row.FindControl("lnkBlock");
            if (lblStatus.Text == "Unblocked")
            {
                lnkBlock.Visible = true;
                lblStatus.Visible = false;
            }
            else if (lblStatus.Text == "Blocked")
            {
                lnkBlock.Visible = false;
                lblStatus.Visible = true;
            }
        }
    }
}