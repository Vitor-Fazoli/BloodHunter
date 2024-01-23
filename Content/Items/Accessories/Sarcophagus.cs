using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Items.Accessories
{

    public class Sarcophagus : ModItem
    {
        public override void SetDefaults()
        {
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = player.GetModPlayer<Common.Players.BloodHunter>();

            p.bloodMax2 += 50;

            //Ranger
            if (p.isItRanger)
            {
                player.statDefense += p.bloodMax2 / 10;
            }
            // Magic
            else
            {
                player.statLifeMax2 -= 20;
                p.getBloodRate -= 120;
            }
        }
    }
}
