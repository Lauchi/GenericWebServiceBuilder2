﻿using System.Collections.Generic;
using Microwave.LanguageModel;

namespace Microwave.WebServiceModel.Application
{
    public class DomainHookBaseClass : DomainClass
    {
        public DomainHookBaseClass()
        {
            Name = "IDomainHook";
            Properties = new List<Property> {new Property {Name = "EventType", Type = "Type"}};
            Methods = new List<DomainMethod>
            {
                new DomainMethod
                {
                    Name = "ExecuteSavely",
                    ReturnType = "Task<HookResult>",
                    Parameters = {new Parameter {Name = "domainEvent", Type = "DomainEventBase"}}
                }
            };
        }
    }
}