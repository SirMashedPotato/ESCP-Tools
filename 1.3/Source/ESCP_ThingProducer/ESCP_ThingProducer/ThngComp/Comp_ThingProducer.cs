using System.Collections.Generic;
using System.Linq;
using RimWorld;
using System;
using System.Text;
using UnityEngine;
using Verse;

namespace ESCP_ThingProducer
{
    public class Comp_ThingProducer : ThingComp
    {
        /* BASIC SHIT */

        public CompProperties_ThingProducer Props
        {
            get
            {
                return (CompProperties_ThingProducer)this.props;
            }
        }

        private int ticksUntilDone;

        private string pausedReason;

        public override void PostPostMake()
        {
            base.PostPostMake();
            ticksUntilDone = 0;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref ticksUntilDone, "ticksUntilDone", 0);
        }

        /* Big boy check stuff */

        public bool CanProgress()
        {
            int flags = 0;

            if (IsMinified)
            {
                return false;
            }

            if(Props.onlyUnroofed && IsRoofed)
            {
                flags++;
                pausedReason = "ESCP_Tools_ThingProducer_Reason_roofed".Translate();
            }

            if (Props.restAtNight && Resting)
            {
                flags++;
                pausedReason = "ESCP_Tools_ThingProducer_Reason_time".Translate();
            }

            if (Props.inTempRange && !Props.viableTempRange.Includes(Temperature))
            {
                flags++;
                pausedReason = "ESCP_Tools_ThingProducer_Reason_temperature".Translate();
            }

            if (Props.disabledWeathers != null && 
                ((!Props.invertWeathers && Props.disabledWeathers.Contains(Weather)) || (Props.invertWeathers && !Props.disabledWeathers.Contains(Weather))))
            {
                flags++;
                pausedReason = "ESCP_Tools_ThingProducer_Reason_weather".Translate();
            }

            if (Props.requireFuel)
            {
                CompRefuelable comp = this.parent.GetComp<CompRefuelable>();
                if (comp != null && !comp.HasFuel)
                {
                    flags++;
                    pausedReason = "ESCP_Tools_ThingProducer_Reason_fuel".Translate();
                }
            }

            if (Props.requirePower)
            {
                CompPowerTrader comp = this.parent.GetComp<CompPowerTrader>();
                if (comp != null && !comp.PowerOn)
                {
                    flags++;
                    pausedReason = "ESCP_Tools_ThingProducer_Reason_power".Translate();
                }
            }

            if (flags > 1)
            {
                pausedReason += " + " + (flags - 1);
            }

            return flags == 0;
        }

        /* The part that does stuff */

        public void Produce()
        {
            if (!Props.items.NullOrEmpty())
            {
                if (Props.random)
                {
                    Spawn(Props.items.RandomElementByWeight(x => x.weight));
            }
                else
                {
                    foreach(ESCP_ThingProducer.Item i in Props.items)
                    {
                        Spawn(i);
                    }
                }
            }
            if (Props.displayMessage)
            {
                Messages.Message("ESCP_Tools_ThingProducer_Message".Translate(this.parent), this.parent, MessageTypeDefOf.PositiveEvent, false);
            }
        }

        public void Spawn(ESCP_ThingProducer.Item item)
        {
            Thing thing = ThingMaker.MakeThing(item.thingDef, item.stuffDef);
            thing.stackCount = item.countRange.RandomInRange;
            GenPlace.TryPlaceThing(thing, this.parent.Position, this.parent.Map, ThingPlaceMode.Near, null, null, default(Rot4));
        }

        /* TICKS 
         * set up so that any can be used
         * Do note that if the building uses fuel, you want to use the regular Ticker type
         */

        public override void CompTick()
        {
            base.CompTick();
            if (CanProgress())
            {
                if (ticksUntilDone >= Props.timeRequired)
                {
                    ticksUntilDone = 0;
                    Produce();
                }
                ticksUntilDone++;
            }
        }

        public override void CompTickRare()
        {
            base.CompTickRare();
            if (CanProgress())
            {
                if (ticksUntilDone >= Props.timeRequired)
                {
                    ticksUntilDone = 0;
                    Produce();
                }
                ticksUntilDone += 250;
            }
        }

        public override void CompTickLong()
        {
            base.CompTickLong();
            if (CanProgress())
            {
                if (ticksUntilDone >= Props.timeRequired)
                {
                    ticksUntilDone = 0;
                    Produce();
                }
                ticksUntilDone += 2000;
            }
        }

        /* GETTERS */

        public override string CompInspectStringExtra() => CanProgress()
            ? "ESCP_Tools_ThingProducer_Progress".Translate(((float)ticksUntilDone / (float)Props.timeRequired).ToStringPercent())
            : IsMinified ? null : "ESCP_Tools_ThingProducer_Paused".Translate(pausedReason);

        protected virtual bool IsMinified => !this.parent.Spawned;  //may need to check something else
        protected virtual bool IsRoofed => this.parent.Position.Roofed(this.parent.Map);
        protected virtual bool Resting => GenLocalDate.DayPercent(this.parent) < 0.25f || GenLocalDate.DayPercent(this.parent) > 0.8f;
        protected virtual float Temperature => !GenTemperature.TryGetTemperatureForCell(this.parent.Position, this.parent.Map, out float cellTemp) ? 20f : cellTemp;
        protected virtual WeatherDef Weather => this.parent.Map.weatherManager.curWeather;

        /* DEBUG */

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Set progress to 0",
                    action = delegate ()
                    {
                        ticksUntilDone = 0;
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Set progress to max",
                    action = delegate ()
                    {
                        ticksUntilDone = Props.timeRequired;
                    }
                };
            }
            yield break;
        }

    }
}
