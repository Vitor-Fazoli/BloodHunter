using Terraria.ModLoader;
using Terraria;

namespace BloodHunter.Content.Buffs
{
    public class CursedSword : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override bool RightClick(int buffIndex)
        {
            return false;
        }
    }
}
