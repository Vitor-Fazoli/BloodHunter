using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Itens.Accessories
{
    /// <summary>
    /// 
    /// </summary>
    public class Sarcophagus : ModItem
    {
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = player.GetModPlayer<Common.Players.BloodHunter>();

            p.bloodMax2 += 100;
            player.statDefense += 5;

            //Ranger
            if (p.isItRanger)
            {

            }
            // Magic
            else
            {
                
            }
        }
    }
}
