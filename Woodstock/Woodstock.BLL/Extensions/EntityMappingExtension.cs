using Woodstock.BLL.DTOs;
using Woodstock.DAL.Entities;

namespace Woodstock.BLL.Extensions
{
    public static class EntityMappingExtension
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };
        }

        public static WatchDTO ToDTO(this Watch watch)
        {
            return new WatchDTO
            {
                Id = watch.Id,
                Title = watch.Title,
                Description = watch.Description,
                Diameter = watch.Diameter,
                Gender = watch.Gender,
                Photo = watch.Photo,
                Price = watch.Price,
            };
        }

        public static ShoppingCartDTO ToDTO(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartDTO
            {
                Id = shoppingCart.Id,
                User = shoppingCart.User.ToDTO(),
                Watch = shoppingCart.Watch.ToDTO(),
            };
        }
    }
}
