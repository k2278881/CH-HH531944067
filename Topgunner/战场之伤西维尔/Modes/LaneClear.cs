﻿using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

using Settings = SivirDamage.Config.Modes.LaneClear;
namespace SivirDamage.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (Settings.mana >= Player.Instance.ManaPercent)
                return;

            if (Settings.UseQ && Q.IsReady())
            {
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, Q.Range));
                foreach (var m in minions)
                {
                    if (Q.GetPrediction(m).CollisionObjects.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable).Count() >= 4)
                    {
                        Q.Cast(m);
                    }
                }
            }

            if (Settings.UseW && W.IsReady())
            {
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsInRange(Player.Instance.Position, Q.Range) && !t.IsDead && t.IsValid && !t.IsInvulnerable);
                if (minions != null && minions.Count() > 3)
                {
                    W.Cast();
                }
            }
        }
    }
}
