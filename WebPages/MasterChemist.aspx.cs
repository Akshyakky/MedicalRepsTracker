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
    cls_ChemistMaster objChemMaster = new cls_ChemistMaster();
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
            int UserId = 0;
            if (strRole == "Admin")
            {
                BranchMid = int.Parse(ddlBranch.SelectedValue);
            }
            else if (Request.Cookies["User_MasterId"] != null)
            {
                UserId= int.Parse(Request.Cookies["User_MasterId"].Value);
                if (Request.Cookies["User_BranchMID"] != null)
                {
                    BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);
                }
            }
            string ChemistName = txtChemistName.Text;
            string ChemistNumber = txtNumber.Text;
            string ChemistAddress = txtCAddress.Text;
            string ChemistContactNo = txtMobile.Text;
            string ChemistLocation = txtLocation.Text;
            if (btnSubmit.Text == "Submit")
            {
                string result = objChemMaster.ChemistMasterTransaction(2, 0, ChemistName, ChemistNumber, ChemistAddress, ChemistContactNo, ChemistLocation, BranchMid, "", "", "", "", UserId);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Chemist Information Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage("Chemist Information Added Successfully");
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

                if (txtChemistName.Text.Trim() != Session["txtChemistName"].ToString().Trim() || txtNumber.Text.Trim() != Session["txtNumber"].ToString().Trim() || txtCAddress.Text.Trim() != Session["txtCAddress"].ToString().Trim() || txtMobile.Text.Trim() != Session["txtMobile"].ToString().Trim() || txtLocation.Text.Trim() != Session["txtLocation"].ToString().Trim() || ddlBranch.SelectedValue != Session["ddlBranch"].ToString().Trim())
                {
                    string result = objChemMaster.ChemistMasterTransaction(3, int.Parse(Session["DocMid"].ToString()), ChemistName, ChemistNumber, ChemistAddress, ChemistContactNo, ChemistLocation, BranchMid, "", "", "", "", UserId);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("Chemist Information Updated Successfully");
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
        txtChemistName.Text = "";
        txtNumber.Text = "";
        txtCAddress.Text = "";
        txtMobile.Text = "";
        txtLocation.Text = "";
        ddlBranch.SelectedIndex = 0;
    }

    private void UpReset()
    {
        txtChemistName.Text = "";
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
        txtSrchChemistName.Text = "";
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
        string DName = txtSrchChemistName.Text;
        string DNumber = txtSrchNumber.Text;
        string DAddress = txtSrchAddress.Text;
        string DContactNumber = txtContactNumber.Text;
        string DLocation = txtSrchLocation.Text;
        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objChemMaster.GetChemistDetails(GetRecords, DName, DNumber, DAddress, DContactNumber, DLocation, BranchMid);
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

            string[] str = e.CommandArgument.ToString().Split(',');
            string ItemCategoryName = str[1].ToString();

            DocMid = int.Parse(str[0].ToString());
            Session["DocMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objChemMaster.GetChemMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtChemistName.Text = ds.Tables[0].Rows[0]["Che_Name"].ToString();
                        txtLocation.Text = ds.Tables[0].Rows[0]["Che_Location"].ToString();
                        txtNumber.Text = ds.Tables[0].Rows[0]["Che_Number"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["Che_ContactNo"].ToString();
                        txtCAddress.Text = ds.Tables[0].Rows[0]["Che_Address"].ToString();
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["Che_BranchMid"].ToString();
                        Session["txtNumber"] = txtNumber.Text;
                        Session["txtMobile"] = txtMobile.Text;
                   
                        Session["txtCAddress"] = txtCAddress.Text;
                        Session["txtChemistName"] = txtChemistName.Text;
                        Session["txtLocation"] = txtLocation.Text;
                        Session["ddlBranch"] = ddlBranch.SelectedValue;
                    }
                }
                divAddItems.Visible = true;
                divViewItem.Visible = false;
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
            }

            if (e.CommandName == "btnDelete")
            {
                string result = objChemMaster.ChemistMasterTransaction(4, DocMid, "", "", "", "", "", 0, "", "", "", "", 0);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("Chemist Information Deleted Successfully");
                    BindDoctorDetails();
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

    }
}