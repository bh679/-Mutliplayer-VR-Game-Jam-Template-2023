using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace EqualReality.Networking.Events
{
	
	/*public class ReceiveNameChangeEvent : MonoBehaviour, IOnEventCallback
	{
		public UnityEvent onReceive; 
		
		void Reset()
		{
		}
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
	
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			
			if(eventCode == SendEventManager.BecomeFacilitatorEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				
				
				onReceive.Invoke();
			}
		}
	}*/

}
