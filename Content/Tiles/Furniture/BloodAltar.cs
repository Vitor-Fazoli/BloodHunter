using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace BloodHunter.Content.Tiles.Furniture
{
    public class BloodAltar : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            DustType = DustID.Stone;

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("ItemName.GrandfatherClock"));
        }
        public override bool RightClick(int x, int y)
        {
            var p = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            //Aparecer tela para escolher especialização

            p.transforming = true;
            return true;
        }
    }
}