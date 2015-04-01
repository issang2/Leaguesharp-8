using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Karthus
{
    static class Program
    {

        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Spell _q;
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

            if (Player.ChampionName != "Karthus") return;
            Game.PrintChat("Urferino Karthus by HyunMi loaded");
            _q = new Spell(SpellSlot.Q, 875);
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsRecalling()) return;
            if (Player.CountEnemiesInRange(875) > 0 && _q.IsReady())
            {
                _q.CastOnUnit(Player.GetEnemiesInRange(875).OrderByDescending(target => target.Health).Last());
            }
        }
    }
}
