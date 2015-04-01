using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Meth_Lissy
{
    internal static class SpellCasts
    {
        public static Spell IceShard;
        public static Spell RingOfFrost;
        public static Spell GlacialPath;
        public static Spell FrozenTomb;
        public static Spell Ignite;
        public static Spell Flash;

        public static void IceShardCast(bool minions, bool groupfocusMinions)
        {
            if (!IceShard.IsReady())return;

            if (minions)
            {
                if (groupfocusMinions)
                {
                    MinionManager.FarmLocation farmLocation =
                        IceShard.GetLineFarmLocation(
                            MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(IceShard.Range),
                                IceShard.Delay, IceShard.Width, IceShard.Speed,
                                Program.Player.Position, IceShard.Range,
                                false, SkillshotType.SkillshotLine), IceShard.Width);
                    if (farmLocation.MinionsHit != 0)
                    {
                        IceShard.Cast(farmLocation.Position);
                    }
                }
                else
                {
                    Obj_AI_Base targetMinion =
                        MinionManager.GetMinions(IceShard.Range)
                            .Where(minion => minion.Health < IceShard.GetDamage(minion))
                            .OrderByDescending(minion => minion.Health)
                            .Last();
                    IceShard.Cast(
                        IceShard.GetPrediction(targetMinion, false, IceShard.Range,
                            new[] {CollisionableObjects.YasuoWall}).CastPosition);
                }
            }
        }

        public static void RingOfFrostCast(bool minions, bool groupfocusMinions)
        {
            if (!RingOfFrost.IsReady())return;

            if (minions)
            {
                if (groupfocusMinions)
                {
                    List<Obj_AI_Base> minionsW = MinionManager.GetMinions(RingOfFrost.Range);
                    if (minionsW.Count >= 3)
                    {
                        RingOfFrost.Cast();
                    }
                }
                else
                {
                    List<Obj_AI_Base> minionsW = MinionManager.GetMinions(RingOfFrost.Range);
                    if (minionsW.Count > 0)
                    {
                        RingOfFrost.Cast();
                    }
                }
            }
            //
        }

        public static void GlacialPathCast(bool minions, bool groupfocusMinions)
        {
            if (!GlacialPath.IsReady())return;

            if (minions)
            {
                if (groupfocusMinions)
                {
                    MinionManager.FarmLocation farmLocation = GlacialPath.GetLineFarmLocation(
                        MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(GlacialPath.Range),
                            GlacialPath.Delay, GlacialPath.Width, GlacialPath.Speed, Program.Player.Position,
                            GlacialPath.Range,
                            false, SkillshotType.SkillshotLine), GlacialPath.Width);
                    if (farmLocation.MinionsHit >= 3)
                    {
                        GlacialPath.Cast(farmLocation.Position);
                    }
                }
                else
                {
                    MinionManager.FarmLocation farmLocation = GlacialPath.GetLineFarmLocation(
                        MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(GlacialPath.Range),
                            GlacialPath.Delay, GlacialPath.Width, GlacialPath.Speed, Program.Player.Position,
                            GlacialPath.Range,
                            false, SkillshotType.SkillshotLine), GlacialPath.Width);
                    if (farmLocation.MinionsHit > 0)
                    {
                        GlacialPath.Cast(farmLocation.Position);
                    }
                }
            }
            //
        }
    }
}
