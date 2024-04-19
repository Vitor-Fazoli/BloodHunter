using BloodHunter.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class Hemorrhage : ModBuff
    {
        public bool hemorrhage;
        public int hemorrhageStack;
        public bool explosion;

        public override bool RightClick(int buffIndex) => false;

        public override void Update(Player player, ref int buffIndex)
        {
            hemorrhage = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (buffIndex == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    float speed = 35f;
                    float angle = MathHelper.ToRadians(45 * i);

                    float x = speed * (float)Math.Cos(angle);
                    float y = speed * (float)Math.Sin(angle);

                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), npc.Center, new Vector2(x, y), ModContent.ProjectileType<BloodStake>(), hemorrhageStack, 0f, Main.myPlayer, 0f, 0f);
                }

                npc.DelBuff(ModContent.BuffType<Hemorrhage>());
            }

            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
              
            npc.lifeRegenCount = 0;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = $"Stack: {hemorrhageStack}";
        }
    }
}