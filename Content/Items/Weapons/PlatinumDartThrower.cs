using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

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
            Item.knockBack = 0.1f;
            Item.noMelee = true;

            // Dart Thrower Properties
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 10.8f;
            Item.useAmmo = AmmoID.Arrow;
        }
    }
}
