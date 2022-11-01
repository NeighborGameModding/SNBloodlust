# Bloodlust
![Bloodlust](Media/Bloodlust.png)

# Installation Guide
1. Use the [SNBypass Installation Guide](https://github.com/NeighborGameModding/SNBypass/blob/main/README.md#how-to-properly-install-melonloader-onto-sn) to propely install MelonLoader and SNBypass (2 required dependencies)
2. [Download the latest version of Bloodlust](https://github.com/NeighborGameModding/SNBloodlust/releases/latest/download/SNBloodlust.dll)
3. Go to your game's root folder and open the Mods folder
4. Place the Bloodlust.dll from step 2 in the Mods folder
5. Run the game

## Need help?
You can ask for support in our [Discord Server](https://discord.gg/6G4u3GmkrJ)

# Uninstallation Guide
Refer to the [Uninstallation Guide from SNBypass](https://github.com/NeighborGameModding/SNBypass/blob/main/README.md#how-to-properly-remove-melonloader-from-sn)

# State
The mod is still in a very early stage of development and may contain bugs or missing features.

# Suggest Your Ideas
You're always welcome to suggest your own ideas! You can do so either in our [Discord Server](https://discord.gg/2gm2ajZ48y), or if you have a Github account, you can create an issue in the [Issues Tab](https://github.com/NeighborGameModding/SNBloodlust/issues).
<br>Before making any suggestions, [make sure that your idea hasn't been suggested before](https://github.com/orgs/NeighborGameModding/projects/1/views/1)!</br>

# Contribution
Do you have any experience with C# and Unity modding? You can always contribute by forking the repository and creating your own pull requests! **Any help is appreciated!**
<br>Before contributing, make sure to read our project structure rules to keep the project maintainable and easy to understand!</br>

1. Do not use obfuscated names outside of [Deobfuscator.cs](Bloodlust/Deobfuscation/Deobfuscator.cs). To rename obfuscated types, use global usings. To use obfuscated instance methods/properties/fields, create extension methods in the DeobfuscatorExtensions class. To use obfuscated static methods/properties/fields, use/create a nested class in the StaticDeobfuscator class and create a static wrapper method inside of it.
2. Ensure that your code is fast. Try not to use loops too often and look for ways around using Harmony Patches.
3. Keep the amount of object allocations to a minimum. Creating too many objects in a short period of time might cause lag spikes due to garbage collection.
4. All referenced assemblies must be added to the [References Folder](Bloodlust/References)