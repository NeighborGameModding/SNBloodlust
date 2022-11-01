﻿using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using HarmonyLib;

namespace Bloodlust.Features.MenuCategories;

[HarmonyPatch]
public static class LobbyMaster
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _antiKick;

    public static void Initialize()
    {
        _antiKick = new("Anti-Kick");
        var forceStartButton = new ButtonElement("Force Start", ForceStart);

        _category = BloodlustMenu.Category.Create("Lobby Master", new()
        {
            _antiKick,
            forceStartButton
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void ForceStart()
    {
        BloodyHoloNetMessenger.SendMessage(new StartMatchMessage());
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.LOBBY;
    }

    [HarmonyPatch(typeof(LobbyPlayer), LobbyPlayerUtils.OnKickMessageMethod)]
    [HarmonyPrefix]
    private static bool OnPlayerKickPatch()
    {
        return !_antiKick.On;
    }
}
