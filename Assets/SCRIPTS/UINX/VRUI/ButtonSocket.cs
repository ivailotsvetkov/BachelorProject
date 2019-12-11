using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace MVR
{ 
    public class ButtonSocket : Injector
    {
        public UINX_ButtonType Type;
        public UINX_ButtonState State;
        [Multiline]
        public string Text;
        public UINX_AddTextEnum AddText;
        public Material Material;
    }
}