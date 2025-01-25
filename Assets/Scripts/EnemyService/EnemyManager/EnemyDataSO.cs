
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData",menuName ="ScriptableObjects/EnemyData")]
public class EnemyDataSO: ScriptableObject
{
    [SerializeField] List<EnemyData> enemyDatas;
    public List<EnemyData> EnemyDatas {  get { return enemyDatas; } }


}


[Serializable]
public class EnemyData
{
    [SerializeField] GameMode gameMode;
    [SerializeField] float destroyTime;

    public GameMode GameMode { get { return gameMode; } }
    public float DestroyTime { get { return destroyTime; } }

}