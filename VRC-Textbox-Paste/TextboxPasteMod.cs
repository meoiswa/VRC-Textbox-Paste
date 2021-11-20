using MelonLoader;
using System.Linq;
using Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRC.SDK3.Components;

namespace VRC_Textbox_Paste
{
    public class TextboxPasteMod : MelonMod
    {
        public override void OnApplicationLateStart()
        {
            if (MelonHandler.Mods.Any(it => it.Info.Name == "UI Expansion Kit"))
            {
                MelonLogger.Msg("Adding UIExpansionKit buttons");
                LastInputSelected.Init();
            }
            else
            {
                MelonLogger.Error("No UIExpansionKit found!");
            }
        }
    }
}
