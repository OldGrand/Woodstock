using System.Threading.Tasks;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL.Interfaces;
using Woodstock.BLL.Pagination;
using AutoMapper;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CatalogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<WatchDTO>> GetItemsOnPage(int pageNum, int itemsOnPage)
        {
            var pagedResult = await _unitOfWork.WatchRepository.GetPaged(pageNum, itemsOnPage);
            return _mapper.Map<PagedResultDTO<WatchDTO>>(pagedResult);
        }
    }
}
