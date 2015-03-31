using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;
// ReSharper disable InvertIf

// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantAssignment
// ReSharper disable UnusedVariable
//
namespace Meth_Lissy
{
    static class Program
    {
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Menu _menu, _targetSelectorMenu;
        private static Orbwalking.Orbwalker _orbwalker;
        private static Spell _iceShard, _ringOfFrost, _glacialPath, _frozenTomb, _ignite, _flash;


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
                    laneclear.AddItem(new MenuItem("hyunmi.lissandra.lasthit.smartPassive", "Smart usage of passive").SetValue(true));
                    laneclear.AddItem(new MenuItem("hyunmi.lissandra.lasthit.groupfocus", "Focus group of minions").SetValue(false));
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

            _iceShard    = new Spell(SpellSlot.Q, 725, TargetSelector.DamageType.Magical);
            _ringOfFrost = new Spell(SpellSlot.W, 450, TargetSelector.DamageType.Magical);
            _glacialPath = new Spell(SpellSlot.E, 1050, TargetSelector.DamageType.Magical);
            _frozenTomb  = new Spell(SpellSlot.R, 550, TargetSelector.DamageType.Magical);
            _ignite      = new Spell(Player.GetSpellSlot("summonerdot"), 600);
            _flash       = new Spell(Player.GetSpellSlot("flash"), 425);
            _iceShard.SetSkillshot(250, 75, 2200, false, SkillshotType.SkillshotLine);
            _glacialPath.SetSkillshot(250, 125, 850, false, SkillshotType.SkillshotLine);

            
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
            if (_ignite.IsReady() && _menu.Item("hyunmi.lissandra.misc.ignite").GetValue<bool>())
            {
                foreach (Obj_AI_Hero target in Player.GetEnemiesInRange(_ignite.Range).Where(target => _ignite.GetDamage(target) > target.Health))
                {
                    _ignite.Cast(target);
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
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _glacialPath.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_glacialPath.Range),
                                    _glacialPath.Delay, _glacialPath.Width, _glacialPath.Speed, Player.Position,
                                    _glacialPath.Range,
                                    false, SkillshotType.SkillshotLine), _glacialPath.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _glacialPath.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
                else
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_glacialPath.Range)
                                .Where(minion => minion.Health < _glacialPath.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _glacialPath.Cast(_glacialPath.GetPrediction(targetMinion, false, _glacialPath.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
            }

            if (!smartPassive)
            {
                if (groupFocus)
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _glacialPath.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_glacialPath.Range),
                                    _glacialPath.Delay, _glacialPath.Width, _glacialPath.Speed, Player.Position,
                                    _glacialPath.Range,
                                    false, SkillshotType.SkillshotLine), _glacialPath.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _glacialPath.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
                else
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_glacialPath.Range)
                                .Where(minion => minion.Health < _glacialPath.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _glacialPath.Cast(_glacialPath.GetPrediction(targetMinion, false, _glacialPath.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] { CollisionableObjects.YasuoWall }).CastPosition);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
            }
        }

        // ReSharper disable once FunctionComplexityOverflow
        private static void LaneClear()
        {
            bool smartPassive = _menu.Item("hyunmi.lissandra.laneclear.smartPassive").GetValue<bool>();
            bool groupFocus = _menu.Item("hyunmi.lissandra.laneclear.groupfocus").GetValue<bool>();
            bool useQ = _menu.Item("hyunmi.lissandra.laneclear.spells.q").GetValue<bool>();
            bool useW = _menu.Item("hyunmi.lissandra.laneclear.spells.w").GetValue<bool>();
            bool useE = _menu.Item("hyunmi.lissandra.laneclear.spells.e").GetValue<bool>();

            if (smartPassive && Player.HasBuff("LissandraPassiveReady"))
            {
                if (groupFocus)
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _glacialPath.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_glacialPath.Range),
                                    _glacialPath.Delay, _glacialPath.Width, _glacialPath.Speed, Player.Position,
                                    _glacialPath.Range,
                                    false, SkillshotType.SkillshotLine), _glacialPath.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _glacialPath.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
                else
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_glacialPath.Range)
                                .Where(minion => minion.Health < _glacialPath.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _glacialPath.Cast(_glacialPath.GetPrediction(targetMinion, false, _glacialPath.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
            }

            if (!smartPassive)
            {
                if (groupFocus)
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _glacialPath.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_glacialPath.Range),
                                    _glacialPath.Delay, _glacialPath.Width, _glacialPath.Speed, Player.Position,
                                    _glacialPath.Range,
                                    false, SkillshotType.SkillshotLine), _glacialPath.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _glacialPath.Cast(farmLocation.Position);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        MinionManager.FarmLocation farmLocation =
                            _iceShard.GetLineFarmLocation(
                                MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_iceShard.Range),
                                    _iceShard.Delay, _iceShard.Width, _iceShard.Speed, Player.Position, _iceShard.Range,
                                    false, SkillshotType.SkillshotLine), _iceShard.Width);
                        if (farmLocation.MinionsHit != 0)
                        {
                            _iceShard.Cast(farmLocation.Position);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
                else
                {
                    if (useQ && !useW && !useE && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useQ && useW && _iceShard.IsReady() && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useE && _glacialPath.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_glacialPath.Range)
                                .Where(minion => minion.Health < _glacialPath.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _glacialPath.Cast(_glacialPath.GetPrediction(targetMinion, false, _glacialPath.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useQ && _iceShard.IsReady())
                    {
                        Obj_AI_Base targetMinion =
                            MinionManager.GetMinions(_iceShard.Range)
                                .Where(minion => minion.Health < _iceShard.GetDamage(minion))
                                .OrderByDescending(minion => minion.Health).Last();
                        if (Math.Abs(targetMinion.Health) > 0.0001)
                        {
                            _iceShard.Cast(_iceShard.GetPrediction(targetMinion, false, _iceShard.Range,
                                new[] {CollisionableObjects.YasuoWall}).CastPosition);
                        }
                    }

                    if (useW && _ringOfFrost.IsReady())
                    {
                        List<Obj_AI_Base> minionsW = MinionManager.GetMinions(_ringOfFrost.Range);
                        if (minionsW.Count > 3)
                        {
                            _ringOfFrost.Cast();
                        }
                    }
                }
            }
        }

        private static void Harass()
        {

        }

        private static void Combo()
        {

        }

        private static void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (Player.Distance(gapcloser.Sender.Position) < _ringOfFrost.Range && _ringOfFrost.IsReady())
            {
                _ringOfFrost.Cast();
                if (_iceShard.IsReady())
                {
                    _iceShard.Cast(_iceShard.GetPrediction(gapcloser.Sender, false, _iceShard.Range, new[] { CollisionableObjects.YasuoWall }).CastPosition);
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_menu.Item("hyunmi.lissandra.drawings.q").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, _iceShard.Range, _iceShard.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.w").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, _ringOfFrost.Range, _ringOfFrost.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.e").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, _glacialPath.Range, _glacialPath.IsReady() ? Color.BlueViolet : Color.Crimson);
            }

            if (_menu.Item("hyunmi.lissandra.drawings.r").GetValue<bool>())
            {
                Render.Circle.DrawCircle(Player.Position, _frozenTomb.Range, _frozenTomb.IsReady() ? Color.BlueViolet : Color.Crimson);
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
    }
}
