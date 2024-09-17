using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ChemistMaster
/// </summary>
public class cls_ChemistMaster
{
    DataManager DBManager = new DataManager();
    public cls_ChemistMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string ChemistMasterTransaction(int Opt, int Che_Mid, string Che_Name, string Che_Number, string Che_Address, string Che_ContactNo, string Che_Location, int Che_BranchMid, string Che_Extra2, string Che_Extra3, string Che_Extra4, string Che_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_ChemistMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Che_Mid", Che_Mid);
            cmd.Parameters.AddWithValue("@Che_Name", Che_Name);
            cmd.Parameters.AddWithValue("@Che_Number", Che_Number);
            cmd.Parameters.AddWithValue("@Che_Address", Che_Address);
            cmd.Parameters.AddWithValue("@Che_ContactNo", Che_ContactNo);
            cmd.Parameters.AddWithValue("@Che_Location", Che_Location);
            cmd.Parameters.AddWithValue("@Che_BranchMid", Che_BranchMid);
            cmd.Parameters.AddWithValue("@Che_Extra2", Che_Extra2);
            cmd.Parameters.AddWithValue("@Che_Extra3", Che_Extra3);
            cmd.Parameters.AddWithValue("@Che_Extra4", Che_Extra4);
            cmd.Parameters.AddWithValue("@Che_Extra5", Che_Extra5);

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

    public DataSet GetChemistDetails(string GetRecords, string Che_Name, string Che_Number, string Che_Address, string Che_ContactNo, string Che_Location, int Che_BranchMid)
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

            SqlCommand cmd = new SqlCommand("usp_GetChemistDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (Che_Name != "")
            {
                cmd.Parameters.AddWithValue("@Che_Name", Che_Name);
            }

            if (Che_Number != "")
            {
                cmd.Parameters.AddWithValue("@Che_Number", Che_Number);
            }

            if (Che_Address != "")
            {
                cmd.Parameters.AddWithValue("@Che_Address", Che_Address);
            }
            if (Che_ContactNo != "")
            {
                cmd.Parameters.AddWithValue("@Che_ContactNo", Che_ContactNo);
            }
            if (Che_Location != "")
            {
                cmd.Parameters.AddWithValue("@Che_Location", Che_Location);
            }
            if (Che_BranchMid > 0)
            {
                cmd.Parameters.AddWithValue("@Che_BranchMid", Che_BranchMid);
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

    public DataSet GetChemMid(int Opt, int Che_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_ChemistMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@Che_Mid", Che_Mid);



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