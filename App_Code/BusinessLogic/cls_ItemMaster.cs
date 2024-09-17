using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ItemMaster
/// </summary>
public class cls_ItemMaster
{
    DataManager DBManager = new DataManager();
	public cls_ItemMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ItemMasterTransaction(int Opt, int Item_Mid, string Item_Name, string Item_Description, string Item_Extra1, string Item_Extra2, string Item_Extra3, string Item_Extra4, string Item_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_ItemMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Item_Mid", Item_Mid);
            cmd.Parameters.AddWithValue("@Item_Name", Item_Name);
            cmd.Parameters.AddWithValue("@Item_Description", Item_Description);
            cmd.Parameters.AddWithValue("@Item_Extra1", Item_Extra1);
            cmd.Parameters.AddWithValue("@Item_Extra2", Item_Extra2);
            cmd.Parameters.AddWithValue("@Item_Extra3", Item_Extra3);
            cmd.Parameters.AddWithValue("@Item_Extra4", Item_Extra4);
            cmd.Parameters.AddWithValue("@Item_Extra5", Item_Extra5);

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
    public DataSet GetItemDetails(string GetRecords, string Item_Name)
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

            SqlCommand cmd = new SqlCommand("usp_GetItemDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (Item_Name != "")
            {
                cmd.Parameters.AddWithValue("@Item_Name", Item_Name);
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
    public DataSet GetItemMid(int Opt, int Item_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_ItemMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@Item_Mid", Item_Mid);



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