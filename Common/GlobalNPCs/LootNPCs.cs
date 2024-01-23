using BloodHunter.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodHunter.Common.GlobalNPCs
{
    public class LootNPCs : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.aiStyle == NPCAIStyleID.Slime)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.TimeDay.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<AdamantiteFragment>(), chanceDenominator: 100));
            }
        }
    }
}
