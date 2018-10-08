﻿namespace DistribuedSystem.Common.Interfaces
{
    public interface IOrderRegisteredEvent
    {
        string OrderId { get; }
        string PickupName { get; }
        string PickupAddress { get; }
        string PickupCity { get; }

        string DeliverName { get; }
        string DeliverAddress { get; }
        string DeliverCity { get; }

        int Weight { get; }
        bool Fragile { get; }
        bool Oversized { get; }
    }
}