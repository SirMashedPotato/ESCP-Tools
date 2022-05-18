using Verse;
using RimWorld;
using System.Reflection;
using System.Linq;

namespace ESCP_BoneChitinFat
{

    [StaticConstructorOnStartup]
    public class ExtraButcherAddonsWorker
    {
        static ExtraButcherAddonsWorker()
        {
            bool logFlag = ESCP_BoneChitinFat_Mod.ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart();

            if (logFlag)
            {
                Log.Message("ESCP Tools: Beggining patching Bone, Chitin, Fat");
            }

            DefDatabase<ThingDef>.AllDefsListForReading.ForEach(action: def =>
            {
                if (def.race != null)
                {
                    var props = ExtraButcherProperties.Get(def);
                    bool flag = props != null;
                    
                    //Chitin
                    if (def.race.leatherDef == null)
                    {
                        if ((flag && props.hasChitin) || !flag && (def.race.FleshType == FleshTypeDefOf.Insectoid || def.race.useMeatFrom?.ToString() == "Megaspider" || (def.tradeTags != null && def.tradeTags.Contains("AnimalInsect"))))
                        {
                            def.race.leatherDef = ThingDefOf.ESCP_Item_Chitin;
                            float f = def.GetStatValueAbstract(StatDefOf.MeatAmount) / 5;
                            def.SetStatBaseValue(StatDefOf.LeatherAmount, f);
                            if (logFlag)
                            {
                                Log.Message("ESCP Tools: Assigned Chitin to: " + def + ", amount = " + f);
                            }
                        }
                       
                    }
                    //Bone
                    if ((def.butcherProducts == null || !def.butcherProducts.Where(x=>x.thingDef == ThingDefOf.ESCP_Item_Bone).Any()))
                    {
                        if ((flag && props.hasBone) || !flag && (def.race.IsFlesh && def.race?.FleshType != FleshTypeDefOf.Insectoid && def.race.useMeatFrom?.ToString() != "Megaspider" && (def.tradeTags == null || !def.tradeTags.Contains("AnimalInsect"))))
                        {
                            if (def.butcherProducts == null)
                            {
                                def.butcherProducts = new System.Collections.Generic.List<ThingDefCountClass> { };
                            }
                            float f = def.GetStatValueAbstract(StatDefOf.MeatAmount) / 5;
                            ThingDefCountClass bone = new ThingDefCountClass(ThingDefOf.ESCP_Item_Bone, (int)f);
                            def.butcherProducts.Add(bone);
                            if (logFlag)
                            {
                                Log.Message("ESCP Tools: Assigned Bone to: " + def + ", amount = " + f);
                            }
                        }

                    }

                    //Fat
                    if (def.butcherProducts == null || !def.butcherProducts.Where(x => x.thingDef == ThingDefOf.ESCP_Item_Fat).Any())
                    {
                        if ((flag && props.hasFat) || !flag && def.race.IsFlesh)
                        {
                            if (def.butcherProducts == null)
                            {
                                def.butcherProducts = new System.Collections.Generic.List<ThingDefCountClass> { };
                            }
                            float f = def.GetStatValueAbstract(StatDefOf.MeatAmount) / 9;
                            ThingDefCountClass fat = new ThingDefCountClass(ThingDefOf.ESCP_Item_Fat, (int)f);
                            def.butcherProducts.Add(fat);
                            if (logFlag)
                            {
                                Log.Message("ESCP Tools: Assigned Fat to: " + def + ", amount = " + f);
                            }
                        }

                    }

                }

            });
        }
    }
}
