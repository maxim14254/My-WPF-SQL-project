using System;
using System.Data.SqlClient;


namespace НФП
{
    public class Rezult
    {
        private readonly string sql;
        private string v;
        private SqlCommand command;
        public Rezult(string rezult_Sila, SqlConnection sqlConnection, string exercise, string table)
        {
            try
            {
                sql = String.Format("SELECT Баллы FROM [{0}] WHERE [{1}] = {2}",table, exercise, rezult_Sila);
                command = new SqlCommand(sql, sqlConnection);
                v = command.ExecuteScalar().ToString();
            }
            catch (System.NullReferenceException)
            {
                float min = float.Parse(new SqlCommand(String.Format("SELECT MIN ([{0}]) FROM [{1}]", exercise,table), sqlConnection).ExecuteScalar().ToString());
                float max = float.Parse(new SqlCommand(String.Format("SELECT MAX ([{0}]) FROM [{1}]", exercise, table), sqlConnection).ExecuteScalar().ToString());
                if (float.Parse(InFloat(rezult_Sila)) < min)
                {
                    sql = String.Format("SELECT TOP 1 Баллы FROM [{0}] WHERE [{1}] > {2}  ORDER BY [{3}]",table, exercise, rezult_Sila, exercise);
                    command = new SqlCommand(sql, sqlConnection);
                    v = command.ExecuteScalar().ToString();
                }
                else if (float.Parse(InFloat(rezult_Sila)) > max)
                {
                    sql = String.Format("SELECT TOP 1 Баллы FROM [{0}] WHERE [{1}] < {2}  ORDER BY [{3}] DESC",table, exercise, rezult_Sila, exercise);
                    command = new SqlCommand(sql, sqlConnection);
                    v = command.ExecuteScalar().ToString();
                }
                else if (v == null)
                {
                    sql = String.Format("SELECT TOP 1 Баллы FROM [{0}] WHERE [{1}] > {2} ORDER BY [{3}]",table, exercise, rezult_Sila, exercise);
                    command = new SqlCommand(sql, sqlConnection);
                    v = command.ExecuteScalar().ToString();
                }
            }
        }
        public int RezultView()
        {
            return Convert.ToInt32(v);
        }

        private string InFloat(string s)
        {
            return s.Replace(".", ",");
        }
    }
}
