using HarmonyLib;

namespace Bloodlust.Features.General;

[HarmonyPatch(typeof(PlayfabBackendAdapter))]
internal static class AntiPlayfab
{
    [HarmonyPatch(BloodyPlayfabBackendAdapter.MelRepRequestMethod)]
    [HarmonyPrefix]
    private static bool MelRepRequestPatch()
    {
        Logger.Msg("Prevented an anti-cheat report");
        return false;
    }

    [HarmonyPatch(BloodyPlayfabBackendAdapter.UpdateUserDataRequestMethod)]
    [HarmonyPrefix]
    private static bool UpdateUserDataRequestPatch([HarmonyArgument(1)] Il2CppSystem.Action<UpdateUserDataRequestResult> callback, UpdateUserDataRequestResult __result)
    {
        __result = new();
        __result.field_Public_Boolean_0 = true;
        callback?.Invoke(__result);

        Logger.Msg("Prevented a user data update request");
        return false;
    }
}
