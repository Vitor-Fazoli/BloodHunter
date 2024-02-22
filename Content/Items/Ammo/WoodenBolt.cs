using BloodHunter.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Ammo
{
    public class WoodenBolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }
        public override void SetDefaults()
        {
            Item.width = 5;
            Item.height = 11;
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType < Projectiles.WoodenBolt>();
            Item.ammo = Item.type;
        }

        public override void AddRecipes()
        {
            CreateRecipe(999)
                .AddIngredient(ItemID.Wood, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
