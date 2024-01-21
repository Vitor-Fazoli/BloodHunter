using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace BloodHunter.Common.Systems
{
    public abstract class DartThrower : ModItem
    {
        public int arrowSpread = 10;

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(arrowSpread));
        }
    }
}
