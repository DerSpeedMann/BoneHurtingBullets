using Rocket.Core.Plugins;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BoneHurtingBullets
{
    public class BoneHurtingBullets : RocketPlugin<BoneHurtingBulletsConfiguration>
    {
        private static readonly System.Random rand = new System.Random();

        protected override void Load()
        {
            Rocket.Core.Logging.Logger.Log("BoneBreakingBullets Loaded, Chances:\n" + String.Join("\n", Configuration.Instance.boneBreakingChances.Select(x => $"{x.Limb}: Min {x.BreakChanceMin}% Max {x.BreakChanceMax}% DamageMin {x.BreakChanceDamageMin} DamageMax {x.BreakChanceDamageMax}").ToArray()));
            DamageTool.playerDamaged += OnPlayerDamage;
        }

        protected override void Unload()
        {
            DamageTool.playerDamaged -= OnPlayerDamage;
        }

        private void OnPlayerDamage(Player player, ref EDeathCause cause, ref ELimb limb, ref CSteamID killer, ref Vector3 direction, ref float damage, ref float times, ref bool canDamage)
        {
            if (cause != EDeathCause.GUN && cause != EDeathCause.PUNCH && cause != EDeathCause.MELEE)
                return;

            string limbName = limb.ToString();

            BulletLimbDamageChance boneBreak = Configuration.Instance.boneBreakingChances.FirstOrDefault(x => x.Limb == limbName);
            if (boneBreak != null && boneBreak.BreakChanceDamageMax - boneBreak.BreakChanceDamageMin > 0)
            {
                //calculate damage percent in given range
                var damagePercent = (damage - boneBreak.BreakChanceDamageMin) / (boneBreak.BreakChanceDamageMax - boneBreak.BreakChanceDamageMin);
                if(damagePercent > 0) //check if enough damage was done
                {
                    //fit beween 0 and 1
                    damagePercent = damagePercent < 0 ? 0 : damagePercent > 1 ? 1 : damagePercent;
                    //calculate breakChance
                    var breakChance = damagePercent * (boneBreak.BreakChanceMax - boneBreak.BreakChanceMin) + boneBreak.BreakChanceMin;

                    if (rand.Next(0, 101) <= breakChance)
                    {
                        player.life.breakLegs();
                    }
                }
            }
        }
    }
}
