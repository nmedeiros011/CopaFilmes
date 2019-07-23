using CopaFilmes.Models;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CopaFilmes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var restClient = new RestClient("https://copadosfilmes.azurewebsites.net/api/filmes");
            var request = new RestRequest(Method.GET);
            IRestResponse response = restClient.Execute(request);

            Filmes filmes = new Filmes();
            filmes.listaFilmes = response.Content.ToString();

            return View("Index", "", filmes);
        }

        public ActionResult Resultado(string selecionados)
        {
            List<Filme> filmes = new List<Filme>();
            List<Filme> listaSelecionados = new List<Filme>();
            List<Filme> eliminatoria = new List<Filme>();
            List<Filme> final = new List<Filme>();

            var restClient = new RestClient("https://copadosfilmes.azurewebsites.net/api/filmes");
            var request = new RestRequest(Method.GET);
            IRestResponse response = restClient.Execute(request);

            filmes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Filme>>(response.Content.ToString());

            List<string> listaId = selecionados.Replace("\"", "").Replace("[", "").Replace("]", "").Replace(" ","").Split(',').ToList();

            foreach (var id in listaId)
            {
                listaSelecionados.Add(filmes.Where(x => x.id == id).First());
            }

            listaSelecionados.Sort((x, y) => string.Compare(x.titulo, y.titulo));

            var i = 0;
            var j = 7;

            for (i = 0; i <= 3; i++)
            {
                if (float.Parse(listaSelecionados[i].nota) >= float.Parse(listaSelecionados[j].nota))
                {
                    eliminatoria.Add(listaSelecionados[i]);
                }
                else
                {
                    eliminatoria.Add(listaSelecionados[j]);
                }

                j--;
            }

            for (i = 0; i <= 2; i += 2)
            {
                if (float.Parse(eliminatoria[i].nota) > float.Parse(eliminatoria[i + 1].nota))
                {
                    final.Add(eliminatoria[i]);
                }
                else if (float.Parse(eliminatoria[i].nota) < float.Parse(eliminatoria[i + 1].nota))
                {
                    final.Add(eliminatoria[i + 1]);
                }
                else if (float.Parse(eliminatoria[i].nota) == float.Parse(eliminatoria[i + 1].nota))
                {
                    eliminatoria.Sort((x, y) => string.Compare(x.titulo, y.titulo));
                    final.Add(eliminatoria[i]);
                }
            }

            if (float.Parse(final[0].nota) < float.Parse(final[1].nota))
            {
                var aux = final[0];
                final[0] = final[1];
                final[1] = aux;
            }
            else if (float.Parse(final[0].nota) == float.Parse(final[1].nota))
            {
                final.Sort((x, y) => string.Compare(x.titulo, y.titulo));
            }

            return Json(new { error = "0", final }, JsonRequestBehavior.AllowGet);
        }
    }
}