using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_DoctorMaster
/// </summary>
public class cls_DoctorMaster
{
    DataManager DBManager = new DataManager();
    public cls_DoctorMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string DoctorMasterTransaction(int Opt, int Doc_Mid, string Doc_Name, string Doc_Number, string Doc_Address, string Doc_ContactNumber, string Doc_Location, int Doc_BranchMid, string Doc_Status, string Doc_Extra3, string Doc_Extra4, string Doc_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_DoctorMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Doc_Mid", Doc_Mid);
            cmd.Parameters.AddWithValue("@Doc_Name", Doc_Name);
            cmd.Parameters.AddWithValue("@Doc_Number", Doc_Number);
            cmd.Parameters.AddWithValue("@Doc_Address", Doc_Address);
            cmd.Parameters.AddWithValue("@Doc_ContactNumber", Doc_ContactNumber);
            cmd.Parameters.AddWithValue("@Doc_Location", Doc_Location);
            cmd.Parameters.AddWithValue("@Doc_BranchMid", Doc_BranchMid);
            cmd.Parameters.AddWithValue("@Doc_Status", Doc_Status);
            cmd.Parameters.AddWithValue("@Doc_Extra3", Doc_Extra3);
            cmd.Parameters.AddWithValue("@Doc_Extra4", Doc_Extra4);
            cmd.Parameters.AddWithValue("@Doc_Extra5", Doc_Extra5);

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

    public DataSet GetDoctorDetails(string GetRecords, string Doc_Name, string Doc_Number, string Doc_Address, string Doc_ContactNumber, string Doc_Location, int Doc_BranchMid)
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

            SqlCommand cmd = new SqlCommand("usp_GetDoctorDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (Doc_Name != "")
            {
                cmd.Parameters.AddWithValue("@Doc_Name", Doc_Name);
            }

            if (Doc_Number != "")
            {
                cmd.Parameters.AddWithValue("@Doc_Number", Doc_Number);
            }

            if (Doc_Address != "")
            {
                cmd.Parameters.AddWithValue("@Doc_Address", Doc_Address);
            }
            if (Doc_ContactNumber != "")
            {
                cmd.Parameters.AddWithValue("@Doc_ContactNumber", Doc_ContactNumber);
            }
            if (Doc_Location != "")
            {
                cmd.Parameters.AddWithValue("@Doc_Location", Doc_Location);
            }
            if (Doc_BranchMid > 0)
            {
                cmd.Parameters.AddWithValue("@Doc_BranchMid", Doc_BranchMid);
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

    public DataSet GetDoctorMid(int Opt, int Doc_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_DoctorMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@Doc_Mid", Doc_Mid);



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

    public List<string> GetDoctorsName(string prefix)
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
            SqlCommand cmd = new SqlCommand("usp_DoctorMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", 6);
            cmd.Parameters.AddWithValue("@Doc_Name", prefix);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //result.Add(string.Format("{0}/{1}", dr["Item_MasterID"], dr["Item_Name"]));
                result.Add(dr["DoctorName"].ToString());
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
    public int GetDoctorMid(int opt, string Item_Name)
    {
        int res = 0;
        try
        {
            bool blnAttmpt = DBManager.OpenSQLConnection();
            if (blnAttmpt == true)
            {
                SqlCommand cmd = new SqlCommand("usp_DoctorMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = DBManager.myConn;
                cmd.Parameters.AddWithValue("@opt", opt);
                cmd.Parameters.AddWithValue("@Doc_Name", Item_Name);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = int.Parse(dr["Doc_Mid"].ToString());
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
