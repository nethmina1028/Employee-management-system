using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Employee_Management
{
    internal class SalaryData
    {


        public string EmployeeId { set; get; } //0

        public string Name { set; get; } //1 

        public string Position { set; get; } //2

        public int Salary { set; get; } //3

      





        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nethm\OneDrive\Documents\employee.mdf;");


        public List<SalaryData> salaryEmployeeListData()
        {
            List<SalaryData> listdata = new List<SalaryData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employees WHERE status ='Active' AND delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SalaryData sd = new SalaryData();

                            sd.EmployeeId = reader["employee_id"].ToString();
                            sd.Name = reader["full_name"].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.Salary = (int)reader["salary"];


                            listdata.Add(sd);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex);
                }
                finally
                {
                    connect.Close();
                }
            }

            return listdata;
        }

    }
}
