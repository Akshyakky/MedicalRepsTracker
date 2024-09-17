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
    cls_ProductMaster objProductMaster = new cls_ProductMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int BranchMid = 0;
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
                        BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);

                        divBrnch.Visible = false;
                    }
                }
            }
            if (!IsPostBack)
            {
                dm.BindDropDown("select 0,'--SELECT--' union all SELECT MP_Mid,MP_Name FROM dbo.tbl_MedicalRepsMaster WHERE MP_AF=1 and MP_BranchMid='" + BranchMid + "'", ddlMedReps);
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
            int ItemMid = objProductMaster.GetProductMid(7, txtProductName.Text);
            int RepMid = int.Parse(ddlMedReps.SelectedValue);
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
            string ProductName = txtProductName.Text;
            int ProductQty = int.Parse(txtQty.Text);
            string ProductDescription = txtDescription.Text;

            if (btnSubmit.Text == "Submit")
            {
                string result = objProductMaster.ProductMasterTransaction(2, 0, ProductName, ProductQty, ProductDescription, BranchMid, ItemMid, RepMid, "", "", "", "", UserId);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Product Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessage("Product Added Successfully");
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

                if (txtProductName.Text.Trim() != Session["txtProductName"].ToString().Trim() || txtQty.Text.Trim() != Session["txtQty"].ToString().Trim() || txtDescription.Text.Trim() != Session["txtDescription"].ToString().Trim() || ddlMedReps.SelectedValue != Session["ddlMedReps"].ToString().Trim() || ddlBranch.SelectedValue != Session["ddlBranch"].ToString().Trim())
                {
                    string result = objProductMaster.ProductMasterTransaction(3, int.Parse(Session["DocMid"].ToString()), ProductName, ProductQty, ProductDescription, BranchMid, ItemMid, RepMid, "", "", "", "", UserId);
                    if (result != "")
                    {

                        int r = int.Parse(result.ToString());
                        if (r > 0)
                        {
                            ShowMessage("Product Updated Successfully");
                            BindDoctorDetails();
                            UpReset();

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
        txtProductName.Text = "";
        txtQty.Text = "";
        txtDescription.Text = "";
        ddlBranch.ClearSelection();
        ddlMedReps.ClearSelection();
    }

    private void UpReset()
    {
        txtProductName.Text = "";
        txtQty.Text = "";
        txtDescription.Text = "";
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
        txtSrchProductName.Text = "";
        txtSrchQty.Text = "";
        txtSrchDescription.Text = "";
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

    /// <summary>
    /// 
    /// </summary>
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

        string MPName = txtSrchMedicalName.Text;
        string PName = txtSrchProductName.Text;
        string PQty = txtSrchQty.Text;
        string PDescription = txtSrchDescription.Text;

        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objProductMaster.GetProductDetails(GetRecords, PName, PQty, PDescription, BranchMid, MPName);
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
                DataSet ds = objProductMaster.GetProdMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtProductName.Text = ds.Tables[0].Rows[0]["Prod_Name"].ToString();

                        txtQty.Text = ds.Tables[0].Rows[0]["Prod_Qty"].ToString();

                        txtDescription.Text = ds.Tables[0].Rows[0]["Prod_Description"].ToString();

                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["Prod_BranchMid"].ToString();
                        ddlBranch_SelectedIndexChanged(null, null);
                        ddlMedReps.SelectedValue = ds.Tables[0].Rows[0]["Prod_MRepMid"].ToString();
                        Session["txtQty"] = txtQty.Text;
                        Session["ddlBranch"] = ddlBranch.SelectedValue;
                        Session["ddlMedReps"] = ddlMedReps.SelectedValue;


                        Session["txtDescription"] = txtDescription.Text;
                        Session["txtProductName"] = txtProductName.Text;

                    }
                }
                divAddItems.Visible = true;
                divViewItem.Visible = false;
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
            }

            if (e.CommandName == "btnDelete")
            {
                string result = objProductMaster.ProductMasterTransaction(4, DocMid, "", 0, "", 0, 0, 0, "", "", "", "", 0);
                int r = int.Parse(result.Split(',')[0]);
                if (r > 0)
                {
                    ShowMessage("Product Deleted Successfully");



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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
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
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT MP_Mid,MP_Name FROM dbo.tbl_MedicalRepsMaster WHERE MP_AF=1 and MP_BranchMid='" + BranchMid + "'", ddlMedReps);
    }

    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {

        int ItemMid = objProductMaster.GetProductMid(7, txtProductName.Text);
        if (ItemMid == 0)
        {
            ShowMessage("Invalid Item");
            txtProductName.Text = "";
        }
    }
}