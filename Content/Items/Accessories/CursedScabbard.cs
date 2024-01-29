using BloodHunter.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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

            if (p.isBloodHunter)
            {
                if (p.bloodCurrent >= p.bloodMax2)
                {
                    player.lifeRegenTime = 0;
                    player.AddBuff(ModContent.BuffType<Buffs.CursedSword>(), p.getBloodRate * 5);

                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        Dust d = Dust.NewDustPerfect(player.position + new Vector2(0, -30), DustID.ManaRegeneration, speed * 5, Scale: 1.5f);
                        d.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Item44, player.position + new Vector2(0, -30));
                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), player.position + new Vector2(0, -30), Vector2.Zero, ModContent.ProjectileType<CursedSword>(), p.bloodMax2 / 2, 5);

                    p.bloodCurrent = 0;
                }
            }
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return player.GetModPlayer<Common.Players.BloodHunter>().isBloodHunter;
        }
    }
}
