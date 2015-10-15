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

public class VestuarioGUI : BaseGui
{
    WidgetConfig TitleCfg = new WidgetConfig(0.0f, -0.25f, 1.0f, 0.2f, 100, "Vestuario");
    WidgetConfig BoxCfg = new WidgetConfig(0.2f, 0f, 0.4f, 0.4f, 60, "");

    WidgetConfig CabezaCfg = new WidgetConfig(-0.075f, -0.175f, 0.15f, 0.05f, 30, "Cabeza");
    WidgetConfig CaraCfg = new WidgetConfig(-0.075f, -0.125f, 0.15f, 0.05f, 30, "Cara");
    WidgetConfig CamisetaCfg = new WidgetConfig(-0.075f, -0.075f, 0.15f, 0.05f, 30, "Camiseta");
    WidgetConfig CalzonasCfg = new WidgetConfig(-0.075f, -0.025f, 0.15f, 0.05f, 30, "Calzonas");
    WidgetConfig BotasCfg = new WidgetConfig(-0.075f, 0.025f, 0.15f, 0.05f, 30, "Botas");
    WidgetConfig GuantesCfg = new WidgetConfig(-0.075f, 0.075f, 0.15f, 0.05f, 30, "Guantes");
    WidgetConfig PelotaCfg = new WidgetConfig(-0.075f, 0.125f, 0.15f, 0.05f, 30, "Pelota");
    WidgetConfig OtrosCfg = new WidgetConfig(-0.075f, 0.175f, 0.15f, 0.05f, 30, "Otros");
    WidgetConfig SignOutCfg = new WidgetConfig(WidgetConfig.WidgetAnchor.Bottom, 0.2f, -0.05f, 0.4f, 0.1f,
                                  TextAnchor.MiddleCenter, 45, "Back");

    public Camera AvatarCamera;

    BaseGui activo;
    WidgetConfig pulsado;
    int numButon = -1;

    public void Start()
    {

      GameObject.Find("VestuarioCamera").GetComponentInChildren<Light>().enabled = true;
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
        if (!AvatarCamera.enabled)
        {
            AvatarCamera.enabled = true;
        }
    }

    protected override void DoGUI()
    {
        GuiLabel(TitleCfg);

        GuiBox(BoxCfg);

        if (GuiButton(SignOutCfg) || Input.GetKey(KeyCode.Escape))
        {
            DoBack();
        }
        else if (GuiButton(CabezaCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = CabezaCfg;
            CabezaCfg = new WidgetConfig(-0.1f, -0.175f, 0.15f, 0.05f, 30, "Cabeza");
            numButon = 1;
        }
        else if (GuiButton(CaraCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = CaraCfg;
            CaraCfg = new WidgetConfig(-0.1f, -0.125f, 0.15f, 0.05f, 30, "Cara");
            numButon = 2;
        }
        else if (GuiButton(CamisetaCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = CamisetaCfg;
            CamisetaCfg = new WidgetConfig(-0.1f, -0.075f, 0.15f, 0.05f, 30, "Camiseta");
            numButon = 3;
        }
        else if (GuiButton(CalzonasCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = CalzonasCfg;
            CalzonasCfg = new WidgetConfig(-0.1f, -0.025f, 0.15f, 0.05f, 30, "Calzonas");
            numButon = 4;
        }
        else if (GuiButton(BotasCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = BotasCfg;
            BotasCfg = new WidgetConfig(-0.1f, 0.025f, 0.15f, 0.05f, 30, "Botas");
            numButon = 5;
        }
        else if (GuiButton(GuantesCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = GuantesCfg;
            GuantesCfg = new WidgetConfig(-0.1f, 0.075f, 0.15f, 0.05f, 30, "Guantes");
            numButon = 6;
        }
        else if (GuiButton(PelotaCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = PelotaCfg;
            PelotaCfg = new WidgetConfig(-0.1f, 0.125f, 0.15f, 0.05f, 30, "Pelota");
            numButon = 7;
        }
        else if (GuiButton(OtrosCfg))
        {
            recolocar();
            if (activo != null)
            {
                activo.enabled = false;
            }
            AvatarCamera.GetComponent<peloGUI>().MakeActive();
            activo = AvatarCamera.GetComponent<peloGUI>();
            pulsado = OtrosCfg;
            OtrosCfg = new WidgetConfig(-0.1f, 0.175f, 0.15f, 0.05f, 30, "Otros");
            numButon = 8;
        }
    }

    void recolocar()
    {
        switch (numButon)
        {
            case 1:
                CabezaCfg = pulsado;
                break;
            case 2:
                CaraCfg = pulsado;
                break;
            case 3:
                CamisetaCfg = pulsado;
                break;
            case 4:
                CalzonasCfg = pulsado;
                break;
            case 5:
                BotasCfg = pulsado;
                break;
            case 6:
                GuantesCfg = pulsado;
                break;
            case 7:
                PelotaCfg = pulsado;
                break;
            case 8:
                OtrosCfg = pulsado;
                break;
        }
    }

    void DoBack()
    {
        AvatarCamera.enabled = false;
        if (activo != null)
        {
            activo.enabled = false;
        }
        recolocar();

        gameObject.GetComponent<MainMenuGui>().MakeActive();


        GameObject.Find("VestuarioCamera").GetComponentInChildren<Light>().enabled = true;
    }
}
