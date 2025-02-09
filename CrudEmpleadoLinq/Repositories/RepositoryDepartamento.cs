using CrudEmpleadoLinq.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace CrudEmpleadoLinq.Repositories
{
    public class RepositoryDepartamento
    {
        DataTable tablaDepartamento;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryDepartamento()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from DEPT";
            SqlDataAdapter adDept = new SqlDataAdapter(sql,connectionString);
            this.tablaDepartamento = new DataTable();
            adDept.Fill(this.tablaDepartamento);

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tablaDepartamento.AsEnumerable()
                           select datos;
            List<Departamento> departamentos = new List<Departamento>();
            foreach (var dato in consulta)
            {
                Departamento departamento = new Departamento();
                departamento.DeptNo = dato.Field<int>("DEPT_NO");
                departamento.Dnombre = dato.Field<string>("DNOMBRE");
                departamento.Loc = dato.Field<string>("LOC");
                departamentos.Add(departamento);
            }
            return departamentos;
        }
        public Departamento FindDepartamento
            (int deptNo)
        {
            string sql = "select * from DEPT where DEPT_NO=@deptno";
            this.com.Parameters.AddWithValue("@deptno", deptNo);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            Departamento departamento = new Departamento();
            this.reader.Read();
            departamento.DeptNo = int.Parse(this.reader["DEPT_NO"].ToString());
            departamento.Dnombre = this.reader["DNOMBRE"].ToString();
            departamento.Loc = this.reader["LOC"].ToString();
            this.cn.Close();
            this.com.Parameters.Clear();
            this.reader.Close();
            return departamento;
        }
        public void UpdateDepartamento
            (Departamento departamento)
        {
            string sql = "update DEPT set DNOMBRE=@dnombre, LOC=@loc where DEPT_NO=@deptno";
            this.com.Parameters.AddWithValue("@deptno",departamento.DeptNo);
            this.com.Parameters.AddWithValue("@dnombre",departamento.Dnombre);
            this.com.Parameters.AddWithValue("@loc",departamento.Loc);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public void CreateDepartamento
            (Departamento departamento)
        {
            string sql = "insert into DEPT values (@deptno, @dnombre, @loc)";
            this.com.Parameters.AddWithValue("@deptno", departamento.DeptNo);
            this.com.Parameters.AddWithValue("@dnombre", departamento.Dnombre);
            this.com.Parameters.AddWithValue("@loc", departamento.Loc);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public void DeleteDepartamento
            (int deptNo)
        {
            string sql = "delete from DEPT where DEPT_NO=@deptno";
            this.com.Parameters.AddWithValue("@deptno", deptNo);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
