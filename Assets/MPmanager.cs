using UnityEngine;
using System.Collections;


using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class MPmanager : RealTimeMultiplayerListener {
	private static MPmanager _instance = null;
	private uint minimumOpponents = 1;
	private uint maximumOpponents = 1;
	private uint gameVariation = 0;
	public MPLobbyListener LobbyListener;

	public static MPmanager Instance {
		get {
			if (_instance == null) {
				_instance = new MPmanager();
			}
			return _instance;
		}
	}

	private MPmanager() {
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

	public void SignOut() {
		PlayGamesPlatform.Instance.SignOut ();
	}
	
	public bool IsAuthenticated() {
		return PlayGamesPlatform.Instance.localUser.authenticated;
	}

	private void StartMatchMaking() {
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
			Application.LoadLevel("MainGame");
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
		ShowMPStatus ("We have received some gameplay messages from participant ID:" + senderId);
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
}
