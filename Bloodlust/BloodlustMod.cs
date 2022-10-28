global using static MelonLoader.Melon<Bloodlust.BloodlustMod>;

using Bloodlust.Menu;
using MelonLoader;

namespace Bloodlust;

public class BloodlustMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        BloodlustMenu.Initialize();
    }
}
