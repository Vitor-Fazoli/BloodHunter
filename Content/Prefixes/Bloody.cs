using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BloodHunter.Content.Prefixes
{
    /// <summary>
    /// 
    /// </summary>
    public class Bloody : ModPrefix
    {
        public virtual float Power => 1f;

        private const int BLOOD_AMOUNT = 10;

        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 2f;
        }

        public override bool CanRoll(Item item)
        {
            Common.Players.BloodHunter player = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            if (player.bloodHunter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f * Power;
        }

        public override void Apply(Item item)
        {
            Common.Players.BloodHunter player = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();
            player.bloodMax += BLOOD_AMOUNT;
        }
    }
}