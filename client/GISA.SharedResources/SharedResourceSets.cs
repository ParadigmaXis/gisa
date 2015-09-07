using System;
using System.Drawing;
using System.Resources;
using System.Collections;
using System.Diagnostics;

namespace GISA.SharedResources
{
	/// <summary>
	/// Summary description for SharedResourceSets.
	/// </summary>
	public class SharedResourceSets
	{
		private Hashtable resourceSets;
		private Hashtable resourceManagers;

		#region Singleton pattern
		private SharedResourceSets() : base()
		{			
			resourceSets = new Hashtable();
		}

		private static SharedResourceSets CurrentSharedResourceSetsInstance;
		public static SharedResourceSets CurrentSharedResourceSets 
		{
			get 
			{
				if ((CurrentSharedResourceSetsInstance == null)) 
				{
					CurrentSharedResourceSetsInstance = new SharedResourceSets();
				}
				return CurrentSharedResourceSetsInstance;
			}
		}
		#endregion		

		private object getResource(Type setClass, string resourceName)
		{
			Hashtable resourceSet = null;
			resourceSet = ((Hashtable)(resourceSets[setClass]));
			if (resourceSet == null) 
			{
				resourceSet = new Hashtable();
				resourceSets.Add(setClass, resourceSet);
			}
			object resource = null;
			resource = resourceSet[resourceName];
			if (resource == null) 
			{
				ResourceManager resourceMan = null;
				if (resourceManagers == null) 
				{
					resourceManagers = new Hashtable();
				}
				resourceMan = ((ResourceManager)(resourceManagers[setClass]));
				if (resourceMan == null) 
				{
					resourceMan = new System.Resources.ResourceManager(setClass);
					resourceManagers.Add(setClass, resourceMan);
				}
				resource = resourceMan.GetObject(resourceName, System.Globalization.CultureInfo.CurrentCulture);
			}
			return resource;
		}

		public Bitmap getImageResource(Type setClass, string resourceName)
		{
			Bitmap resource;
			try 
			{
				resource = ((Bitmap)(getResource(setClass, resourceName)));
			} 
			catch (MissingManifestResourceException ex) 
			{
				Trace.WriteLine(ex.ToString());
				resource = new Bitmap(32, 32);
				// TODO: assign default bitmap image instead of assigning a new empty one
			}
			return resource;
		}

		public string getTextResource(Type setClass, string resourceName)
		{
			string resource;
			try 
			{
				resource = ((string)(getResource(setClass, resourceName)));
			} 
			catch (MissingManifestResourceException ex) 
			{
				Trace.WriteLine(ex.ToString());
				resource = string.Empty;
			}
			return resource;
		}	
	}
}