using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quests
{

    public class QuestManager : MonoBehaviour
    { // building a singleton
        public static QuestManager instance = null;

        public List<Quest> quests = new List<Quest>();

        private Dictionary<string, Quest> questDatabase = new Dictionary<string, Quest>();

        private List<Quest> activeQuests = new List<Quest>();// for seeing UI on what quests are active

        public List<Quest> GetActiveQuests() => activeQuests;

        //pass a player into this function
        public void UpdateQuest(string _id)
        {
            //first you get the quest
            // * this is the same as checking if the jey exists if it does, return it
            // * TryGetValue returns a boolean if it successfully got the item
            if(questDatabase.TryGetValue(_id , out Quest quest))
            
            {
                if(quest.stage == QuestStage.InProgress)
                {
                    // * Check if the quest is ready to compleete, if it is, update the stage
                    // * Otherwise, retain the stage
                    quest.stage = quest.CheckQuestCompletion() ?
                        QuestStage.RequirementsMet :
                        quest.stage;
                }
            }
        }

        //takes in the player too
        public void CompleteQuest(string _id)
        {
            //get the quest
            if (questDatabase.TryGetValue(_id, out Quest quest))
            {
                quest.stage = QuestStage.Complete;
                activeQuests.Remove(quest);

                //psuedo
                /*1 Find all related quests that are going to be unlocked                 
                 *2 update their stages                 
                 *3 give player reward
                 */

                //1
                foreach(string questId in quest.unlockedQuests)
                {
                    //2
                    if(questDatabase.TryGetValue(questId, out Quest unlocked))
                    {
                        unlocked.stage = QuestStage.Unlocked;
                    }
                }
                //3
                // quest.reward += 
             
            }
        }

        public void AcceptQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.Unlocked)
                {
                    quest.stage = QuestStage.InProgress;
                    activeQuests.Add(quest);
                }
            }
        }


        private void Awake()
        {
            // if instance = this
            if (instance == null)
            {
                instance = this;
            }
            // * If the instance is aready set and it isn't this gameObject
            // * destroy this GameObject
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
       // end building singleton



        // Start is called before the first frame update
        void Start()
        {
            quests.Clear();
            // Find all the quests in the game
            quests.AddRange(FindObjectsOfType<Quest>());

            //* for each functio is specific to list types that just functions
            //* like a for each loop with lambdas.
            quests.ForEach(quest =>
            {
                if (!questDatabase.ContainsKey(quest.title))
                    questDatabase.Add(quest.title, quest);
                else
                    Debug.LogError("You can't do that yet, this already exists");
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}