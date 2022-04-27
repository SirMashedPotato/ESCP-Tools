using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace ESCP_ChunkCrushing
{
    public class CrushableOutputDef : Def
    {
        public ThingDef outputThingDef;
        public IntRange outputRange = new IntRange(1, 15);
        public float chance = 1f;   //1 is assumed to be the default value, anything excessively larger may cause issues
        public bool isSpecial = false;
    }
}
