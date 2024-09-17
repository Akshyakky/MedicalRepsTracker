using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_UserMaster
/// </summary>
public class cls_UserMaster
{
    DataManager DBManager = new DataManager();
    public cls_UserMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string UserMasterTransaction(int Opt, int User_Id, string User_Name, string User_Psw, string User_Role, int User_BranchMid, string User_MobileNo, string User_Extra1, string User_Extra2, string User_Extra3, string User_Extra4, string User_Extra5,int UserId)
    {
        int result = 0;
        string FinalResult = "";
        try
        {
            bool blnattemptconn = DBManager.OpenSQLConnection();
            if (blnattemptconn == false)
            {
                DBManager.CloseConnection();
                DBManager.OpenSQLConnection();
            }
            SqlCommand cmd = new SqlCommand("usp_UserMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@User_Id", User_Id);
            cmd.Parameters.AddWithValue("@User_Name", User_Name);
            cmd.Parameters.AddWithValue("@User_Psw", User_Psw);
            cmd.Parameters.AddWithValue("@User_Role", User_Role);
            cmd.Parameters.AddWithValue("@User_BranchMid", User_BranchMid);
            cmd.Parameters.AddWithValue("@User_MobileNo", User_MobileNo);
            cmd.Parameters.AddWithValue("@User_Extra1", User_Extra1);
            cmd.Parameters.AddWithValue("@User_Extra2", User_Extra2);
            cmd.Parameters.AddWithValue("@User_Extra3", User_Extra3);
            cmd.Parameters.AddWithValue("@User_Extra4", User_Extra4);
            cmd.Parameters.AddWithValue("@User_Extra5", User_Extra5);
      
            cmd.Parameters.AddWithValue("@UserId", UserId);
         

            cmd.Parameters.Add("@RecordExists", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            result = cmd.ExecuteNonQuery();
            string RecordExists = cmd.Parameters["@RecordExists"].Value.ToString();
            if (RecordExists == "")
                FinalResult = result.ToString();
            else
                FinalResult = RecordExists + "," + result.ToString();
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            result = 0;
            FinalResult = "0";
        }
        finally
        {
            DBManager.CloseConnection();
        }
        return FinalResult;
    }

    public DataSet GetUserDetails(string GetRecords, string User_Name, string User_Role, int User_BranchMid, string User_MobileNo)
    {
        DataSet ds_Dispensary = new DataSet();
        try
        {
            bool blnattemptconn = DBManager.OpenSQLConnection();
            if (blnattemptconn == false)
            {
                DBManager.CloseConnection();
                DBManager.OpenSQLConnection();
            }

            SqlCommand cmd = new SqlCommand("usp_GetUserDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (User_Name != "")
            {
                cmd.Parameters.AddWithValue("@User_Name", User_Name);
            }

            if (User_Role != "")
            {
                cmd.Parameters.AddWithValue("@User_Role", User_Role);
            }
            if (User_BranchMid >0)
            {
                cmd.Parameters.AddWithValue("@User_BranchMid", User_BranchMid);
            }
            if (User_MobileNo != "")
            {
                cmd.Parameters.AddWithValue("@User_MobileNo", User_MobileNo);
            }
         
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds_Dispensary);
            }
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
        }
        finally
        {
            DBManager.CloseConnection();
        }

        return ds_Dispensary;
    }

    public DataSet GetUserMid(int Opt, int User_Id)
    {
        DataSet ds_Dispensary = new DataSet();
        try
        {
            bool blnattemptconn = DBManager.OpenSQLConnection();
            if (blnattemptconn == false)
            {
                DBManager.CloseConnection();
                DBManager.OpenSQLConnection();
            }

            SqlCommand cmd = new SqlCommand("usp_UserMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@User_Id", User_Id);

                        

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds_Dispensary);
            }
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
        }
        finally
        {
            DBManager.CloseConnection();
        }

        return ds_Dispensary;
    }

    public DataSet GetLoginDetails(int Opt, string User_Name,string User_Psw)
    {
        DataSet ds_Dispensary = new DataSet();
        try
        {
            bool blnattemptconn = DBManager.OpenSQLConnection();
            if (blnattemptconn == false)
            {
                DBManager.CloseConnection();
                DBManager.OpenSQLConnection();
            }

            SqlCommand cmd = new SqlCommand("usp_UserMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@User_Name", User_Name);

            cmd.Parameters.AddWithValue("@User_Psw", User_Psw);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds_Dispensary);
            }
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
        }
        finally
        {
            DBManager.CloseConnection();
        }

        return ds_Dispensary;
    }
}