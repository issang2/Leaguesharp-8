using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;
using System.Media;
using System.Net;

// ReSharper disable InvertIf

// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantAssignment
// ReSharper disable UnusedVariable
//
namespace Meth_Lissy
{
    static class Program
    {
        public static readonly Obj_AI_Hero Player = ObjectManager.Player;
        public static Menu _menu, _targetSelectorMenu;
        private static Orbwalking.Orbwalker _orbwalker;


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
            if (Player.BaseSkinName != "Lissandra")
            {
                return;
            }
            Game.PrintChat("Meth_Lissandra by HyunMi loaded");
            #region Menu
            {
                _menu = new Menu("[HyunMi]-" + Player.BaseSkinName, "hyunmi.lissandra", true);

                {
                    _targetSelectorMenu = new Menu("Target selector", "hyunmi.lissandra.targetselector");
                    TargetSelector.AddToMenu(_targetSelectorMenu);
                    _menu.AddSubMenu(_targetSelectorMenu);

                    _menu.AddSubMenu(new Menu("Orbwalker", "hyunmi.lissandra.orbwalker"));
                    _orbwalker = new Orbwalking.Orbwalker(_menu.SubMenu("hyunmi.lissandra.orbwalker"));
                }

                //combo
                Menu combo = new Menu("Combo", "hyunmi.lissandra.combo");
                {
                    Menu spells = new Menu("Spells", "hyunmi.lissandra.combo.spells");
                    {
                        spells.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.q", "Use Q").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.w", "Use W").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.e", "Use E").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.r", "Use R").SetValue(true));
                        Menu manaLimiters = new Menu("Mana limiters", "hyunmi.lissandra.combo.spells.manalimiters");
                        {
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.manalimiters.q", "Q mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.manalimiters.w", "W mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.manalimiters.e", "E mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.manalimiters.r", "R mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.combo.spells.manalimiters.notification", "Set to 0 to disable"));
                        }
                        spells.AddSubMenu(manaLimiters);
                    }
                    combo.AddSubMenu(spells);
                    //settings go here
                }
                _menu.AddSubMenu(combo);

                //harass
                Menu harass = new Menu("Harass", "hyunmi.lissandra.harass");
                {
                    Menu spells = new Menu("Spells", "hyunmi.lissandra.harass.spells");
                    {
                        spells.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.q", "Use Q").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.w", "Use W").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.e", "Use E").SetValue(true));
                        Menu manaLimiters = new Menu("Mana limiters", "hyunmi.lissandra.harass.spells.manalimiters");
                        {
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.manalimiters.q", "Q mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.manalimiters.w", "W mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.manalimiters.e", "E mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.harass.spells.manalimiters.notification", "Set to 0 to disable "));
                        }
                        spells.AddSubMenu(manaLimiters);
                    }
                    harass.AddSubMenu(spells);
                    //settings go here
                }
                _menu.AddSubMenu(harass);

                //laneclear
                Menu laneclear = new Menu("Laneclear", "hyunmi.lissandra.laneclear");
                {
                    Menu spells = new Menu("Spells", "hyunmi.lissandra.laneclear.spells");
                    {
                        spells.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.q", "Use Q").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.w", "Use W").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.e", "Use E").SetValue(true));
                        Menu manaLimiters = new Menu("Mana limiters", "hyunmi.lissandra.laneclear.spells.manalimiters");
                        {
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.manalimiters.q", "Q mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.manalimiters.w", "W mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.manalimiters.e", "E mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.laneclear.spells.manalimiters.notification", "Set to 0 to disable "));
                        }
                        spells.AddSubMenu(manaLimiters);
                    }
                    laneclear.AddSubMenu(spells);
                    //settings go here
                    laneclear.AddItem(new MenuItem("hyunmi.lissandra.laneclear.harass", "Auto harass").SetValue(true));
                    laneclear.AddItem(new MenuItem("hyunmi.lissandra.laneclear.smartPassive", "Smart usage of passive").SetValue(true));
                    laneclear.AddItem(new MenuItem("hyunmi.lissandra.laneclear.groupfocus", "Focus group of minions").SetValue(false));
                }
                _menu.AddSubMenu(laneclear);

                //lasthit
                Menu lasthit = new Menu("Lasthit", "hyunmi.lissandra.lasthit");
                {
                    Menu spells = new Menu("Spells", "hyunmi.lissandra.lasthit.spells");
                    {
                        spells.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.q", "Use Q").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.w", "Use W").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.e", "Use E").SetValue(true));
                        Menu manaLimiters = new Menu("Mana limiters", "hyunmi.lissandra.lasthit.spells.manalimiters");
                        {
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.manalimiters.q", "Q mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.manalimiters.w", "W mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.manalimiters.e", "E mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.lasthit.spells.manalimiters.notification", "Set to 0 to disable "));
                        }
                        spells.AddSubMenu(manaLimiters);
                    }
                    lasthit.AddSubMenu(spells);
                    lasthit.AddItem(new MenuItem("hyunmi.lissandra.lasthit.smartPassive", "Smart usage of passive").SetValue(true));
                    lasthit.AddItem(new MenuItem("hyunmi.lissandra.lasthit.groupfocus", "Focus group of minions").SetValue(false));
                }
                _menu.AddSubMenu(lasthit);

               //flee
                Menu flee = new Menu("Flee", "hyunmi.lissandra.flee");
                {
                    Menu spells = new Menu("Spells", "hyunmi.lissandra.flee.spells");
                    {
                        spells.AddItem(new MenuItem("hyunmi.lissandra.flee.spells.q", "Use Q").SetValue(true));
                        spells.AddItem(new MenuItem("hyunmi.lissandra.flee.spells.e", "Use E").SetValue(true));
                        Menu manaLimiters = new Menu("Mana limiters", "hyunmi.lissandra.flee.spells.manalimiters");
                        {
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.flee.spells.manalimiters.q", "Q mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.flee.spells.manalimiters.e", "E mana limiter").SetValue(new Slider(0)));
                            manaLimiters.AddItem(new MenuItem("hyunmi.lissandra.flee.spells.manalimiters.notification", "Set to 0 to disable"));
                        }
                        spells.AddSubMenu(manaLimiters);
                    }
                    flee.AddSubMenu(spells);
                    //settings go here
                }
                _menu.AddSubMenu(flee);

                //misc
                Menu misc = new Menu("Misc", "hyunmi.lissandra.misc");
                {
                    misc.AddItem(new MenuItem("hyunmi.lissandra.misc.ignite", "AutoIgnite on killable").SetValue(true));
                }
                _menu.AddSubMenu(misc);

                //sounds
                Menu sounds = new Menu("Sounds", "hyunmi.lissandra.sounds");
                {
                    sounds.AddItem(new MenuItem("hyunmi.lissandra.sounds.onkill", "Much sweg sound on kill").SetValue(true));
                }
                _menu.AddSubMenu(sounds);

                //drawings
                Menu drawings = new Menu("Drawings", "hyunmi.lissandra.drawings");
                {
                    drawings.AddItem(new MenuItem("hyunmi.lissandra.drawings.q", "Draw Q range").SetValue(true));
                    drawings.AddItem(new MenuItem("hyunmi.lissandra.drawings.w", "Draw W range").SetValue(false));
                    drawings.AddItem(new MenuItem("hyunmi.lissandra.drawings.e", "Draw E range").SetValue(true));
                    drawings.AddItem(new MenuItem("hyunmi.lissandra.drawings.r", "Draw R range").SetValue(false));
                    drawings.AddItem(new MenuItem("hyunmi.lissandra.drawings.minions", "Draw minion last hits").SetValue(false));
                }
                _menu.AddSubMenu(drawings);
            }
            _menu.AddToMainMenu();
            #endregion

            SpellCasts.IceShard    = new Spell(SpellSlot.Q, 725, TargetSelector.DamageType.Magical);
            SpellCasts.RingOfFrost = new Spell(SpellSlot.W, 450, TargetSelector.DamageType.Magical);
            SpellCasts.GlacialPath = new Spell(SpellSlot.E, 1050, TargetSelector.DamageType.Magical);
            SpellCasts.FrozenTomb  = new Spell(SpellSlot.R, 550, TargetSelector.DamageType.Magical);
            SpellCasts.Ignite      = new Spell(Player.GetSpellSlot("summonerdot"), 600);
            SpellCasts.Flash       = new Spell(Player.GetSpellSlot("flash"), 425);
            SpellCasts.IceShard.SetSkillshot(250, 75, 2200, false, SkillshotType.SkillshotLine);
            SpellCasts.GlacialPath.SetSkillshot(250, 125, 850, false, SkillshotType.SkillshotLine);

            
            SoundEngine.Main();
            Drawing.OnDraw += Drawing_OnDraw;
            AntiGapcloser.OnEnemyGapcloser += AntiGapcloser_OnEnemyGapcloser;
            GameObject.OnCreate += GameObject_OnCreate;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {

        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (SpellCasts.Ignite.IsReady() && _menu.Item("hyunmi.lissandra.misc.ignite").GetValue<bool>())
            {
                int dmg;
                if (Player.Level == 1)
                {
                    dmg = 70;
                }
                else
                {
                    dmg = ((Player.Level - 1)*20) + 70;
                }
                List<Obj_AI_Hero> enemies = Player.GetEnemiesInRange(SpellCasts.Ignite.Range);
                foreach (Obj_AI_Hero tar in enemies.Where(tar => tar.Health <= dmg))
                {
                    SpellCasts.Ignite.CastOnUnit(tar);
                }
            }
            switch (_orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    LaneClear();
                    break;
                case Orbwalking.OrbwalkingMode.LastHit:
                    LastHit();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
            }
        }

        //TODO: add manalimiters
        //TODO: remove redundancy
        // ReSharper disable once FunctionComplexityOverflow
        private static void LastHit()
        {
            bool smartPassive = _menu.Item("hyunmi.lissandra.lasthit.smartPassive").GetValue<bool>();
            bool groupFocus = _menu.Item("hyunmi.lissandra.lasthit.groupfocus").GetValue<bool>();
            bool useQ = _menu.Item("hyunmi.lissandra.lasthit.spells.q").GetValue<bool>();
            bool useW = _menu.Item("hyunmi.lissandra.lasthit.spells.w").GetValue<bool>();
            bool useE = _menu.Item("hyunmi.lissandra.lasthit.spells.e").GetValue<bool>();

            if (smartPassive && Player.HasBuff("LissandraPassiveReady"))
            {
                if (groupFocus)
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, true);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, true);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, true);
                    }
                }
                else
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, false);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, false);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, false);
                    }
                }
            }

            if (!smartPassive)
            {
                if (groupFocus)
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, true);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, true);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, true);
                    }
                }
                else
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, false);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, false);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, false);
                    }
                }
            }
        }

        //TODO: add manalimiters
        //TODO: remove redundancy
        // ReSharper disable once FunctionComplexityOverflow
        private static void LaneClear()
        {
            bool smartPassive = _menu.Item("hyunmi.lissandra.laneclear.smartPassive").GetValue<bool>();
            bool groupFocus = _menu.Item("hyunmi.lissandra.laneclear.groupfocus").GetValue<bool>();
            bool useQ = _menu.Item("hyunmi.lissandra.laneclear.spells.q").GetValue<bool>();
            bool useW = _menu.Item("hyunmi.lissandra.laneclear.spells.w").GetValue<bool>();
            bool useE = _menu.Item("hyunmi.lissandra.laneclear.spells.e").GetValue<bool>();
            bool harass = _menu.Item("hyunmi.lissandra.laneclear.harass").GetValue<bool>();

            if (harass)
            {
                Harass();
            }

            if (smartPassive && Player.HasBuff("LissandraPassiveReady"))
            {
                if (groupFocus)
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, true);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, true);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, true);
                    }
                }
                else
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, false);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, false);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, false);
                    }
                }
            }

            if (!smartPassive)
            {
                if (groupFocus)
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, true);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, true);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, true);
                    }
                }
                else
                {
                    if (useW)
                    {
                        SpellCasts.RingOfFrostCast(true, false);
                    }

                    if (useE)
                    {
                        SpellCasts.GlacialPathCast(true, false);
                    }

                    if (useQ)
                    {
                        SpellCasts.IceShardCast(true, false);
                    }
                }
            }
        }

        private static void Harass()
        {
            bool useQ = _menu.Item("hyunmi.lissandra.harass.spells.q").GetValue<bool>();
            bool useW = _menu.Item("hyunmi.lissandra.harass.spells.w").GetValue<bool>();
            bool useE = _menu.Item("hyunmi.lissandra.harass.spells.e").GetValue<bool>();
        }

        //TODO: add manalimers
        private static void Combo()
        {
            bool useQ = _menu.Item("hyunmi.lissandra.combo.spells.q").GetValue<bool>();
            bool useW = _menu.Item("hyunmi.lissandra.combo.spells.w").GetValue<bool>();
            bool useE = _menu.Item("hyunmi.lissandra.combo.spells.e").GetValue<bool>();
            bool useR = _menu.Item("hyunmi.lissandra.combo.spells.r").GetValue<bool>();

        }

        private static void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (Player.Distance(gapcloser.Sender.Position) < SpellCasts.RingOfFrost.Range && SpellCasts.RingOfFrost.IsReady())
            {
                SpellCasts.RingOfFrost.Cast();
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_menu.Item("hyunmi.lissandra.drawings.q").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, SpellCasts.IceShard.Range, SpellCasts.IceShard.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.w").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, SpellCasts.RingOfFrost.Range, SpellCasts.RingOfFrost.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.e").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, SpellCasts.GlacialPath.Range, SpellCasts.GlacialPath.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.r").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, SpellCasts.FrozenTomb.Range, SpellCasts.FrozenTomb.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.minions").GetValue<bool>())
            {
                var minions = MinionManager.GetMinions(1200);
                foreach (Obj_AI_Base minion in minions.Where(minion => minion.Health < Player.GetAutoAttackDamage(minion)/0.85))
                {
                    Render.Circle.DrawCircle(minion.Position, 75, Color.BlueViolet);
                }
            }
        }

        private static class SoundEngine
        {
            private static readonly SoundPlayer SoundPlayer = new SoundPlayer();
            private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
            private static readonly string Degrec = BaseDir + "degrec.wav";

            private static int _dubs = 0;
            private static int _trips = 0;
            private static int _quads = 0;
            private static int _pents = 0;
            private static int _timesLagged = 0;


            public static void Main()
            {
                Play("https://raw.githubusercontent.com/AlterEgojQuery/ElDegrec/master/ElDegrec/ElDegrec/Resources/Degrec.wav");
                CookieContainer cookieContainer = new CookieContainer();
                BetterWebClient webclient = new BetterWebClient(cookieContainer);
                webclient.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/AlterEgojQuery/ElDegrec/master/ElDegrec/ElDegrec/Resources/Degrec.wav"), BaseDir + "degrec.wav");
                Play(Degrec);
                Game.OnUpdate +=Game_OnUpdate_SoundEngine;
            }

            private static void Play(string soundLocation)
            {
                SoundPlayer.SoundLocation = soundLocation;
                SoundPlayer.LoadAsync();
                SoundPlayer.Play();
            }

            private static void Game_OnUpdate_SoundEngine(EventArgs args)
            {
                if (_dubs != Player.DoubleKills)
                {
                    Play(Degrec);
                    _dubs = Player.DoubleKills;
                }

                if (_trips != Player.TripleKills)
                {
                    Play(Degrec);
                    _trips = Player.TripleKills;
                }

                if (_quads != Player.QuadraKills)
                {
                    Play(Degrec);
                    _quads = Player.QuadraKills;
                }
            }
        }
    }
}
