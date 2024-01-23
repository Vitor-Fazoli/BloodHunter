using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Weapons
{
    public class PlatinumDartThrower : ModItem
    {
        //todo: Create a abstract class to inherit from for all Dart Throwers
        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 62;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;

            // Use Properties
            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.reuseDelay = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 8;
            Item.knockBack = 0.5f;
            Item.noMelee = true;

            // Dart Thrower Properties
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
        }
    }
}
