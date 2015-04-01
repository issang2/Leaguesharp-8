using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace Sona
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

        private static void Game_OnGameStart(EventArgs eventArgs)
        {
            if (Player.ChampionName != "Sona") return;

            Game.PrintChat("Urferino Sona by HyunMi loaded");
            _q = new Spell(SpellSlot.Q, 850);
            _w = new Spell(SpellSlot.W, 1000);
            _e = new Spell(SpellSlot.E, 350);
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsRecalling())return;
            if (Player.CountAlliesInRange(1000) > 0)
            {
                _w.Cast();
            }

            if (Player.CountEnemiesInRange(850) > 0)
            {
                _q.Cast();
            }

            _e.Cast();
        }
    }
}
