using System;
using UnityEngine;

namespace FloorIt
{
	/// <summary>
	/// Defines fields that triggers will use.
	/// </summary>
	[Trigger]
	public class Trigger
	{
		public readonly KeyCode currentKey;
		public readonly string name, description;
		
		public delegate void Fire();
		public readonly Fire fireEvent;
		
		public Trigger(string name, KeyCode key, Fire fireEvent, string description = "Unnamed trigger") {
			this.name = name;
			this.currentKey = key;
			this.fireEvent = fireEvent;
			this.description = description;
		}
		
		protected internal void Listen() {
			if(Input.GetKeyDown(currentKey))
				fireEvent();
		}
		
	}
}
