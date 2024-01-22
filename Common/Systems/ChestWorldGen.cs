using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using BloodHunter.Content.Items;

namespace BloodHunter.Common.Systems
{
    public class ChestWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {
            // Chest to spawn suspicious potion - Frozen Chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }

                Tile chestTile = Main.tile[chest.x, chest.y];

                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 11 * 36)
                {
                    if (WorldGen.genRand.NextBool(3))
                        continue;

                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<SuspiciousPotion>());
                            return;
                        }
                    }
                }
            }
        }
    }
}
