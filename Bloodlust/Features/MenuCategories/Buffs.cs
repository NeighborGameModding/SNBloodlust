using Bloodlust.Deobfuscation;
using Bloodlust.Deobfuscation.Enums;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.GameplayMode.Cameras;
using MelonLoader;
using System.Collections;
using UnityEngine;

namespace Bloodlust.Features.MenuCategories;

public static class Buffs
{
    private static BloodlustMenu.Category _category;

    private static object _desyncFixCoroutine;

    public static void Initialize()
    {
        var _noclipToggle = new ToggleElement("Noclip", ToggleNoclip, KeyCode.N);
        var _godModeToggle = new ToggleElement("God Mode", ToggleGodMode, KeyCode.G);
        var _desyncFixToggle = new ToggleElement("Desync Fix", ToggleDesyncFix, defaultValue: true);

        _category = BloodlustMenu.Category.Create("Buffs", new()
        {
            _godModeToggle,
            _noclipToggle,
            _desyncFixToggle
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void ToggleDesyncFix(bool value)
    {
        if (value)
            _desyncFixCoroutine = MelonCoroutines.Start(DesyncFixCoroutine());
        else if (_desyncFixCoroutine != null)
            MelonCoroutines.Stop(_desyncFixCoroutine);
    }

    private static IEnumerator DesyncFixCoroutine()
    {
        for (; ; )
        {
            yield return new WaitForSecondsRealtime(2f);
            var lp = BloodyPlayerController.GetLocalPlayer();
            var actor = lp.GetCurrentActor();
            if (actor == null)
                break;

            lp.GetNetObject().SendMessage(Messages.CreateTeleportPlayerMessage(actor.transform.position), MessageTarget.Others);
        }
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
