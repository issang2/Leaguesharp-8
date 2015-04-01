using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Sivir
{
    static class Program
    {
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Spell _q, _w, _e;
        static void Main(string[] args)
        {
            if (Game.Mode == GameMode.Running)
            {
                Game_OnGameStart(new EventArgs());
            }

            Game.OnStart += Game_OnGameStart;
        }

        private static void Game_OnGameStart(EventArgs args)
        {
            if (Player.ChampionName != "Sivir") return;

            Game.PrintChat("Urferino Sivir by HyunMi loaded");
            _q = new Spell(SpellSlot.Q, 1250);
            _w = new Spell(SpellSlot.W);
            _e = new Spell(SpellSlot.E);
            _q.SetSkillshot(0, 100, 1350, false, SkillshotType.SkillshotLine);
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (_w.IsReady())_w.Cast();
            if (_e.IsReady())_e.Cast();
            if (!_q.IsReady()) return;
            if (Player.CountEnemiesInRange(1250) <= 0)
            {
                _q.Cast( MinionManager.GetBestLineFarmLocation(MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(1250), _q.Delay, _q.Width, _q.Speed,
                    Player.Position, _q.Range, false, SkillshotType.SkillshotLine), _q.Width, _q.Range).Position);
            }
            else
            {
                _q.Cast(
                    _q.GetPrediction(
                        Player.GetEnemiesInRange(1250).OrderByDescending(target => target.Health).Last(), false,
                        _q.Range).CastPosition);
            }
        }
    }
}
