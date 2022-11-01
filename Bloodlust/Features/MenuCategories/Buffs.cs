using Bloodlust.Deobfuscation;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.GameplayMode.Cameras;
using MelonLoader;
using UnityEngine;

namespace Bloodlust.Features.MenuCategories;

public static class Buffs
{
    private static BloodlustMenu.Category _category;

    public static void Initialize()
    {
        var _noclipToggle = new ToggleElement("Noclip", ToggleNoclip, KeyCode.N);
        var _godModeToggle = new ToggleElement("God Mode", ToggleGodMode, KeyCode.G);

        _category = BloodlustMenu.Category.Create("Buffs", new()
        {
            _noclipToggle,
            _godModeToggle
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void ToggleGodMode(bool value)
    {
        var lp = BloodyPlayerController.GetLocalPlayer();
        if (lp == null)
            return;

        if (value)
            lp.Buff(PlayerBuff.INVINCIBLE);
        else
            lp.Debuff(PlayerBuff.INVINCIBLE);
    }

    private static void ToggleNoclip(bool value)
    {
        if (BloodyPlayerController.GetLocalPlayer() == null && value)
            return;

        ToggleColliders(!value);

        if (value)
            MelonEvents.OnUpdate.Subscribe(RunNoclip);
        else
            MelonEvents.OnUpdate.Unsubscribe(RunNoclip);
    }

    private static void RunNoclip()
    {
        var lp = BloodyPlayerController.GetLocalPlayer();
        var actor = lp.GetCurrentActor();
        if (actor == null)
            return;

        var cam = GameCamera.instance.transform;

        actor.transform.position += (cam.forward * Input.GetAxisRaw("Vertical") + cam.right * Input.GetAxisRaw("Horizontal")).normalized * 10f * Time.deltaTime;
    }

    private static void ToggleColliders(bool value)
    {
        if (BloodyPlayerController.GetLocalPlayer() == null)
            return;

        foreach (var actor in BloodyPlayerController.GetLocalPlayer().GetActors())
        {
            var collider = actor.GetComponent<Collider>();
            if (collider == null)
                continue;

            collider.enabled = value;
        }
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.GAMEPLAY; // This should be changed to only when the local player is alive
    }
}
