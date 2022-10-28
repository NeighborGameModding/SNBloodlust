using GameModes.Shared.Models.Customization;
using HarmonyLib;
using Ui.Screens.Preload;

namespace Bloodlust.Features.General;

[HarmonyPatch]
internal static class UnlockAllItems
{
    private static bool _initialized;

    [HarmonyPatch(typeof(PreloadScreen), BloodyPreloadScreen.OnLoadMethod)]
    [HarmonyPrefix]
    private static void Initialize()
    {
        if (_initialized)
            return;

        var items = UnityEngine.Resources.FindObjectsOfTypeAll<CustomizationItemInfo>();
        Logger.Msg("Unlocking all items: " + items.Count.ToString());
        foreach (var item in items)
        {
            item.isDefaultValue = true;
        }

        var emotions = UnityEngine.Resources.FindObjectsOfTypeAll<CustomizationEmotionsInfo>();
        Logger.Msg("Unlocking all emotions: " + emotions.Count.ToString());
        foreach (var emotion in emotions)
        {
            emotion.isDefaultValue = true;
            emotion.allowedActorType = ActorType.ANY;
        }

        _initialized = true;
    }
}
