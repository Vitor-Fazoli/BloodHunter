using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Accessories
{
    public class SacrificialDagger : ModItem
    {
        private int timer = 0;
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 32;
            Item.height = 32;
            Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Common.Players.BloodHunter p = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            timer++;
            if (!p.IsBloodFull())
            {
                if (timer > 1200)
                {
                    int amount = player.statLife / 4;
                    p.ReceiveBlood(player, 5);
                    p.UpdateBadLifeRegen();
                    player.Hurt(PlayerDeathReason.LegacyDefault(), amount, 0, false);

                    timer = 0;
                }
            }
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>().isBloodHunter;
        }
    }
}
