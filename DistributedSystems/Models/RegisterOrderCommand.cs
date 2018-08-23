using System;
using OrderService.common;

namespace OrderRegistration.Web.Models
{
    public class RegisterOrderCommand : IRegisterOrderCommand
    {
        private OrderViewModel _order;

        public RegisterOrderCommand(OrderViewModel model)
        {
            _order = model;
        }

        public string PickupAddress => _order.PickupAddress;

        public string PickupName => _order.PickupName;

        public string PickupCity => _order.PickupCity;

        public string DeliveryAddress => _order.DeliveryAddress;

        public string DeliveryName => _order.DeliveryName;

        public string DeliveryCity => _order.DeliveryCity;

        public int PackageWeight => _order.PackageWeight;
    }
}
