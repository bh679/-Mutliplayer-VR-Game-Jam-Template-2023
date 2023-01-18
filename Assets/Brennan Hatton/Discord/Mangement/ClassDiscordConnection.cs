/*
Brennan Hatton 18-01-2023

Sends formatted message to discord webhooks with device info.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace BrennanHatton.Discord
{

	[System.Serializable]
	public class KnownIds
	{
		public string id, name, contact;
		public bool debug = true;
	}
	
	public class ClassDiscordConnection : MonoBehaviourPunCallbacks
	{
		//Public and private webhooks
		public DiscordWebhook webhook, debugWebHook;
		
		//Singlton
		public static ClassDiscordConnection Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
	    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
		//names of known devices
		public List<KnownIds> ids;
		
		//formatted information of this device and application
		string deviceData;
		
		int id = -2;
		
		public string USER_STR = "Learner", JOINED_MESSAGE = "joined the metaverse.";
		
		void Start()
		{
			//set formatted information of this device and application
			deviceData = "   ``Platform: "+Application.platform.ToString() +".   App V: "+ Application.version+"`` ";
		}
		
		//get id id device in known ids
		public int GetId(string _id)
		{
			for(int i =0; i < ids.Count; i++)
			{
				if(ids[i].id == _id)
					return i;
			}
			
			return -1;
		}
		
		//get name from known ids
		public string GetName()
		{
			if(id <= -1)
				return "A "+USER_STR;
			else if(ids.Count > 0)
				return "**"+ids[id].name + "**";
			else
				return "id not found";
		}
		
		//send a message
		//it auto adds device info
		public void SendMessage(string message)
		{
			//if device info is saved, and marked as debug device
			if(id >= 0 && ids[id].debug)
				debugWebHook.SendMessage(GetName() + " " + message + deviceData + GetClassData());
			else //send as public hook
				webhook.SendMessage(GetName() + " " + message + deviceData + GetClassData());
		}
		
		//get information about current players
		public string GetClassData()
		{
			string returnData = "```"+PhotonNetwork.PlayerList.Length.ToString() + " "+USER_STR+"s:";
			
			for(int i = 0 ; i < PhotonNetwork.PlayerList.Length; i++)
			{
					
				returnData += "\n"+PhotonNetwork.PlayerList[i].ActorNumber
					+" : "+PhotonNetwork.PlayerList[i].NickName;
					
				if(PhotonNetwork.PlayerList[i].IsLocal)
					returnData += " - id:" + SystemInfo.deviceUniqueIdentifier;
			}
			
			return returnData+"```";
		}
		
		public override void OnJoinedRoom()
		{
			
			base.OnJoinedRoom();
			
			if(id == -2)
				id = GetId(SystemInfo.deviceUniqueIdentifier);
			
			SendMessage(JOINED_MESSAGE);
			
		}
		
	}
}