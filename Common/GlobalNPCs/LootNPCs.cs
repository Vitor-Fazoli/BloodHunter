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
            if (npc.type == NPCID.EyeofCthulhu)
            {
                foreach (var rule in npcLoot.Get())
                {
                    if (rule is DropBasedOnExpertMode dropBasedOnExpertMode && dropBasedOnExpertMode.ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.BeeGun))
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        //original.Add(ModContent.ItemType<Cont.>());
                        oneFromOptionsDrop.dropIds = [.. original];
                    }
                }
            }

            if (npc.type == NPCID.BloodNautilus)
            {

            }

            if (npc.type == NPCID.WallofFlesh)
            {
                
            }

            if (npc.type == NPCID.Retinazer)
            {
            
            }

            if (npc.type == NPCID.Spazmatism)
            {
                
            }

            if (npc.type == NPCID.CultistBoss)
            {
            
            }

            if (npc.type == NPCID.DukeFishron)
            {
            
            }

            // 0.5% chance to drop Adamantite Fragment of any tipe of slime
            if (npc.aiStyle == NPCAIStyleID.Slime)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.TimeDay.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<AdamantiteFragment>(), chanceDenominator: 20));
            }
        }
    }
}
