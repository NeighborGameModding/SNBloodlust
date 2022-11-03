using Bloodlust.Deobfuscation;
using Bloodlust.Utils;
using HarmonyLib;
using Il2CppSystem.IO;
using MelonLoader;

namespace Bloodlust.Features.General;

[HarmonyPatch(typeof(PlayfabBackendAdapter))]
internal static class AntiPlayfab
{
    private static readonly string _loadoutSaveFilePath = Path.Combine(MelonUtils.UserDataDirectory, "Loadout.json");

    // Not needed with the bypass tbh
    //[HarmonyPatch(BloodyPlayfabBackendAdapter.MelRepRequestMethod)]
    //[HarmonyPrefix]
    //private static bool MelRepRequestPatch()
    //{
    //    Logger.Msg("Prevented an anti-cheat report");
    //    return false;
    //}

    internal static void Initialize()
    {
        HarmonyUtils.PatchObfuscated(typeof(PlayfabBackendAdapter), BloodyPlayfabBackendAdapter.GetLoadoutRequestMethod, new(new GetLoadoutPatchDel(GetLoadoutRequestPatch).Method));
    }

    private static bool GetLoadoutRequestPatch([HarmonyArgument(0)] Il2CppSystem.Action<GetLoadoutRequestResult> callback, ref GetLoadoutRequestResult __result)
    {
        Logger.Msg("Loading local loadout");

        var loadout = LoadLocalLoadout();
        if (loadout == null)
        {
            Logger.Msg("No local loadout found. Requesting loadout from PlayFab...");
            return true;
        }

        __result = new();
        __result.SetLoadoutResult(loadout, callback);

        return false;
    }

    [HarmonyPatch(BloodyPlayfabBackendAdapter.UpdateLoadoutRequestMethod)]
    [HarmonyPrefix]
    private static bool UpdateLoadoutRequestPatch([HarmonyArgument(0)] Loadout loadout, [HarmonyArgument(1)] Il2CppSystem.Action<UpdateLoadoutRequestResult> callback, ref UpdateLoadoutRequestResult __result)
    {
        Logger.Msg("Prevented a loadout update request");

        SaveLoadoutLocally(loadout);

        __result = new();
        __result.SetResultReceived(callback);

        return false;
    }

    public static Loadout LoadLocalLoadout()
    {
        string loadoutJson;
        try
        {
            loadoutJson = File.ReadAllText(_loadoutSaveFilePath);
        }
        catch
        {
            return null;
        }

        LoadoutSerializer loadoutSerializer;
        try
        {
            loadoutSerializer = UnityEngine.JsonUtility.FromJson<LoadoutSerializer>(loadoutJson);
        }
        catch
        {
            loadoutSerializer = null;
        }

        if (loadoutSerializer == null)
        {
            try
            {
                File.Delete(_loadoutSaveFilePath);
            }
            catch
            {

            }
            return null;
        }

        return loadoutSerializer.Unbox();
    }

    public static void SaveLoadoutLocally(Loadout loadout)
    {
        var serializedLoadout = UnityEngine.JsonUtility.ToJson(loadout.Box());
        Directory.CreateDirectory(Path.GetDirectoryName(_loadoutSaveFilePath));
        File.WriteAllText(_loadoutSaveFilePath, serializedLoadout);

        Logger.Msg("Loadout saved locally");
    }

    private delegate bool GetLoadoutPatchDel(Il2CppSystem.Action<GetLoadoutRequestResult> callback, ref GetLoadoutRequestResult __result);
}
