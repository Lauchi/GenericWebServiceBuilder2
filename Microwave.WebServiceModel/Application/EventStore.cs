﻿using System.Collections.Generic;
using Microwave.LanguageModel;
using DomainEventBaseClass = Microwave.WebServiceModel.Domain.DomainEventBaseClass;

namespace Microwave.WebServiceModel.Application
{
    public class EventStore : DomainClass
    {
        public EventStore()
        {
            Name = "EventStore";
            Methods = new List<DomainMethod>
            {
                new DomainMethod
                {
                    Name = "AddEvents",
                    ReturnType = "Task",
                    Parameters = {new Parameter {Name = "domainEvents", Type = "List<DomainEventBase>"}}
                }
            };
            Properties = new List<Property>
            {
                new Property {Name = "EventStoreRepository", Type = new EventStoreRepositoryInterface().Name},
                new Property {Name = "DomainHooks", Type = $"IEnumerable<{new DomainHookBaseClass().Name}>"}
            };
            Methods = new List<DomainMethod>
            {
                new DomainMethod
                {
                    Name = "AppendAll",
                    ReturnType = $"async Task<{new HookResultBaseClass().Name}>",
                    Parameters = { new Parameter
                    {
                        Name = "domainEvents",
                        Type = $"List<{new DomainEventBaseClass().Name}>"
                    }}
                }
            };
        }
    }
}