using Microsoft.AspNetCore.Mvc;

namespace VRodriguezMovie.Controllers
{
    public class CineController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();
            ML.Cine cine = new ML.Cine();
            if (result.Correct)
            {
                cine.Cines = result.Objects;
                return View(cine);
            }
            else
            {
                return View(cine);

            }
           
        }
        [HttpGet]
        public ActionResult Form (int? IdCine )
        {
           
            ML.Result resultzona = BL.Zona.GetAll();
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            if (resultzona.Correct)
            {
                cine.Zona.Zonas = resultzona.Objects;
              
            }
            if (IdCine == null)
            {
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cine.GetById(IdCine.Value);
                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;
                    cine.Zona.Zonas = resultzona.Objects;
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "ocurrio un problema";
                    return View("Modal");

                }
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
           
            if (cine.IdCine == 0)
            {
                result = BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Registrado con éxito";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "ocurrio problema";
                    return View("Modal");
                }
            }
            else
            {
                result = BL.Cine.Update(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "actualizado";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "ocurrio un problema";
                    return View("Modal");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(ML.Cine cine)
        {
            ML.Result result = BL.Cine.Delete(cine);
            if (result.Correct)
            {
                ViewBag.Message = "Eliminado exitosamente";
                return View("Modal");
            }
            else
            {
                ViewBag.Message = "ocurrio un problema";
                return View("Modal");
            }
        }

    }
}
