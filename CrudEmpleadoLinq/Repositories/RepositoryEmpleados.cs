using CrudEmpleadoLinq.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CrudEmpleadoLinq.Repositories
{
    public class RepositoryEmpleados
    {
        DataTable tablaEmpleados;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public RepositoryEmpleados()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from EMP";
            SqlDataAdapter adEmp = new SqlDataAdapter(sql, connectionString);
            this.tablaEmpleados = new DataTable();
            adEmp.Fill(this.tablaEmpleados);
            //Ado
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Empleado> GetEmpleados()
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           select datos;
            List<Empleado> empleados = new List<Empleado>();
            foreach (var row in consulta)
            {
                Empleado empleado = new Empleado();
                empleado.EMP_NO = row.Field<int>("EMP_NO");
                empleado.APELLIDO = row.Field<string>("APELLIDO");
                empleado.OFICIO = row.Field<string>("OFICIO");
                empleado.DIR = row.Field<int>("DIR");
                empleado.FECHA_ALT = row.Field<DateTime>("FECHA_ALT");
                empleado.SALARIO = row.Field<int>("SALARIO");
                empleado.COMISION = row.Field<int>("COMISION");
                empleado.DEPT_NO = row.Field<int>("DEPT_NO");
                empleados.Add(empleado);
            }
            return empleados;
        }

        public Empleado FindEmpleado
            (int empNo)
        {
            string sql = "select * from EMP where EMP_NO=@empno";
            this.com.Parameters.AddWithValue("@empno", empNo);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            Empleado empleado = new Empleado();
            this.reader.Read();
            empleado.EMP_NO = int.Parse(this.reader["EMP_NO"].ToString());
            empleado.APELLIDO = this.reader["APELLIDO"].ToString();
            empleado.OFICIO = this.reader["OFICIO"].ToString();
            empleado.DIR = int.Parse(this.reader["DIR"].ToString());
            empleado.FECHA_ALT = DateTime.Parse(this.reader["FECHA_ALT"].ToString());
            empleado.SALARIO = int.Parse(this.reader["SALARIO"].ToString());
            empleado.COMISION = int.Parse(this.reader["COMISION"].ToString());
            empleado.DEPT_NO = int.Parse(this.reader["DEPT_NO"].ToString());
            this.cn.Close();
            this.reader.Close();
            this.com.Parameters.Clear();
            return empleado;
        }
        public void UpdateEmpleado
            (Empleado empleado)
        {
            string sql = "update EMP set APELLIDO=@apellido, OFICIO=@oficio, " +
                "DIR=@dir, FECHA_ALT=@fechaalt, SALARIO=@salario, COMISION=@comision, " +
                "DEPT_NO=@deptno where EMP_NO=@empno";
            this.com.Parameters.AddWithValue("@empno",empleado.EMP_NO);
            this.com.Parameters.AddWithValue("@apellido",empleado.APELLIDO);
            this.com.Parameters.AddWithValue("@oficio",empleado.OFICIO);
            this.com.Parameters.AddWithValue("@dir",empleado.DIR);
            this.com.Parameters.AddWithValue("@fechaalt",empleado.FECHA_ALT);
            this.com.Parameters.AddWithValue("@salario",empleado.SALARIO);
            this.com.Parameters.AddWithValue("@comision",empleado.COMISION);
            this.com.Parameters.AddWithValue("@deptno",empleado.DEPT_NO);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public void DeleteEmpleado
            (int empNo)
        {
            string sql = "delete from EMP where EMP_NO=@empno";
            this.com.Parameters.AddWithValue("@empno",empNo);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public void CreateEmpleado
            (Empleado empleado)
        {
            string sql = "insert into EMP values(@empno, @apellido, @oficio, @dir, @fechaalt, @salario, @comision, @deptno)";
            this.com.Parameters.AddWithValue("@empno", empleado.EMP_NO);
            this.com.Parameters.AddWithValue("@apellido", empleado.APELLIDO);
            this.com.Parameters.AddWithValue("@oficio", empleado.OFICIO);
            this.com.Parameters.AddWithValue("@dir", empleado.DIR);
            this.com.Parameters.AddWithValue("@fechaalt", empleado.FECHA_ALT);
            this.com.Parameters.AddWithValue("@salario", empleado.SALARIO);
            this.com.Parameters.AddWithValue("@comision", empleado.COMISION);
            this.com.Parameters.AddWithValue("@deptno", empleado.DEPT_NO);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public List<string> GetOficios()
        {
            var consulta = (from datos in this.tablaEmpleados.AsEnumerable()
                           select datos.Field<string>("OFICIO")).Distinct();
            return consulta.ToList();
        }
        public List<Empleado> GetEmpleadosOficios
            (string oficio)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio
                           select datos;
            List<Empleado> empleados = new List<Empleado>();
            foreach (var row in consulta)
            {
                Empleado empleado = new Empleado();
                empleado.EMP_NO = row.Field<int>("EMP_NO");
                empleado.APELLIDO = row.Field<string>("APELLIDO");
                empleado.OFICIO = row.Field<string>("OFICIO");
                empleado.DIR = row.Field<int>("DIR");
                empleado.FECHA_ALT = row.Field<DateTime>("FECHA_ALT");
                empleado.SALARIO = row.Field<int>("SALARIO");
                empleado.COMISION = row.Field<int>("COMISION");
                empleado.DEPT_NO = row.Field<int>("DEPT_NO");
                empleados.Add(empleado);
            }
            return empleados;

        }
    }
}
