﻿global using static MelonLoader.Melon<Bloodlust.BloodlustMod>;
using Bloodlust.Features.MenuCategories;
using Bloodlust.Menu;
using MelonLoader;

namespace Bloodlust;

public class BloodlustMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        BloodlustMenu.Initialize();

        LobbyMaster.Initialize();
        LoadoutModifiers.Initialize();
        LobbyList.Initialize();

        MovementModifiers.Initialize();
        SkillModifiers.Initialize();
        ItemExploits.Initialize();
        VisualAdvantages.Initialize();
    }
}
