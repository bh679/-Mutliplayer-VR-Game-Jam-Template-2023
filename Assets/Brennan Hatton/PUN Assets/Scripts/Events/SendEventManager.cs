using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace BrennanHatton.LibraryOfBabel.Networking.Events
{
	
	public class SendEventManager : MonoBehaviourPunCallbacks
	{
		// If you have multiple custom events, it is recommended to define them in the used class
		public const byte ChangeAvatarEventCode = 100,
		ControllerButtonPressEventCode = 102,
		NameChangeEventCode = 103;
		
		
	
	
		public static void SendNameChangeEvent()
		{
			Debug.Log("public static void SendChangeScriptEvent ");
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(NameChangeEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendChangeAvatarEvent(int id)
		{
			object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ChangeAvatarEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		public static void SendControllerButtonPress(int buttonId)
		{
			Debug.Log("SendNetworkedButton " + buttonId);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, buttonId}; 
			
			//object[] content = new object[] { id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(ControllerButtonPressEventCode, content, raiseEventOptions, SendOptions.SendReliable);
			
		}
	}

}