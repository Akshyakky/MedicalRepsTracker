using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_ItemMaster : System.Web.UI.Page
{
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    DataManager dm = new DataManager();
    cls_ItemMaster objItemMaster = new cls_ItemMaster();
    clsEncryptDecrypt objEncrpt = new clsEncryptDecrypt();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                BindItemDetails();

            }
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("Page_Load", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void BindItemDetails()
    {
        string ItemName = txtSrchItemName.Text;

        

        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objItemMaster.GetItemDetails(GetRecords, ItemName);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvItemDetail.DataSource = ds.Tables[1];
                gvItemDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
            else
            {
                gvItemDetail.EmptyDataText = "No records found";
                gvItemDetail.DataBind();
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
            if (Request.Cookies["User_MasterId"] != null)
            {
                UserId = int.Parse(Request.Cookies["User_MasterId"].Value);
            }
            string ItemName = txtItemName.Text;
            string ItemDescription = txtItemDescription.Text;

            

            if (btnSubmit.Text == "Submit")
            {
                string result = objItemMaster.ItemMasterTransaction(2, 0, ItemName, ItemDescription, "", "", "", "","", UserId);
               
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Item Information Already Exist.");

                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage("Item Information Added Successfully");
                            BindItemDetails();


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

                if (txtItemName.Text.Trim() != Session["txtItemName"].ToString().Trim() || txtItemDescription.Text.Trim() != Session["txtItemDescription"].ToString().Trim())
                {
                    string result = objItemMaster.ItemMasterTransaction(3, int.Parse(Session["ItemMid"].ToString()), ItemName, ItemDescription, "", "", "", "", "",UserId);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("Item Information Updated Successfully");

                            UpReset();
                            BindItemDetails();
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
                    BindItemDetails();
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

        txtItemName.Text = "";
        txtItemDescription.Text = "";

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
        txtItemName.Text = "";
        txtItemDescription.Text = "";

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

        txtSrchItemName.Text = "";



    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindItemDetails();

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
            BindItemDetails();
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
            Session["ItemMid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objItemMaster.GetItemMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        txtItemName.Text = ds.Tables[0].Rows[0]["Item_Name"].ToString();
                        txtItemDescription.Text = ds.Tables[0].Rows[0]["Item_Description"].ToString();


                        Session["txtItemName"] = txtItemName.Text;



                        Session["txtItemDescription"] = txtItemDescription.Text;

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
                string result = objItemMaster.ItemMasterTransaction(4, DocMid, "", "", "", "", "", "", "", UserId);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("Item Information Deleted Successfully");



                    BindItemDetails();
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