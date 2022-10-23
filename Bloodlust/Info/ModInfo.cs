using Bloodlust.Info;
using MelonLoader;
using System;

[assembly: MelonInfo(typeof(Bloodlust.BloodlustMod), ModInfo.Name, ModInfo.Version, ModInfo.Author, ModInfo.GitLink)]
[assembly: MelonGame("Hologryph")]
[assembly: MelonColor(ConsoleColor.Red)]
[assembly: MelonAuthorColor(ConsoleColor.DarkRed)]

namespace Bloodlust.Info;

public static class ModInfo
{
    public const string Version = "1.0.0";
    public const string Name = "Bloodlust";
    public const string Author = "SN Modding";
    public const string GitLink = "https://github.com/NeighborGameModding/SNBloodlust";
}
