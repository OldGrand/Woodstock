using System.Linq;
using TempusHiring.BusinessLogic.DTOs;
using TempusHiring.DataAccess.Entities;

namespace TempusHiring.BusinessLogic.Extensions
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
                CountInStock = watch.CountInStock,
                SaledCount = watch.SaledCount,
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
                Count = shoppingCart.Count,
            };
        }

        public static OrderDTO ToDTO(this Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                IsOrderCompleted = order.IsOrderCompleted,
                OrderDate = order.OrderDate,
                OrderWatchLinks = order.OrderWatchLinks.Select(_ => _.ToDTO()),
                PaymentMethod = order.PaymentMethod,
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
                User = order.User.ToDTO(),
                UserId = order.UserId,
            };
        }

        public static OrderWatchLinkDTO ToDTO(this OrderWatchLink orderWatchLink)
        {
            return new OrderWatchLinkDTO
            {
                Id = orderWatchLink.Id,
                Count = orderWatchLink.Count,
                Order = orderWatchLink.Order.ToDTO(),
                OrderId = orderWatchLink.OrderId,
                Watch = orderWatchLink.Watch.ToDTO(),
                WatchId = orderWatchLink.WatchId,
            };
        }
    }
}