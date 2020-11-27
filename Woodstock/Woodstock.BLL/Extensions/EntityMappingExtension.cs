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
                Count = shoppingCart.Count,
                IsChecked = shoppingCart.IsChecked,
                UserId = shoppingCart.UserId,
                User = shoppingCart.User.ToDTO(),
                WatchId = shoppingCart.WatchId,
                Watch = shoppingCart.Watch.ToDTO(),
            };
        }

        public static OrderWatchLink ToOrder(this ShoppingCart shoppingCart, Order order)
        {
            return new OrderWatchLink
            {
                Order = order,
                OrderId = order.Id,
                Watch = shoppingCart.Watch,
                WatchId = shoppingCart.WatchId,
            };
        }
    }
}