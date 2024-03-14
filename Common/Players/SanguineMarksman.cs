using BloodHunter.Content.Buffs;
using BloodHunter.Content.Items;
using log4net.Core;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodHunter.Common.Players
{
    public class SanguineMarksman : ModPlayer
    {
        public int essence;
        private const int _essence = 5;
        public int essenceMax = 20;

        public override void Initialize()
        {
            essence = _essence;
        }
        public override void ResetEffects()
        {
            essence = _essence;
        }
        public override void UpdateDead()
        {
            essence = _essence;
        }
        public override void PostUpdateMiscEffects()
        {
            var v = Player.GetModPlayer<BloodHunter>();
            if (IsSanguineMarksman())
            {
                if (v.bloodCurrent >= v.bloodMax2)
                {
                    Player.AddBuff(ModContent.BuffType<Malediction>(), 300 + (v.GET_BLOOD_RATE_MAX - v.getBloodRate));
                    if(Main.projectile.Any(p => p.active && p.type == ModContent.ProjectileType<Content.Projectiles.Malediction>()))
                    {
                        return;
                    }
                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Player.position + new Vector2(0, -30), Vector2.Zero, ModContent.ProjectileType<Content.Projectiles.Malediction>(), v.bloodMax2 / 10, 0);
                    v.bloodCurrent = 0;
                }

                if (essence >= essenceMax)
                {
                    essence = essenceMax;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var v = Player.GetModPlayer<BloodHunter>();

            if (IsSanguineMarksman())
            {
                if (v.canGetBlood)
                {
                    if (target.type != NPCID.TargetDummy)
                    {
                        Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                           0, -2), ModContent.ItemType<LifeEssence>(), 1);

                        v.canGetBlood = false;
                    }
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            var v = Player.GetModPlayer<BloodHunter>();

            if (IsSanguineMarksman())
            {
                if (v.canGetBlood)
                {
                    if (target.type != NPCID.TargetDummy)
                    {
                        Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                           0, -2), ModContent.ItemType<LifeEssence>(), 1);

                        v.canGetBlood = false;
                    }
                }
            }
        }
        private bool IsSanguineMarksman()
        {
            var v = Player.GetModPlayer<BloodHunter>();

            return v.isBloodHunter is true && v.specialization == Specialization.SanguineMarksman;
        }
    }
}
