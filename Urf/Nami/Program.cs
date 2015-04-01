using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Nami
{
    static class Program
    {

        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Spell _w, _e;
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
            if (Player.ChampionName != "Nami") return;
            Game.PrintChat("Urferino Nami by HyunMi loaded");
            _w = new Spell(SpellSlot.E, 725);
            _e = new Spell(SpellSlot.E, 800);
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.CountEnemiesInRange(725) > 0 && _w.IsReady())
            {
                _w.CastOnUnit(Player.GetEnemiesInRange(725).OrderByDescending(target => target.Health).Last());
            }

            if (Player.CountAlliesInRange(725) > 0 && _w.IsReady())
            {
                _w.CastOnUnit(Player.GetAlliesInRange(725).OrderByDescending(target => target.Health).Last());
            }

            if (Player.CountAlliesInRange(800) > 0 && _e.IsReady())
            {
                _e.CastOnUnit(Player.GetAlliesInRange(800).OrderByDescending(target => target.GoldEarned).First());
            }
        }
    }
}
