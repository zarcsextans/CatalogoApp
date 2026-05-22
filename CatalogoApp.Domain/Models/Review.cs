namespace CatalogoApp.Domain.Models
{
    public class Review
    {
        public int ItemId { get; set; }

        public string Usuario { get; set; }
            = string.Empty;

        public string Comentario { get; set; }
            = string.Empty;

        public int Estrellas { get; set; }
    }
}