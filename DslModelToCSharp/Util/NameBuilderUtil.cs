﻿using DslModel.Domain;

namespace DslModelToCSharp.Util
{
    public class NameBuilderUtil
    {
        public string BuildCommandHandlerName(DomainClass domainClass)
        {
            return $"{domainClass.Name}CommandHandler";
        }

        public string CreateCommandName(DomainClass domainClass, CreateMethod createMethod)
        {
            return $"{domainClass.Name}{createMethod.Name}Command";
        }

        public string UpdateCommandName(DomainClass domainClass, DomainMethod method)
        {
            return $"{domainClass.Name}{method.Name}Command";
        }

        public string EventName(DomainClass domainClass, DomainMethod method)
        {
            return $"{domainClass.Name}{method.Name}Event";
        }

        public string EventName(DomainClass domainClass, CreateMethod method)
        {
            return $"{domainClass.Name}{method.Name}Event";
        }
    }
}