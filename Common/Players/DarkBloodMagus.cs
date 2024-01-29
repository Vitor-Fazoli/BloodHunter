using log4net.Core;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodHunter.Common.Players
{
    public class DarkBloodMagus : ModPlayer
    {
        public int darkBloodBonus;
        public int _darkBloodBonus = 0;

        public override void Initialize()
        {
            darkBloodBonus = _darkBloodBonus;
        }
        public override void ResetEffects()
        {
            darkBloodBonus = _darkBloodBonus;
        }
        public override void PostUpdateMiscEffects()
        {
            var v = Player.GetModPlayer<BloodHunter>();
            if (IsDarkBloodMagus())
            {
                if (v.canGetBlood)
                {
                    v.bloodCurrent += (1 + darkBloodBonus);
                }
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            var v = Player.GetModPlayer<BloodHunter>();

            if (IsDarkBloodMagus())
            {
                if (v.bloodCurrent >= manaConsumed / 2)
                {
                    v.bloodCurrent -= manaConsumed / 2;
                    Player.statMana += manaConsumed;
                }
            }
        }

        private bool IsDarkBloodMagus()
        {
            var v = Player.GetModPlayer<BloodHunter>();

            return v.isBloodHunter is true && v.specialization == Specialization.DarkbloodMagus;
        }
    }
}

