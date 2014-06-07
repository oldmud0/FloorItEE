using System;
using UnityEngine;

namespace FloorIt
{
	/// <summary>
	/// Defines fields that triggers will use.
	/// </summary>
	public class Trigger
	{
		public readonly KeyCode currentKey;
		public readonly string name, description, nameAsStoredInConfigFile;
		public bool enabled;
		
		public const string defaultDescription = "N/A";
		
		public delegate void Fire();
		public readonly Fire fireEvent;
		
		public delegate void Init();
		private Init initEvent = delegate {return;};
		public Init InitEvent {get {return initEvent;} set {initEvent();}}
		
		public Trigger(string name, KeyCode defaultKey, Fire fireEvent, string description = defaultDescription) {
			this.name = name;
			//this.nameAsStoredInConfigFile = name + (description != defaultDescription ? (" ("+ description +")")); //NAME (description if available)
			this.nameAsStoredInConfigFile = name; //NAME;
			this.enabled = FloorItPlugin.configFile.GetValue(nameAsStoredInConfigFile + "_ENABLED", true);
			this.currentKey = FloorItPlugin.configFile.GetValue(nameAsStoredInConfigFile, defaultKey);
			this.fireEvent = fireEvent;
			this.description = description;
		}
		
		protected internal void Listen() {
			if(enabled && Input.GetKeyDown(currentKey))
				fireEvent();
		}
		
	}
}
