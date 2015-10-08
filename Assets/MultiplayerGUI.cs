﻿// <copyright file="MainMenuGui.cs" company="Google Inc.">
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

public class  MultiplayerGUI: BaseGui
{
    WidgetConfig TitleCfg = new WidgetConfig(0.0f, -0.2f, 1.0f, 0.2f, 100, "Penaltis");
    WidgetConfig QuickMatchCfg = new WidgetConfig(0.0f, -0.1f, 0.8f, 0.1f, 60, "Juego rapido");
    WidgetConfig InviteCfg = new WidgetConfig(0.0f, 0.0f, 0.8f, 0.1f, 60, "Invitar");
    WidgetConfig InboxCfg = new WidgetConfig(0.0f, 0.1f, 0.8f, 0.1f, 60, "ver invitaciones");
    WidgetConfig SignOutCfg = new WidgetConfig(WidgetConfig.WidgetAnchor.Bottom, 0.2f, -0.05f, 0.4f, 0.1f,
                                  TextAnchor.MiddleCenter, 45, "Back");

    public void Start()
    {
        // no op
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

        if (GuiButton(QuickMatchCfg))
        {
            GameConector.CreateQuickGame();
            //gameObject.GetComponent<RaceGui>().MakeActive();
        }
        else if (GuiButton(InviteCfg))
        {
            GameConector.CreateWithInvitationScreen();
            //gameObject.GetComponent<RaceGui>().MakeActive();
        }
        else if (GuiButton(InboxCfg))
        {
            GameConector.AcceptFromInbox();
            //gameObject.GetComponent<RaceGui>().MakeActive();
        }
        else if (GuiButton(SignOutCfg))
        {
            DoBack();
        }
    }

    void DoBack()
    {
        gameObject.GetComponent<MainMenuGui>().MakeActive();
    }
}
