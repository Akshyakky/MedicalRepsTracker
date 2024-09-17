using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_BranchMaster : System.Web.UI.Page
{
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    DataManager dm = new DataManager();
    cls_BranchMaster objBranchMaster = new cls_BranchMaster();
    clsEncryptDecrypt objEncrpt = new clsEncryptDecrypt();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindRSM(ddlRSM);
                BindRSM1(ddlRSMSrch);
                BindBranchDetails();
                

            }
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("Page_Load", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void BindRSM(DropDownList ddlRSM)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT RSM_Mid,RSM_Name FROM dbo.tbl_RSMMaster WHERE RSM_AF=1 ", ddlRSM);
    }

    private void BindRSM1(DropDownList ddlRSM)
    {
        dm.BindDropDown("select 0,'--ALL--' union all SELECT RSM_Mid,RSM_Name FROM dbo.tbl_RSMMaster WHERE RSM_AF=1 ", ddlRSM);
    }

    private void BindBranchDetails()
    {
        string RSMName = "";
        string BrnchName = txtSrchBranchName.Text;
        if(ddlRSMSrch.SelectedIndex>0)
        {
            RSMName = ddlRSMSrch.SelectedItem.Text;
        }
        
        


        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objBranchMaster.GetBranchDetails(GetRecords, BrnchName, RSMName);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvBranchDetail.DataSource = ds.Tables[1];
                gvBranchDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
            else
            {
                gvBranchDetail.EmptyDataText = "No records found";
                gvBranchDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();
                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int UserId = 0;
            int RSMMid = 0;
            if (ddlRSM.SelectedIndex > 0)
            {
                RSMMid = int.Parse(ddlRSM.SelectedValue);
            }
            if (Request.Cookies["User_MasterId"] != null)
            {
                UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
            }
            string BranchName = txtBranchName.Text;
            string BranchDescription = txtBranchDescription.Text;



            if (btnSubmit.Text == "Submit")
            {
                string result = objBranchMaster.BranchMasterTransaction(2, 0, BranchName, RSMMid, BranchDescription, "", "", "", "", UserId);

                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Branch Information Already Exist.");

                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage(" Branch Information Added Successfully");
                            BindBranchDetails();


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

                if (ddlRSM.SelectedValue != Session["ddlRSM"].ToString().Trim() || txtBranchName.Text.Trim() != Session["txtBranchName"].ToString().Trim() || txtBranchDescription.Text.Trim() != Session["txtBranchDescription"].ToString().Trim())
                {
                    string result = objBranchMaster.BranchMasterTransaction(3, int.Parse(Session["BranchMid"].ToString()), BranchName, RSMMid, BranchDescription, "", "", "", "", UserId);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("Branch Information Updated Successfully");

                            UpReset();
                            BindBranchDetails();
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
                    BindBranchDetails();
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
    private void UpReset()
    {

        txtBranchName.Text = "";
        txtBranchDescription.Text = "";

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
    private void ClearAdd()
    {
        txtBranchName.Text = "";
        txtBranchDescription.Text = "";
        ddlRSM.ClearSelection();

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
    private void ClearView()
    {
        ddlShowTop.SelectedIndex = 1;
        ddlRSMSrch.ClearSelection();

        txtSrchBranchName.Text = "";



    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindBranchDetails();

        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("btnSearch_Click", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ClearView();
            BindBranchDetails();
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
            Session["BranchMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objBranchMaster.GetBranchMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        txtBranchName.Text = ds.Tables[0].Rows[0]["Branch_Name"].ToString();
                        txtBranchDescription.Text = ds.Tables[0].Rows[0]["Branch_Description"].ToString();
                        ddlRSM.SelectedValue = ds.Tables[0].Rows[0]["Branch_RSMMid"].ToString();
                        Session["ddlRSM"] = ddlRSM.SelectedValue;
                        Session["txtBranchName"] = txtBranchName.Text;



                        Session["txtBranchDescription"] = txtBranchDescription.Text;

                    }
                }
                divAddItems.Visible = true;
                divViewItem.Visible = false;
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
            }

            if (e.CommandName == "btnDelete")
            {
                int UserId = 0;
                if (Request.Cookies["User_MasterId"] != null)
                {
                    UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
                }
                string result = objBranchMaster.BranchMasterTransaction(4, DocMid, "",0, "", "", "", "", "", UserId);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("Branch Information Deleted Successfully");



                    BindBranchDetails();
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
    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

}