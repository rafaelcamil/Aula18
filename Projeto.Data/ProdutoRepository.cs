using Projeto.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace Projeto.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string connectionString;

        public ProdutoRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["AULA"].ConnectionString;
        }

        public void Insert(Produto produto)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "insert into Produto(IdProduto, Nome, Preco, Quantidade) "
                          + "values(@IdProduto, @Nome, @Preco, @Quantidade)";

                conn.Execute(query, produto);
            }
        }

        public void Update(Produto produto)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "update Produto set Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade "
                          + "where IdProduto = @IdProduto";

                conn.Execute(query, produto);
            }
        }

        public void Delete(Guid idProduto)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "delete from Produto where IdProduto = @IdProduto";

                conn.Execute(query, new { IdProduto = idProduto });
            }
        }

        public List<Produto> FindAll()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "select * from Produto";

                return conn.Query<Produto>(query).ToList();
            }
        }

        public Produto FindById(Guid idProduto)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "select * from Produto where IdProduto = @IdProduto";

                return conn.QuerySingleOrDefault<Produto>(query, new { IdProduto = idProduto });
            }
        }
    }
}
