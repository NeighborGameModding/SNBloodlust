global using static MelonLoader.Melon<Bloodlust.BloodlustMod>;
using Bloodlust.Features.General;
using Bloodlust.Features.MenuCategories;
using Bloodlust.Menu;
using MelonLoader;

namespace Bloodlust;

public class BloodlustMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        AntiPlayfab.Initialize();

        BloodlustMenu.Initialize();

        LobbyMaster.Initialize();
        Identity.Initialize();
        LobbyList.Initialize();

        GameController.Initialize();
        Buffs.Initialize();
        ItemExploits.Initialize();
        VisualAdvantages.Initialize();
    }
}
