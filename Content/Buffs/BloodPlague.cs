using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class BloodPlague : ModBuff
    {
        public override bool RightClick(int buffIndex)
        {
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            var p = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            tip = $"level: {p.level}\n {p.xp} / {p.xpMax} \n Damage Bonus: {p.level}%";
        }
    }
}
