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
		public TextAsset webhook;
		string webhook_link;
		
		public bool startMessage = true;
	
	    void Start()
		{
			webhook_link = webhook.text;
			
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