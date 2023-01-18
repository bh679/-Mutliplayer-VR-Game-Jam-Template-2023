/* Brennan Hatton - 2022
Sends a message to a discord webhook link
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace BrennanHatton.Discord
{
	public class DiscordWebhook : MonoBehaviour
	{
		public string webhook_link = "https://discord.com/api/webhooks/1032136544213024778/tx4KgBHPS0b_v4OsQAAzDZXmVVba2APe8aW33F96nONzcz-nkQCpJqWXZX5L6WqApjqd";
		
		public bool startMessage = true;
	
	    void Start()
		{
			if(startMessage)
		    	SendMessage("App Started");
	    }
	    
		public void SendMessage(string message)
		{
			StartCoroutine(SendWebhook(webhook_link, message, (success) =>
			{
				if (success)
					Debug.Log("Message Sent");
			}));
		}
	
	    IEnumerator SendWebhook(string link, string message, System.Action<bool> action)
	    {
	        WWWForm form = new WWWForm();
	        form.AddField("content", message);
	        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
	        {
	            yield return www.SendWebRequest();
	
	            if (www.isNetworkError || www.isHttpError)
	            {
	                Debug.Log(www.error);
	                action(false);
	            }
	            else
	                action(true);
	        }
	    }
	}
}