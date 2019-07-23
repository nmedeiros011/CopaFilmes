using Microsoft.VisualStudio.TestTools.UnitTesting;
using CopaFilmes;
using CopaFilmes.Controllers;
using CopaFilmes.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CopaFilmesTeste
{
    public class resultadoModel
    {
        public string error { get; set; }
        public List<Filme> final { get; set; }

    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string selecionados = "[\"tt3606756\", \"tt4881806\", \"tt5164214\", \"tt7784604\", \"tt4154756\", \"tt5463162\", \"tt3778644\", \"tt3501632\"]";

            HomeController homeController = new HomeController();

            JsonResult resultadoFinal = homeController.Resultado(selecionados) as JsonResult;
            
            IDictionary<string, object> data = new RouteValueDictionary(resultadoFinal.Data);

            List<Filme> resultadoFinalData = data["final"] as List<Filme>;

            Filme campeao = new Filme()
            {
                id = "tt4154756",
                ano = "2018",
                nota = "8.8",
                titulo = "Vingadores: Guerra Infinita"
            }
            ;
            Filme vice = new Filme()
            {
                id = "tt3606756",
                ano = "2018",
                nota = "8.5",
                titulo = "Os Incríveis 2"
            };

            List<Filme> esperado = new List<Filme>()
            {
                campeao,
                vice
            };

            Assert.AreEqual(esperado[0].id, resultadoFinalData[0].id);
            Assert.AreEqual(esperado[1].id, resultadoFinalData[1].id);
        }
    }
}
