using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_MedicalRepsMaster
/// </summary>
public class cls_MedicalRepsMaster
{
    DataManager DBManager = new DataManager();
	public cls_MedicalRepsMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string MedicalRepsMasterTransaction(int Opt, int MP_Mid, string MP_Name, string MP_Number, string MP_Address, string MP_ContactNo, string MP_UserName, string MP_Password,int MP_BranchMid, string MP_Extra1, string MP_Extra2, string MP_Extra3, string MP_Extra4, string MP_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_MedicalRepsMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@MP_Mid", MP_Mid);
            cmd.Parameters.AddWithValue("@MP_Name", MP_Name);
            cmd.Parameters.AddWithValue("@MP_Number", MP_Number);
            cmd.Parameters.AddWithValue("@MP_Address", MP_Address);
            cmd.Parameters.AddWithValue("@MP_ContactNo", MP_ContactNo);
            cmd.Parameters.AddWithValue("@MP_UserName", MP_UserName);
            cmd.Parameters.AddWithValue("@MP_Password", MP_Password);
            cmd.Parameters.AddWithValue("@MP_BranchMid", MP_BranchMid); 
            cmd.Parameters.AddWithValue("@MP_Extra1", MP_Extra1);
            cmd.Parameters.AddWithValue("@MP_Extra2", MP_Extra2);
            cmd.Parameters.AddWithValue("@MP_Extra3", MP_Extra3);
            cmd.Parameters.AddWithValue("@MP_Extra4", MP_Extra4);
            cmd.Parameters.AddWithValue("@MP_Extra5", MP_Extra5);
      
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

    public DataSet GetMedRepsDetails(string GetRecords, string MP_Name, string MP_Number, string MP_Address, string MP_ContactNo, string MP_UserName,int MP_BranchMid)
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

            SqlCommand cmd = new SqlCommand("usp_GetMedRepDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (MP_Name != "")
            {
                cmd.Parameters.AddWithValue("@MP_Name", MP_Name);
            }

            if (MP_Number != "")
            {
                cmd.Parameters.AddWithValue("@MP_Number", MP_Number);
            }

            if (MP_Address != "")
            {
                cmd.Parameters.AddWithValue("@MP_Address", MP_Address);
            }
            if (MP_ContactNo != "")
            {
                cmd.Parameters.AddWithValue("@MP_ContactNo", MP_ContactNo);
            }
            if (MP_UserName != "")
            {
                cmd.Parameters.AddWithValue("@MP_UserName", MP_UserName);
            }
            if (MP_BranchMid >0)
            {
                cmd.Parameters.AddWithValue("@MP_BranchMid", MP_BranchMid);
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

    public DataSet GetMedRepsMid(int Opt, int MP_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_MedicalRepsMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@MP_Mid", MP_Mid);



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

    public DataSet GetLoginDetails(int Opt, string MP_UserName, string MP_Password)
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

            SqlCommand cmd = new SqlCommand("usp_MedicalRepsMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@MP_UserName", MP_UserName);

            cmd.Parameters.AddWithValue("@MP_Password", MP_Password);

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