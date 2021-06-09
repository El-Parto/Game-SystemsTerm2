using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public enum QuestStage
    {
        Locked, //* We can't actually get the quest.
        Unlocked, //* The quest is now available to be accepted
        InProgress,//* we accepted the quest
        RequirementsMet, // * we have done all things in the quest, just need to "hand it in"
        Complete//* quest is done and we can ignore it

    }
    //a struct because demonstrations
    [System.Serializable]
    public abstract class Quest : MonoBehaviour
    {
        // what does a quest need?
        public QuestReward reward;//the reward type?
        public QuestStage stage;


        public string title;
        [TextArea] public string description;

        


        public int requiredLevel;
        [Tooltip("The title of the previous quest in the chain.")]
        public string previousQuest;
        [Tooltip("The title of the quests to be unlocked")]
        public string[] unlockedQuests;

        //public virtual bool CheckQuestCompletion() { return false; }
        public abstract bool CheckQuestCompletion();


    }
    
    [System.Serializable]
    public struct QuestReward
    {
        // what does a quest need?
        public float experience;
        public int gold;
        //public float factionIncrease;

    }
}