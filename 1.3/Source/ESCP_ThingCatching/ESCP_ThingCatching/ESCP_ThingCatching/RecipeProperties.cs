using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace ESCP_ThingCatching
{
    class RecipeProperties : DefModExtension
    {

        public List<string> catchTags;

        public float effeciencyMult = 1f;

        public static RecipeProperties Get(Def def)
        {
            return def.GetModExtension<RecipeProperties>();
        }
    }
}
