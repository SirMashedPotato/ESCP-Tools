﻿using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using RimWorld.Planet;

namespace ESCP_Misc
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.ESCP_Tools_Misc");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
