using System;

namespace Libs.Popups
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PopupConstructorAttribute : Attribute
    {
        
    }

    [AttributeUsage(AttributeTargets.Property)]
    
    public class PopupPropertyAttribute : Attribute
    {
        
    }
}