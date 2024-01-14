using BloodHunter.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Accessories
{
    public class CursedScabbard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = player.GetModPlayer<Common.Players.BloodHunter>();

            if (p.bloodHunter)
            {
                if (p.bloodCurrent >= p.bloodMax2)
                {
                    player.AddBuff(ModContent.BuffType<Buffs.CursedSword>(), p.getBloodRate * 10);
                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), player.position + new Vector2(0, -30), Vector2.Zero, ModContent.ProjectileType<CursedSword>(), p.bloodMax2 / 2, 5);
                    p.bloodCurrent = 0;
                }
            }
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return player.GetModPlayer<Common.Players.BloodHunter>().bloodHunter;
        }
    }
}
