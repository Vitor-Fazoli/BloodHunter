using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Common.Systems
{
    public abstract class BoltCaster : ModItem
    {
        public int arrowSpread = 10;
        public int redCritical = 100;

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(arrowSpread));
        }
    }
}
