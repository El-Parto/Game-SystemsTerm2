using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Factions
{
    public string factionName;
    [SerializeField, Range(-1,1)]
    float _approval;
    public float approval
    {
        set
        {
            _approval = Mathf.Clamp(value, -1, 1);
        }
        get
        {
            return _approval;
        }
    }

    public Factions(float initialAprroval) // constructor
    {
        approval = initialAprroval;
    }
}

public class FactionManager : MonoBehaviour
{

    
   // [SerializeField]
    public Dictionary<string, Factions> factions;

    [SerializeField]
    List<Factions> initialiseFactions; //two collections of factions? 
    // * Well no, this way you don't need to use dictionary and create a custom layout(?) 

    public static FactionManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        factions = new Dictionary<string, Factions>();
        //factions.Add("Orange you glad", new Factions());
        // now we have a loop that goes through all factions
        foreach(Factions faction in initialiseFactions)
        {
            factions.Add(faction.factionName, faction);
        }

    }


    public float? FactionsApproval(string factionName, float value)
    {
        if (factions.ContainsKey(factionName))
        {
            factions[factionName].approval += value;
            return factions[factionName].approval;
        }
        return null;
        //say you had a bannana, you could lose aproval for the apple faction -example-
    }
    public float? /*Get*/FactionsApproval(string factionName)
    {
        if (factions.ContainsKey(factionName))
        {
            
            return factions[factionName].approval;
        }
        return null;
    }

    /* Changing Facton approval
    public void approvalChanger()
    {
        FactionManager.instance.FactionsApproval("AppleClan", -0.05f);
    }
    */

}
