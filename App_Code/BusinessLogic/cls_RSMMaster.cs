using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_RSMMaster
/// </summary>
public class cls_RSMMaster
{
    DataManager DBManager = new DataManager();
	public cls_RSMMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string RSMMasterTransaction(int Opt, int RSM_Mid, string RSM_RegionName, string RSM_Name, string RSM_UserName, string RSM_Password, string RSM_Extra1, string RSM_Extra2, string RSM_Extra3, string RSM_Extra4, string RSM_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_RSMMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@RSM_Mid", RSM_Mid);
            cmd.Parameters.AddWithValue("@RSM_RegionName", RSM_RegionName);
            cmd.Parameters.AddWithValue("@RSM_Name", RSM_Name);

            cmd.Parameters.AddWithValue("@RSM_UserName", RSM_UserName);
            cmd.Parameters.AddWithValue("@RSM_Password", RSM_Password);

            cmd.Parameters.AddWithValue("@RSM_Extra1", RSM_Extra1);
            cmd.Parameters.AddWithValue("@RSM_Extra2", RSM_Extra2);
            cmd.Parameters.AddWithValue("@RSM_Extra3", RSM_Extra3);
            cmd.Parameters.AddWithValue("@RSM_Extra4", RSM_Extra4);
            cmd.Parameters.AddWithValue("@RSM_Extra5", RSM_Extra5);

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
    public DataSet GetRSMDetails(string GetRecords, string RSM_RegionName, string RSM_Name, string RSM_UserName)
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

            SqlCommand cmd = new SqlCommand("usp_GetRSMDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (RSM_RegionName != "")
            {
                cmd.Parameters.AddWithValue("@RSM_RegionName", RSM_RegionName);
            }

            if (RSM_Name != "")
            {
                cmd.Parameters.AddWithValue("@RSM_Name", RSM_Name);
            }

            if (RSM_UserName != "")
            {
                cmd.Parameters.AddWithValue("@RSM_UserName", RSM_UserName);
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
    public DataSet GetRSMMid(int Opt, int RSM_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_RSMMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@RSM_Mid", RSM_Mid);



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
    public DataSet GetLoginDetails(int Opt, string RSM_UserName, string RSM_Password)
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

            SqlCommand cmd = new SqlCommand("usp_RSMMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@RSM_UserName", RSM_UserName);

            cmd.Parameters.AddWithValue("@RSM_Password", RSM_Password);

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