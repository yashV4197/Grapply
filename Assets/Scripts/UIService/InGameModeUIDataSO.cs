using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InGameModeUIData",menuName ="ScriptableObjects/InGameModeUIData")]
public class InGameModeUIDataSO: ScriptableObject
{
    [SerializeField] List<InGameModeUIDataCollection> inGameModeUIDataCollections;

    public List<InGameModeUIDataCollection> InGameModeUIDataCollections { get { return inGameModeUIDataCollections; } }

}

[Serializable]
public class InGameModeUIDataCollection
{
    [SerializeField] GameMode gameMode;
    [SerializeField] float timer;
    [SerializeField] int balloonsRequired;

    public GameMode GameMode { get { return gameMode; } }
    public float Timer { get { return timer; } }
    public int BalloonsRequired { get {  return balloonsRequired; } }


}