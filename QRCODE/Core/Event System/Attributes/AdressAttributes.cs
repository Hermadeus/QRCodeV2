using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.Events
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class AddressAttribute : Attribute
    {
        public AddressAttribute(ushort pin, Type typeReference)
        {
            this.Pin = pin;
            TypeReference = typeReference;
        }
        
        public ushort Pin { get; private set; }
        
        public Type TypeReference { get; private set; }
    }
}
