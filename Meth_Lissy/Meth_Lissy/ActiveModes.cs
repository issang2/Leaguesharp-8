namespace Meth_Lissy
{
    class ActiveModes
    {
        public static void LaneClear(bool smartPassive, bool groupFocus, bool useQ, bool useW, bool useE, bool harass)
        {
            if (harass)
                Harass();

            if (smartPassive && Program.Player.HasBuff("LissandraPassiveReady"))
            {
                if (groupFocus)
                {
                    if (useW)
                        SpellCasts.RingOfFrostCast(true, true);
                    if (useE)
                        SpellCasts.GlacialPathCast(true, true);
                    if (useQ)
                        SpellCasts.IceShardCast(true, true);
                }
                else
                {
                    if (useW)
                        SpellCasts.RingOfFrostCast(true, false);
                    if (useE)
                        SpellCasts.GlacialPathCast(true, false);
                    if (useQ)
                        SpellCasts.IceShardCast(true, false);
                }
            }

            if (!smartPassive)
            {
                if (groupFocus)
                {
                    if (useW)
                        SpellCasts.RingOfFrostCast(true, true);
                    if (useE)
                        SpellCasts.GlacialPathCast(true, true);
                    if (useQ)
                        SpellCasts.IceShardCast(true, true);
                }
                else
                {
                    if (useW)
                        SpellCasts.RingOfFrostCast(true, false);
                    if (useE)
                        SpellCasts.GlacialPathCast(true, false);
                    if (useQ)
                        SpellCasts.IceShardCast(true, false);
                }
            }
        }

        public static void Harass()
        {
            
        }
    }
}
