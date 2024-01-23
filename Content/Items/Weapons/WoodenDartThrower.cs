using BloodHunter.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Weapons
{
    public class WoodenDartThrower : DartThrower
    {
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
            Item.damage = 2;
            Item.knockBack = 0.1f;
            Item.noMelee = true;

            // Dart Thrower Properties
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Arrow;
        }
    }
}
