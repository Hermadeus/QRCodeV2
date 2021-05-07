using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode
{
    public interface IWrapper<out T>
    {
        T Value { get; }
    }
}
