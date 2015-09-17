using UnityEngine;
using System.Collections;

public interface MPLobbyListener {
	void SetLobbyStatusMessage(string message);
	void HideLobby();
}

public class MPController : MonoBehaviour, MPLobbyListener {
	public GUISkin guiSkin;
	private bool _showLobbyDialog;
	private string _lobbyMessage;

	public void SetLobbyStatusMessage(string message) {
		_lobbyMessage = message;
	}
	public void HideLobby() {
		_lobbyMessage = "";
		_showLobbyDialog = false;
	}
	// Use this for initialization
	void Start () {
		MPmanager.Instance.SignInAndStartMPGame ();
	}
}
