using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_StockiestMaster
/// </summary>
public class cls_StockiestMaster
{
    DataManager DBManager = new DataManager();
    public cls_StockiestMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string StockiestMasterTransaction(int Opt, int ST_Mid, string ST_Name, string ST_Number, string ST_Address, string ST_ContactNumber, int ST_BranchMid,string ST_Location,  string ST_Extra1, string ST_Extra2, string ST_Extra3, string ST_Extra4, string ST_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_StockiestMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@ST_Mid", ST_Mid);
            cmd.Parameters.AddWithValue("@ST_Name", ST_Name);
            cmd.Parameters.AddWithValue("@ST_Number", ST_Number);
            cmd.Parameters.AddWithValue("@ST_Address", ST_Address);
            cmd.Parameters.AddWithValue("@ST_ContactNumber", ST_ContactNumber);
            cmd.Parameters.AddWithValue("@ST_BranchMid", ST_BranchMid);

            cmd.Parameters.AddWithValue("@ST_Location", ST_Location);
            cmd.Parameters.AddWithValue("@ST_Extra1", ST_Extra1);
            cmd.Parameters.AddWithValue("@ST_Extra2", ST_Extra2);
            cmd.Parameters.AddWithValue("@ST_Extra3", ST_Extra3);

            cmd.Parameters.AddWithValue("@ST_Extra4", ST_Extra4);
            cmd.Parameters.AddWithValue("@ST_Extra5", ST_Extra5);

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
    public DataSet GetStockiestDetails(string GetRecords, string ST_Name, string ST_Number, string ST_Address, string ST_ContactNumber, int ST_BranchMid,string ST_Location)
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

            SqlCommand cmd = new SqlCommand("usp_GetStockiestDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (ST_Name != "")
            {
                cmd.Parameters.AddWithValue("@ST_Name", ST_Name);
            }

            if (ST_Number != "")
            {
                cmd.Parameters.AddWithValue("@ST_Number", ST_Number);
            }

            if (ST_Address != "")
            {
                cmd.Parameters.AddWithValue("@ST_Address", ST_Address);
            }
            if (ST_ContactNumber != "")
            {
                cmd.Parameters.AddWithValue("@ST_ContactNumber", ST_ContactNumber);
            }
            if (ST_BranchMid > 0)
            {
                cmd.Parameters.AddWithValue("@ST_BranchMid", ST_BranchMid);
            }
            if (ST_Location != "")
            {
                cmd.Parameters.AddWithValue("@ST_Location", ST_Location);
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

    public DataSet GetStockiestMid(int Opt, int ST_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_StockiestMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@ST_Mid", ST_Mid);



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