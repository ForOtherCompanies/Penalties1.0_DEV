using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class exitGUI : BaseGui
{
    WidgetConfig TitleCfg = new WidgetConfig(0.0f, -0.1f, 1.0f, 0.2f, 70, "¿Está seguro que quiere salir?");

    WidgetConfig YesCfg = new WidgetConfig(-0.15f, 0.05f, 0.1f, 0.05f, 40, "SI");

    WidgetConfig NoCfg = new WidgetConfig(0.15f, 0.05f, 0.1f, 0.05f, 40, "NO");

    public void Start()
    {

    }

    public void Update()
    {
        // if an invitation arrived, switch to the "invitation incoming" GUI
        // or directly to the game, if the invitation came from the notification
        Invitation inv = InvitationManager.Instance.Invitation;
        if (inv != null)
        {
            if (InvitationManager.Instance.ShouldAutoAccept)
            {
                // jump straight into the game, since the user already indicated
                // they want to accept the invitation!
                InvitationManager.Instance.Clear();
                RaceManager.AcceptInvitation(inv.InvitationId);
                gameObject.GetComponent<RaceGui>().MakeActive();
            }
            else
            {
                // show the "incoming invitation" screen
                gameObject.GetComponent<IncomingInvitationGui>().MakeActive();
            }
        }
    }

    protected override void DoGUI()
    {
        GuiLabel(TitleCfg);


        if (GuiButton(NoCfg))
        {
            gameObject.GetComponent<MainMenuGui>().MakeActive();
        }
        else if (GuiButton(YesCfg) || Input.GetKey(KeyCode.Escape))
        {
            DoSignOut();
            Application.Quit();
        }
    }
    void DoSignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
}
