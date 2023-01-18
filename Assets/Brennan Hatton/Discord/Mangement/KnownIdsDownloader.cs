/*
Brennan Hatton - 2022
Pulls list of devices from server
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Discord
{
	[System.Serializable]
	public class JsonDataType
	{
		public KnownIds[] ids;
	}
	
	public class KnownIdsDownloader : MonoBehaviour
	{
		public ClassDiscordConnection idManager;
		public string json;
		public JsonDataType jsonData;
		
		public bool updateJson = false;//should be readonly getter for public
		
		public const string rootURL = "https://equalreality.com/Licensing/"; ///opt/bitnami/apps/wordpress/htdocs/
		public const string JSONFilename = "knownDevices.txt";
		
		void Reset()
		{
			idManager = this.GetComponent<ClassDiscordConnection>();
			
			jsonData = new JsonDataType();
			jsonData.ids = idManager.ids.ToArray();
			
			json = JsonUtility.ToJson(jsonData, true);
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			StartCoroutine(getJSON());
		}
	    
		IEnumerator getJSON()
		{
			updateJson = false;
			int waitTmer = 0;
			
			while(json == null)
			{
				yield return new WaitForSeconds(waitTmer);
				
				using(WWW web = new WWW(rootURL+JSONFilename))
				{
					yield return web;
			    	
					json = web.text;
				}
				
				waitTmer += 1 + waitTmer* 2;
			}
			
			jsonData = JsonUtility.FromJson<JsonDataType>(json);
			
			//loaded = Time.time;
			updateJson = true;
			
			idManager.ids = new List<KnownIds>();
			for(int i = 0; i < jsonData.ids.Length; i++)
			{
				idManager.ids.Add(jsonData.ids[i]);
			}
				
		}
	}
}