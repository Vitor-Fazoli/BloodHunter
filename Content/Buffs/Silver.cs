using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BloodHunter.Content.Buffs
{
    public class Silver : ModBuff
    {
        public override bool RightClick(int buffIndex) => false;
    }

    public class SilverPlayer : ModPlayer
    {
        public bool Silver;

        public override void ResetEffects()
        {
            Silver = false;
        }
        public override void UpdateDead()
        {
            Silver = false;
        }
        public override void UpdateBadLifeRegen()
        {
            if (Silver)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
            }
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Silver)
            {
                if (Main.rand.Next(10) < 1)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Torch, 0, -1.86f, 100, Color.White, 1.2f);
                    Main.dust[dust].noGravity = true;
                }
            }
        }
    }
}