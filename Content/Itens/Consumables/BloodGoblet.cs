using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Itens.Consumables
{
    public class BloodGoblet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ManaCrystal);
        }

        public override bool CanUseItem(Player player)
        {
             Common.Players.BloodHunter p = player.GetModPlayer<Common.Players.BloodHunter>();

            return p.bloodGoblet < p.max_blood_goblet;
        }

        public override bool? UseItem(Player player)
        {
            Common.Players.BloodHunter p = player.GetModPlayer<Common.Players.BloodHunter>();

            if (p.bloodGoblet < p.max_blood_goblet)
            {
                return null;
            }

            p.bloodMax += p.quantity_blood_per_goblet;
            p.bloodGoblet++;

            return true;
        }

        public override void AddRecipes()
        {
            //CreateRecipe()
              //  .AddIngredient<ExampleItem>()
                //.AddTile<Tiles.Furniture.ExampleWorkbench>()
                //.Register();
        }
    }
}
