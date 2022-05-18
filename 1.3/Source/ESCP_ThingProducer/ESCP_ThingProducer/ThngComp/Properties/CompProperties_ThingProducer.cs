using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace ESCP_ThingProducer
{
    public class CompProperties_ThingProducer : CompProperties
	{
		public CompProperties_ThingProducer()
		{
			this.compClass = typeof(Comp_ThingProducer);
		}

		public List<Item> items;			//list of the item class, as seen below
		public bool random = true;			//generates a single item from the list, otherwise generates them all
		public int timeRequired = 300000;   //desired days * 60,000, defaulting to 5 days
		public bool displayMessage = true;	//displays a message when something os produced

		/* requirements for progress to be made */
		public bool requireFuel = false;	//requires the building to have the fueled comp
		public bool requirePower = false;	//requires the building to have the power comp
		public bool onlyUnroofed = false;	//doesn't progress if under a roof, if enabled
		public bool restAtNight = false;	//doesn't progress at night, if enabled
		public bool inTempRange = false;	//only progresses within the below range, if enabled
		public FloatRange viableTempRange = new FloatRange(10, 35);	//THIS IS IN CELSIUS CAUSE FARENHEIT IS STUPID, also Tynan uses celsius so fuck you Rick
		public List<WeatherDef> disabledWeathers;
		public bool invertWeathers = false; //inverts disabledWeathers, progress is now only made during the listed weather

	}

    public class Item
    {
		public ThingDef thingDef;
		public IntRange countRange;
		public int weight = 1;
		public ThingDef stuffDef = null;
    }
}
