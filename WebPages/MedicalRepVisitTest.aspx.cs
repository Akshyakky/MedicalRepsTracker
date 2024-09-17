using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_MedicalRepVisitTest : System.Web.UI.Page
{
    DataManager dm = new DataManager();
    DataTable dt = new DataTable();
    cls_Transaction objMedRepVisit = new cls_Transaction();
    cls_ProductMaster objProduct = new cls_ProductMaster();
    cls_MedicineTransaction objMedicineTrans = new cls_MedicineTransaction();

    cls_DoctorMaster objDoctor = new cls_DoctorMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CalndrVDate.end
            AddEmptyRows();
            int BranchId = 0;
            if (Request.Cookies["MP_BranchMid"] != null)
            {
                BranchId = int.Parse(Request.Cookies["MP_BranchMid"].Value);
            }
            else
            {
                Response.Redirect("../SessionExpired.aspx");
            }
            //dm.BindDropDown("select 0,'--SELECT--' union all SELECT Doc_Mid,Doc_Name FROM dbo.tbl_DoctorMaster WHERE Doc_AF=1 and Doc_Status<>'Blocked' ", ddlDoctor);
            //BindDoctors(ddlDoctor, BranchId);
            BindChemist(ddlChemist, BranchId);
            BindStockiest(ddlStockiest, BranchId);
            BindBranchManager(ddlBM);
            BindRSM(ddlRSM);
        }
    }

    private void BindRSM(DropDownList ddlRSM)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT RSM_Mid,RSM_Name FROM dbo.tbl_RSMMaster WHERE RSM_AF=1 ", ddlRSM);
    }

    private void BindBranchManager(DropDownList ddlBM)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT User_Id,User_Name FROM dbo.tbl_UserMaster WHERE User_AF=1 and User_Role='BranchAdmin' ", ddlBM);
    }

    private void BindStockiest(DropDownList ddlStockiest, int BranchId)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT ST_Mid,ST_Name FROM dbo.tbl_StockiestMaster WHERE ST_AF=1 and ST_BranchMid='" + BranchId + "'", ddlStockiest);
    }

    private void BindChemist(DropDownList ddlChemist, int BranchId)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT Che_Mid,Che_Name FROM dbo.tbl_ChemistMaster WHERE Che_AF=1 and Che_BranchMid='" + BranchId + "'", ddlChemist);
    }

    private void BindDoctors(DropDownList ddlDoctor, int BranchId)
    {
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT Doc_Mid,Doc_Name FROM dbo.tbl_DoctorMaster WHERE Doc_AF=1 and Doc_Status<>'Blocked' and Doc_BranchMid ='" + BranchId + "'", ddlDoctor);
    }

    protected void rdbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbType.SelectedIndex == 0)
        {
            divDoctor.Visible = true;
            divChemist.Visible = false;
            divStockiest.Visible = false;
            DivAskMeetChemist.Visible = true;
            if (rdbAMeet.SelectedIndex == 0)
            {
                divChemist.Visible = true;
            }
            else
            {
                divChemist.Visible = false;
            }
        }

        else if (rdbType.SelectedIndex == 1)
        {
            divChemist.Visible = true;
            divDoctor.Visible = false;
            divStockiest.Visible = false;
            DivAskMeetChemist.Visible = false;
        }
        else if (rdbType.SelectedIndex == 2)
        {
            divStockiest.Visible = true;
            divDoctor.Visible = false;
            divChemist.Visible = false;
            DivAskMeetChemist.Visible = false;
        }
    }

    protected void rdbMedGiven_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMedGiven.SelectedIndex == 0)
        {
            divMedDetails.Visible = true;
            divMedDetails1.Visible = true;

            divMedicineView.Visible = true;
        }
        else if (rdbMedGiven.SelectedIndex == 1)
        {
            divMedDetails.Visible = false;
            divMedDetails1.Visible = false;
            divMedicineView.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (hdlatitude.Value != "" && hdlongitude.Value != "")
        {
            int BMMid = 0;
            int RSMMid = 0;
            if (rbdRSMorBM.SelectedIndex == 1 || rbdRSMorBM.SelectedIndex == 3)
            {
                if (ddlBM.SelectedIndex > 0)
                {
                    BMMid = int.Parse(ddlBM.SelectedValue);
                }

            }
            if (rbdRSMorBM.SelectedIndex == 2 || rbdRSMorBM.SelectedIndex == 3)
            {
                if (ddlRSM.SelectedIndex > 0)
                {
                    RSMMid = int.Parse(ddlRSM.SelectedValue);
                }
            }
            string WorkedWith = rbdRSMorBM.SelectedItem.Text;
            string Status = "Pending";
            string VTime = ddlTime.SelectedItem.Text + ":" + ddlMints.SelectedItem.Text + " " + ddlAMPM.SelectedItem.Text;
            if (rdbMedGiven.SelectedIndex == 0)
            {
                int UserId = 0;
                int ChemMid = 0;
                if (rdbType.SelectedIndex == 0)
                {
                    if (rdbAMeet.SelectedIndex == 0)
                    {
                        ChemMid = int.Parse(ddlChemist.SelectedValue);
                    }
                }
                if (Request.Cookies["MP_Mid"] != null)
                {
                    UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
                }
                else
                {
                    Response.Redirect("../SessionExpired.aspx");
                }
                string Location = txtLocation.Text;
                string Type = rdbType.SelectedItem.Text;
                int Doc_Stock_ChemMid = 0;
                if (rdbType.SelectedIndex == 0)
                {
                    if (txtDoctor.Text.Contains('-'))
                    {
                        Doc_Stock_ChemMid = int.Parse(txtDoctor.Text.Split('-')[1].ToString()); //added on 11/09/2019                           
                    }
                        //int.Parse(ddlDoctor.SelectedValue);
                }
                else if (rdbType.SelectedIndex == 1)
                {

                    Doc_Stock_ChemMid = int.Parse(ddlChemist.SelectedValue);
                }
                else if (rdbType.SelectedIndex == 2)
                {
                    Doc_Stock_ChemMid = int.Parse(ddlStockiest.SelectedValue);

                }
                string VisitDate = "";
                if (txtVisitDate.Text != "")
                {
                    VisitDate = DateTime.ParseExact(txtVisitDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                }
                string ChemMeets = rdbAMeet.SelectedItem.Text;
                string MedicineGiven = rdbMedGiven.SelectedItem.Text;
                int ProdMid = 0;
                int Qty = 0;
                string ProdName = "";
                string result = "";
                int TransactionMid = 0;
                if (gvedicineDetails.Rows.Count > 0)
                {
                    result = objMedRepVisit.TransactionTransaction(2, 0, UserId, Type, Doc_Stock_ChemMid, VisitDate, ChemMeets, Location, hdlatitude.Value, hdlongitude.Value, VTime, Status, WorkedWith, BMMid, RSMMid, txtComments.Text, "", "", "", ChemMid, "", UserId);

                    int Rs = int.Parse(result.Split(',')[1]);
                    TransactionMid = Rs + 1;


                }
                else
                {
                    ShowMessage("Add atleast one sample medicine details");

                }
                if (gvedicineDetails.Rows.Count > 0)
                {


                    for (int i = 0; i < gvedicineDetails.Rows.Count; i++)
                    {
                        Label lblProdMid = (Label)gvedicineDetails.Rows[i].FindControl("lblProdMid");
                        if (lblProdMid.Text != "")
                        {
                            ProdMid = int.Parse(lblProdMid.Text);
                        }
                        Label lblQty = (Label)gvedicineDetails.Rows[i].FindControl("lblQty");
                        if (lblQty.Text != "")
                        {
                            Qty = int.Parse(lblQty.Text);
                        }
                        Label lblProdName = (Label)gvedicineDetails.Rows[i].FindControl("lblProdName");
                        if (lblProdName.Text != "")
                        {
                            ProdName = lblProdName.Text;
                        }
                        objMedicineTrans.MedicineTransactionTransaction(2, 0, TransactionMid, ProdMid, ProdName, Qty, "", "", "", "", "", UserId, UserId);
                    }
                }

                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Medical Reps visit Information Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessageAndRedirect("Medical Reps visit Information Added Successfully", "MedicalRepVisit.aspx");
                            // BindMedRepsDetails();

                            ClearAdd();
                        }
                    }


                }
                else
                {
                    ShowMessage("Unsuccessfull try again");
                }
            }
            else if (rdbMedGiven.SelectedIndex == 1)
            {
                int ChemMid = 0;
                if (rdbAMeet.SelectedIndex == 0)
                {
                    ChemMid = int.Parse(ddlChemist.SelectedValue);
                }
                int UserId = 0;

                if (Request.Cookies["MP_Mid"] != null)
                {
                    UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
                }
                else
                {
                    Response.Redirect("../SessionExpired.aspx");
                }
                string Location = txtLocation.Text;
                string Type = rdbType.SelectedItem.Text;
                int Doc_Stock_ChemMid = 0;
                if (rdbType.SelectedIndex == 0)
                {
                    if (txtDoctor.Text.Contains('-'))
                    {
                        Doc_Stock_ChemMid = int.Parse(txtDoctor.Text.Split('-')[1].ToString()); //added on 11/09/2019                           
                    }

                        //0;
                        //objDoctor.GetDoctorMid(7, txtDoctor.Text);//added on 10/09/2019
                    //int.Parse(ddlDoctor.SelectedValue);
                }
                else if (rdbType.SelectedIndex == 1)
                {

                    Doc_Stock_ChemMid = int.Parse(ddlChemist.SelectedValue);
                }
                else if (rdbType.SelectedIndex == 2)
                {
                    Doc_Stock_ChemMid = int.Parse(ddlStockiest.SelectedValue);

                }
                string VisitDate = "";
                if (txtVisitDate.Text != "")
                {
                    VisitDate = DateTime.ParseExact(txtVisitDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                }
                string ChemMeets = rdbAMeet.SelectedItem.Text;
                string MedicineGiven = rdbMedGiven.SelectedItem.Text;
                string result = objMedRepVisit.TransactionTransaction(2, 0, UserId, Type, Doc_Stock_ChemMid, VisitDate, ChemMeets, Location, hdlatitude.Value, hdlongitude.Value, VTime, Status, WorkedWith, BMMid, RSMMid, txtComments.Text, "", "", "", ChemMid, "", UserId);
                if (result != "")
                {
                    if (result.Contains("True"))
                    {
                        ShowMessage("This Medical Reps visit Information Already Exist.");
                    }
                    else
                    {
                        int r = int.Parse(result.Split(',')[1]);
                        if (r > 0)
                        {
                            ShowMessageAndRedirect("Medical Reps visit Information Added Successfully", "MedicalRepVisit.aspx");
                            // BindMedRepsDetails();
                            ClearAdd();


                        }
                    }


                }
                else
                {
                    ShowMessage("Unsuccessfull try again");
                }
            }

        }
        else
        {
            ShowMessage("Turn on the location");
        }
    }

    private void ShowMessageAndRedirect(string msgs, string Page)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("function Redirect()");
            sb.Append("{");
            sb.Append(" window.location.href='" + Page + "';");
            sb.Append("}");
            sb.Append("alert('" + msgs + "');");
            sb.Append("setTimeout('Redirect()', 0);");
            Response.Write("<script>" + sb + "</script>");
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    private void ClearAdd()
    {

        txtMedicine.Text = "";
        txtQty.Text = "";
        gvedicineDetails.DataSource = null;
        gvedicineDetails.DataBind();
    }

    private void Clear()
    {

        txtMedicine.Text = "";
        txtQty.Text = "";
        gvedicineDetails.DataSource = null;
        gvedicineDetails.DataBind();
        txtLocation.Text = "";
      
        txtVisitDate.Text = "";
        rdbAMeet.SelectedIndex = 1;
        rdbType.SelectedIndex = 0;
        rdbMedGiven.SelectedIndex = 1;
        ddlChemist.SelectedIndex = 0;
        //ddlDoctor.SelectedIndex = 0;
        ddlStockiest.SelectedIndex = 0;

        txtDoctor.Text = "";//added on 10/09/2019
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("MedicalRepVisit.aspx");
    }

    protected void btnAddView_Click(object sender, EventArgs e)
    {

    }



    protected void gvStockOutItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            dt = (DataTable)Session["DataTable"];
            int ItemMid = int.Parse(e.CommandArgument.ToString());
            var query = dt.AsEnumerable().Where(r => r.Field<int>("ProductMid") == ItemMid);

            foreach (var row in query.ToList())
                row.Delete();
            gvedicineDetails.DataSource = dt;
            gvedicineDetails.DataBind();
            if (gvedicineDetails.Rows.Count > 0)
            {
                gvedicineDetails.Visible = true;

            }
            else
            {
                gvedicineDetails.Visible = false;
            }
        }
    }

    protected void gvStockOutItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnAdd_Click1(object sender, EventArgs e)
    {

        lblAvailQty.Text = Label20.Text;
        int flag = 0;
        int ProductMid = 0;
        string ProductName = "";
        if (txtMedicine.Text != "")
        {
            ProductName = txtMedicine.Text;
            ProductMid = objProduct.GetProductMid(7, ProductName);
        }
        int Qty = int.Parse(txtQty.Text);

        if (ProductMid != 0)
        {
            if (int.Parse(txtQty.Text) >= int.Parse(lblAvailQty.Text))
            {
                ShowMessage("Quantity should be less than available quantity");
                //string msg = "Quantity should be less than available quantity";
                txtQty.Text = "";
                txtMedicine.Text = "";
                //string Message = "alert('" + msg + "');"; 
                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", Message, true);

            }
            else
            {

                if (Session["DataTable"] == null)
                {
                    dt.Rows.Add(ProductMid, ProductName.ToString(), Qty);
                }
                else
                {
                    dt = (DataTable)Session["DataTable"];
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string IName = dt.Rows[i]["ProductName"].ToString();
                            int QtyOld = int.Parse(dt.Rows[i]["Qty"].ToString());

                            if (IName == txtMedicine.Text.ToString())
                            {
                                Qty += QtyOld;
                                dt.Rows.RemoveAt(i);
                                dt.Rows.Add(ProductMid, ProductName.ToString(), Qty);
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 1)
                    {

                    }
                    else
                    {

                        dt.Rows.Add(ProductMid, ProductName.ToString(), Qty);
                    }
                }
                Session["DataTable"] = dt;
                gvedicineDetails.DataSource = dt;
                gvedicineDetails.DataBind();
                gvedicineDetails.Visible = true;
                //Clear();
                txtMedicine.Text = "";
                txtQty.Text = "";
                lblAvailQty.Text = "";
                Label20.Text = "";
                decimal NetAmt = 0;
                //for (int i = 0; i < gvStockOutItem.Rows.Count; i++)
                //{
                //    Label lblTotAmount = (Label)gvStockOutItem.Rows[i].FindControl("lblTotAmount");
                //    if (lblTotAmount.Text != "")
                //    {
                //        NetAmt += decimal.Parse(lblTotAmount.Text);
                //    }
                //}
            }
        }
        else
        {
            ShowMessage("Please Enter Valid Product Name");
            //string msg = "Please Enter Valid Item Name";
            txtQty.Text = "";
            //string Message = "alert('" + msg + "');";
            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", Message, true);
        }
    }
    public void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", Message, true);
    }
    private void AddEmptyRows()
    {
        if (dt.Columns.Count == 0)
        {
            dt.Columns.Add("ProductMid", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Qty", typeof(string));

        }
        Session["DataTable"] = dt;
    }

    protected void txtMedicine_TextChanged(object sender, EventArgs e)
    {
        int Amt = 0;
        int UserId = 0;

        if (Request.Cookies["MP_Mid"] != null)
        {
            UserId = int.Parse(Request.Cookies["MP_Mid"].Value);
        }
        else
        {
            Response.Redirect("../SessionExpired.aspx");
        }
        int ItemMid = objProduct.GetProductMid(7, txtMedicine.Text);
        if (ItemMid != 0)
        {
            string Qty = "select (isnull((select sum(Prod_Qty) from dbo.tbl_ProductMaster " +
" where Prod_AF = 1 and   Prod_ItemMid = '" + ItemMid + "' and Prod_MRepMid='" + UserId + "'),0) " +
" -isnull((select sum(MedTrans_Qty) from dbo.tbl_MedicineTransaction " +
" inner join tbl_Transaction on Trans_Mid=MedTrans_TransMid" +
 " where MedTrans_AF = 1 and MedTrans_ItemMid='" + ItemMid + "' and MedTrans_RepMid='" + UserId + "'),0)) as availqty";
            string Qry = "  select * from dbo.tbl_ProductMaster where Prod_Name='" + txtMedicine.Text + "'";
            DataSet ds = dm.GetDataSet(Qty + Qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblAvailQty.Text = ds.Tables[0].Rows[0]["availqty"].ToString();
                    //  lblMeasuring.Text = ds.Tables[1].Rows[0]["Item_MeasuringUnit"].ToString();
                    if (gvedicineDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvedicineDetails.Rows.Count; i++)
                        {
                            Label lblItemName = (Label)gvedicineDetails.Rows[i].FindControl("lblProdName");
                            Label lblQty = (Label)gvedicineDetails.Rows[i].FindControl("lblQty");
                            if (lblItemName.Text == txtMedicine.Text)
                            {
                                Amt = Amt + int.Parse(lblQty.Text);
                            }
                        }
                    }
                    lblAvailQty.Text = (int.Parse(ds.Tables[0].Rows[0]["availqty"].ToString()) - Amt).ToString();
                    Label20.Text = lblAvailQty.Text;
                }
                else
                {
                    lblAvailQty.Text = "0";
                }
            }
        }
        else
        {
            ShowMessage("Please Enter Valid Item Name");
            //string msg = "Please Enter Valid Item Name";
            txtQty.Text = "";
            //string Message = "alert('" + msg + "');";
            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", Message, true);
        }
        txtQty.Focus();

    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        int Amt = 0;
        int ItemMid = objProduct.GetProductMid(7, txtMedicine.Text);
        if (ItemMid != 0)
        {
            string Qty = "select (isnull((select sum(Prod_Qty) from dbo.tbl_ProductMaster " +
" where Prod_AF = 1 and Prod_BranchMid = 1 and Prod_Mid = 1),0) " +
" -isnull((select sum(MedTrans_Qty) from dbo.tbl_MedicineTransaction inner join [dbo].[tbl_ProductMaster] on Prod_Mid = MedTrans_ProdMid " +
 " where MedTrans_AF = 1 and Prod_BranchMid = 1 and MedTrans_ProdMid = 1),0)) as availqty";
            string Qry = "  select * from dbo.tbl_ProductMaster where Prod_Name='" + txtMedicine.Text + "'";
            DataSet ds = dm.GetDataSet(Qty + Qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblAvailQty.Text = ds.Tables[0].Rows[0]["availqty"].ToString();
                    //  lblMeasuring.Text = ds.Tables[1].Rows[0]["Item_MeasuringUnit"].ToString();
                    if (gvedicineDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvedicineDetails.Rows.Count; i++)
                        {
                            Label lblItemName = (Label)gvedicineDetails.Rows[i].FindControl("lblProdName");
                            Label lblQty = (Label)gvedicineDetails.Rows[i].FindControl("lblQty");
                            if (lblItemName.Text == txtMedicine.Text)
                            {
                                Amt = Amt + int.Parse(lblQty.Text);
                            }
                        }
                    }
                    lblAvailQty.Text = (int.Parse(ds.Tables[0].Rows[0]["availqty"].ToString()) - Amt).ToString();

                }
                else
                {
                    lblAvailQty.Text = "0";
                }
            }
        }
        else
        {
            ShowMessage("Please Enter Valid Item Name");
            //string msg = "Please Enter Valid Item Name";
            txtQty.Text = "";
            //string Message = "alert('" + msg + "');";
            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", Message, true);
        }
        txtQty.Focus();
    }

    protected void txtVisitDate_TextChanged(object sender, EventArgs e)
    {


    }

    protected void rdbAMeet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbAMeet.SelectedIndex == 0)
        {
            divChemist.Visible = true;
        }
        else
        {
            divChemist.Visible = false;
        }
    }

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        int BranchId = 0;
        if (Request.Cookies["MP_BranchMid"] != null)
        {
            BranchId = int.Parse(Request.Cookies["MP_BranchMid"].Value);
        }
        int DocMid = 0;
        if (rdbType.SelectedIndex == 0)
        {
            //if (ddlDoctor.SelectedIndex > 0)
            //{
            //    DocMid = int.Parse(ddlDoctor.SelectedValue);
            //}
            if (txtDoctor.Text.Contains('-'))
            {
                DocMid = int.Parse(txtDoctor.Text.Split('-')[1].ToString()); //added on 11/09/2019         
                    //objDoctor.GetDoctorMid(7, txtDoctor.Text);//added on 10/09/2019
            }
        }
        dm.BindDropDown("select 0,'--SELECT--' union all SELECT Che_Mid,Che_Name FROM dbo.tbl_ChemistMaster " +
            " inner join tbl_ChemistPerDoctor on ChemistPerDoctor_ChemistMid=Che_Mid WHERE Che_AF=1 and ChemistPerDoctor_DoctorMid='" + DocMid + "' and Che_BranchMid='" + BranchId + "'", ddlChemist);
    }

    protected void rbdRSMorBM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbdRSMorBM.SelectedIndex == 0)
        {
            divBranchM.Visible = false;
            divRSM.Visible = false;
        }
        else if (rbdRSMorBM.SelectedIndex == 1)
        {
            divRSM.Visible = false;
            divBranchM.Visible = true;
        }
        else if (rbdRSMorBM.SelectedIndex == 2)
        {
            divBranchM.Visible = false;
            divRSM.Visible = true;
        }
        else if (rbdRSMorBM.SelectedIndex == 3)
        {
            divBranchM.Visible = true;
            divRSM.Visible = true;
        }
    }
    protected void txtDoctor_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int BranchId = 0;
            if (Request.Cookies["MP_BranchMid"] != null)
            {
                BranchId = int.Parse(Request.Cookies["MP_BranchMid"].Value);
            }
            int DocMid = 0;
            if (rdbType.SelectedIndex == 0)
            {
                //if (ddlDoctor.SelectedIndex > 0)
                //{
                //    DocMid = int.Parse(ddlDoctor.SelectedValue);
                //}
                if (txtDoctor.Text.Contains('-'))
                {
                    DocMid = int.Parse(txtDoctor.Text.Split('-')[1].ToString()); //added on 11/09/2019         
                    //objDoctor.GetDoctorMid(7, txtDoctor.Text);//added on 10/09/2019
                }
            }
            dm.BindDropDown("select 0,'--SELECT--' union all SELECT Che_Mid,Che_Name FROM dbo.tbl_ChemistMaster " +
                " inner join tbl_ChemistPerDoctor on ChemistPerDoctor_ChemistMid=Che_Mid WHERE Che_AF=1 and ChemistPerDoctor_DoctorMid='" + DocMid + "' and Che_BranchMid='" + BranchId + "'", ddlChemist);
        }
        catch (Exception ex)
        {

        }
    }
}