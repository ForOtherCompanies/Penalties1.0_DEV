using UnityEngine;
using System.Collections;

public interface MPLobbyListener {
	void SetLobbyStatusMessage(string message);
	void HideLobby();
	void ShowLobby ();
}

public class MPController : MonoBehaviour, MPLobbyListener {
	public GUISkin guiSkin;
	private bool _showLobbyDialog;
	private string _lobbyMessage;

	public void ShowLobby (){
		_showLobbyDialog = true;
	}

	public void SetLobbyStatusMessage(string message) {
		_lobbyMessage = message;
	}
	public void HideLobby() {
		_lobbyMessage = "";
		_showLobbyDialog = false;
	}
	// Use this for initialization
	void Start () {
		if (!MPmanager.Instance.IsAuthenticated()) {
			MPmanager.Instance.SignInAndStartMPGame ();
		} else {
			MPmanager.Instance.TrySilentSignIn();
		}
	}
	void Update(){
		if (_showLobbyDialog) {
			GUI.skin = guiSkin;
			GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), _lobbyMessage);
		}
	}
}
