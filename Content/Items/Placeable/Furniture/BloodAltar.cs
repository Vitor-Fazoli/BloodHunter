using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Placeable.Furniture
{
    public class BloodAltar : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BloodAltar>());
            Item.width = 26;
            Item.height = 22;
            Item.value = 500;
        }
        public override void OnCreated(ItemCreationContext context)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Blood, speed * 5, Scale: 1.5f);
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.DD2_BetsyScream, Main.LocalPlayer.Center);
            Main.NewText("You feel your blood leaving your body", Color.Red);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 200)
                .AddIngredient(ItemID.SuspiciousLookingEye, 1)
                .AddIngredient(ItemID.IronShortsword)
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 200)
                .AddIngredient(ItemID.SuspiciousLookingEye, 1)
                .AddIngredient(ItemID.LeadShortsword)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}