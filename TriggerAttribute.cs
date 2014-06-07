using System;
using System.ComponentModel;

namespace FloorIt
{
	/// <summary>
	/// Used for marking features ready to be used by FloorIt and bindable to a key.
	/// This class will automatically place each trigger in a list. Each trigger will also be rebinded based on what the config file says, and they will be listened to in the MonoBehavior.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class TriggerAttribute : Attribute
	{
		public TriggerAttribute()
		{
			
		}
	}
}
