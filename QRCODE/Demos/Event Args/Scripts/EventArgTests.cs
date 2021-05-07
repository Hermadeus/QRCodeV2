using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

public class EventArgTests : MonoBehaviour
{
    public GenericUnityEvent UnityEvent;

    private void Awake()
    {
        var arg = new FloatEventArgs()
        {
            Value = 150f
        };
        
        UnityEvent.AddListener(DebugEventArg);
        
        UnityEvent.Invoke(arg);
    }

    public void DebugEventArg(EventArgs args)
    {
        if (args is FloatEventArgs)
        {
            var cast = args as FloatEventArgs;
            Debug.Log(cast.Value);
        }
    }
}
