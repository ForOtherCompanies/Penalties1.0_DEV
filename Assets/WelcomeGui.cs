// <copyright file="WelcomeGui.cs" company="Google Inc.">
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
using GooglePlayGames.BasicApi;

public class WelcomeGui : BaseGui
{
    WidgetConfig TitleCfg = new WidgetConfig(0.0f, -0.2f, 1.0f, 0.2f, 100, "Wellcome");
    WidgetConfig PlayCfg = new WidgetConfig(0.0f, -0.1f, 1f, 0.2f, 60, "Introduce tu nombre");

    WidgetConfig NameCfg = new WidgetConfig(-0.05f, -0.0f, 0.2f, 0.05f, 30, "");

    private string nombre = "Name";
    WidgetConfig ContinuarCfg = new WidgetConfig(0.1f, -0.0f, 0.1f, 0.05f, 30, "Seguir");

    WidgetConfig SignInCfg = new WidgetConfig(0.0f, 0.1f, 0.55f, 0.05f, 30, "Iniciar sesion en Google play serices");
    public Info inf;

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

    void Update()
    {/*
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

        }*/
    }

    protected override void DoGUI()
    {
        GuiLabel(TitleCfg);
        GuiLabel(PlayCfg);
        nombre = GuiTextField(NameCfg, nombre);
        if (GuiButton(ContinuarCfg))
        {
            inf.SetNombre(nombre);
            gameObject.GetComponent<MainMenuGui>().MakeActive();
        }else if(Input.GetKey(KeyCode.Escape)){
            
            Application.Quit();
        }
        else if (GuiButton(SignInCfg))
        {
            PlayGamesPlatform.Instance.Authenticate(mAuthCallback);
        }
    }
}
