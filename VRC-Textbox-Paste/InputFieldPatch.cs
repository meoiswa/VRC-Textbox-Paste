using HarmonyLib;
using MelonLoader;
using UnityEngine.UI;
using TMPro;
using VRC.SDK3.Components;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using UIExpansionKit.API;

namespace VRC_Textbox_Paste
{
    public static class LastInputSelected
    {
        private static InputField Input;
        private static TMP_InputField TmpInput;
        private static VRCUrlInputField VUInput;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Init()
        {
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Send 'Copy'", SendCopyEvent);
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Send 'Paste'", SendPasteEvent);
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Send 'Return'", SendPasteEvent);
        }

        public static void SetLastInput(InputField input)
        {
            Input = input;
            TmpInput = null;
            VUInput = null;
        }

        public static void SetLastInput(TMP_InputField input)
        {
            Input = null;
            TmpInput = input;
            VUInput = null;
        }

        public static void SetLastInput (VRCUrlInputField input)
        {
            Input = null;
            TmpInput = null;
            VUInput = input;
        }

        public static void SendCopyEvent()
        {
            if (Input != null)
            {
                Input.ProcessEvent(Event.KeyboardEvent("^C"));
            }
            else if (TmpInput != null)
            {
                TmpInput.ProcessEvent(Event.KeyboardEvent("^C"));
            }
            else if (VUInput != null)
            {
                VUInput.ProcessEvent(Event.KeyboardEvent("^C"));
            }
        }

        public static void SendPasteEvent()
        {
            if (Input != null)
            {
                Input.ProcessEvent(Event.KeyboardEvent("^V"));
            }
            else if (TmpInput != null)
            {
                TmpInput.ProcessEvent(Event.KeyboardEvent("^V"));
            }
            else if (VUInput != null)
            {
                VUInput.ProcessEvent(Event.KeyboardEvent("^V"));
            }
        }

        public static void SendEnterEvent()
        {
            if (Input != null)
            {
                Input.ProcessEvent(Event.KeyboardEvent("return"));
            }
            else if (TmpInput != null)
            {
                TmpInput.ProcessEvent(Event.KeyboardEvent("return"));
            }
            else if (VUInput != null)
            {
                VUInput.ProcessEvent(Event.KeyboardEvent("return"));
            }
        }
    }

    [HarmonyPatch(typeof(InputField), "OnSelect")]
    class InputFieldOnSelectPatch
    {
        public static void Prefix(InputField __instance)
        {
            MelonDebug.Msg($"An InputField was selected: {__instance} ({__instance.name})");
            LastInputSelected.SetLastInput(__instance);
        }
    }

    [HarmonyPatch(typeof(TMP_InputField), "OnSelect")]
    class TMP_InputFieldOnSelectPatch
    {
        public static void Prefix(TMP_InputField __instance)
        {
            MelonDebug.Msg($"A TMP_InputField was selected: {__instance} ({__instance.name})");
            LastInputSelected.SetLastInput(__instance);
        }
    }

    [HarmonyPatch(typeof(VRCUrlInputField), "OnSelect")]
    class VRCUrlInputFieldOnSelectPatch
    {
        public static void Prefix(VRCUrlInputField __instance)
        {
            MelonDebug.Msg($"A VRCUrlInputField was selected: {__instance} ({__instance.name})");
            LastInputSelected.SetLastInput(__instance);
        }
    }
}
