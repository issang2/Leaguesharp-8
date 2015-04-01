using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Karma
{
    static class Program
    {
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Spell _shield;
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
            if (Player.ChampionName != "Karma") return;

            Game.PrintChat("Urferino Karma by HyunMi loaded");
            _shield = new Spell(SpellSlot.E, 800);

            Game.OnUpdate += Game_OnUpdate;

        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (_shield.IsReady())
            {
                _shield.CastOnUnit(Player.GetAlliesInRange(800).LastOrDefault());
            }
        }
    }
}
