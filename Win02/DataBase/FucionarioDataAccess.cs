using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;
using Win02.Modelo;

namespace Win02.DataBase
{
    internal class FucionarioDataAccess
    {
        private static SqlCeConnection con = new SqlCeConnection(@"Data Source=C:\Users\Ivo Xavier\source\repos\Solucao02\Win02\DataBase\bancoDados.sdf");
        public static DataTable PegarFuncionario()
        {
            
            SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM Funcionario", con);
           
            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds.Tables[0];
        }

        public static bool SalvarFuncionario(Funcionario funcionario)
        {
            string sql = "INSERT INTO [Funcionario](Nome, Email, Salario, Sexo, TipoContrato,DataRegisto,DataAtualizacao) VALUES(@Nome, @Email, @Salario, @Sexo, @TipoContrato,@DataRegisto,@DataAtualizacao)";
            SqlCeCommand comando = new SqlCeCommand(sql, con);
            comando.Parameters.AddWithValue("@Nome", funcionario.Nome );
            comando.Parameters.Add("@Email", "");
            comando.Parameters.Add("@Salario", "");
            comando.Parameters.Add("@Sexo", "");
            comando.Parameters.Add("@TipoContrato", "");
            comando.Parameters.Add("@DataRegisto", "");

            con.Open();
            if (comando.ExecuteNonQuery() > 0)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
    }
}
