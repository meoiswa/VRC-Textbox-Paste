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
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Paste", SendPasteEvent);
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
    }

    [HarmonyPatch(typeof(InputField), "OnSelect")]
    class InputFieldOnSelectPatch
    {
        public static void Prefix(InputField __instance)
        {
            MelonLogger.Msg($"An InputField was selected: {__instance} ({__instance.name})");
            LastInputSelected.SetLastInput(__instance);
        }
    }

    [HarmonyPatch(typeof(TMP_InputField), "OnSelect")]
    class TMP_InputFieldOnSelectPatch
    {
        public static void Prefix(TMP_InputField __instance)
        {
            MelonLogger.Msg($"A TMP_InputField was selected: {__instance} ({__instance.name})");
            LastInputSelected.SetLastInput(__instance);
        }
    }

    [HarmonyPatch(typeof(VRCUrlInputField), "OnSelect")]
    class VRCUrlInputFieldOnSelectPatch
    {
        public static void Prefix(VRCUrlInputField __instance)
        {
            MelonLogger.Msg($"A VRCUrlInputField was selected: {__instance} ({__instance.name})");
            LastInputSelected.SetLastInput(__instance);
        }
    }
}
