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
			print("FloorIt Enterprise Edition enabled.\nLoaded commands: ");
			foreach(Trigger trigger in triggerList) 
				print(trigger.name +" - "+ trigger.description + " ("+trigger.currentKey+") \n");
		}
		
		void Update() {
			foreach(Trigger trigger in triggerList) trigger.Listen();
		}
		
		void OnDisable() {
			configFile.save();
		}
		
		void addTriggers() {
			triggerList.AddRange( new[] {
				new Trigger("THROTTLE_TO_100", configFile.GetValue("THROTTLE_TO_100",KeyCode.Z), delegate {FlightInputHandler.state.mainThrottle = 1;}, "Throttle to 100%")
			                     });
		}
		
	}
}