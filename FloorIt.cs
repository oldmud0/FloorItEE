/*
 * FloorIt Enterprise Edition
 */
 
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KSP;
using UnityEngine;

namespace FloorIt
{
	/// <summary>
	/// Main class of FloorIt. This inherits MonoBehaviour which the FloorIt assembly a legitimate plugin.
	/// </summary>
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class FloorItPlugin : MonoBehaviour
	{
		internal List<Trigger> triggerList = new List<Trigger>();
		internal static KSP.IO.PluginConfiguration configFile;
		
		public void Awake() {
			
			//First, we need to look for the FloorIt slim plugin. It conflicts with this one. We'll throw an exception for it.
			if( Array.Find(AppDomain.CurrentDomain.GetAssemblies(), item => item.FullName == "FloorIt") != null ) {
				print("ERROR: FloorIt slim edition found. Initialization aborted.");
				throw new Exception("FloorIt slim edition found.");
			}
			
			configFile = KSP.IO.PluginConfiguration.CreateForType<FloorItPlugin>();
			configFile.load();
			
			addTriggers();
		}
		
		public void Start() {
			string mesg = "";
			
			mesg += "FloorIt Enterprise Edition enabled.\nLoaded commands: ";
			foreach(Trigger trigger in triggerList) 
				mesg += trigger.name +" - "+ trigger.description + " ("+trigger.currentKey+") \n";
			print(mesg);
		}
		
		void OnDisable() {
			configFile.save();
		}
		
		void Update() {
			if(Input.anyKeyDown)
				foreach(Trigger trigger in triggerList) 
					trigger.Listen();
		}
		
		void addTriggers() {
			triggerList.AddRange( new[] {
				new Trigger("THROTTLE_TO_100", KeyCode.Z, 		      () => FlightInputHandler.state.mainThrottle = 1,                                              "Throttle to 100%"        ),
				new Trigger("INCREMENT_BY_X",  KeyCode.KeypadPlus,    () => FlightInputHandler.state.mainThrottle += configFile.GetValue("INCREMENT_AMOUNT", .25f), "Increment throttle by X" ),
				new Trigger("DECREMENT_BY_X",  KeyCode.KeypadEnter,   () => FlightInputHandler.state.mainThrottle -= configFile.GetValue("DECREMENT_AMOUNT", .25f), "Decrement throttle by X" ),
				new Trigger("SET_CUSTOM_1",    KeyCode.KeypadDivide,  () => FlightInputHandler.state.mainThrottle -= configFile.GetValue("CUSTOM_1_VALUE", .25f),   "Set custom value (key 1)"),
				new Trigger("SET_CUSTOM_2",    KeyCode.KeypadMultiply,() => FlightInputHandler.state.mainThrottle -= configFile.GetValue("CUSTOM_2_VALUE", .5f),    "Set custom value (key 2)"),
				new Trigger("SET_CUSTOM_3",    KeyCode.KeypadMinus,   () => FlightInputHandler.state.mainThrottle -= configFile.GetValue("CUSTOM_3_VALUE", .75f),   "Set custom value (key 3)")
                });
		}
		
	}
}