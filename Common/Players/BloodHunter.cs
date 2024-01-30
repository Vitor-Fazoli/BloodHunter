using BloodHunter.Content.Buffs;
using BloodHunter.Content.Items;
using BloodHunter.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodHunter.Common.Players
{

    public struct Specialization
    {
        public const short SanguineMarksman = 01;
        public const short DarkbloodMagus = 02;
    }
    public class BloodHunter : ModPlayer
    {
        public bool sunResistance;

        public Color eyeColor;
        public bool isBloodHunter;
        public int bloodCurrent;
        public int bloodMax;
        public int bloodMax2;

        public int getBloodCurrent;
        public int getBloodRate = 600;
        public readonly int GET_BLOOD_RATE_MAX = 600;
        public bool canGetBlood = true;

        public const int LEVEL_MAX = 10;
        public int level;
        public int countToXp;
        public int xp;
        public int xpMax;

        public short specialization = Specialization.SanguineMarksman;

        public bool transforming;
        private int transformingAI;

        public bool IsBloodFull()
        {
            return bloodCurrent == bloodMax2;
        }
        public override void Initialize()
        {
            ResetVariables();
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
            specialization = Specialization.SanguineMarksman;

            bloodCurrent = Utils.Clamp(bloodCurrent, 0, bloodMax2);
            Player.eyeColor = new Color(255, 0, 0);

            UpdateBuffs();
            LevelSystem();
            UpdateStats();
            InitialTransforming();
        }
        private void ResetVariables()
        {
            bloodMax2 = SpecBloodMax2();

            getBloodRate = 600;
        }
        private void UpdateStats()
        {
            Player.GetDamage(DamageClass.Generic) += level * 0.01f;
        }
        private void InitialTransforming()
        {
            if (transforming)
            {
                transformingAI++;


                Player.gravity = -0.07f;
                Player.velocity.X = 0;


                if (transformingAI >= 50)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(2f, 2f);
                        Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Blood, speed * 5, Scale: 1.5f);
                        d.noGravity = true;
                    }
                    transforming = false;
                    Player.gravity = Player.defaultGravity;
                }
            }
        }
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

        private int SpecBloodMax2()
        {
            return specialization switch
            {
                Specialization.SanguineMarksman => 80,
                Specialization.DarkbloodMagus => 150,
                _ => 100,
            };
        }

        public void ReceiveBlood(Player player, int amount)
        {
            bloodCurrent += amount;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, 10, 10), new Color(200, 0, 255), amount, true);
            SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, player.position);
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
            packet.Write(isBloodHunter);
            packet.Write(xp);
            packet.Write(xpMax);
            packet.Write(specialization);
            packet.Send(toWho, fromWho);
        }
        public override void CopyClientState(ModPlayer clientClone)/* tModPorter Suggestion: Replace Item.Clone usages with Item.CopyNetStateTo */
        {
            BloodHunter clone = clientClone as BloodHunter;
            clone.isBloodHunter = isBloodHunter;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            BloodHunter clone = clientPlayer as BloodHunter;

            if (isBloodHunter != clone.isBloodHunter)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["bloodHunter"] = isBloodHunter;
            tag["eyeColor"] = eyeColor;
            tag["xp"] = xp;
            tag["xpMax"] = xpMax;
            tag["level"] = level;
            tag["specialization"] = specialization;
        }

        public override void LoadData(TagCompound tag)
        {
            isBloodHunter = tag.GetBool("bloodHunter");
            eyeColor = tag.Get<Color>("eyeColor");
            xp = tag.GetInt("xp");
            xpMax = tag.GetInt("xpMax");
            level = tag.GetInt("level");
            specialization = tag.GetShort("specialization");
        }
        #endregion
    }
}
