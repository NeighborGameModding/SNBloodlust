using Bloodlust.Deobfuscation;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.GameplayMode.Cameras;
using MelonLoader;
using UnityEngine;

namespace Bloodlust.Features.MenuCategories;

internal class MovementModifiers
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _noclipToggle;

    public static void Initialize()
    {
        _noclipToggle = new("Noclip", ToggleNoclip, KeyCode.N);
        _category = BloodlustMenu.Category.Create("Movement Modifiers", new()
        {
            _noclipToggle
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
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
        _category.Enabled = gameMode == GameMode.GAMEPLAY;
    }
}
