using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Materials
{
    public class AdamantiteFragment : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Lime;
            Item.maxStack = 99;
            Item.material = true;
        }
    }
}
