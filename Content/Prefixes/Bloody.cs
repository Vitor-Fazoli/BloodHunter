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

        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 5f;
        }

        public override bool CanRoll(Item item)
        {
            return Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>().bloodHunter;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f * Power;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<Common.Players.BloodHunter>().bloodMax2 += 10;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {

            yield return new TooltipLine(Mod, "PrefixWeaponAwesome", BloodTooltip.Format(Power))
            {
                IsModifier = true,
            };
        }

        public static LocalizedText BloodTooltip
        {
            get; private set;
        }
        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override void SetStaticDefaults()
        {
            BloodTooltip = Mod.GetLocalization($"{LocalizationCategory}.{nameof(BloodTooltip)}");
            _ = AdditionalTooltip;
        }
    }
}
