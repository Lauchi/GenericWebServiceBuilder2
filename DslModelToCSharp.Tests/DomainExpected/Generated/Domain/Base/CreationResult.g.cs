//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Collections.Generic;
    
    
    public class CreationResult<T>
        where T :  class
    {
        
        private Boolean _Ok;
        
        private List<DomainEventBase> _DomainEvents;
        
        private List<string> _DomainErrors;
        
        private T _CreatedEntity;
        
        private CreationResult(Boolean Ok, List<DomainEventBase> DomainEvents, List<string> DomainErrors, T CreatedEntity)
        {
            this._Ok = Ok;
            this._DomainEvents = DomainEvents;
            this._DomainErrors = DomainErrors;
            this._CreatedEntity = CreatedEntity;
        }
        
        public Boolean Ok
        {
            get
            {
                return this._Ok;
            }
        }
        
        public List<DomainEventBase> DomainEvents
        {
            get
            {
                return this._DomainEvents;
            }
        }
        
        public List<string> DomainErrors
        {
            get
            {
                return this._DomainErrors;
            }
        }
        
        public T CreatedEntity
        {
            get
            {
                return this._CreatedEntity;
            }
        }
        
        public static CreationResult<T> OkResult(List<DomainEventBase> DomainEvents, T CreatedEntity)
        {
            return new CreationResult<T>(true, DomainEvents, new List<string>(), CreatedEntity);
        }
        
        public static CreationResult<T> ErrorResult(List<string> DomainErrors)
        {
            return new CreationResult<T>(false, new List<DomainEventBase>(), DomainErrors, null);
        }
    }
}
