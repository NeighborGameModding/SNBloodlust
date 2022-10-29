global using static MelonLoader.Melon<Bloodlust.BloodlustMod>;
using Bloodlust.Features.MenuCategories;
using Bloodlust.Menu;
using MelonLoader;

namespace Bloodlust;

public class BloodlustMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        BloodlustMenu.Initialize();

        LobbyList.Initialize();

        MovementModifiers.Initialize();
    }
}
