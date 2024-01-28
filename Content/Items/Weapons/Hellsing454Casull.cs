using BloodHunter.Common.Systems;
using BloodHunter.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Weapons
{
    public class Hellsing454Casull : ModItem
    {
        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 52;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
            Items.scale = 0.7f;

            // Use Properties
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 8;
            Item.knockBack = 0.5f;
            Item.noMelee = true;

            // Gun Properties
            Item.shoot = 10;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }

            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.2f, 0.5f);
                Dust d = Dust.NewDustPerfect(position, DustID.ManaRegeneration, speed * 5, Scale: 1.5f);
                d.noGravity = true;
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
