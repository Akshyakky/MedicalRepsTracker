using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_RSMMaster : System.Web.UI.Page
{
    DataManager dm = new DataManager();
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    cls_RSMMaster objRSM = new cls_RSMMaster();
    clsEncryptDecrypt objEncrpt = new clsEncryptDecrypt();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindRSMDetails();

            }
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("Page_Load", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }
    private void BindRSMDetails()
    {
        string RSMRegionName = txtSrchRegionName.Text;
        string RSMName = txtSearchRSMName.Text; 
     
     
        string RSMUserName  = txtSrchUsername.Text;

        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objRSM.GetRSMDetails(GetRecords, RSMRegionName, RSMName, RSMUserName);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvRSMDetail.DataSource = ds.Tables[1];
                gvRSMDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found of " + strTotCount;
            }
            else
            {
                gvRSMDetail.EmptyDataText = "No records found";
                gvRSMDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();
                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found of " + strTotCount;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Role = "Admin";
            int UserId = 0;
            if (Request.Cookies["User_MasterId"] != null)
            {
                UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
            }
            string RSMRegionName = txtRegionName.Text;
            string RSMName = txtRSMName.Text;  
          
            string RSMUserName = txtUsername.Text;
            string RSMPsw = objEncrpt.Encrypt(txtPassword.Text);
            if (btnSubmit.Text == "Submit")
            {
                string result = objRSM.RSMMasterTransaction(2, 0, RSMRegionName, RSMName, RSMUserName, RSMPsw, Role, "", "", "", "", UserId);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This RSM Information Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage("RSM Information Added Successfully");

                            BindRSMDetails();

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


                if (txtRegionName.Text.Trim() != Session["txtRegionName"].ToString().Trim() || txtRSMName.Text.Trim() != Session["txtRSMName"].ToString().Trim() ||  txtUsername.Text.Trim() != Session["txtUsername"].ToString().Trim())
                {
                    string result = objRSM.RSMMasterTransaction(3, int.Parse(Session["RSMMid"].ToString()), RSMRegionName, RSMName, RSMUserName, RSMPsw, "", "", "", "", "", UserId);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("RSM Information Updated Successfully");

                            UpReset();
                            BindRSMDetails();
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
        txtRSMName.Text = "";
        txtRegionName.Text = ""; 
      
        txtUsername.Text = "";
        txtPassword.Text = "";
    }
    public void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", Message, true);
    }
    private void UpReset()
    {
        txtRegionName.Text = "";
        txtRSMName.Text = ""; 
       
        txtUsername.Text = "";
        btnReset.Text = "Reset";
        btnSubmit.Text = "Submit";
        divViewItem.Visible = true;
        divAddItems.Visible = false;
        DivPassword.Visible = true;
        DivRenterpassword.Visible = true;
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
        txtSrchRegionName.Text = "";
        txtSearchRSMName.Text = ""; 
       
        txtSrchUsername.Text = "";
        ddlShowTop.SelectedIndex = 1;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindRSMDetails();

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
            BindRSMDetails();
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
            Session["RSMMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objRSM.GetRSMMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0) 
                    {
                        DivPassword.Visible = false;
                        DivRenterpassword.Visible = false;
                        txtRegionName.Text = ds.Tables[0].Rows[0]["RSM_RegionName"].ToString();
                        txtUsername.Text = ds.Tables[0].Rows[0]["RSM_UserName"].ToString();
                        txtRSMName.Text = ds.Tables[0].Rows[0]["RSM_Name"].ToString();


                        Session["txtRegionName"] = txtRegionName.Text;
                        Session["txtUsername"] = txtUsername.Text;


                        Session["txtRSMName"] = txtRSMName.Text;
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
                string result = objRSM.RSMMasterTransaction(4, DocMid, "", "", "", "", "", "", "", "", "", UserId);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("RSM Information Deleted Successfully");



                    BindRSMDetails();
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