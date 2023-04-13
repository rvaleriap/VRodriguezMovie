using Microsoft.AspNetCore.Mvc;

namespace VRodriguezMovie.Controllers
{
    public class EstadisticaController : Controller
    {
        public ActionResult Estadistica()
        {
            ML.Estadistica estadistica = new ML.Estadistica();
            ML.Result result = BL.Estadistica.Porcentaje();
           
                estadistica = (ML.Estadistica)result.Object;
         
           

            return View(estadistica);
        }
    }
}
