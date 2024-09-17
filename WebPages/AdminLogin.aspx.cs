using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebPages_Login : System.Web.UI.Page
{
    DataManager ObjDM = new DataManager();
    clsEncryptDecrypt onjEncryptDecript = new clsEncryptDecrypt();
    cls_UserMaster objUserMaster = new cls_UserMaster();
    Cls_ErrorHandler errObj = new Cls_ErrorHandler();
    cls_RSMMaster objRSMaster = new cls_RSMMaster();

    Random objRandom = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FnGenerateCaptcha();
            rbdType.SelectedIndex = 0;
            txtUserName.Focus();
        }
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        try
        {
            string strUserName = txtUserName.Text;
            string strPassword = onjEncryptDecript.Encrypt(txtPassword.Text);
            string Show = onjEncryptDecript.Decrypt(txtPassword.Text);

            int intAdminMid = 0;
            int intBranchMID = 0;

            //if (txtCaptcha.Text == lblSum.Text)
            //{
            if (rbdType.SelectedIndex >= 0)
            {
                if (rbdType.SelectedIndex == 0 )
                {
                    DataSet ds_AdminRole = objUserMaster.GetLoginDetails(5, strUserName, strPassword);
                    if (ds_AdminRole != null)
                    {
                        if (ds_AdminRole.Tables[0].Rows.Count > 0)
                        {
                            intAdminMid = int.Parse(ds_AdminRole.Tables[0].Rows[0]["User_Id"].ToString());
                            intBranchMID = int.Parse(ds_AdminRole.Tables[0].Rows[0]["User_BranchMid"].ToString());
                            string Role = ds_AdminRole.Tables[0].Rows[0]["User_Role"].ToString();

                            Response.Cookies["User_MasterId"].Value = intAdminMid.ToString();
                            Response.Cookies["User_MasterId"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_LoginUserName"].Value = strUserName;
                            Response.Cookies["User_LoginUserName"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_BranchMID"].Value = intBranchMID.ToString();
                            Response.Cookies["User_BranchMID"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_Pwd"].Value = strPassword.ToString();
                            Response.Cookies["User_Pwd"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_Role"].Value = Role;
                            Response.Cookies["User_Role"].Expires = DateTime.Now.AddDays(1);



                            Response.Redirect("AdminHome.aspx", false);
                        }
                        else
                        {
                            ShowMessage("Invalid Username or Password");
                            txtUserName.Focus();
                        }


                    }
                }
                else if (rbdType.SelectedIndex == 1)
                {
                    DataSet ds_AdminRole = objRSMaster.GetLoginDetails(5, strUserName, strPassword);
                    if (ds_AdminRole != null)
                    {
                        if (ds_AdminRole.Tables[0].Rows.Count > 0)
                        {
                            intAdminMid = int.Parse(ds_AdminRole.Tables[0].Rows[0]["RSM_Mid"].ToString());
                            intBranchMID = 3;
                            string Role = ds_AdminRole.Tables[0].Rows[0]["RSM_Extra1"].ToString();


                            Response.Cookies["RSM"].Value = "RSM";
                            Response.Cookies["RSM"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_MasterId"].Value = intAdminMid.ToString();
                            Response.Cookies["User_MasterId"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_LoginUserName"].Value = strUserName;
                            Response.Cookies["User_LoginUserName"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_BranchMID"].Value = intBranchMID.ToString();
                            Response.Cookies["User_BranchMID"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_Pwd"].Value = strPassword.ToString();
                            Response.Cookies["User_Pwd"].Expires = DateTime.Now.AddDays(1);

                            Response.Cookies["User_Role"].Value = Role;
                            Response.Cookies["User_Role"].Expires = DateTime.Now.AddDays(1);



                            Response.Redirect("AdminHome.aspx", false);
                        }
                        else
                        {
                            ShowMessage("Invalid Username or Password");
                            txtUserName.Focus();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errObj.SaveReportInXML("btnSignIn_Click", ex.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

    protected void pmgBtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FnGenerateCaptcha();
    }
    protected void lnkBtnRefresh_Click(object sender, EventArgs e)
    {
        FnGenerateCaptcha();
    }
    private void FnGenerateCaptcha()
    {
        try
        {
            int intNum1 = objRandom.Next(0, 9);
            int intNum2 = objRandom.Next(0, 9);

            int TotalSum = intNum1 + intNum2;

            lblRandomNumber.Text = intNum1.ToString() + " + " + intNum2.ToString() + " = ";
            lblSum.Text = TotalSum.ToString();
        }
        catch (Exception ex)
        {
            errObj.SaveReportInXML("FnGenerateCaptcha()", ex.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }
    private void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", Message, true);

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

        FnClear();

    }

    private void FnClear()
    {
        try
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtCaptcha.Text = "";
            FnGenerateCaptcha();
            txtUserName.Focus();
        }
        catch (Exception ex)
        {
            errObj.SaveReportInXML("btnReset_Click", ex.ToString());
            ex.Data.Clear();
            Response.Redirect("../oops.aspx");
        }
    }

}