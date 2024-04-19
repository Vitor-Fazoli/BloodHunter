using log4net.Core;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodHunter.Common.Players.Specs
{
    public class TwilightMage : ModPlayer
    {
        public int twilightMageBonus;
        public int _twilightMageBonus = 0;

        public override void Initialize()
        {
            twilightMageBonus = _twilightMageBonus;
        }
        public override void ResetEffects()
        {
            twilightMageBonus = _twilightMageBonus;
        }
        public override void PostUpdateMiscEffects()
        {
            var v = Player.GetModPlayer<BloodHunter>();
            if (IsTwilightMage())
            {
                if (v.canGetBlood)
                {
                    v.bloodCurrent += 1 + twilightMageBonus;

                    v.canGetBlood = false;
                }
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            var v = Player.GetModPlayer<BloodHunter>();

            if (IsTwilightMage())
            {
                if (v.bloodCurrent >= manaConsumed / 2)
                {
                    v.bloodCurrent -= manaConsumed / 2;
                    Player.statMana += manaConsumed;
                }
            }
        }

        private bool IsTwilightMage() => Player.GetModPlayer<BloodHunter>().isBloodHunter && Player.GetModPlayer<BloodHunter>().specialization == Specialization.TwilightMage;
    }
}

