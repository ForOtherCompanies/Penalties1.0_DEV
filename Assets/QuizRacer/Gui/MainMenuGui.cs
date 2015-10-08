// <copyright file="MainMenuGui.cs" company="Google Inc.">
// Copyright (C) 2014 Google Inc.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>

using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.BasicApi;

public class MainMenuGui : BaseGui
{
    WidgetConfig TitleCfg = new WidgetConfig(0.0f, -0.2f, 1.0f, 0.2f, 100, "Penaltis");
    WidgetConfig TrainingCfg = new WidgetConfig(0.0f, -0.1f, 0.8f, 0.1f, 60, "Entrenamiento");
    WidgetConfig MPCfg = new WidgetConfig(0.0f, 0.0f, 0.8f, 0.1f, 60, "Multijugador");
    WidgetConfig VestCfg = new WidgetConfig(0.0f, 0.1f, 0.8f, 0.1f, 60, "Vesturios");
    WidgetConfig SignOutCfg = new WidgetConfig(WidgetConfig.WidgetAnchor.Bottom, 0.2f, -0.05f, 0.4f, 0.1f,
                                  TextAnchor.MiddleCenter, 45, "Sign Out");

    bool mAuthOnStart = true;
    bool conectado = false;
    System.Action<bool> mAuthCallback;

    void Start()
    {
        mAuthCallback = (bool success) =>
        {
            if (success)
            {
                conectado = true;
            }
        };

        var config = new PlayGamesClientConfiguration.Builder()
            .WithInvitationDelegate(InvitationManager.Instance.OnInvitationReceived)
            .Build();

        PlayGamesPlatform.InitializeInstance(config);

        // make Play Games the default social implementation
        PlayGamesPlatform.Activate();


        // enable debug logs (note: we do this because this is a sample; on your production
        // app, you probably don't want this turned on by default, as it will fill the user's
        // logs with debug info).
        PlayGamesPlatform.DebugLogEnabled = true;

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
        // try silent authentication
        if (!PlayGamesPlatform.Instance.IsAuthenticated() && !conectado)
        {
            if (mAuthOnStart)
            {
                PlayGamesPlatform.Instance.Authenticate(mAuthCallback, true);
            }
            else
            {
                PlayGamesPlatform.Instance.Authenticate(mAuthCallback);

            }

        }
    }

    protected override void DoGUI()
    {
        GuiLabel(TitleCfg);

        if (GuiButton(TrainingCfg))
        {
            //GameConector.CreateQuickGame();
            //gameObject.GetComponent<RaceGui>().MakeActive();
        }
        else if (GuiButton(MPCfg))
        {
            //GameConector.CreateWithInvitationScreen();
            gameObject.GetComponent<MultiplayerGUI>().MakeActive();
        }
        else if (GuiButton(VestCfg))
        {
            //GameConector.AcceptFromInbox();
            //gameObject.GetComponent<RaceGui>().MakeActive();
        }
        else if (GuiButton(SignOutCfg))
        {
            DoSignOut();
        }
    }

    void DoSignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        gameObject.GetComponent<WelcomeGui>().SetAuthOnStart(false);
        gameObject.GetComponent<WelcomeGui>().MakeActive();
    }
}
