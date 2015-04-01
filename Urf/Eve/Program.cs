using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Eve
{
    static class Program
    {
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Spell _q, _e;
        static void Main(string[] args)
        {
            if (Game.Mode == GameMode.Running)
            {
                Game_OnGameStart(new EventArgs());
            }

            Game.OnStart += Game_OnGameStart;
        }

        private static void Game_OnGameStart(EventArgs eventArgs)
        {
            if (Player.ChampionName != "Evelynn") return;
            Game.PrintChat("Urferino Evelynn by HyunMi loaded");
            _q = new Spell(SpellSlot.Q, 500);
            _e = new Spell(SpellSlot.E, 225);
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (_q.IsReady())
            {
                if (Player.CountEnemiesInRange(500) > 0)
                {
                    _q.CastOnUnit(Player.GetEnemiesInRange(500).OrderByDescending(tar => tar.Health).Last());
                }
                else
                {
                    Obj_AI_Base targetMinion = MinionManager.GetMinions(500).OrderByDescending(minion => minion.Health).Last();
                    _q.CastOnUnit(targetMinion);
                }
            }

            if (_e.IsReady())
            {
                _e.Cast();
            }
        }
    }
}
