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
        private static SqlCeConnection con = new SqlCeConnection(@"Data Source=C:\Users\Ivo Xavier\Documents\GitHub\CSharpLearn\Win02\DataBase\bancoDados.sdf");
        public static DataTable PegarFuncionario()
        {
            
            SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM Funcionario", con);
           
            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds.Tables[0];
        }

        public static bool SalvarFuncionario(Funcionario funcionario)
        {
            string sql = "INSERT INTO [Funcionario](Nome, Email, Salario, Sexo, TipoContrato,DataRegisto) VALUES(@Nome, @Email, @Salario, @Sexo, @TipoContrato,@DataRegisto)";
            SqlCeCommand comando = new SqlCeCommand(sql, con);
            comando.Parameters.AddWithValue("@Nome", funcionario.Nome );
            comando.Parameters.AddWithValue("@Email", funcionario.Email);
            comando.Parameters.AddWithValue("@Salario", funcionario.Salario);
            comando.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
            comando.Parameters.AddWithValue("@TipoContrato", funcionario.TipoContrato);
            comando.Parameters.AddWithValue("@DataRegisto", DateTime.Now);

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

        public static bool AtualizarFuncionario(Funcionario funcionario)
        {
            string sql = "UPDATE [Funcionario] SET Nome = @Nome, Email = @Email, Salario =@Salario, Sexo = @Sexo, TipoContrato = @TipoContrato,DataAtualizacao = @DataAtualizacao WHERE Id = @Id";
            SqlCeCommand comando = new SqlCeCommand(sql, con);
            comando.Parameters.AddWithValue("@Id", funcionario.id);
            comando.Parameters.AddWithValue("@Nome", funcionario.Nome);
            comando.Parameters.AddWithValue("@Email", funcionario.Email);
            comando.Parameters.AddWithValue("@Salario", funcionario.Salario);
            comando.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
            comando.Parameters.AddWithValue("@TipoContrato", funcionario.TipoContrato);
            comando.Parameters.AddWithValue("@DataAtualizacao", funcionario.DataAtualizacao);

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
        public static Funcionario PegarFuncionario(int id)
        {
            string sql = "SELECT * FROM [Funcionario] WHERE Id = @id";
            SqlCeCommand comando = new SqlCeCommand(sql, con);
            comando.Parameters.AddWithValue("@id", id);

            con.Open();

            SqlCeDataReader resposta = comando.ExecuteReader();

            Funcionario funcionario = new Funcionario();
            while (resposta.Read())
            {
                //GetInt32(0) refre a coluna da dataTable do Id
                funcionario.id = resposta.GetInt32(0);
                funcionario.Nome = resposta.GetString(1);
                funcionario.Email = resposta.GetString(2);
                funcionario.Salario = resposta.GetDecimal(3);
                funcionario.Sexo = resposta.GetString(4);
                funcionario.TipoContrato = resposta.GetString(5);
                funcionario.DataRegisto = resposta.GetDateTime(6);
                if (resposta.IsDBNull(7))
                {
                    //funcionario.DataAtualizacao = null;
                }
                else
                {
                    funcionario.DataAtualizacao = resposta.GetDateTime(7);
                }
                    
            }
            con.Close();
            return funcionario;
        }

        public static bool ExcluirFuncionario(int id)
        {
            string sql = "DELETE FROM [Funcionario] WHERE Id = @id";
            SqlCeCommand comando = new SqlCeCommand(sql, con);
            comando.Parameters.AddWithValue("@id", id);

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
