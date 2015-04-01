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
            if (Player.ChampionName != "Urgot") return;

            Game.PrintChat("Urferino Sivir by HyunMi loaded");
            
            _w = new Spell(SpellSlot.W);
            
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (_w.IsReady())_w.Cast();

        }
    }
}
