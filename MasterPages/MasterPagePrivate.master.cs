using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterPages_MasterPagePrivate : System.Web.UI.MasterPage
{

    DataManager objDM = new DataManager();
    //clsInstituteAdmin objInstituteAdmin = new clsInstituteAdmin();

    protected void Page_Load(object sender, EventArgs e)
    {
        SetCurrentPage();
        if (!IsPostBack)
        {
           // liMasterSubMenu.Visible = false;
            if (Request.Cookies["User_MasterId"] != null)
            {
                if (Request.Cookies["User_Role"] != null)
                {
                    lblWelcomeUser.Text= Request.Cookies["User_LoginUserName"].Value;
                    string Role = Request.Cookies["User_Role"].Value;
                    if (Role.ToUpper() == "BRANCHADMIN")
                    {
                       
                        //liMasterSubMenu.Visible = true;
                        idUserMaster.Visible = false;
                        LiMVisitReport.Visible = false;
                        LiChemPerDoctor.Visible = true;
                        LiRSMMaster.Visible = false;
                        LiBranchMaster.Visible = false;
                        LiWorkings.Visible = false;
                        Li1RepVisit.Visible = false;
                        //idDoctorMaster.Visible = true;
                        //idChemistMaster.Visible = true;
                        //idProducts.Visible = true;
                        //idReps.Visible = true;
                        //Li1.Visible = false;
                        //LiVisitReport.Visible = true;
                        //LiSapleConsumedReport.Visible = true;
                        //liChartsSubMenu.Visible = true;
                        //LiMVisitReport.Visible = false;
                    }
                    else
                    {
                        LiRSMMaster.Visible = true;
                        if (Request.Cookies["RSM"] != null)
                        {
                            string RSM = Request.Cookies["RSM"].Value;
                            if(RSM=="RSM")
                            {
                                LiRSMMaster.Visible = false;
                            }
                            else
                            {
                                LiRSMMaster.Visible = true;
                            }
                        }
                        //liMasterSubMenu.Visible = true;
                        idUserMaster.Visible = true;
                        LiMVisitReport.Visible = false;
                        LiChemPerDoctor.Visible = false;
                      
                        LiBranchMaster.Visible = true;
                        LiWorkings.Visible = true;
                        Li1RepVisit.Visible = false;
                        //idDoctorMaster.Visible = false;
                        //idChemistMaster.Visible = false;
                        //idProducts.Visible = false;
                        //idReps.Visible = false;
                        //Li1.Visible = false;
                        //LiVisitReport.Visible = true;
                        //LiSapleConsumedReport.Visible = true;
                        //liChartsSubMenu.Visible = true;
                        //LiMVisitReport.Visible = false;
                    }
                }
            }
            else if(Request.Cookies["MP_Mid"] != null)
            {
                lblWelcomeUser.Text = Request.Cookies["MP_UserName"].Value;
                liMasterSubMenu.Visible = false;
                Li1RepVisit.Visible = true;
                LiVisitReport.Visible = true;
                LiSapleConsumedReport.Visible = false;
                liChartsSubMenu.Visible = false;
                LiMVisitReport.Visible = true;
                LiVisitReport.Visible = false;
                LiSapleConsumedReport.Visible = false;
                LiChemPerDoctor.Visible = false;
                LiRSMMaster.Visible = false;
                LiBranchMaster.Visible = false;
                LiWorkings.Visible = false;
            }
            else
            {
                Response.Redirect("../SessionExpired.aspx");
            }
        }
    }


    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["User_MasterId"] != null)
        {
            Response.Cookies["User_MasterId"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["User_MasterId"].Value = null;
        }
        if (Request.Cookies["MP_Mid"] != null)
        {
            Response.Cookies["MP_Mid"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["MP_Mid"].Value = null;
        }
        if (Request.Cookies["RSM"] != null)
        {
            Response.Cookies["RSM"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["RSM"].Value = null;
        }
            Response.Redirect("repslogin.aspx");
    }
    private void SetCurrentPage()
    {
        var pagename = Convert.ToString(GetPageName());
        switch (pagename)
        {
            case "AdminHome.aspx":
                HomePage.Attributes.Add("class", "active");
                break;

            case "UserMaster.aspx":
                liMasterSubMenu.Attributes.Add("class", "active");
              
                break;

            case "MasterDoctor.aspx":
                idDoctorMaster.Attributes.Add("class", "active");
             
                break;

            case "BranchMaster.aspx":
                LiBranchMaster.Attributes.Add("class", "active");
                break;

            case "RSMMaster.aspx":
                LiRSMMaster.Attributes.Add("class", "active");
                break;

            case "ChemistPerDoctor.aspx":
                LiChemPerDoctor.Attributes.Add("class", "active");
                break;

            case "MasterChemist.aspx":
                idChemistMaster.Attributes.Add("class", "active");
                break;

            case "ProductMaster.aspx":
                idProductMaster.Attributes.Add("class", "active");
                break;

            case "MedicalRepMaster.aspx":
                idReps.Attributes.Add("class", "active");
                break;

            case "StockiestMaster.aspx":
                idStockiest.Attributes.Add("class", "active");
                break;

            case "MedicalRepVisit.aspx":
                Li1RepVisit.Attributes.Add("class", "active");
                break;

            case "AdminVisitReport.aspx":
                LiVisitReport.Attributes.Add("class", "active");
                break;

            case "MedicalRepsVisitReport.aspx":
                LiMVisitReport.Attributes.Add("class", "active");
                break;

            case "SampleConsumedReport.aspx":
                LiSapleConsumedReport.Attributes.Add("class", "active");
                break;

            case "VisitsChart.aspx":
                LiLVChart.Attributes.Add("class", "active");
                break;

            case "MedicalRepVisitChart.aspx":
                LiMVChart.Attributes.Add("class", "active");
                break;

            case "SampleConsumedChart.aspx":
                LiSChart.Attributes.Add("class", "active");
                break;
            case "ItemMaster.aspx":
                LiItemMaster.Attributes.Add("class", "active");
                break; 

            case "ChangePassword.aspx":
                ChangePassword.Attributes.Add("class", "active");
                break;

        }
    }

    private object GetPageName()
    {
        string pagename = Request.Url.ToString().Split('/').Last();
        return Request.Url.ToString().Split('/').Last();
    }
    protected string SetCssClass(string page)
    {
        return Request.Url.AbsolutePath.ToLower().EndsWith(page.ToLower()) ? "active" : "";
    }

}
