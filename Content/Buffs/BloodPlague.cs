using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class BloodPlague : ModBuff
    {

        public int xp = 0;
        public int xpMax = 50;
        public override bool RightClick(int buffIndex)
        {
            return false;
        }
        public override LocalizedText Description => base.Description.WithFormatArgs(xp,xpMax);

        public override void Update(Player player, ref int buffIndex)
        {
            var p  = player.GetModPlayer<Common.Players.BloodHunter>();

            xp = p.xp;
            xpMax = p.xpMax;
        }
    }
}
