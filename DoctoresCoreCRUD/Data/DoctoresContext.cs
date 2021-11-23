using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DoctoresCoreCRUD.Models;

namespace DoctoresCoreCRUD.Data
{
    public class DoctoresContext
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public DoctoresContext(String cadenaconexion)
        {
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.com.CommandType = System.Data.CommandType.Text;
        }

        public List<Doctor> GetDoctores()
        {
            String sql = "select * from doctor";
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            List<Doctor> listadoctores = new List<Doctor>();
            while (this.reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Numero = (int)this.reader["DOCTOR_NO"];
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = (int)this.reader["SALARIO"];
                listadoctores.Add(doctor);
            }

            this.reader.Close();
            this.cn.Close();
            return listadoctores;
        }

        public Doctor FindDoctor(int iddoctor)
        {
            String sql = "select * from doctor where doctor_no=@id";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@id", iddoctor);
            this.com.Parameters.Add(pamid);
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            Doctor doctor = new Doctor();
            this.reader.Read();           
            doctor.Apellido = this.reader["APELLIDO"].ToString();
            doctor.Numero = (int)this.reader["DOCTOR_NO"];
            doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
            doctor.Salario = (int)this.reader["SALARIO"];

            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return doctor;
        }

        public int InsertDoctor(int iddoctor, String apellido, String especialidad, int salario)
        {
            int rhc = this.GetHospitalID();
            String sql = "insert into doctor values (@rhc, @id, @apellido, @especialidad, @salario)";
            this.com.CommandText = sql;
            SqlParameter pamrhc = new SqlParameter("@rhc", rhc);
            SqlParameter pamid = new SqlParameter("@id", iddoctor);
            SqlParameter pamape = new SqlParameter("@apellido", apellido);
            SqlParameter pamespe = new SqlParameter("@especialidad", especialidad);
            SqlParameter pamsal = new SqlParameter("@salario", salario);
            this.com.Parameters.Add(pamrhc);
            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamape);
            this.com.Parameters.Add(pamespe);
            this.com.Parameters.Add(pamsal);
            this.cn.Open();
            int insertados = this.com.ExecuteNonQuery();
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return insertados;
        }

        public int UpdateDoctor(int iddoctor, String apellido, String especialidad, int salario)
        {
            String sql = "update doctor set  apellido=@apellido, especialidad=@especialidad," +
                " salario=@salario where doctor_no=@id";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@id", iddoctor);
            SqlParameter pamape = new SqlParameter("@apellido", apellido);
            SqlParameter pamespe = new SqlParameter("@especialidad", especialidad);
            SqlParameter pamsal = new SqlParameter("@salario", salario);
            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamape);
            this.com.Parameters.Add(pamespe);
            this.com.Parameters.Add(pamsal);
            this.cn.Open();
            int modificados = this.com.ExecuteNonQuery();
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return modificados;
        }

        public int DeleteDoctor(int iddoctor)
        {
            String sql = "delete from doctor where doctor_no=@id";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@id", iddoctor);
            this.com.Parameters.Add(pamid);
            this.cn.Open();
            int eliminados = this.com.ExecuteNonQuery();
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return eliminados;
        }
        public int GetHospitalID()
        {
            Random random = new Random();
            int rhc = random.Next(1, 5);
            if(rhc == 1)
            {
                rhc = 17;
            }else if(rhc == 2)
            {
                rhc = 18;
            }
            else if (rhc == 3)
            {
                rhc = 19;
            }
            else if (rhc == 4)
            {
                rhc = 22;
            }
            else if (rhc == 5)
            {
                rhc = 45;
            }
            return rhc;
        }
    }
}
