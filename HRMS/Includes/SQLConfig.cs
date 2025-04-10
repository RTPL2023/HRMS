using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Microsoft.Data.SqlClient;
namespace HRMS.Includes
{
    public class SQLConfig
    {                      
        public SqlConnection con = new SqlConnection("server=192.168.137.1;database=HRMS;UID=sa;password=Rishi@2022;TrustServerCertificate=True");
        //public SqlConnection con = new SqlConnection("server=115.187.62.28;database=HRMS;UID=sa;password=Rishi@2022;TrustServerCertificate=True");
 //    public SqlConnection con = new SqlConnection("server=SQL8010.site4now.net;database=db_aa6509_rishihrms;UID=db_aa6509_rishihrms_admin;password=Rishi@2022;TrustServerCertificate=True");
 
        private SqlCommand cmd;
        private SqlDataAdapter da;
        public DataTable dt;
        int result;



        public void Execute_CUD(string sql, string msg_false, string msg_true)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    //MessageBox.Show(msg_true);
                }
                else
                {
                    // MessageBox.Show(msg_false);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void Execute_Query(string sql)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public int Update_Execute_Query(string sql)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = 0;
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public int Execute_Query_WithRetValue(string sql)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        //public DataTable Load_DTG(string sql)
        //{
        //    try
        //    {
        //        con.Open();
        //        cmd = new SqlCommand();
        //        da = new SqlDataAdapter();
        //        dt = new DataTable();


        //        cmd.Connection = con;
        //        cmd.CommandText = sql;
        //        da.SelectCommand = cmd;
        //        da.Fill(dt);



        //    }
        //    catch (Exception ex)
        //    {
        //       // MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        da.Dispose();
        //    }
        //    return dt;
        //}

        public DataTable Load_DTG(string sql)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand();
                da = new SqlDataAdapter();
                dt = new DataTable();


                cmd.Connection = con;
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                da.Fill(dt);
                //  dtg.DataSource = dt;


            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                da.Dispose();

            }

            return dt;
        }

        //public void fiil_CBO(string sql, ComboBox cbo)
        //{
        //    try
        //    {
        //        con.Open();
        //        cmd = new SqlCommand();
        //        da = new SqlDataAdapter();
        //        dt = new DataTable();


        //        cmd.Connection = con;
        //        cmd.CommandText = sql;
        //        da.SelectCommand = cmd;
        //        da.Fill(dt);
        //        cbo.DataSource = dt;
        //        cbo.ValueMember = "ID";
        //        cbo.DisplayMember = "Name";


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        da.Dispose();
        //    }

        //}

        public void singleResult(string sql)

        {
            try
            {
                
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd = new SqlCommand();
                da = new SqlDataAdapter();
                dt = new DataTable();
                
                


                cmd.Connection = con;
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                
                 //MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
                da.Dispose();
               
            }

        }

        public void loadReports(string sql)

        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                da = new SqlDataAdapter();
                dt = new DataTable();


                cmd.Connection = con;
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                da.Dispose();
            }
        }

        //public object ReturnScalar(Hashtable hs, string StorProcName)
        //{
        //    //  SqlCommand com = null;
        //    try
        //    {

        //        cmd = new SqlCommand(StorProcName);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        foreach (System.Collections.DictionaryEntry di in hs)
        //            cmd.Parameters.Add("@" + di.Key.ToString(), di.Value);
        //        return cmd.ExecuteScalar();
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogErrorsInDB(ex.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}
        //public DataTable ReturnScalar(string StorProcName, Hashtable hs)
        //{
        //    //  SqlCommand com = null;
        //    try
        //    {
        //        da = new SqlDataAdapter();
        //        dt = new DataTable();
        //        con.Open();
        //        cmd = new SqlCommand(StorProcName, con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        foreach (System.Collections.DictionaryEntry di in hs)
        //            cmd.Parameters.Add("@" + di.Key.ToString(), di.Value);
        //        //  cmd.ExecuteScalar();
        //        cmd.CommandTimeout = 600;
        //        da.SelectCommand = cmd;
        //        da.Fill(dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        //LogErrorsInDB(ex.Message);
        //        dt = null;
        //        return dt;
        //    }
        //    finally
        //    {
        //        con.Close();
        //        da.Dispose();
        //        cmd.Dispose();
        //    }
        //}

        //public SqlDataReader ReturnScalar(string StorProcName, DataTable dt)
        //{
        //    SqlDataReader reader;
        //    //  SqlCommand com = null;
        //    try
        //    {
        //        cmd = GetStoredProc(StorProcName);
        //        cmd.Parameters.Add("@tblDepents", dt);
        //        return cmd.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogErrorsInDB(ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}


        //public SqlDataReader ReturnScalar(DataTable dt, string StorProcName)
        //{
        //    SqlDataReader reader;
        //    //  SqlCommand com = null;
        //    try
        //    {
        //        cmd = GetStoredProc(StorProcName);
        //        cmd.Parameters.Add("@tblDepents", dt);
        //        return cmd.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogErrorsInDB(ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}
        //public DataTable ReturnScalar(String qry, string StorProcName)
        //{
        //    SqlDataReader reader;
        //    //  SqlCommand com = null;
        //    try
        //    {
        //        da = new SqlDataAdapter();
        //        dt = new DataTable();
        //        //con.Open();
        //        //cmd = new SqlCommand(StorProcName, con);
        //        //cmd.CommandType = CommandType.StoredProcedure;
        //        cmd = GetStoredProc(StorProcName);
        //        cmd.Parameters.Add("@qry", qry);
        //        // return cmd.ExecuteReader();
        //        //  Message = "Incorrect syntax near 'UPADATE labourmaster set balsickleavedays=11, coincf=5 Where id=2;'."
        //        da.SelectCommand = cmd;
        //        da.Fill(dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogErrorsInDB(ex.Message);
        //        dt = null;
        //        return dt;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}
        public SqlDataReader ReturnScalar(string sql)
        {
            SqlDataReader reader;
            //  SqlCommand com = null;
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;

                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                // LogErrorsInDB(ex.Message);
                return null;
            }
            finally
            {
                cmd.Dispose();
            }
        }




        /// <summary>
        /// Returns the specified stored procedure.
        /// </summary>
        /// <param name="sProcName">Name of the stored procedure in the database to return</param>
        /// <returns></returns>
        public SqlCommand GetStoredProc(string sProcName)
        {
            // if (!bOpen) Connect();

            try
            {
                con.Open();
                cmd = new SqlCommand(sProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;		// Mark the Command as a SPROC
                return cmd;
            }
            catch (Exception ex)
            {
                // LogErrorsInDB(ex.Message);
                return null;
            }
        }

        public int Insert(string tableName, Dictionary<string, object> fieldValues)
                {
            string[] fields = fieldValues.Keys.ToArray();
            StringBuilder insertFieldsStr = new StringBuilder();
            StringBuilder insertParamsStr = new StringBuilder();
            for (int i = 0; i < fields.Length; i++)
            {
                insertFieldsStr.AppendFormat("{0},", fields[i]);
                insertParamsStr.AppendFormat("@{0},", fields[i]);
            }

            var fieldParams = new Dictionary<string, object>(fieldValues);
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", tableName, insertFieldsStr.ToString().TrimEnd(','), insertParamsStr.ToString().TrimEnd(','));
            return Execute(sql, fieldParams);
        }



        public int Execute(string sql, Dictionary<string, object> paramValues)
        {
            //con.Open();
            try
            {
                con.Open();
                {
                    using (var command = new SqlCommand(sql, con))
                    {
                        foreach (KeyValuePair<String, object> value in paramValues)
                        {
                            if (value.Value == null)
                                command.Parameters.Add(new SqlParameter(value.Key, DBNull.Value));
                            else
                                command.Parameters.Add(new SqlParameter(value.Key, value.Value));
                        }

                        var result = command.ExecuteNonQuery();
                        if (sql.IndexOf("INSERT") == 0 && result > 0)
                        {
                            //       command.Parameters.Clear();
                            //     command.CommandText = $"SELECT LAST_INSERT_ID()";
                            //     lastInsertID = command.ExecuteScalar();
                        }
                       
                    }
                }
            }
            catch(Exception ex)
            {
                string mag = ex.ToString();
            }
            finally
            {
                con.Close();

            }
            return result;
        }

        public int ExecuteUpdate(string sql, Dictionary<string, object> paramValues, Dictionary<string, object> crParamValues)
        {
            try
            {


                con.Open();
                {
                    using (var command = new SqlCommand(sql, con))
                    {
                        foreach (KeyValuePair<String, object> value in paramValues)
                        {
                            if (value.Value == null)
                                command.Parameters.Add(new SqlParameter(value.Key, DBNull.Value));
                            else
                                command.Parameters.Add(new SqlParameter(value.Key, value.Value));
                        }
                        foreach (KeyValuePair<String, object> value in crParamValues)
                        {
                            if (value.Value == null)
                                command.Parameters.Add(new SqlParameter(value.Key, DBNull.Value));
                            else
                                command.Parameters.Add(new SqlParameter(value.Key, value.Value));
                        }

                        var result = command.ExecuteNonQuery();
                        if (sql.IndexOf("INSERT") == 0 && result > 0)
                        {
                            //command.Parameters.Clear();
                            //command.CommandText = $"SELECT LAST_INSERT_ID()";
                            //lastInsertID = command.ExecuteScalar();
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }


        public int Update(string tableName, Dictionary<string, object> fieldValues, Dictionary<string, object> criteriaValues = null)
        {
            string[] fields = fieldValues.Keys.ToArray();
            StringBuilder updateFieldsStr = new StringBuilder();
            for (int i = 0; i < fields.Length; i++)
                updateFieldsStr.AppendFormat("{0}=@{0},", fields[i]);

            var fieldParams = new Dictionary<string, object>(fieldValues);
            var crfieldParams = new Dictionary<string, object>();

            StringBuilder criteriaStr = new StringBuilder();
            if (criteriaValues != null)
            {
                criteriaStr.Append(" WHERE ");
                string[] crfields = criteriaValues.Keys.ToArray();
                for (int i = 0; i < crfields.Length; i++)
                {
                    if (i > 0) criteriaStr.Append(" AND ");
                    criteriaStr.AppendFormat("{0}=@{0}", crfields[i]);
                    crfieldParams.Add(crfields[i], criteriaValues[crfields[i]]);
                }
            }

            string sql = string.Format("UPDATE {0} SET {1} {2}", tableName, updateFieldsStr.ToString().TrimEnd(','), criteriaStr.ToString());
            return ExecuteUpdate(sql, fieldParams, crfieldParams);
        }

    }
}
