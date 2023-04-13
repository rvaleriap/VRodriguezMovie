using Microsoft.AspNetCore.Mvc;

namespace VRodriguezMovie.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            ML.Result result = BL.Usuario.GetByUserName(UserName,Password);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                if(Password== usuario.Password)
                {
                    ViewBag.Message = "Bienvenido";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta";
                    RedirectToAction();
                }
            }
            else
            {
                ViewBag.Message = "Usuario Incorrecto";
                RedirectToAction();
            }
            return View("ModalLogin");

        }

      
    }
}
