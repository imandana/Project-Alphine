﻿using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Database/ActorData")]
public class ActorData : ScriptableObject
{
    public string actorName;
    public string actorNickname;
//    public ActorData[] actorClass;
    public int initLevel;
    public int maxLevel;

    [TextArea]
    public string description;

    public Sprite face;
    public Sprite characterWorld;
    public Sprite battler;

    //TODO : Equipment

    //TODO : Traits

    [TextArea]
    public string notes;

    public void OnEnable()
    {
        if (actorName == null &&
            actorNickname == null)
        {
            Init();
        }
    }

    public void Init()
    {
        Sprite sp = Resources.Load<Sprite>("Image");

        actorName = "player";
        actorNickname = "actorNickname";
//        actorClass = Resources.LoadAll<ActorData>("path");
        initLevel = 1;
        maxLevel = 99;
        description = "insert your description here";
        face = sp;
        characterWorld = sp;
        battler = sp;
        notes = "write your notes here, it won't affect the game though.";
    }
}