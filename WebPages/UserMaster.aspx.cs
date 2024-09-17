using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_UserMaster : System.Web.UI.Page
{
    string strRole = "";
    DataManager dm = new DataManager();
    clsEncryptDecrypt onjEncryptDecript = new clsEncryptDecrypt();
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    cls_UserMaster objUserMaster = new cls_UserMaster();
    cls_BranchMaster objBMaster = new cls_BranchMaster();
    clsEncryptDecrypt objEncrpt = new clsEncryptDecrypt();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindBranch(ddlSrchBranch);
                BindBranch(ddlBranch);
                BindUserDetails();

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

            string UserName = txtUserName.Text;
            string UserContactNumber = txtNumber.Text;
            string UserPsw = objEncrpt.Encrypt(txtPassword.Text);
            string UserRole = "";
            if (ddlRole.SelectedIndex > 0)
            {

                UserRole = ddlRole.SelectedItem.Text;
            }
            int BranchMid = 0;
            if (ddlBranch.SelectedIndex > 0)
            {
                BranchMid = int.Parse(ddlBranch.SelectedValue);
            }
            if (btnSubmit.Text == "Submit")
            {
                string result = objUserMaster.UserMasterTransaction(2, 0, UserName, UserPsw, UserRole, BranchMid, UserContactNumber, "", "", "", "", "", 0);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This User Information Already Exist.");

                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage(" User Information Added Successfully");
                            BindUserDetails();


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

                if (txtUserName.Text.Trim() != Session["txtUserName"].ToString().Trim() || txtNumber.Text.Trim() != Session["txtNumber"].ToString().Trim() || ddlBranch.SelectedValue != Session["ddlBranch"].ToString().Trim() || ddlRole.SelectedItem.Text.Trim() != Session["ddlRole"].ToString().Trim())
                {
                    string result = objUserMaster.UserMasterTransaction(3, int.Parse(Session["UserMid"].ToString()), UserName, UserPsw, UserRole, BranchMid, UserContactNumber, "", "", "", "", "", 0);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("User Information Updated Successfully");

                            UpReset();
                            BindUserDetails();
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
                    BindUserDetails();
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
        txtUserName.Text = "";
        txtNumber.Text = "";
        txtPassword.Text = "";
        txtCnfrmPaswrd.Text = "";
        ddlRole.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
    }

    private void UpReset()
    {
        DivPassword.Visible = true;
        DivRenterpassword.Visible = true;
        txtUserName.Text = "";
        txtNumber.Text = "";
        txtCnfrmPaswrd.Text = "";
        txtPassword.Text = "";
        ddlRole.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
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
        DivPassword.Visible = true;
        DivRenterpassword.Visible = true;
        ClearAdd();
        btnSubmit.Text = "Submit";
        btnReset.Text = "Reset";


    }
    protected void btnAddView_Click(object sender, EventArgs e)
    {
        divViewItem.Visible = true;
        divAddItems.Visible = false;
        DivPassword.Visible = true;
        DivRenterpassword.Visible = true;
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
        ddlShowTop.SelectedIndex = 1;
        ddlSrchBranch.SelectedIndex = 0;
        ddlSrchRole.SelectedIndex = 0;
        txtSrchUserName.Text = "";

        txtSrchContactNumber.Text = "";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindUserDetails();

        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("btnSearch_Click", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void BindUserDetails()
    {
        string UsrName = txtSrchUserName.Text;
        string CNumber = txtSrchContactNumber.Text;
        string SrchRole = "";
        if (ddlSrchRole.SelectedIndex > 0)
        {
            SrchRole = ddlSrchRole.SelectedItem.Text;
        }
        int SrchBranch = 0;
        if (ddlSrchBranch.SelectedIndex > 0)
        {
            SrchBranch = int.Parse(ddlSrchBranch.SelectedValue);
        }

        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objUserMaster.GetUserDetails(GetRecords, UsrName, SrchRole, SrchBranch, CNumber);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvUserDetail.DataSource = ds.Tables[1];
                gvUserDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
            else
            {
                gvUserDetail.EmptyDataText = "No records found";
                gvUserDetail.DataBind();
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
            BindUserDetails();
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
            Session["UserMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objUserMaster.GetUserMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DivPassword.Visible = false;
                        DivRenterpassword.Visible = false;
                        txtUserName.Text = ds.Tables[0].Rows[0]["User_Name"].ToString();
                        txtNumber.Text = ds.Tables[0].Rows[0]["User_MobileNo"].ToString();
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["User_BranchMid"].ToString();
                        ddlRole.SelectedValue = ds.Tables[0].Rows[0]["User_Role"].ToString();

                        Session["txtNumber"] = txtNumber.Text;

                        Session["ddlBranch"] = ddlBranch.SelectedValue;
                        Session["ddlRole"] = ddlRole.SelectedValue;

                        Session["txtUserName"] = txtUserName.Text;

                    }
                }
                divAddItems.Visible = true;
                divViewItem.Visible = false;
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
            }

            if (e.CommandName == "btnDelete")
            {
                string result = objUserMaster.UserMasterTransaction(4, DocMid, "", "", "", 0, "", "", "", "", "", "", 0);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("User Information Deleted Successfully");



                    BindUserDetails();
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
        if (Request.Cookies["User_MasterId"] != null)
        {
            if (Request.Cookies["User_Role"] != null)
            {
                if (Request.Cookies["User_Role"].Value == "Admin")
                {
                    strRole = Request.Cookies["User_Role"].Value;
                }
                else
                {
                    strRole = Request.Cookies["User_Role"].Value;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (strRole == "Admin")
            {
                Label lblpsw = (Label)e.Row.FindControl("lblpsw");
                lblpsw.Text = objEncrpt.Decrypt(lblpsw.Text);
            }
            else
            {
                gvUserDetail.Columns[7].Visible = false;
            }
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {



    }
}