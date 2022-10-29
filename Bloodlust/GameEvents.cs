using HarmonyLib;
using MelonLoader;

namespace Bloodlust;

[HarmonyPatch]
public static class GameEvents
{
    public static GameMode CurrentGameMode { get; set; }

    public static readonly MelonEvent<GameMode> OnGameModeChanged = new();

    [HarmonyPatch(typeof(GameModeController), AppControllerUtils.SetGameModeMethod)]
    [HarmonyPostfix]
    private static void OnGameModeSet([HarmonyArgument(0)] GameMode newGameMode)
    {
        if (newGameMode == CurrentGameMode)
            return;

        CurrentGameMode = newGameMode;
        OnGameModeChanged.Invoke(newGameMode);
    }
}
