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
    
    
    public interface IUser
    {
        
        UserCreateEvent UserCreate(String Name);
        
        UserUpdateAgeEvent UserUpdateAge(Int32 Age);
    }
    
    public partial class User : IUser
    {
        
        private String _Name;
        
        private Int32 _Age;
        
        public String Name
        {
            get
            {
                return this._Name;
            }
        }
        
        public Int32 Age
        {
            get
            {
                return this._Age;
            }
        }
    }
}
