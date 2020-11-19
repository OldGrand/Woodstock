using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL.Interfaces;
using Woodstock.BLL.Pagination;
using AutoMapper;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Services
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShopService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<UserDTO>> GetItemsOnPage(int pageNum, int itemsOnPage)
        {
            var pagedResult = await _unitOfWork.WatchRepository.GetPaged(pageNum, itemsOnPage);
            return _mapper.Map<PagedResultDTO<UserDTO>>(pagedResult);
        }
    }
}
