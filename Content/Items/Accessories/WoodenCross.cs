using BloodHunter.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Accessories
{

    public class WoodenCross : ModItem
    {
        public override void SetDefaults()
        {
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = player.GetModPlayer<Common.Players.BloodHunter>();

            p.bloodMax2 -= 20;
        }
    }
}
