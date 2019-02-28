using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;

namespace Projeto.Data.Contracts
{
    /// <summary>
    /// Repositório de dados para a entidade Produto
    /// </summary>
    
    public interface IProdutoRepository
    {

        /// <summary>
        ///Método para inserir um novo produto na base de dados
        /// </summary>
        /// <param name="produto">Entidade contendo os dados do Produto </param>
        void Insert(Produto produto);


        /// <summary>
        ///Método para alterar um produto na base de dados
        /// </summary>
        /// <param name="produto">Entidade contendo os dados do Produto </param>
        void Update(Produto produto);


        /// <summary>
        ///Método para deletar um produto da base de dados
        /// </summary>
        /// <param name="idProduto"> Identificador GUID do produto </param>
        void Delete(Guid IdProduto);



        List<Produto> FindAll();


        Produto FindById(Guid IdProduto);
    }
}
