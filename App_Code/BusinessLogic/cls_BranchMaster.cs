using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_BranchMaster
/// </summary>
public class cls_BranchMaster
{
    DataManager DBManager = new DataManager();
	public cls_BranchMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string BranchMasterTransaction(int Opt, int Branch_Mid, string Branch_Name,int Branch_RSMMid, string Branch_Description, string Branch_Extra2, string Branch_Extra3, string Branch_Extra4, string Branch_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_BranchMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Branch_Mid", Branch_Mid);
            cmd.Parameters.AddWithValue("@Branch_Name", Branch_Name);
            cmd.Parameters.AddWithValue("@Branch_RSMMid", Branch_RSMMid); 
            cmd.Parameters.AddWithValue("@Branch_Description", Branch_Description);
            cmd.Parameters.AddWithValue("@Branch_Extra2", Branch_Extra2);
            cmd.Parameters.AddWithValue("@Branch_Extra3", Branch_Extra3);
            cmd.Parameters.AddWithValue("@Branch_Extra4", Branch_Extra4);
            cmd.Parameters.AddWithValue("@Branch_Extra5", Branch_Extra5);

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

    public DataSet GetBranchDetails(string GetRecords, string Branch_Name,string RSM_Name)
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

            SqlCommand cmd = new SqlCommand("usp_GetBranchDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (Branch_Name != "")
            {
                cmd.Parameters.AddWithValue("@Branch_Name", Branch_Name);
            }

            if (RSM_Name !="")
            {
                cmd.Parameters.AddWithValue("@RSM_Name", RSM_Name);
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

    public DataSet GetBranchMid(int Opt, int Branch_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_BranchMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@Branch_Mid", Branch_Mid);



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
    public int GetRSMMid(int opt, int Branch_Mid)
    {
        int res = 0;
        try
        {
            bool blnAttmpt = DBManager.OpenSQLConnection();
            if (blnAttmpt == true)
            {
                SqlCommand cmd = new SqlCommand("usp_BranchMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = DBManager.myConn;
                cmd.Parameters.AddWithValue("@opt", opt);
                cmd.Parameters.AddWithValue("@Branch_Mid", Branch_Mid);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = int.Parse(dr["Branch_RSMMid"].ToString());
                }
                return res;
            }
            else
            {
                return res;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            return res;
        }
        finally
        {
            DBManager.CloseConnection();
        }
    }

}