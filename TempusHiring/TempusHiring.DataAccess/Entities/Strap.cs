namespace TempusHiring.DataAccess.Entities
{
    public class Strap 
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        

        public int WristSizeId { get; set; }
        public virtual WristSize WristSize { get; set; }
        public int StrapMaterialId { get; set; }
        public virtual StrapMaterial StrapMaterial { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
    }
}
