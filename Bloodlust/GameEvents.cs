using HarmonyLib;
using MelonLoader;
using System;

namespace Bloodlust;

public static class GameEvents
{
    public static GameMode CurrentGameMode { get; set; }

    public static readonly MelonEvent<GameMode> OnGameModeChanged = new();

    public static void Initialize()
    {
        Instance.HarmonyInstance.Patch(typeof(GameModeController).GetMethod(AppControllerUtils.SetGameModeMethod), postfix: new(new Action<GameMode>(OnGameModeSet).Method));
    }

    private static void OnGameModeSet([HarmonyArgument(0)] GameMode newGameMode)
    {
        if (newGameMode == CurrentGameMode)
            return;

        CurrentGameMode = newGameMode;
        OnGameModeChanged.Invoke(newGameMode);
    }
}
