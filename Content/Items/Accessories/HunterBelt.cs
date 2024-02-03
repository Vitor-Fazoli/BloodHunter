using BloodHunter.Common.Players;
using BloodHunter.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Accessories
{
    public class HunterBelt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 1;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            var bloodHunter = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            return bloodHunter.specialization == Specialization.SanguineMarksman;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var bloodHunter = player.GetModPlayer<Common.Players.BloodHunter>();
            var sanguineMarksman = player.GetModPlayer<Common.Players.SanguineMarksman>();

            sanguineMarksman.essence += 1;
            bloodHunter.bloodMax2 -= 5;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<BlessedOil>(3)
            .AddIngredient(ItemID.Leather, 5)
            .AddIngredient(ItemID.WoodenArrow, 100)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
