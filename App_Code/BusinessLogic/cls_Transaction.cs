using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_Transaction
/// </summary>
public class cls_Transaction
{
    DataManager DBManager = new DataManager();
    public cls_Transaction()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string TransactionTransaction(int Opt, int Trans_Mid, int Trans_RepMid, string Trans_Type, int Trans_Doc_Chem_Mid, string Trans_VisitDateTime, string Trans_ChemistMeet, string Trans_Location, string Trans_Loc_Lan, string Trans_Loc_Lat, string Trans_VisitTime, string Trans_Status, string Trans_Worked, int Trans_BMMid, int Trans_RSMMid, string Trans_Extra1, string Trans_Extra2, string Trans_Extra3, string Trans_Extra4, int Trans_ChemMid, string Trans_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_Transaction");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Trans_Mid", Trans_Mid);
            cmd.Parameters.AddWithValue("@Trans_RepMid", Trans_RepMid);
            cmd.Parameters.AddWithValue("@Trans_Type", Trans_Type);
            cmd.Parameters.AddWithValue("@Trans_Doc_Chem_Mid", Trans_Doc_Chem_Mid);
            cmd.Parameters.AddWithValue("@Trans_VisitDateTime", Trans_VisitDateTime);
            cmd.Parameters.AddWithValue("@Trans_ChemistMeet", Trans_ChemistMeet);
            cmd.Parameters.AddWithValue("@Trans_Location", Trans_Location);
            cmd.Parameters.AddWithValue("@Trans_Loc_Lan", Trans_Loc_Lan);
            cmd.Parameters.AddWithValue("@Trans_Loc_Lat", Trans_Loc_Lat);
            cmd.Parameters.AddWithValue("@Trans_VisitTime", Trans_VisitTime);
            cmd.Parameters.AddWithValue("@Trans_Status", Trans_Status);

            cmd.Parameters.AddWithValue("@Trans_Worked", Trans_Worked);
            cmd.Parameters.AddWithValue("@Trans_BMMid", Trans_BMMid);
            cmd.Parameters.AddWithValue("@Trans_RSMMid", Trans_RSMMid);

            cmd.Parameters.AddWithValue("@Trans_Extra1", Trans_Extra1);
            cmd.Parameters.AddWithValue("@Trans_Extra2", Trans_Extra2);
            cmd.Parameters.AddWithValue("@Trans_Extra3", Trans_Extra3);
            cmd.Parameters.AddWithValue("@Trans_Extra4", Trans_Extra4);
            cmd.Parameters.AddWithValue("@Trans_Extra5", Trans_Extra5);
            cmd.Parameters.AddWithValue("@Trans_ChemMid", Trans_ChemMid);
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
    public string ApproveReject(int Opt, int Trans_Mid, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_Transaction");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Trans_Mid", Trans_Mid);
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
    public List<string> getLocationName(string prefix)
    {
        List<string> result = new List<string>();
        try
        {
            bool blnattemptconn = DBManager.OpenSQLConnection();
            if (blnattemptconn == false)
            {
                DBManager.CloseConnection();
                DBManager.OpenSQLConnection();
            }
            SqlCommand cmd = new SqlCommand("usp_Transaction");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", 4);
            cmd.Parameters.AddWithValue("@Trans_Extra1", prefix);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //result.Add(string.Format("{0}/{1}", dr["Item_MasterID"], dr["Item_Name"]));
                result.Add(dr["loc"].ToString());
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
        return result;
    }
}