using System;
using System.Linq;
using System.Threading.Tasks;
using Woodstock.DAL.Entities;
using Woodstock.DAL.Interfaces;

namespace Woodstock.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private WoodstockDbContext _context;

        private IQueryable<User> _userRepository;
        private IQueryable<BodyMaterial> _bodyMaterialRepository;
        private IQueryable<Color> _colorRepository;
        private IQueryable<Country> _countryRepository;
        private IQueryable<GlassMaterial> _glassMaterialRepository;
        private IQueryable<Manufacturer> _manufacturerRepository;
        private IQueryable<Mechanism> _mechanismRepository;
        private IQueryable<MechanismType> _mechanismTypeRepository;
        private IQueryable<Order> _orderRepository;
        private IQueryable<ShoppingCart> _shoppingCartRepository;
        private IQueryable<Strap> _strapRepository;
        private IQueryable<StrapMaterial> _strapMaterialRepository;
        private IQueryable<Watch> _watchRepository;

        public IQueryable<User> UserRepository => _userRepository ??= _context.Set<User>();
        public IQueryable<BodyMaterial> BodyMaterialRepository => _bodyMaterialRepository ??= _context.Set<BodyMaterial>();
        public IQueryable<Color> ColorRepository => _colorRepository ??= _context.Set<Color>();
        public IQueryable<Country> CountryRepository => _countryRepository ??= _context.Set<Country>();
        public IQueryable<GlassMaterial> GlassMaterialRepository => _glassMaterialRepository ??= _context.Set<GlassMaterial>();
        public IQueryable<Manufacturer> ManufacturerRepository => _manufacturerRepository ??= _context.Set<Manufacturer>();
        public IQueryable<Mechanism> MechanismRepository => _mechanismRepository ??= _context.Set<Mechanism>();
        public IQueryable<MechanismType> MechanismTypeRepository => _mechanismTypeRepository ??= _context.Set<MechanismType>();
        public IQueryable<Order> OrderRepository => _orderRepository ??= _context.Set<Order>();
        public IQueryable<ShoppingCart> ShoppingCartRepository => _shoppingCartRepository ??= _context.Set<ShoppingCart>();
        public IQueryable<Strap> StrapRepository => _strapRepository ??= _context.Set<Strap>();
        public IQueryable<StrapMaterial> StrapMaterialRepository => _strapMaterialRepository ??= _context.Set<StrapMaterial>();
        public IQueryable<Watch> WatchRepository => _watchRepository ??= _context.Set<Watch>();

        public UnitOfWork(WoodstockDbContext context)
        {
            _context = context;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _context.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
