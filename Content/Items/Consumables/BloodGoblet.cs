using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BloodHunter.Content.Items.Consumables
{
    public class BloodGoblet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 1;
            Item.consumable = true;
            Item.sellPrice(silver: 50);
        }

        public override bool CanUseItem(Player player)
        {
            Common.Players.BloodHunter p = player.GetModPlayer<Common.Players.BloodHunter>();

            return p.bloodGoblet < p.MAX_BLOOD_GOBLET && p.bloodHunter;
        }

        public override bool? UseItem(Player player)
        {
            Common.Players.BloodHunter p = player.GetModPlayer<Common.Players.BloodHunter>();


            p.bloodMax += 10;
            p.bloodGoblet++;

            PopupText.NewText(new AdvancedPopupRequest
            {
                Text = "Your maximum blood has been increased by 10",
                Color = Color.Purple
            }, player.position + new Vector2(0, -20));
            return true;
        }
    }
}
