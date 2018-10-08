using System;
using System.Collections.Generic;
using System.Text;
using DistribuedSystem.Common.Interfaces;

namespace RegistrationService.Messages
{
    public class OrderRegisteredEvent : IOrderRegisteredEvent
    {
        private IRegisterOrderCommand command;
        private string orderId;
        public OrderRegisteredEvent(IRegisterOrderCommand command, string orderId)
        {
            this.command = command;
            this.orderId = orderId;
        }
        public string OrderId => orderId;
        public string PickupName => command.PickupName;
        public string PickupAddress => command.PickupAddress;
        public string PickupCity => command.PickupCity;

        public string DeliverName => command.DeliverName;
        public string DeliverAddress => command.DeliverAddress;
        public string DeliverCity => command.DeliverCity;

        public int Weight => command.Weight;
        public bool Fragile => command.Fragile;
        public bool Oversized => command.Oversized;
    }
}
