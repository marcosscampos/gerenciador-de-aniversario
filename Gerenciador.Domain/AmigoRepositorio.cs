using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Gerenciador.Domain {
    public class AmigoRepositorio {
        readonly SqlDataAdapter adapter = new SqlDataAdapter();
        public string _con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyDocs\Desktop\Faculdade\C#\ASP.NET MVC\AT_Marcos_Vinicius\AT_Marcos_Vinicius\Gerenciador.Domain\Data\GerenciadorDomain.mdf;Integrated Security=True";
        public IEnumerable<Amigos> SelecionarTodosOsAmigos()
        {
            using (var con = new SqlConnection(_con))
            {
                var cmdtxt = "SELECT * FROM Amigo";
                var cmd = new SqlCommand(cmdtxt, con);
                Amigos amigo = null;
                var amigos = new List<Amigos>();
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            amigo = new Amigos();
                            amigo.Id = (int)reader["Id"];
                            amigo.Nome = reader["Nome"].ToString();
                            amigo.Sobrenome = reader["Sobrenome"].ToString();
                            amigo.Aniversario = Convert.ToDateTime(reader["Aniversario"]);
                            amigos.Add(amigo);
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
                return amigos;
            }
        }
        public void AdicionarAmigos(string nome, string sobrenome, DateTime aniversario)
        {
            var cmdtxt = "INSERT INTO Amigo(Nome, Sobrenome, Aniversario) values(@Nome, @Sobrenome, @Aniversario)";
            using (var con = new SqlConnection(_con))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdtxt;
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", sobrenome);
                    cmd.Parameters.AddWithValue("@Aniversario", aniversario);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public void AtualizarAmigo(int id, Amigos amigo)
        {
            var cmdtxt = "UPDATE Amigo SET Nome = @Nome, Sobrenome = @Sobrenome, Aniversario = @Aniversario where Id = @Id";
            using (var con = new SqlConnection(_con))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdtxt;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nome", amigo.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", amigo.Sobrenome);
                    cmd.Parameters.AddWithValue("@Aniversario", amigo.Aniversario);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public IEnumerable<Amigos> SelecionarAmigo(int Id)
        {
            var con_ = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyDocs\Desktop\Faculdade\C#\ASP.NET MVC\AT_Marcos_Vinicius\AT_Marcos_Vinicius\Gerenciador.Domain\Data\GerenciadorDomain.mdf;Integrated Security=True";
            using (var con = new SqlConnection(con_))
            {
                var cmdtxt = $"SELECT * FROM AMIGO WHERE ID = {Id}";
                var cmd = new SqlCommand(cmdtxt, con);

                Amigos amigo = null;
                try
                {
                    con.Open();

                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            amigo = new Amigos
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Aniversario = Convert.ToDateTime(reader["Aniversario"])
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

                yield return amigo;
            }
        }

        public void DeletarAmigo(int id, Amigos amigo)
        {
            var cmdtxt = "DELETE Amigo where Id = @Id";
            using (var con = new SqlConnection(_con))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdtxt;
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    } catch(Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
