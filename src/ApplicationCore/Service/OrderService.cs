using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IBasketService _basketService;

        public OrderService(IRepository<Order> _orderRepo, IBasketService basketService)
        {
            this._orderRepo = _orderRepo;
            _basketService = basketService;
        }

        public async Task<Order> CreateOrderAsync(string buyerId, Address address)
        {
            var basket = await _basketService.GetBasketAsync(buyerId);
            var order = new Order()
            {
                BuyerId = buyerId,
                OrderDate = DateTimeOffset.Now,
                ShipToAddress = address,
                Items = basket.Items.Select(x => new OrderItem()
                {
                    PictureUri = x.Product.PictureUri,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    UnitPrice = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList()
            };
            return await _orderRepo.AddAsync(order);
        }
    }
}