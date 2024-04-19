using BloodHunter.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodHunter.Common.Players
{
    /// <summary>
    /// this is a struct to store all specializations of blood hunter
    /// </summary>
    public struct Specialization
    {
        public const short MidnightRanger = 01;
        public const short TwilightMage = 02;
        public const short MoonReaver = 03;
    }

    /// <summary>
    /// this is a common class of blood hunter to store all the blood hunter data and methods to use in Specializations
    /// </summary>
    public class BloodHunter : ModPlayer
    {
        /// <summary>
        /// is a bool to check if the player already have sun resistance
        /// </summary>
        public bool sunResistance;

        /// <summary>
        /// change the eye color of the player when the player is a blood hunter
        /// </summary>
        public Color eyeColor;

        /// <summary>
        /// return true if the player is a blood hunter
        /// </summary>
        public bool isBloodHunter;

        /// System to store the blood of the player ( like a mana )
        public int bloodCurrent;
        public int bloodMax;
        public int bloodMax2;

        public int getBloodCurrent;
        public int getBloodRate = 10;
        public readonly int GET_BLOOD_RATE_MAX = 600;
        public bool canGetBlood = true;

        /// Level System of blood hunter
        public const int LEVEL_MAX = 10;
        public int level;
        public int countToXp;
        public int xp;
        public int xpMax;

        /// <summary>
        /// Specialization of blood hunter
        /// </summary>
        public short specialization = Specialization.MoonReaver;

        public void ZeroBlood()
        {
            bloodCurrent = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the amount of blood is in maximum</returns>
        public bool IsBloodFull()
        {
            return bloodCurrent == bloodMax2;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            ResetVariables();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void UpdateDead()
        {
            ResetVariables();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ResetEffects()
        {
            ResetVariables();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PostUpdateMiscEffects()
        {
            specialization = Specialization.MoonReaver;

            bloodCurrent = Utils.Clamp(bloodCurrent, 0, bloodMax2);
            Player.eyeColor = new Color(255, 0, 0);

            UpdateBuffs();
            LevelSystem();
            UpdateStats();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetVariables()
        {
            bloodMax2 = GetSpecBloodMax();

            getBloodRate = GetSpecBloodRate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateStats()
        {
            Player.GetDamage(DamageClass.Generic) += level * 0.01f;
        }
        

        /// <summary>
        /// 
        /// </summary>
        private void UpdateBuffs()
        {
            if (isBloodHunter)
            {

                getBloodCurrent++;

                if (getBloodCurrent >= getBloodRate)
                {
                    getBloodCurrent = 0;
                    canGetBlood = true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSpecBloodMax()
        {
            return specialization switch
            {
                Specialization.MidnightRanger => 80,
                Specialization.TwilightMage => 150,
                Specialization.MoonReaver => 200,
                _ => 100,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSpecBloodRate()
        {
            return specialization switch
            {
                Specialization.MidnightRanger => 600,
                Specialization.TwilightMage => 150,
                Specialization.MoonReaver => 120,
                _ => 100,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        public void ReceiveBlood(Player player, int amount)
        {
            bloodCurrent += amount;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, 10, 10), new Color(200, 0, 255), amount, true);
            SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, player.position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="levelRate"></param>
        /// <returns></returns>
        private static int ToLevelUp(int level, float levelRate)
        {
            return (int)(50 * Math.Pow(levelRate, level - 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        private void LevelSystem()
        {
            xpMax = ToLevelUp(level, 1.5f);

            if (xp >= xpMax)
            {
                level = LevelUp(level);
                xp = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toWho"></param>
        /// <param name="fromWho"></param>
        /// <param name="newPlayer"></param>
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Player.whoAmI);
            packet.Write(isBloodHunter);
            packet.Write(xp);
            packet.Write(xpMax);
            packet.Write(specialization);
            packet.Send(toWho, fromWho);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientClone"></param>
        public override void CopyClientState(ModPlayer clientClone)/* tModPorter Suggestion: Replace Item.Clone usages with Item.CopyNetStateTo */
        {
            BloodHunter clone = clientClone as BloodHunter;
            clone.isBloodHunter = isBloodHunter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientPlayer"></param>
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            BloodHunter clone = clientPlayer as BloodHunter;

            if (isBloodHunter != clone.isBloodHunter)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public override void SaveData(TagCompound tag)
        {
            tag["bloodHunter"] = isBloodHunter;
            tag["eyeColor"] = eyeColor;
            tag["xp"] = xp;
            tag["xpMax"] = xpMax;
            tag["level"] = level;
            tag["specialization"] = specialization;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public override void LoadData(TagCompound tag)
        {
            isBloodHunter = tag.GetBool("bloodHunter");
            eyeColor = tag.Get<Color>("eyeColor");
            xp = tag.GetInt("xp");
            xpMax = tag.GetInt("xpMax");
            level = tag.GetInt("level");
            specialization = tag.GetShort("specialization");
        }
    }
}
