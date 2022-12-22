using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using Bloodlust.Utils;
using HarmonyLib;
using System;

namespace Bloodlust.Features.MenuCategories;

public static class LobbyMaster
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _antiKick;
    //private static ToggleElement _syncMyEmotes;

    public static void Initialize()
    {
        _antiKick = new("Anti-Kick");
        //_syncMyEmotes = new("Sync My Emotes");

        var forceStartButton = new ButtonElement("Force Start", ForceStart);

        _category = BloodlustMenu.Category.Create("Lobby Master", new()
        {
            _antiKick,
            //_syncMyEmotes,
            forceStartButton
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);

        HarmonyUtils.PatchObfuscated(typeof(LobbyPlayer), LobbyPlayerUtils.OnKickMessageMethod, new(new Func<bool>(OnPlayerKickPatch).Method));
    }

    private static bool OnPlayerKickPatch()
    {
        return !_antiKick.On;
    }

    private static void ForceStart()
    {
        BloodyHoloNetMessenger.SendMessage(new StartMatchMessage());
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.LOBBY;
    }

    //[HarmonyPatch(typeof(LobbyPlayer), LobbyPlayerUtils.OnEmotionMessageMethod)]
    //[HarmonyPrefix]
    //private static void OnEmotionPatch([HarmonyArgument(0)] LobbyPlayerDoEmotionMessage message, LobbyPlayer __instance)
    //{
    //    if (!_syncMyEmotes.On || !__instance.IsLocal())
    //        return;

    //    var emotionID = message.GetEmotionID();
    //    foreach (var player in BloodyPlayerController.GetAllLobbyPlayers())
    //    {
    //        if (player.IsLocal())
    //            continue;

    //        player.GetNetObject().SendMessage(Messages.CreateLobbyPlayerDoEmotionMessage(emotionID));
    //    }
    //}
}
