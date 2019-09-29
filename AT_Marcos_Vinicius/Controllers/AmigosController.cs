using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gerenciador.Domain;

namespace AT_Marcos_Vinicius.Controllers
{
    public class AmigosController : Controller
    {
        AmigoRepositorio repositorio = new AmigoRepositorio();
        public ActionResult Index()
        {
            var amigos = repositorio.SelecionarTodosOsAmigos();
            return View(amigos);

        }


        public ActionResult Details(int id) => View(BuscarPessoaPelo(id));

        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(Amigos amigo)
        {
            try
            {
                repositorio.AdicionarAmigos(amigo.Nome, amigo.Sobrenome, amigo.Aniversario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id) => View(BuscarPessoaPelo(id));

        [HttpPost]
        public ActionResult Edit(int id, Amigos amigo)
        {
            Amigos amigos = new Amigos();
            amigos.Nome = amigo.Nome;
            amigos.Sobrenome = amigo.Sobrenome;
            amigos.Aniversario = amigo.Aniversario;

            try
            {
                repositorio.AtualizarAmigo(id, amigos);
                return RedirectToAction("Details", new { id = amigo.Id });
            }
            catch
            {
                return View();
            }

        }


        public ActionResult Delete(int id) => View(BuscarPessoaPelo(id));


        [HttpPost]
        public ActionResult Delete(int id, Amigos amigo)
        {
            Amigos amigos = new Amigos
            {
                Nome = amigo.Nome,
                Sobrenome = amigo.Sobrenome,
                Aniversario = amigo.Aniversario
            };
            try
            {
                repositorio.DeletarAmigo(id, amigos);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search()
        {
            string nome = "";
            return View(BuscarPeloNome(nome));

        }

        [HttpPost]
        public ActionResult Search(string nome)
        {
            List<Amigos> buscarAmigo = BuscarPeloNome(nome);
            try
            {
                return View(buscarAmigo);
            }
            catch
            {
                return View();
            }

        }

        public Amigos BuscarPessoaPelo(int id)
        {
            Amigos amigosEncontrados = new Amigos();
            foreach (Amigos a in repositorio.SelecionarAmigo(id))
            {
                if (a.Id == id)
                {
                    amigosEncontrados.Id = id;
                    amigosEncontrados.Nome = a.Nome;
                    amigosEncontrados.Sobrenome = a.Sobrenome;
                    amigosEncontrados.Aniversario = a.Aniversario;
                }
            }
            return amigosEncontrados;
        }

        public List<Amigos> BuscarPeloNome(string nome)
        {
            var listaAmigos = repositorio.SelecionarTodosOsAmigos();
            List<Amigos> resultList = new List<Amigos>();
            foreach (Amigos amigo in listaAmigos)
            {
                if (amigo.Nome.Contains(nome))
                {
                    resultList.Add(amigo);
                }
            }
            return resultList;
        }
    }
}


