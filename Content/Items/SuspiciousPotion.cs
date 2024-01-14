using BloodHunter.Content.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items
{
    public class SuspiciousPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, ModContent.BuffType<BloodPlague>(), 1);
            Item.value = Item.buyPrice(gold: 3);
            Item.consumable = true;
        }
        public override void OnConsumeItem(Player player)
        {
            Common.Players.BloodHunter p = player.GetModPlayer<Common.Players.BloodHunter>();

            player.AddBuff(ModContent.BuffType<BloodPlague>(), 1);
            p.eyeColor = player.eyeColor;
            p.bloodHunter = true;
        }
    }
}
