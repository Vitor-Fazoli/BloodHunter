using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Materials
{
    public class BlessedOil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 40;
            Item.rare = ItemRarityID.White;
            Item.material = true;
        }
    }
}
