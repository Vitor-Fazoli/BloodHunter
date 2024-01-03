using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace BloodHunter.Common.Systems
{
    public class KeybindSystem : ModSystem
    {
        //First Ability

        public static ModKeybind RandomBuffKeybind
        {
            get; private set;
        }

        public static ModKeybind FirstAbility
        {
            get; private set;
        }

        public static ModKeybind SecondAbility
        {
            get; private set;
        }

        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to English users is in en-US.hjson
            RandomBuffKeybind = KeybindLoader.RegisterKeybind(Mod, "RandomBuff", "P");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            RandomBuffKeybind = null;
        }

        //Second Ability
    }
}
