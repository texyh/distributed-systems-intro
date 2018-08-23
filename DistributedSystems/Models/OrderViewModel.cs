using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderRegistration.Web.Models
{
    public class OrderViewModel
    {
        public string PickupAddress { get;  }

        public string PickupName { get;  }

        public string PickupCity { get;  }

        public string DeliveryAddress { get;  }

        public string DeliveryName { get;  }
        
        public string DeliveryCity { get;  }

        public int PackageWeight { get;  }
        
    }
}
