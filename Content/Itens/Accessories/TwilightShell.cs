using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BloodHunter.Content.Itens.Accessories
{
    public class TwilightShell : ModItem
    {
        public override void SetDefaults()
        {
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>();

            if (p.blood != 0)
            {
                p.
            }

        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return Main.LocalPlayer.GetModPlayer<Common.Players.BloodHunter>().bloodHunter;
        }
    }
}
