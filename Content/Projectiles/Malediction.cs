﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Projectiles
{
    public class Malediction : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.tileCollide = false;

            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 0;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
            {
                return;
            }

            DamageAndAreaScale();
            Movement();
            Visuals();
        }
       
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<Buffs.Malediction>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<Buffs.Malediction>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }
        private void DamageAndAreaScale()
        {
            var v = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();
            Projectile.damage = (int)(v.bloodMax2 / 16);
            Projectile.width = 100 + (int)(v.bloodMax2 / 2);
            Projectile.height = 100 + (int)(v.bloodMax2 / 2);
        }
        private void Movement()
        {
            Projectile.Center = Main.player[Projectile.owner].Center;
            Projectile.rotation += 0.01f;
        }

        private void Visuals()
        {
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.55f);
        }
    }
}
