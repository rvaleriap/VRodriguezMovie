using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
       public static ML.Result GetByUserName(string UserName, string Password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.CineContext context = new DL.CineContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{UserName}','{Password}'").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "No se logra mostrar el usuario";
            }
            return result;
        }

       
    }
}