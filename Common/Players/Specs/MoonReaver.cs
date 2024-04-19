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

namespace BloodHunter.Common.Players.Specs
{
    /// <summary>
    /// a specialization of blood hunter for melee damage, this spec is focused on life steal and receive damage
    /// </summary>
    public class MoonReaver : ModPlayer
    {
        /// <summary>
        /// a bool to check if the player can causes hemorrhage in your target
        /// </summary>
        bool canHitHemorrhage = false;

        /// <summary>
        /// When do you Hurt the player, the player will get blood
        /// </summary>
        /// <param name="info">This is a data about the hit</param>
        public override void OnHurt(Player.HurtInfo info)
        {
            var player = Player.GetModPlayer<BloodHunter>();

            if (IsMoonReaver() && player.canGetBlood)
            {
                player.bloodCurrent += info.Damage / 2; 
            }
        }

        public override void PostUpdateMiscEffects()
        {
            var player = Player.GetModPlayer<BloodHunter>();

            if (IsMoonReaver() && player.IsBloodFull())
            {
                player.ZeroBlood();
                canHitHemorrhage = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (canHitHemorrhage && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(ModContent.BuffType<Hemorrhage>(), 300);
                canHitHemorrhage = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMoonReaver() => Player.GetModPlayer<BloodHunter>().isBloodHunter && Player.GetModPlayer<BloodHunter>().specialization == Specialization.MoonReaver;
    }
}
