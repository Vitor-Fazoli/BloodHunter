using System;
using System.Linq;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using BloodHunter.Content.Itens.Materials;

namespace BloodHunter.Common.GlobalNPCs
{
    public class LootNPCs : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // 0.5% chance to drop Adamantite Fragment of any tipe of slime
            if (npc.aiStyle == NPCAIStyleID.Slime)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.TimeDay.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<AdamantiteFragment>(), chanceDenominator: 50));
            }
        }
    }
}
