using Bloodlust.Deobfuscation;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.GameplayMode.Cameras;
using HarmonyLib;
using MelonLoader;
using System;
using UnityEngine;

namespace Bloodlust.Features.MenuCategories;

[HarmonyPatch]
public static class VisualAdvantages
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _playerESPToggle;

    public static void Initialize()
    {
        _playerESPToggle = new ToggleElement("Player ESP", TogglePlayerESP, UnityEngine.KeyCode.Keypad1);

        _category = BloodlustMenu.Category.Create("Visual Advantages", new()
        {
            _playerESPToggle
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.GAMEPLAY;
    }

    private static void TogglePlayerESP(bool value)
    {
        if (value)
            MelonEvents.OnGUI.Subscribe(DrawPlayerESP);
        else
            MelonEvents.OnGUI.Unsubscribe(DrawPlayerESP);
    }

    private static void DrawPlayerESP()
    {
        var cam = GameCamera.instance?.prop_Camera_0;
        if (cam == null)
            return;

        var players = BloodyPlayerController.GetAllPlayers();
        if (players == null)
            return;

        foreach (var player in players)
        {
            var actor = player.GetCurrentActor();
            var pos = cam.WorldToScreenPoint(actor.transform.position);
            if (pos.z < 0)
                continue;

            pos.y = Screen.height - pos.y;

            var size = new Vector2(100, 20);
            GUI.Box(new(pos.x - size.x / 2, pos.y - size.y / 2, size.x, size.y), "Player");
        }
    }
}
