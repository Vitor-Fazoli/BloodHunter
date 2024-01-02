using BloodHunter.Content.Buffs;
using BloodHunter.Content.Itens;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodHunter.Common.Players
{
    /// <summary>
    /// This is an archetype of a new branch for the magic and ranged classes that will change the way they play.
    /// </summary>
    public class BloodHunter : ModPlayer
    {
        public bool sunResistance = false;

        public Color eyeColor;
        public bool bloodHunter = false;
        public int blood = 0;
        public int bloodMax = 100;
        public const int BLOOD_MAX = 2000;

        public int regen = 1;
        public const int REGE_MAX = 5;
        public int regenRate = 210;
        public const int REGEN_MAX = 30; //0.05 seconds
        private int regenTimer = 0;

        public int getBloodRate = 300; // 0.5 second
        public const int GET_BLOOD_RATE_MAX = 60; //0.1 second
        public bool canGetBlood = true;
        private int getBloodTime = 0;

        public bool rangedCanGetBlood = false;

        public bool IsBloodFull()
        {
            return blood == bloodMax;
        }

        #region getting blood
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // For Magic Weapons
            if (bloodHunter && canGetBlood && hit.DamageType == DamageClass.Magic)
            {
                if (target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                       0, -2), ModContent.ItemType<LifeEssence>(), 1);

                    canGetBlood = false;
                }
            }
            // For Ranged Weapons
            else if (bloodHunter && canGetBlood && rangedCanGetBlood)
            {
                if (target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                       0, -2), ModContent.ItemType<LifeEssence>(), 1);

                    canGetBlood = false;
                }
            }
        }
        #endregion

        
        public override void PostUpdate()
        {
            if (bloodHunter)
            {
                getBloodTime++;

                if (getBloodTime >= getBloodRate)
                {
                    canGetBlood = true;
                    getBloodTime = 0;
                }

                if (blood >= bloodMax)
                {
                    blood = bloodMax;
                }

                if (blood <= 0)
                {
                    blood = 0;
                }

                Player.eyeColor = new Color(255, 0, 0);

                Player.AddBuff(ModContent.BuffType<BloodPlague>(), 2);
                bool ZoneSunHeight = Player.ZoneOverworldHeight || Player.ZoneSkyHeight;

                if (Main.dayTime)
                {
                    if (Player.behindBackWall || !ZoneSunHeight)
                    {
                        Player.AddBuff(ModContent.BuffType<Night>(), 2);
                    }
                    else
                    {
                        if (!sunResistance)
                        {
                            Player.AddBuff(ModContent.BuffType<Day>(), 2);
                        }
                    }
                }
                else
                {
                    Player.AddBuff(ModContent.BuffType<Night>(), 2);

                    regenTimer++;

                    if (blood != bloodMax)
                    {
                        if (regenTimer >= regenRate)
                        {
                            blood += regen;
                            regenTimer = 0;
                        }
                    }

                }
            }
        }

        #region data saving
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Player.whoAmI);
            packet.Write(bloodHunter);
            packet.Send(toWho, fromWho);
        }
        public override void CopyClientState(ModPlayer clientClone)/* tModPorter Suggestion: Replace Item.Clone usages with Item.CopyNetStateTo */
        {
            BloodHunter clone = clientClone as BloodHunter;
            clone.bloodHunter = bloodHunter;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            BloodHunter clone = clientPlayer as BloodHunter;

            if (bloodHunter != clone.bloodHunter)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["bloodHunter"] = bloodHunter;
            tag["eyeColor"] = eyeColor;
        }

        public override void LoadData(TagCompound tag)
        {
            bloodHunter = tag.GetBool("bloodHunter");
            eyeColor = tag.Get<Color>("eyeColor");
        }
        #endregion
    }
}
