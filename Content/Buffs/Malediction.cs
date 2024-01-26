using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class Malediction : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override bool RightClick(int buffIndex)
        {
            return true;
        }
    }
}
