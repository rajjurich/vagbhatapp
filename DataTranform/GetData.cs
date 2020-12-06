using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace DataTranform
{
    public class GetData
    {
        public DataTable GetPatientData()
        {
            DataTable dt = new DataTable();

            string connstr = @"Provider = Microsoft.ACE.OLEDB.12.0; data source = vagbhatdb.mdb";
            try
            {
                OleDbConnection conn = new OleDbConnection
                {
                    ConnectionString = connstr
                };
                conn.Open();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM patient_details";
                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                dt.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
            }

            return dt;
        }
        public DataTable GetPatientExData(string patient_id)
        {
            DataTable dt = new DataTable();

            string connstr = @"Provider = Microsoft.ACE.OLEDB.12.0; data source = vagbhatdb.mdb";
            try
            {
                OleDbConnection conn = new OleDbConnection
                {
                    ConnectionString = connstr
                };
                conn.Open();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM patient_ex_details " +
                    " where patient_id=" + patient_id + "";
                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                dt.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
            }

            return dt;
        }

        public double GetFees(string patient_id, DateTime dt)
        {
            string connstr = @"Provider = Microsoft.ACE.OLEDB.12.0; data source = vagbhatdb.mdb";
            try
            {
                OleDbConnection conn = new OleDbConnection
                {
                    ConnectionString = connstr
                };
                conn.Open();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT fee FROM patient_fee " +
                    " where patient_id=" + patient_id + " and fee_date = DateValue('" + dt + "')";
                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                var val = Convert.ToDouble(reader["fee"].ToString());
                reader.Close();
                return val;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
            }

            return 0;
        }
    }
}
