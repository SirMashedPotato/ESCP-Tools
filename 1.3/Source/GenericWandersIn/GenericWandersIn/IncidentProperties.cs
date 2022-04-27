using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace GenericWandersIn
{
    class IncidentProperties : DefModExtension
    {
        public PawnKindDef kindDef;
        public IntRange max = new IntRange(3, 5);
        public bool leaveMapAfterTime = true;

        public static IncidentProperties Get(Def def)
        {
            return def.GetModExtension<IncidentProperties>();
        }
    }
}
