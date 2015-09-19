using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class MPmanager : RealTimeMultiplayerListener {
	private static MPmanager _instance = null;
	private uint minimumOpponents = 1;
	private uint maximumOpponents = 1;
	private uint gameVariation = 0;
	public MPLobbyListener LobbyListener;
	public GameModeManager modoJuego;
	public MPUpdateListener updateListener;

	private byte _protocolVersion = 1;
	// Byte + Byte + 2 floats for position + 2 floats for velcocity + 1 float for rotZ
	//cada float es 4 bytes
	private int _updateMessageLength = 26;
	private List<byte> _updateMessage;

	public static MPmanager Instance {
		get {
			if (_instance == null) {
				_instance = new MPmanager();
			}
			return _instance;
		}
	}

	private MPmanager() {
		_updateMessage = new List<byte>(_updateMessageLength);
	
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();
	}

	public void SignInAndStartMPGame() {
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => {
				if (success) {
					Debug.Log ("We're signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
					// We could start our game now
				} else {
					Debug.Log ("Oh... we're not signed in.");
				}
			});
		} else {
			Debug.Log ("You're already signed in.");
			// We could also start our game now
		}
	}

	public void TrySilentSignIn() {
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.Authenticate ((bool success) => {
				if (success) {
					Debug.Log ("Silently signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
				} else {
					Debug.Log ("Oh... we're not signed in.");
				}
			}, true);
		} else {
			Debug.Log("We're already signed in");
		}
	}

	public void ComenzarMultiPlayer(){
		StartMatchMaking ();
	}

	public void SignOut() {
		PlayGamesPlatform.Instance.SignOut ();
	}
	
	public bool IsAuthenticated() {
		return PlayGamesPlatform.Instance.localUser.authenticated;
	}

	private void StartMatchMaking() {
		LobbyListener.ShowLobby ();
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame (minimumOpponents, maximumOpponents, gameVariation, this);
	}
	
	public void OnRoomSetupProgress (float percent)
	{
		ShowMPStatus ("We are " + percent + "% done with setup");
	}
	
	public void OnRoomConnected (bool success)
	{
		if (success) {
			LobbyListener.HideLobby();
			LobbyListener = null;
			modoJuego.ActivateMultplayer();
			ShowMPStatus ("We are connected to the room! I would probably start our game now.");
		} else {
			ShowMPStatus ("Uh-oh. Encountered some error connecting to the room.");
		}
	}
	
	public void OnLeftRoom ()
	{
		ShowMPStatus ("We have left the room. We should probably perform some clean-up tasks.");
	}
	
	public void OnPeersConnected (string[] participantIds)
	{
		foreach (string participantID in participantIds) {
			ShowMPStatus ("Player " + participantID + " has joined.");
		}
	}
	
	public void OnPeersDisconnected (string[] participantIds)
	{
		foreach (string participantID in participantIds) {
			ShowMPStatus ("Player " + participantID + " has left.");
		}
	}
	
	public void OnRealTimeMessageReceived (bool isReliable, string senderId, byte[] data)
	{
		// We'll be doing more with this later...
		byte messageVersion = (byte)data[0];
		// Let's figure out what type of message this is.
		char messageType = (char)data[1];
		Vector3 posicion, velocidad;
		if (messageType == 'U' && data.Length == _updateMessageLength) { 
			float posX = System.BitConverter.ToSingle(data, 2);
			float posY = System.BitConverter.ToSingle(data, 6);
			float posZ = System.BitConverter.ToSingle(data, 10);
			float velX = System.BitConverter.ToSingle(data, 14);
			float velY = System.BitConverter.ToSingle(data, 18);
			float velZ = System.BitConverter.ToSingle(data, 22);
			Debug.Log ("Player " + senderId + " is at (" + posX + ", " + posY +", "+posZ+ ") " +
			           "traveling (" + velX + ", " + velY +", "+velZ+ ")");
			// We'd better tell our GameController about this.
			posicion = new Vector3(posX,posY,posZ);
			velocidad = new Vector3(velX,velY,velZ);
			if (updateListener != null) {
				updateListener.UpdateReceived(senderId, posicion, velocidad);
			}
		}
	}
	
	public void OnParticipantLeft (Participant participant)
	{
		ShowMPStatus ("Player " + participant.ParticipantId + " has left.");
	}
	
	private void ShowMPStatus(string message) {
		Debug.Log(message);
		if (LobbyListener != null) {
			LobbyListener.SetLobbyStatusMessage(message);
		}
	}

	public List<Participant> GetAllPlayers() {
		return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
	}

	public string GetMyParticipantId() {
		return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
	}

	public void SendMyUpdate(Vector3 posicion, Vector3 velocity) {
		_updateMessage.Clear ();
		_updateMessage.Add (_protocolVersion);
		_updateMessage.Add ((byte)'U');
		_updateMessage.AddRange (System.BitConverter.GetBytes (posicion.x));  
		_updateMessage.AddRange (System.BitConverter.GetBytes (posicion.y));  
		_updateMessage.AddRange (System.BitConverter.GetBytes (posicion.z));
		_updateMessage.AddRange (System.BitConverter.GetBytes (velocity.x));
		_updateMessage.AddRange (System.BitConverter.GetBytes (velocity.y));
		_updateMessage.AddRange (System.BitConverter.GetBytes (velocity.z));
		byte[] messageToSend = _updateMessage.ToArray(); 
		Debug.Log ("Sending my update message  " + messageToSend + " to all players in the room");
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true, messageToSend);
	}

}
