using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using mapPos = Core.Enumerators.Positions.MapPosition;

namespace Core
{
    public static class ObjectiveController
    {
        private static List<TurretBase> GetTurretData(List<Obj_AI_Turret> turrets)
        {
            mapPos.Initialize();
            List<TurretBase> dataList = turrets.Select(turret => new TurretBase(turret)).ToList();
            return dataList;
        }

        private class TurretBase
        {
            private Obj_AI_Turret _turret;
            private int _priority;
            private bool _difficult;//is tower difficult to kill or not
            private string _location;

            public TurretBase(Obj_AI_Turret turret)
            {
                _turret = turret;
                _location = GetTurretLocation(turret);
                _priority = GetPriority(turret);
                _difficult = IsDifficult(_priority);
            }
        }


        private static List<Obj_AI_Turret> GetTurrets(Vector3 position, int range)
        {
            return position.GetObjects<Obj_AI_Turret>(range).Where(turret => turret.Team != DataBases.Player.Team).ToList();
        }

        private static bool IsDifficult(int priority)
        {
            return priority != 0 && priority != 1;
        }

        //TODO: refine
        private static int GetPriority(Obj_AI_Turret turret)
        {
            int priority = 0;
            switch (GetTurretLocation(turret))
            {
                case "Top":
                    priority = 0;
                    break;
                case "Mid":
                    priority = 2;
                    break;
                case "Bot":
                    priority = 1;
                    break;
            }
            return priority;
        }

        //TODO: refine
        private static string GetTurretLocation(Obj_AI_Turret turret)
        {
            if (mapPos.onBotLane(turret))
            {
                return TurretLocations.Bot.ToString();
            }

            if (mapPos.onMidLane(turret))
            {
                return TurretLocations.Mid.ToString();
            }

            return mapPos.onTopLane(turret) ? TurretLocations.Top.ToString() : null;
        }

        private enum TurretLocations
        {
            Top,
            Bot,
            Mid,
        }
    }
}
