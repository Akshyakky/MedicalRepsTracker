using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_ChemistPerDoctor : System.Web.UI.Page
{
    string strRole = "";
    DataManager dm = new DataManager();
    Cls_ErrorHandler objError = new Cls_ErrorHandler();
    cls_ChemistPerDoctor objChemistPerDoctor = new cls_ChemistPerDoctor();
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
                        //divBrnch.Visible = true;
                    }
                    else
                    {
                        strRole = Request.Cookies["User_Role"].Value;
                        BranchMid = int.Parse(Request.Cookies["User_BranchMID"].Value);
                        //divBrnch.Visible = false;
                    }
                }
            }
            if (!IsPostBack)
            {

                BindDoctor(ddlDoctor, BranchMid);
                BindChemist(ddlChemist, BranchMid);
                // BindDoctor(ddlSearchDoctor);
                // BindChemist(ddlSearchchemist);
                BindDetails();
            }
        }
        catch (Exception ex)
        {
            objError.SaveReportInXML("Page_Load", ex.Data.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void BindDetails()
    {



        string SrchDoctor = txtSearchDoctorName.Text;

        string SrchChemist = txtSearchChemistName.Text;

        string GetRecords = "";
        if (ddlShowTop.SelectedIndex > 0)
        {
            GetRecords = ddlShowTop.SelectedItem.Text;
        }
        DataSet ds = objChemistPerDoctor.GetChemistPerDoctorDetails(GetRecords, SrchDoctor, SrchChemist);
        if (ds != null)
        {
            string strTotCount = ds.Tables[0].Rows[0]["TotalRecords"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvChemistperDoctorDetail.DataSource = ds.Tables[1];
                gvChemistperDoctorDetail.DataBind();
                //lblRecordCount.Text = gvItems.Rows.Count.ToString();

                lblRecordsCount.Text = ds.Tables[1].Rows.Count + " Records Found out of " + strTotCount;
            }
            else
            {
                gvChemistperDoctorDetail.EmptyDataText = "No records found";
                gvChemistperDoctorDetail.DataBind();
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
            string Description = txtDescription.Text;

            int DoctorName = 0;
            if (ddlDoctor.SelectedIndex > 0)
            {
                DoctorName = int.Parse(ddlDoctor.SelectedValue);
            }
            int ChemistName = 0;
            if (ddlChemist.SelectedIndex > 0)
            {
                ChemistName = int.Parse(ddlChemist.SelectedValue);
            }
            if (btnSubmit.Text == "Submit")
            {

                int result = objChemistPerDoctor.ChemistPerDoctorTransaction(2, 0, DoctorName, ChemistName, Description, UserId);

                if (result > 0)
                {
                    ShowMessage("Information Added Successfully");
                    BindDetails();


                    ClearAdd();


                }
                else
                {
                    ShowMessage("Unsuccessfull try again");
                }





            }
            else if (btnSubmit.Text == "Update")
            {

                if (ddlDoctor.SelectedValue != Session["ddlDoctor"].ToString().Trim() || ddlChemist.SelectedItem.Text.Trim() != Session["ddlChemist"].ToString().Trim() || txtDescription.Text != Session["txtDescription"].ToString().Trim())
                {
                    int result = objChemistPerDoctor.ChemistPerDoctorTransaction(3, int.Parse(Session["Mid"].ToString()), DoctorName, ChemistName, Description, UserId);


                    if (result > 0)
                    {
                        ShowMessage("Information Updated Successfully");

                        UpReset();
                        BindDetails();
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
                    BindDetails();
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

        ddlDoctor.SelectedIndex = 0;
        ddlChemist.SelectedIndex = 0;
        txtDescription.Text = "";
    }
    private void UpReset()
    {

        ddlDoctor.SelectedIndex = 0;
        ddlChemist.SelectedIndex = 0;
        txtDescription.Text = "";
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

    private void BindDoctor(DropDownList ddl, int BranchMid)
    {

        string QRY = "select 0,'--SELECT--' union all SELECT Doc_Mid,Doc_Name FROM dbo.tbl_DoctorMaster WHERE Doc_AF=1 ";
        if (BranchMid > 0)
        {
            QRY += " and Doc_BranchMid='" + BranchMid + "'";
        }
        dm.BindDropDown(QRY, ddl);

    }
    private void BindChemist(DropDownList ddl, int BranchMid)
    {
        string QRY = "select 0,'--SELECT--' union all SELECT Che_Mid,Che_Name FROM dbo.tbl_ChemistMaster WHERE Che_AF=1 ";
        if (BranchMid > 0)
        {
            QRY += " and Che_BranchMid='" + BranchMid + "'";
        }
        dm.BindDropDown(QRY, ddl);

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
        ddlShowTop.SelectedIndex = 1;

        txtSearchDoctorName.Text = "";
        txtSearchChemistName.Text = "";


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindDetails();

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
            BindDetails();
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

            DocMid = int.Parse(e.CommandArgument.ToString());


            Session["Mid"] = DocMid;
            if (e.CommandName == "btnEdit")
            {
                DataSet ds = objChemistPerDoctor.GetChemPerDocMid(1, DocMid);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {


                        ddlDoctor.SelectedValue = ds.Tables[0].Rows[0]["ChemistPerDoctor_DoctorMid"].ToString();
                        ddlChemist.SelectedValue = ds.Tables[0].Rows[0]["ChemistPerDoctor_ChemistMid"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["ChemistPerDoctor_Description"].ToString();

                        Session["ddlDoctor"] = ddlDoctor.SelectedValue;
                        Session["ddlChemist"] = ddlChemist.SelectedValue;

                        Session["txtDescription"] = txtDescription.Text;

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
                int result = objChemistPerDoctor.ChemistPerDoctorTransaction(4, DocMid, 0, 0, "", UserId);

                if (result > 0)
                {
                    ShowMessage("Information Deleted Successfully");



                    BindDetails();
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