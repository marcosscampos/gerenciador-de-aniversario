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

        
        public ActionResult Edit(int id)
        {
            return View();
        }

        
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
                return RedirectToAction("Index");
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
            Amigos amigos = new Amigos();
            amigos.Nome = amigo.Nome;
            amigos.Sobrenome = amigo.Sobrenome;
            amigos.Aniversario = amigo.Aniversario;
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
            string pesquisarAmigo = "";
            var repo = new AmigoRepositorio();
            return View(BuscarPeloNome(pesquisarAmigo));

        }

        [HttpPost]
        public ActionResult Search(string pesquisarAmigo)
        {

            try
            {
            List<Amigos> buscarAmigo = BuscarPeloNome(pesquisarAmigo);
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
            foreach (Amigos a in repositorio.SelecionarTodosOsAmigos())
            {
                if (a.Id == id)
                {
                    amigosEncontrados.Id = id;
                    amigosEncontrados.Nome = a.Nome;
                    amigosEncontrados.Sobrenome = a.Sobrenome;
                    amigosEncontrados.Aniversario = a.Aniversario;
                    break;
                }
            }
            return amigosEncontrados;
        }

        public List<Amigos> AniversariantesDoDia(DateTime aniversario)
        {
            List<Amigos> amigosAniversariantes = new List<Amigos>();
            var amigosLista = repositorio.SelecionarTodosOsAmigos();
            foreach(Amigos a in amigosLista)
            {
                if(a.Aniversario == DateTime.Now)
                {
                    amigosAniversariantes.Add(a);
                }
            }
            return amigosAniversariantes;
        }

        public List<Amigos> BuscarPeloNome(string pesquisarAmigo)
        {
            List<Amigos> buscarAmigo = new List<Amigos>();
            var listaAmigos = repositorio.SelecionarTodosOsAmigos();
            foreach (Amigos a in listaAmigos)
            {
                if (a.Nome.Contains(pesquisarAmigo))
                {
                    buscarAmigo.Add(a);
                }
            }
            return buscarAmigo;
        }
    }
}
