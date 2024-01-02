﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class Night : ModBuff
    {
        public override bool RightClick(int buffIndex) => false;

        public override void Update(Player Player, ref int buffIndex)
        {
            Player.GetModPlayer<NightPlayer>().night = true;
        }
    }
    public class NightPlayer : ModPlayer
    {
        public bool night;

        public override void ResetEffects()
        {
            night = false;
        }
        public override void UpdateDead()
        {
            night = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (night)
            {
                Player.moveSpeed += 0.3f;

                Player.GetDamage(DamageClass.Generic) += 0.2f;

                Player.nightVision = true;
            }
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            Lighting.AddLight(Player.position, 0.5f, 0.5f, 0.5f);
        }
    }
}