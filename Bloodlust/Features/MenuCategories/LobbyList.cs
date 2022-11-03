using Bloodlust.Deobfuscation;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using Bloodlust.Utils;
using HarmonyLib;
using System;
using Ui.Screens.CustomGame;

namespace Bloodlust.Features.MenuCategories;

[HarmonyPatch]
public static class LobbyList
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _bypassPasswordsToggle;

    public static void Initialize()
    {
        _bypassPasswordsToggle = new ToggleElement("Bypass Passwords");

        _category = BloodlustMenu.Category.Create("Lobby List", new()
        {
            _bypassPasswordsToggle
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);

        HarmonyUtils.PatchObfuscated(typeof(CustomGameScreen), BloodyCustomGameScreen.OnJoinAttemptMethod, new(new Func<CustomGameScreen, bool>(BypassPassword).Method));
    }

    private static bool BypassPassword(CustomGameScreen __instance)
    {
        if (!_bypassPasswordsToggle.On)
            return true;

        Logger.Msg("Bypassing Lobby Password");
        AppControllerUtils.JoinLobby(__instance.GetSelectedLobby());

        return false;
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.MENU;
    }
}
