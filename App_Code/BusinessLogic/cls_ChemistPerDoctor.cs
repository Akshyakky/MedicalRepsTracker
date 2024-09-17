using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ChemistPerDoctor
/// </summary>
public class cls_ChemistPerDoctor
{
    DataManager DBManager = new DataManager();
	public cls_ChemistPerDoctor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ChemistPerDoctorTransaction(int opt, int ChemistPerDoctor_Mid, int ChemistPerDoctor_DoctorMid, int ChemistPerDoctor_ChemistMid, string ChemistPerDoctor_Description, int UserId

       )
    {
        int result = 0;
        try
        {
            DBManager.OpenSQLConnection();
            SqlCommand cmd = new SqlCommand("usp_ChemistPerDoctor");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", opt);
            cmd.Parameters.AddWithValue("@ChemistPerDoctor_Mid", ChemistPerDoctor_Mid);
            cmd.Parameters.AddWithValue("@ChemistPerDoctor_DoctorMid", ChemistPerDoctor_DoctorMid);
            cmd.Parameters.AddWithValue("@ChemistPerDoctor_ChemistMid", ChemistPerDoctor_ChemistMid);
            cmd.Parameters.AddWithValue("@ChemistPerDoctor_Description", ChemistPerDoctor_Description);
            cmd.Parameters.AddWithValue("@UserId", UserId);
         


            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
        }
        finally
        {
            DBManager.CloseConnection();
        }
        return result;
    }
    public DataSet GetChemistPerDoctorDetails(string GetRecords, string Doc_Name, string Che_Name)
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

            SqlCommand cmd = new SqlCommand("usp_GetChemistPerDoctor");
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

            if (Che_Name != "")
            {
                cmd.Parameters.AddWithValue("@Che_Name", Che_Name);
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

    public DataSet GetChemPerDocMid(int Opt, int ChemistPerDoctor_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_ChemistPerDoctor");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@ChemistPerDoctor_Mid", ChemistPerDoctor_Mid);



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