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
            objectdialog.musicSlider.value = 0;
            objectdialog.backText.text =
                "( ͡° ͜ʖ ͡°) ";
            objectdialog.backText.rectTransform.sizeDelta = new Vector2(objectdialog.backText.preferredWidth,objectdialog.backText.flexibleHeight);
            return null;
        }
    }
}