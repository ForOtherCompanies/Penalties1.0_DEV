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

public class gameUI : BaseGui
{

    public MacthController mController;
    public Info informacion;
    WidgetConfig TiradorCfg = new WidgetConfig(-0.35f, -0.275f, 1.0f, 0.2f, 60,"");

    WidgetConfig PorteroCfg = new WidgetConfig(0.2f, -0.275f, 1.0f, 0.2f, 60, "Com");


    WidgetConfig[] GolTiradorCfg = new WidgetConfig[5];
    WidgetConfig[] GolPorteroCfg = new WidgetConfig[5];
    int[] estadoJuegoT = new int[5];
    int[] estadoJuegoP = new int[5];

    WidgetConfig SignOutCfg = new WidgetConfig(WidgetConfig.WidgetAnchor.Bottom, 0.45f, -0.05f, 0.1f, 0.05f,
                                  TextAnchor.MiddleCenter, 30, "Back");


    public void Start()
    {
        float posT = -0.45f;
        float posP = 0.1f;
        float incremento = 0.075f;
        for (int i = 0; i < 5; ++i)
        {
            GolTiradorCfg[i] = new WidgetConfig(posT, -0.2f, 0.07f, 0.07f, 0, "");
            GolPorteroCfg[i] = new WidgetConfig(posP, -0.2f, 0.07f, 0.07f, 0, "");
            estadoJuegoP[i] = 0;
            estadoJuegoT[i] = 0;
            posT += incremento;
            posP += incremento;

        }
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
        GuiLabel(TiradorCfg, informacion.GetNombre());
        GuiLabel(PorteroCfg);

        for (int i = 0; i < 5; ++i)
        {
            GuiPelota(GolTiradorCfg[i], estadoJuegoT[i]);
            GuiPelota(GolPorteroCfg[i], estadoJuegoP[i]);
        }

        if (GuiButton(SignOutCfg) || Input.GetKey(KeyCode.Escape))
        {
            DoBack();
        }
    }

    public void DoBack()
    {
        gameObject.GetComponent<TrainingGUI>().MakeActive();
        gameObject.GetComponentInParent<CameraController>().DesactivarGameCamera();
        mController.Desactivar();
    }

    //tirador significa si soy o no el tirador
    //acierto para portero significa parada
    public void AccionGol(bool acierto, int fase, bool tirador)
    {
        if (tirador)
        {
            if (acierto)
            {
                estadoJuegoT[fase] = 2;
            }
            else
            {

                estadoJuegoT[fase] = 1;
            }
        }
        else
        {
            if (acierto)
            {
                estadoJuegoP[fase] = 2;
            }
            else
            {

                estadoJuegoP[fase] = 1;
            }
        }
    }
}
