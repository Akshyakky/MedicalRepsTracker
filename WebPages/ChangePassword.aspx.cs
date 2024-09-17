using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_ChangePassword : System.Web.UI.Page
{
    clsEncryptDecrypt objEnc = new clsEncryptDecrypt();
    cls_UserMaster objUser = new cls_UserMaster();
    cls_MedicalRepsMaster objReps = new cls_MedicalRepsMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblIncCurPwd.Visible = false;
        string CurrentPwd = objEnc.Encrypt(txtCurrentPassword.Text);
        string NewPwd = objEnc.Encrypt(txtNewPassword.Text);
        string SPwd = "";
        int StaffMid = 0;
       // if (Request.Cookies["User_Id"] != null)
            if (Request.Cookies["User_MasterId"] != null)
            
        {
            if (Request.Cookies["User_Pwd"] != null)
            {
                SPwd = Request.Cookies["User_Pwd"].Value;
            }
            if (Request.Cookies["User_Id"] != null)
            {
                StaffMid = int.Parse(Request.Cookies["User_Id"].Value);
            }
            if (CurrentPwd == SPwd)
            {
                string result = objUser.UserMasterTransaction(6, StaffMid,"",NewPwd,"",0,"","","","","","", StaffMid);
                if (result != "")
                {
                    ShowMessageAndRedirect("Password Changed Successfully", "AdminLogin.aspx");
                    txtConfirmPassword.Text = "";
                    txtCurrentPassword.Text = "";
                    txtNewPassword.Text = "";
                }
                else
                {
                    ShowMessage("Unsuccessfull try again");

                }
            }
            else
            {
                lblIncCurPwd.Visible = true;
            }
        }
        else
        {
            if (Request.Cookies["MP_Password"] != null)
            {
                SPwd = Request.Cookies["MP_Password"].Value;
            }
            if (Request.Cookies["MP_Mid"] != null)
            {
                StaffMid = int.Parse(Request.Cookies["MP_Mid"].Value);
            }
            if (CurrentPwd == SPwd)
            {
                string result = objReps.MedicalRepsMasterTransaction(6, StaffMid,"","","","","",NewPwd,0,"","","","","",  StaffMid);
                if (result != "")
                {
                    ShowMessageAndRedirect("Password Changed Successfully", "RepsLogin.aspx");
                    txtConfirmPassword.Text = "";
                    txtCurrentPassword.Text = "";
                    txtNewPassword.Text = "";
                }
                else
                {
                    ShowMessage("Unsuccessfull try again");

                }
            }
            else
            {
                lblIncCurPwd.Visible = true;
            }

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
    private void ShowMessage(string msg)
    {
        string Message = "alert('" + msg + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", Message, true);

    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
}