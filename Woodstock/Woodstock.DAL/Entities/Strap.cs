namespace Woodstock.DAL.Entities
{
    public class Strap 
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public string Size { get; set; }

        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int StrapId { get; set; }
        public virtual StrapMaterial StrapMaterial { get; set; }
    }
}
