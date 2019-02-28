using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Data.Contracts;
using Projeto.Entities;

namespace Projeto.Data.Tests
{
    [TestClass]
    public class ProdutoRepositoryTest
    {
        private IProdutoRepository repository;

        #region Constantes

        Produto _produto = new Produto()
        {
            IdProduto = Guid.NewGuid(),
            Nome = "Notebook",
            Preco = 2000m,
            Quantidade = 50
        };

        private const string _nome = "Notebook";
        private const decimal _preco = 2000.0m;
        private const int _quantidade = 500;

        private const string _nomeEdicao = "Monitor";
        private const decimal _precoEdicao = 1000.0m;
        private const int _quantidadeEdicao = 1500;

        #endregion
        [TestInitialize]
        public void Initialize()
        {
            repository = new ProdutoRepository();
        }

        [TestMethod]
        public void TestRepository()
        {
            var idProduto = TestInsert();
            Assert.AreNotEqual(Guid.Empty, idProduto);

            var produto = TestFindById(idProduto);
            Assert.IsNotNull(produto);
            Assert.AreEqual(idProduto, produto.IdProduto);
            Assert.AreEqual(_nome, produto.Nome);
            Assert.AreEqual(_preco, produto.Preco);
            Assert.AreEqual(_quantidade, produto.Quantidade);

            TestUpdate(produto);
            var produtoEditado = TestFindById(produto.IdProduto);

            Assert.IsNotNull(produtoEditado);
            Assert.AreEqual(idProduto, produtoEditado.IdProduto);
            Assert.AreEqual(_nomeEdicao, produtoEditado.Nome);
            Assert.AreEqual(_precoEdicao, produtoEditado.Preco);
            Assert.AreEqual(_quantidadeEdicao, produtoEditado.Quantidade);

            var lista = TestFindAll();

            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Count > 0);

            Assert.IsNotNull(lista.SingleOrDefault(p => p.IdProduto.Equals(idProduto)
                                                    && p.Nome.Equals(_nomeEdicao)
                                                    && p.Preco.Equals(_precoEdicao)
                                                    && p.Quantidade == _quantidadeEdicao));

            TestDelete(idProduto);
            var produtoExclusao = TestFindById(idProduto);

            Assert.IsNull(produtoExclusao);

        }

        private Guid TestInsert()
        {
            try
            {
                var produto = new Produto()
                {
                    IdProduto = Guid.NewGuid(),
                    Nome = _nome,
                    Preco = _preco,
                    Quantidade = _quantidade
                };

                repository.Insert(produto);
                return produto.IdProduto;
            }
            catch (Exception e)
            {
                Assert.Fail("Deu merda ao inserir" + e.Message);
                return Guid.Empty;
            }
        }

        private Produto TestFindById(Guid idProduto)
        {
            try
            {

            return repository.FindById(idProduto);
            }
            catch (Exception e)
            {
                Assert.Fail("Erro ao obter produto por id: " + e.Message);
                return null;
            }

        }

        private void TestUpdate(Produto produto)
        {
            try
            {
                produto.Nome = _nomeEdicao;
                produto.Preco = _precoEdicao;
                produto.Quantidade = _quantidadeEdicao;

                repository.Update(produto);

            }
            catch (Exception e)
            {
                Assert.Fail("Falha ao atualziar o produto: " + e.Message);
            }
        }
            
        private List<Produto> TestFindAll()
        {
            try
            {
                return repository.FindAll();
            }
            catch (Exception e)
            {
                Assert.Fail("Falha ao consultar todos os produtos: " + e.Message);
                return null;
            }
        }

        private void TestDelete(Guid idProduto)
        {
            try
            {

            repository.Delete(idProduto);
            }
            catch (Exception e)
            {

                Assert.Fail("Falha ao excluir o produto: " + e.Message);
            }
        }
    }
}
