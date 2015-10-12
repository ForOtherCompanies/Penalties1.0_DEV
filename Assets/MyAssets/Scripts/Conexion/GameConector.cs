// <copyright file="RaceManager.cs" company="Google Inc.">
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

using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using System;

public class GameConector : RealTimeMultiplayerListener
{
    const string RaceTrackName = "Match";
    const int QuickGameOpponents = 1;
    const int GameVariant = 0;
    static GameConector sInstance = null;
    const int MinOpponents = 1;
    const int MaxOpponents = 3;


    public enum MachState
    {
        SettingUp,
        Playing,
        Finished,
        SetupFailed,
        Aborted
    }

    ;

    private MachState mRaceState = MachState.SettingUp;

    // how many points each of our fellow racers has
    private Dictionary<string, int> mRacerScore = new Dictionary<string, int>();

    // whether or not we received the final score for each participant id
    private HashSet<string> mGotFinalScore = new HashSet<string>();

    // my participant ID
    private string mMyParticipantId = "";

    // my rank (1st, 2nd, 3rd, 4th, or 0 to mean 'no rank yet')
    // This is updated every time we get a finish notification from a peer
    private int mFinishRank = 0;

    // room setup progress
    private float mRoomSetupProgress = 0.0f;

    // speed of the "fake progress" (to keep the player happy)
    // during room setup
    const float FakeProgressSpeed = 1.0f;
    const float MaxFakeProgress = 30.0f;
    float mRoomSetupStartTime = 0.0f;

    private GameConector()
    {
        mRoomSetupStartTime = Time.time;
    }

    public static void CreateQuickGame()
    {
        sInstance = new GameConector();
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(QuickGameOpponents,
            QuickGameOpponents,
            GameVariant,
            sInstance);
    }

    public static void CreateWithInvitationScreen()
    {
        sInstance = new GameConector();
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(
            MinOpponents,
            MaxOpponents,
            GameVariant,
            sInstance);
    }

    public static void AcceptFromInbox()
    {
        sInstance = new GameConector();
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(sInstance);
    }

    public static void AcceptInvitation(string invitationId)
    {
        sInstance = new GameConector();
        PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, sInstance);
    }

    public MachState State
    {
        get
        {
            return mRaceState;
        }
    }

    public static GameConector Instance
    {
        get
        {
            return sInstance;
        }
    }

    public int FinishRank
    {
        get
        {
            return mFinishRank;
        }
    }

    public float RoomSetupProgress
    {
        get
        {
            float fakeProgress = (Time.time - mRoomSetupStartTime) * FakeProgressSpeed;
            if (fakeProgress > MaxFakeProgress)
            {
                fakeProgress = MaxFakeProgress;
            }
            float progress = mRoomSetupProgress + fakeProgress;
            return progress < 99.0f ? progress : 99.0f;
        }
    }

    private void SetupTrack()
    {
        GameObject cameras = GameObject.Find("Camaras");
        GameObject gameController = GameObject.Find("GameController");
        sendName(gameController.GetComponent<Info>().GetNombre());
        List<Participant> listaParticipantes = GetRacers();
        string ID = GetSelf().ParticipantId;
        foreach(Participant p in listaParticipantes){
            if (p.ParticipantId != ID)
            {
                if (Convert.ToInt32(p.ParticipantId) > Convert.ToInt32(ID))
                {
                    gameController.GetComponent<MacthController>().SetPosition(2);
                }
                else
                {
                    gameController.GetComponent<MacthController>().SetPosition(1);
                }
            }
        }

        gameController.GetComponent<MacthController>().ActivarModoActual(new ModoVSia());
        cameras.GetComponentInChildren<gameUI>().MakeActive();
        cameras.GetComponent<CameraController>().ActivarGameCamera();
    }

    private void TearDownTrack()
    {
        /*
        BehaviorUtils.MakeVisible(GameObject.Find(RaceTrackName), false);
        foreach (string name in CarNames)
        {
            GameObject car = GameObject.Find(name);
            car.GetComponent<CarController>().Reset();
            BehaviorUtils.MakeVisible(car, false);
        }
         */
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            mRaceState = MachState.Playing;
            mMyParticipantId = GetSelf().ParticipantId;
            SetupTrack();
        }
        else
        {
            mRaceState = MachState.SetupFailed;
        }
    }

    public void OnLeftRoom()
    {
        if (mRaceState != MachState.Finished)
        {
            mRaceState = MachState.Aborted;
        }
    }

    public void OnParticipantLeft(Participant participant)
    {
    }

    public void OnPeersConnected(string[] peers)
    {
    }

    public void OnPeersDisconnected(string[] peers)
    {
        foreach (string peer in peers)
        {
            // if this peer has left and hasn't finished the race,
            // consider them to have abandoned the race (0 score!)
            mGotFinalScore.Add(peer);
            mRacerScore[peer] = 0;
            RemoveCarFor(peer);
        }

        // if, as a result, we are the only player in the race, it's over
        List<Participant> racers = GetRacers();
        if (mRaceState == MachState.Playing && (racers == null || racers.Count < 2))
        {
            mRaceState = MachState.Aborted;
        }
    }

    private void RemoveCarFor(string participantId)
    {/*
        foreach (string name in CarNames)
        {
            GameObject obj = GameObject.Find(name);
            CarController cc = obj.GetComponent<CarController>();
            if (participantId.Equals(cc.ParticipantId))
            {
                BehaviorUtils.MakeVisible(obj, false);
            }
        }
      */
    }

    private bool showingWaitingRoom = false;

    public void OnRoomSetupProgress(float percent)
    {
        if (!showingWaitingRoom)
        {
            showingWaitingRoom = true;
            PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
        }
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        Vector3 position;
        Quaternion rotation;
        switch (data[0])
        {
            case (byte)'N':
                string name = GetString(data);
                name = name.Substring(1);
                GameObject.Find("GameController").GetComponent<MacthController>().SetOponentName(name);
                break;
            case (byte)'B':
                position.x = BitConverter.ToSingle(data, 1);
                position.y = BitConverter.ToSingle(data, 5);
                position.z = BitConverter.ToSingle(data, 9);
                rotation.x = BitConverter.ToSingle(data, 13);
                rotation.y = BitConverter.ToSingle(data, 17);
                rotation.z = BitConverter.ToSingle(data, 21);
                rotation.w = BitConverter.ToSingle(data, 25);
                GameObject.Find("GameController").GetComponent<MacthController>().SetBallPosition(position,rotation);
                break;
            case (byte)'P':
                position.x = BitConverter.ToSingle(data, 1);
                position.y = BitConverter.ToSingle(data, 5);
                position.z = BitConverter.ToSingle(data, 9);
                rotation.x = BitConverter.ToSingle(data, 13);
                rotation.y = BitConverter.ToSingle(data, 17);
                rotation.z = BitConverter.ToSingle(data, 21);
                rotation.w = BitConverter.ToSingle(data, 25);

                GameObject.Find("GameController").GetComponent<MacthController>().SetPorteroPosition(position, rotation);
                break;

        }

    }

    public void CleanUp()
    {
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
        TearDownTrack();
        mRaceState = MachState.Aborted;
        sInstance = null;
    }
    /*
    public float GetRacerProgress(string participantId)
    {
        return GetRacerPosition(participantId) / (float)PointsToFinish;
    }
     */

    public int GetRacerPosition(string participantId)
    {
        if (mRacerScore.ContainsKey(participantId))
        {
            return mRacerScore[participantId];
        }
        else
        {
            return 0;
        }
    }

    private Participant GetSelf()
    {
        return PlayGamesPlatform.Instance.RealTime.GetSelf();
    }

    private List<Participant> GetRacers()
    {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    private Participant GetParticipant(string participantId)
    {
        return PlayGamesPlatform.Instance.RealTime.GetParticipant(participantId);
    }

    public void UpdateSelf(float deltaT, int pointsToAdd)
    {/*
        int pos = GetRacerPosition(mMyParticipantId);

        if (pos >= PointsToFinish)
        {
            // already finished
            return;
        }

        pos += pointsToAdd;
        pos = pos < 0 ? 0 : pos >= PointsToFinish ? PointsToFinish : pos;
        mRacerScore[mMyParticipantId] = pos;

        if (pos >= PointsToFinish)
        {
            // we finished the race!
            FinishRace();
        }
        else if (pointsToAdd > 0)
        {
            // broadcast position update to peers
            BroadCastPosition(pos);
        }*/
    }

    private void sendName(string str)
    {
        str = 'N' + str;
        byte[] bytes = new byte[str.Length * sizeof(char) + 1];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, bytes);
    }

    public void sendBola(Vector3 position,Quaternion rotation){

        byte[] bytes = new byte[25];
        bytes[0] = (byte)'B';
        Buffer.BlockCopy(bytes, 1, BitConverter.GetBytes(position.x), 0, 4);
        Buffer.BlockCopy(bytes, 5, BitConverter.GetBytes(position.y), 0, 4);
        Buffer.BlockCopy(bytes, 9, BitConverter.GetBytes(position.z), 0, 4);
        Buffer.BlockCopy(bytes, 13, BitConverter.GetBytes(rotation.x), 0, 4);
        Buffer.BlockCopy(bytes, 17, BitConverter.GetBytes(rotation.y), 0, 4);
        Buffer.BlockCopy(bytes, 21, BitConverter.GetBytes(rotation.z), 0, 4);
        Buffer.BlockCopy(bytes, 25, BitConverter.GetBytes(rotation.w), 0, 4);

        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, bytes);
    }

    public void sendPortero(Vector3 position, Quaternion rotation)
    {
        byte[] bytes = new byte[25];
        bytes[0] = (byte)'P';
        Buffer.BlockCopy(bytes, 1, BitConverter.GetBytes(position.x), 0, 4);
        Buffer.BlockCopy(bytes, 5, BitConverter.GetBytes(position.y), 0, 4);
        Buffer.BlockCopy(bytes, 9, BitConverter.GetBytes(position.z), 0, 4);
        Buffer.BlockCopy(bytes, 13, BitConverter.GetBytes(rotation.x), 0, 4);
        Buffer.BlockCopy(bytes, 17, BitConverter.GetBytes(rotation.y), 0, 4);
        Buffer.BlockCopy(bytes, 21, BitConverter.GetBytes(rotation.z), 0, 4);
        Buffer.BlockCopy(bytes, 25, BitConverter.GetBytes(rotation.w), 0, 4);
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, bytes);
    }


    private string GetString(byte[] bytes)
    {
        char[] chars = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }

    private void UpdateMyRank()
    {
        int numRacers = GetRacers().Count;
        if (mGotFinalScore.Count < numRacers)
        {
            mFinishRank = 0; // undefined for now
        }
        int myScore = mRacerScore[mMyParticipantId];
        int rank = 1;
        foreach (string participantId in mRacerScore.Keys)
        {
            if (mRacerScore[participantId] > myScore)
            {
                ++rank;
            }
        }
        mFinishRank = rank;
    }
}
