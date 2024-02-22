using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Projectiles
{
    public class WoodenBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 11;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                int dust = Dust.NewDust(Projectile.Center, Projectile.width / 2, Projectile.height / 2, DustID.WoodFurniture, 0, 0, 100, Scale: 1.5f);
                Main.dust[dust].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
