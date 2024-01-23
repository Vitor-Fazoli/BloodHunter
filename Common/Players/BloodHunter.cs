using BloodHunter.Content.Buffs;
using BloodHunter.Content.Items;
using Microsoft.Xna.Framework;
using System;
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
        public bool sunResistance;

        public readonly int MAX_BLOOD_GOBLET = 5;
        public int bloodGoblet = 0;

        public Color eyeColor;
        public bool bloodHunter;
        public bool blessedhunter;
        public int bloodCurrent;
        public int bloodMax;
        public const int defaultBloodMax = 100;
        public int bloodMax2;

        public int getBloodCurrent;
        public int getBloodRate = 600;
        public bool canGetBlood = true;

        public bool isItRanger = false;
        public int essence = 1;
        public int essenceMax = 10;

        public const int LEVEL_MAX = 10;
        public int level;
        public int countToXp;
        public int xp;
        public int xpMax;

        public int classCooldown = 0;

        public bool IsBloodFull()
        {
            return bloodCurrent == bloodMax2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // For Ranged Weapons
            if (bloodHunter && canGetBlood && isItRanger)
            {
                if (target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X - 25 + Main.rand.Next(25), target.Center.Y - 2 + Main.rand.Next(2)), new Vector2(
                       0, -2), ModContent.ItemType<LifeEssence>(), 1);

                    canGetBlood = false;
                }
            }

            if (bloodHunter && target.life <= 0)
            {
                xp++;
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.DamageType == DamageClass.Ranged && canGetBlood && isItRanger)
            {
                if (target.type != NPCID.TargetDummy)
                {
                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(target.Center.X, target.Center.Y), new Vector2(0, -2), ModContent.ItemType<LifeEssence>(), 1);
                }
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if (!isItRanger)
            {
                if (bloodCurrent >= manaConsumed / 2)
                {
                    bloodCurrent -= manaConsumed / 2;
                    Player.statMana += manaConsumed;
                }
            }
        }
        public override void Initialize()
        {
            bloodMax = defaultBloodMax;
        }
        public override void UpdateDead()
        {
            ResetVariables();
        }
        public override void ResetEffects()
        {
            ResetVariables();
        }
        public override void PostUpdateMiscEffects()
        {
            bloodCurrent = Utils.Clamp(bloodCurrent, 0, bloodMax2);
            Player.eyeColor = new Color(255, 0, 0);

            UpdateBuffs();
            RegenerateBlood();
            LevelSystem();
            UpdateStats();
            UpdateBlessed();
        }
        private void ResetVariables()
        {
            bloodMax2 = bloodMax;
            getBloodRate = 600;
            blessedhunter = false;
        }
        private void UpdateBlessed()
        {
            if (bloodMax2 <= 5)
            {
                blessedhunter = true;
            }
        }
        private void UpdateStats()
        {
            Player.GetDamage(DamageClass.Generic) += level * 0.01f;
        }
        private void UpdateBuffs()
        {
            if (bloodHunter)
            {
                if (isItRanger)
                {
                    getBloodCurrent++;

                    if (getBloodCurrent >= getBloodRate)
                    {
                        getBloodCurrent = 0;
                        canGetBlood = true;
                    }

                    if (bloodCurrent >= bloodMax && classCooldown <= 0)
                    {
                        Player.AddBuff(BuffID.Inferno, 60 * 30);
                        bloodCurrent = 0;
                    }
                }


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
                }
            }
        }
        private void RegenerateBlood()
        {
            if (!isItRanger)
            {
                if (bloodCurrent <= 0)
                {
                    bloodCurrent = 0;
                }

                getBloodCurrent++;

                if (bloodCurrent < bloodMax2)
                {
                    if (getBloodCurrent >= getBloodRate / 5)
                    {
                        bloodCurrent += 1;

                        getBloodCurrent = 0;
                    }
                }
            }
        }
        private static int ToLevelUp(int level, float levelRate)
        {
            return (int)(50 * Math.Pow(levelRate, level - 1));
        }
        private static int LevelUp(int level)
        {
            if (level < LEVEL_MAX)
            {
                return level += 1;
            }
            else
            {
                return level;
            }
        }
        private void LevelSystem()
        {
            xpMax = ToLevelUp(level, 1.5f);

            if (xp >= xpMax)
            {
                level = LevelUp(level);
                xp = 0;
            }
        }

        #region data saving
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Player.whoAmI);
            packet.Write(bloodHunter);
            packet.Write(isItRanger);
            packet.Write(xp);
            packet.Write(xpMax);
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
            tag["bloodGoblet"] = bloodGoblet;
            tag["eyeColor"] = eyeColor;
            tag["isItRanger"] = isItRanger;
            tag["xp"] = xp;
            tag["xpMax"] = xpMax;
            tag["level"] = level;
        }

        public override void LoadData(TagCompound tag)
        {
            bloodHunter = tag.GetBool("bloodHunter");
            eyeColor = tag.Get<Color>("eyeColor");
            bloodGoblet = tag.GetAsInt("bloodGoblet");
            isItRanger = tag.GetBool("isItRanger");
            xp = tag.GetInt("xp");
            xpMax = tag.GetInt("xpMax");
            level = tag.GetInt("level");
        }
        #endregion
    }
}
