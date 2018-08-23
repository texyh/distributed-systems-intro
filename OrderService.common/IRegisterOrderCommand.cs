using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.common
{
    public interface IRegisterOrderCommand
    {
         string PickupAddress { get;  }

         string PickupName { get;  }

         string PickupCity { get;  }

         string DeliveryAddress { get;  }

         string DeliveryName { get;  }

         string DeliveryCity { get;  }

         int PackageWeight { get;  }
    }
}
