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
    cls_MedicalRepsMaster objMedReps= new cls_MedicalRepsMaster();
    Cls_ErrorHandler errObj = new Cls_ErrorHandler();
  

    Random objRandom = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FnGenerateCaptcha();
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
                DataSet ds_AdminRole = objMedReps.GetLoginDetails(5,strUserName,strPassword);
                if (ds_AdminRole != null)
                {
                    if (ds_AdminRole.Tables[0].Rows.Count > 0)
                    {
                        intAdminMid = int.Parse(ds_AdminRole.Tables[0].Rows[0]["MP_Mid"].ToString());
                        intBranchMID = int.Parse(ds_AdminRole.Tables[0].Rows[0]["MP_BranchMid"].ToString());
                        //string Role = ds_AdminRole.Tables[0].Rows[0]["User_Role"].ToString();

                        Response.Cookies["MP_Mid"].Value = intAdminMid.ToString();
                        Response.Cookies["MP_Mid"].Expires = DateTime.Now.AddDays(1);

                        Response.Cookies["MP_UserName"].Value = strUserName;
                        Response.Cookies["MP_UserName"].Expires = DateTime.Now.AddDays(1);

                        Response.Cookies["MP_BranchMid"].Value = intBranchMID.ToString();
                        Response.Cookies["MP_BranchMid"].Expires = DateTime.Now.AddDays(1);

                        Response.Cookies["MP_Password"].Value = strPassword.ToString();
                        Response.Cookies["MP_Password"].Expires = DateTime.Now.AddDays(1);

                        //Response.Cookies["User_Role"].Value = Role;
                        //Response.Cookies["User_Role"].Expires = DateTime.Now.AddDays(1);

                        //saving log
                        //objLog.FnSaveLog("Logged In", strUserName);

                        Response.Redirect("Home.aspx", false);
                    }
                    else
                    {
                        ShowMessage("Invalid Username or Password");
                        txtUserName.Focus();
                    }
                }
            //}
            //else
            //{
            //    ShowMessage("Invalid Captcha");
            //    txtCaptcha.Focus();
            //}

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