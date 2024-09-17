using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ProductMaster
/// </summary>
public class cls_ProductMaster
{
    DataManager DBManager = new DataManager();
    public cls_ProductMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string ProductMasterTransaction(int Opt, int Prod_Mid, string Prod_Name, int Prod_Qty, string Prod_Description, int Prod_BranchMid, int Prod_ItemMid, int Prod_MRepMid, string Prod_Extra2, string Prod_Extra3, string Prod_Extra4, string Prod_Extra5, int UserId)
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
            SqlCommand cmd = new SqlCommand("usp_ProductMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@Prod_Mid", Prod_Mid);
            cmd.Parameters.AddWithValue("@Prod_Name", Prod_Name);
            cmd.Parameters.AddWithValue("@Prod_Qty", Prod_Qty);
            cmd.Parameters.AddWithValue("@Prod_Description", Prod_Description);
            cmd.Parameters.AddWithValue("@Prod_BranchMid", Prod_BranchMid);
            cmd.Parameters.AddWithValue("@Prod_MRepMid", Prod_MRepMid);
            cmd.Parameters.AddWithValue("@Prod_ItemMid", Prod_ItemMid);
            cmd.Parameters.AddWithValue("@Prod_Extra2", Prod_Extra2);
            cmd.Parameters.AddWithValue("@Prod_Extra3", Prod_Extra3);
            cmd.Parameters.AddWithValue("@Prod_Extra4", Prod_Extra4);
            cmd.Parameters.AddWithValue("@Prod_Extra5", Prod_Extra5);

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

    public DataSet GetProductDetails(string GetRecords, string Prod_Name, string Prod_Qty, string Prod_Description, int Prod_BranchMid, string Rep_Name)
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

            SqlCommand cmd = new SqlCommand("usp_GetProductDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            if (GetRecords != "")
            {
                cmd.Parameters.AddWithValue("@GetRecords", GetRecords);
            }

            if (Prod_Name != "")
            {
                cmd.Parameters.AddWithValue("@Prod_Name", Prod_Name);
            }

            if (Rep_Name != "")
            {
                cmd.Parameters.AddWithValue("@Rep_Name", Rep_Name);
            }

            if (Prod_Qty != "")
            {
                cmd.Parameters.AddWithValue("@Prod_Qty", Prod_Qty);
            }

            if (Prod_Description != "")
            {
                cmd.Parameters.AddWithValue("@Prod_Description", Prod_Description);
            }
            if (Prod_BranchMid > 0)
            {
                cmd.Parameters.AddWithValue("@Prod_BranchMid", Prod_BranchMid);
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

    public DataSet GetProdMid(int Opt, int Prod_Mid)
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

            SqlCommand cmd = new SqlCommand("usp_ProductMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;

            cmd.Parameters.AddWithValue("@Opt", Opt);

            cmd.Parameters.AddWithValue("@Prod_Mid", Prod_Mid);



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
    public List<string> getProductName(string prefix)
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
            SqlCommand cmd = new SqlCommand("usp_ItemMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", 6);
            cmd.Parameters.AddWithValue("@Item_Name", prefix);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //result.Add(string.Format("{0}/{1}", dr["Item_MasterID"], dr["Item_Name"]));
                result.Add(dr["Item_Name"].ToString());
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
    public int GetProductMid(int opt, string Item_Name)
    {
        int res = 0;
        try
        {
            bool blnAttmpt = DBManager.OpenSQLConnection();
            if (blnAttmpt == true)
            {
                SqlCommand cmd = new SqlCommand("usp_ProductMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = DBManager.myConn;
                cmd.Parameters.AddWithValue("@opt", opt);
                cmd.Parameters.AddWithValue("@Item_Name", Item_Name);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res = int.Parse(dr["Item_Mid"].ToString());
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