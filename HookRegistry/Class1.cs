using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine;
using System.Collections;
using Mono.CSharp;
using System;


namespace Hooks
{
    [RuntimeHook]
    public class Class1
    {
        public Class1()
        {
            HookRegistry.Register(OnCall);
        }

        object OnCall(string typeName, string methodName, object thisObj, params object[] args)
        {
            var objectdialog = (OptionsUI) thisObj;
            var myconsole = new GameObject().AddComponent<Console>();

            return null;
        }
    }
}