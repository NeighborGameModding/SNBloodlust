using Bloodlust.Features.General;
using Bloodlust.Menu;
using HarmonyLib;
using HoloNetwork.NetworkObjects;
using MelonLoader;

namespace Bloodlust;

public class BloodlustMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        AntiPlayfab.Initialize();
        BloodlustMenu.Initialize();
    }
}
