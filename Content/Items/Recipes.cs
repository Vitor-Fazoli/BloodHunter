using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BloodHunter.Content.Items
{
    public class Recipes : ModSystem
    {
        public override void AddRecipes()
        {
            //todo: Add recipes for Suspicious Potion
            ////////////////////////////////////////////////////////////////////////////////////
            //                         Adamantite Fragment Recipes                            //
            ////////////////////////////////////////////////////////////////////////////////////

            Recipe recipe = Recipe.Create(ItemID.AdamantiteBar, 1);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.AdamantiteFragment>(), 20);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();

            ////////////////////////////////////////////////////////////////////////////////////
            //                                   TEMPLATE                                     //
            ////////////////////////////////////////////////////////////////////////////////////


        }
    }
}