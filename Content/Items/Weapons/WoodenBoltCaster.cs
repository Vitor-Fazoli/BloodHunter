﻿using BloodHunter.Common.Systems;
using BloodHunter.Content.Items.Ammo;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Weapons
{
    public class WoodenBoltCaster : BoltCaster
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
            Item.damage = 3;
            Item.knockBack = 0.1f;
            Item.crit = 21;
            Item.noMelee = true;

            // BoltCaster Properties
            Item.shoot = ModContent.ProjectileType<Projectiles.WoodenBolt>();
            Item.useAmmo = ModContent.ItemType<WoodenBolt>();
            Item.shootSpeed = 13f;
        }
    }
}
