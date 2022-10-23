using Bloodlust.Menu;
using HarmonyLib;
using MelonLoader;
using UniverseLib;

namespace Bloodlust;

public class BloodlustMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        BloodlustMenu.Initialize();
    }

    [HarmonyPatch(typeof(HoloNetMessenger), nameof(HoloNetMessenger.Method_Public_Static_Void_Object1Public6Vo66666666Unique_EnumPublicSealedvaOtAlSe5vSeUnique_0))]
    [HarmonyPatch(typeof(HoloNetMessenger), nameof(HoloNetMessenger.Method_Public_Static_Void_Object1Public6Vo66666666Unique_EnumPublicSealedvaOtAlSe5vSeUnique_1))]
    public static class MsgLogger
    {
        private static void Prefix([HarmonyArgument(0)] HoloNetGlobalMessage message)
        {
            Melon<BloodlustMod>.Logger.Msg(message.GetActualType().FullName);
        }
    }
}
