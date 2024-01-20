using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace BloodHunter.Content.Rarities
{
    internal class TwilightRed : ModRarity
    {
        public override Color RarityColor => new(Main.DiscoR, 0, (byte)(Main.DiscoB / 2f), Main.DiscoB * 1.5f);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            return Type;
        }
    }
}
