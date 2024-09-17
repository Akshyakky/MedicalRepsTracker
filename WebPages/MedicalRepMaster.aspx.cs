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
    clsEncryptDecrypt onjEncryptDecript = new clsEncryptDecrypt();
    DataManager dm = new DataManager();
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    cls_MedicalRepsMaster objMedRep = new cls_MedicalRepsMaster();
    clsEncryptDecrypt objEncrpt = new clsEncryptDecrypt();
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
                BindMedRepsDetails();
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
            int UserId = 0;
            if (strRole == "Admin")
            {
                BranchMid = int.Parse(ddlBranch.SelectedValue);
            }
            else if (Request.Cookies["User_MasterId"] != null)
            {
                UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
                if (Request.Cookies["User_BranchMID"] != null)
                {
                    BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);
                }
            }
            string MedRepName = txtMedicalRepName.Text;
            string MedRepNumber = txtNumber.Text;
            string MedRepAddress = txtCAddress.Text;
            string MedRepContactNo = txtMobile.Text;
            string MedRepUserName = txtUsername.Text;
            string MedRepPsw = objEncrpt.Encrypt(txtPassword.Text);
            if (btnSubmit.Text == "Submit")
            {
                string result = objMedRep.MedicalRepsMasterTransaction(2, 0, MedRepName, MedRepNumber, MedRepAddress, MedRepContactNo, MedRepUserName, MedRepPsw, BranchMid, "", "", "", "", "", UserId);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Medical Reps Information Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage("Medical Reps Information Added Successfully");

                            BindMedRepsDetails();

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

                if (txtMedicalRepName.Text.Trim() != Session["txtMedicalRepName"].ToString().Trim() || txtNumber.Text.Trim() != Session["txtNumber"].ToString().Trim() || txtCAddress.Text.Trim() != Session["txtCAddress"].ToString().Trim() || txtMobile.Text.Trim() != Session["txtMobile"].ToString().Trim() || txtUsername.Text.Trim() != Session["txtUsername"].ToString().Trim() || ddlBranch.SelectedValue != Session["ddlBranch"].ToString().Trim())
                {
                    string result = objMedRep.MedicalRepsMasterTransaction(3, int.Parse(Session["DocMid"].ToString()), MedRepName, MedRepNumber, MedRepAddress, MedRepContactNo, MedRepUserName, MedRepPsw, BranchMid, "", "", "", "", "", UserId);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("Medical Reps Information Updated Successfully");

                            UpReset();
                            BindMedRepsDetails();
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
        txtMedicalRepName.Text = "";
        txtNumber.Text = "";
        txtCAddress.Text = "";
        txtMobile.Text = "";
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtCnfrmPaswrd.Text = "";
        ddlBranch.SelectedIndex = 0;
    }

    private void UpReset()
    {
        txtMedicalRepName.Text = "";
        txtNumber.Text = "";
        txtCAddress.Text = "";
        txtMobile.Text = "";
        txtUsername.Text = "";
        ddlBranch.ClearSelection();
        btnReset.Text = "Reset";
        btnSubmit.Text = "Submit";
        divViewItem.Visible = true;
        divAddItems.Visible = false;
        DivPassword.Visible = true;
        DivRenterpassword.Visible = true;
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
        txtSrchMedicalName.Text = "";
        txtContactNumber.Text = "";
        txtSrchNumber.Text = "";
        txtSrchUsername.Text = "";
        ddlShowTop.SelectedIndex = 1;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindMedRepsDetails();

        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("btnSearch_Click", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void BindMedRepsDetails()
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
        string MPName = txtSrchMedicalName.Text;
        string MPNumber = txtSrchNumber.Text;
        string MPAddress = txtSrchAddress.Text;
        string MPontactNumber = txtContactNumber.Text;
        string MPUserName = txtSrchUsername.Text;

        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objMedRep.GetMedRepsDetails(GetRecords, MPName, MPNumber, MPAddress, MPontactNumber, MPUserName, BranchMid);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvMRepsDetail.DataSource = ds.Tables[1];
                gvMRepsDetail.DataBind();
                

                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found of " + strTotCount;
            }
            else
            {
                gvMRepsDetail.EmptyDataText = "No records found";
                gvMRepsDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();
                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found of " + strTotCount;
            }
        }
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ClearView();
            BindMedRepsDetails();
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

            string[] str = e.CommandArgument.ToString().Split(',');
            string ItemCategoryName = str[1].ToString();

            DocMid = int.Parse(str[0].ToString());
            Session["DocMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objMedRep.GetMedRepsMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DivPassword.Visible = false;
                        DivRenterpassword.Visible = false;

                        txtMedicalRepName.Text = ds.Tables[0].Rows[0]["MP_Name"].ToString();
                        txtUsername.Text = ds.Tables[0].Rows[0]["MP_UserName"].ToString();
                        txtNumber.Text = ds.Tables[0].Rows[0]["MP_Number"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["MP_ContactNo"].ToString();
                        txtCAddress.Text = ds.Tables[0].Rows[0]["MP_Address"].ToString();
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["MP_BranchMid"].ToString();
                        Session["txtNumber"] = txtNumber.Text;
                        Session["txtMobile"] = txtMobile.Text;
                        Session["ddlBranch"] = ddlBranch.SelectedValue;
                        Session["txtCAddress"] = txtCAddress.Text;
                        Session["txtMedicalRepName"] = txtMedicalRepName.Text;
                        Session["txtUsername"] = txtUsername.Text;
                    }
                }

                divAddItems.Visible = true;
                divViewItem.Visible = false;
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
            }

            if (e.CommandName == "btnDelete")
            {
                string result = objMedRep.MedicalRepsMasterTransaction(4, DocMid, "", "", "", "", "", "", 0, "", "", "", "", "", 0);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("Medical Reps Information Deleted Successfully");



                    BindMedRepsDetails();
                }
                else
                {
                    ShowMessage("Unsuccessfull try again");
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
            Label lblpsw = (Label)e.Row.FindControl("lblpsw");
            lblpsw.Text = objEncrpt.Decrypt(lblpsw.Text);
        }
    }

}