using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace VRodriguezMovie.Controllers
{
    public class MovieController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult GetPopularMovie()
        {
            Models.Movie movie = new Models.Movie();
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("movie/popular?api_key=fa20eaffa82fbf3ff103092e00522a01&language=es-ES&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    movie.Movies = new List<object>();
                    foreach(var resultItem in resultJson.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.Id = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Titulo = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }

            }
            return View(movie);
        }
       
        public ActionResult GetFavorito()
        {
            Models.Movie movie = new Models.Movie();
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("account/18702744/favorite/movies?api_key=fa20eaffa82fbf3ff103092e00522a01&session_id=c370747f48423648f7755a8a8f304a6ff4573538&language=es-ES&sort_by=created_at.asc&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJson.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.Id = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Titulo = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }

            }
            return View(movie);

        }
        public ActionResult AddFavorites(Models.Movie movie)
        {
            movie.media_type = "movie";
            movie.media_id = movie.Id;
            movie.favorite = true;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //HTTP POST
                var postTask = client.PostAsJsonAsync("account/18702744/favorite?api_key=fa20eaffa82fbf3ff103092e00522a01&session_id=c370747f48423648f7755a8a8f304a6ff4573538", movie);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    ViewBag.Mensaje = "Se ha agregado a favoritos";
                    return PartialView("Modal");

                }
                else
                {
                    ViewBag.Mensaje = "No se ha añadido a favoritos";
                    return PartialView("Modal");
                }
            }
        }

        public ActionResult DeleteFavorites(Models.Movie movie)
        {
            movie.media_type = "movie";
            movie.media_id = movie.Id;
            movie.favorite = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //HTTP POST
                var postTask = client.PostAsJsonAsync("account/18702744/favorite?api_key=fa20eaffa82fbf3ff103092e00522a01&session_id=c370747f48423648f7755a8a8f304a6ff4573538", movie);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    ViewBag.Mensaje = "Se ha agregado a favoritos";
                    return PartialView("Modal");

                }
                else
                {
                    ViewBag.Mensaje = "No se ha añadido a favoritos";
                    return PartialView("Modal");
                }
            }
        }
    }
}
