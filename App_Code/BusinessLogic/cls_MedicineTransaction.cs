using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_MedicineTransaction
/// </summary>
public class cls_MedicineTransaction
{
    public cls_MedicineTransaction()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    DataManager DBManager = new DataManager();
    public int MedicineTransactionTransaction(int Opt, int MedTrans_Mid, int MedTrans_TransMid, int MedTrans_ProdMid, string MedTrans_ProdName,
        int MedTrans_Qty, string MedTrans_Extra1, string MedTrans_Extra2, string MedTrans_Extra3, string MedTrans_Extra4, string MedTrans_Extra5,  int MedTrans_RepMid, int UserId)
    {
        int result = 0;
        try
        {
            bool blnattemptconn = DBManager.OpenSQLConnection();
            if (blnattemptconn == false)
            {
                DBManager.CloseConnection();
                DBManager.OpenSQLConnection();
            }
            SqlCommand cmd = new SqlCommand("usp_MedicineTransaction");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = DBManager.myConn;
            cmd.Parameters.AddWithValue("@opt", Opt);
            cmd.Parameters.AddWithValue("@MedTrans_Mid", MedTrans_Mid);
            cmd.Parameters.AddWithValue("@MedTrans_TransMid", MedTrans_TransMid);
            cmd.Parameters.AddWithValue("@MedTrans_ItemMid", MedTrans_ProdMid);
            cmd.Parameters.AddWithValue("@MedTrans_ProdName", MedTrans_ProdName);
            cmd.Parameters.AddWithValue("@MedTrans_Qty", MedTrans_Qty);
            cmd.Parameters.AddWithValue("@MedTrans_Extra1", MedTrans_Extra1);
            cmd.Parameters.AddWithValue("@MedTrans_Extra2", MedTrans_Extra2);
            cmd.Parameters.AddWithValue("@MedTrans_Extra3", MedTrans_Extra3);
            cmd.Parameters.AddWithValue("@MedTrans_Extra4", MedTrans_Extra4);
            cmd.Parameters.AddWithValue("@MedTrans_Extra5", MedTrans_Extra5);
            //cmd.Parameters.AddWithValue("@MedTrans_ItemMid", MedTrans_ItemMid);
            cmd.Parameters.AddWithValue("@MedTrans_RepMid", MedTrans_RepMid);

            cmd.Parameters.AddWithValue("@UserId", UserId);



            result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            result = 0;
        }
        finally
        {
            DBManager.CloseConnection();
        }
        return result;
    }

}