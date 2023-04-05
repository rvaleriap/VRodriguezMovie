namespace VRodriguezMovie.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public List<Object> Movies { get; set; }
        public string media_type { get; set; }
        public string media_id { get; set; }
        
        public bool favorite { get; set; }
    }
}
