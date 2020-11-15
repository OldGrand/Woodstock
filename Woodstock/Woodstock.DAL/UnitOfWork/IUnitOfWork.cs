using System;
using System.Linq;
using System.Threading.Tasks;
using Woodstock.DAL.Entities;

namespace Woodstock.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IQueryable<BodyMaterial> BodyMaterialRepository { get; }
        public IQueryable<Color> ColorRepository { get; }
        public IQueryable<Country> CountryRepository { get; }
        public IQueryable<GlassMaterial> GlassMaterialRepository { get; }
        public IQueryable<Manufacturer> ManufacturerRepository { get; }
        public IQueryable<Mechanism> MechanismRepository { get; }
        public IQueryable<MechanismType> MechanismTypeRepository { get; }
        public IQueryable<Order> OrderRepository { get; }
        public IQueryable<ShoppingCart> ShoppingCartRepository { get; }
        public IQueryable<Strap> StrapRepository { get; }
        public IQueryable<StrapMaterial> StrapMaterialRepository { get; }
        public IQueryable<Watch> WatchRepository { get; }

        Task SaveChangesAsync();
    }
}
