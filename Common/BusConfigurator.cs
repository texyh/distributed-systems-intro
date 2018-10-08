﻿using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DistribuedSystem.Common
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
                registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConstants.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConstants.UserName);
                    hst.Password(RabbitMqConstants.Password);
                });

                registrationAction?.Invoke(cfg, host);
            }); 
        }
    }
}
