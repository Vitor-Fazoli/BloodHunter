using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class Silver : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = 0;
        }
    }
}
