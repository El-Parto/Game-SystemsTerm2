using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FactionChanger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI factionNameText;
    [SerializeField]
    private TextMeshProUGUI abilityNameText;
    [SerializeField]
    private TextMeshProUGUI stabilityNameText;
    [SerializeField]
    private TextMeshProUGUI klassNameText;


    // [SerializeField]
    //private Button classText;


    public List<string> factionStrings = new List<string>();
    [SerializeField]
    private string faction = "OompaLoompa";
    private string faction1 = "PootisBird";
    private string faction2 = "Trolls";
    private int factionNumber = 0;


    public List<string> klassStrings = new List<string>();

    private string[] klassArray = { "ThreeLancer", "CharWizzard", "Monke" };
    private string klass = "ThreeLancer";
    private string klass1 = "CharWizzard";
    private string klass2 = "Monke";
    private int klassNumber = 0;


    public List<string> abilityStrings = new List<string>();

    
    private string ability = "Jumper";
    private string ability1 = "Ambler";
    private string ability2 = "Runner";
    private int abilityNumber;

    public List<string> stabilityStrings = new List<string>();

    private string stability = "Plain Average";
    private string stability1 = "Smoll";
    private string stability2 = "Beeg";

    // Start is called before the first frame update
    void Start()
    {
        //Factions included into the list
        factionStrings.Add(faction);
        factionStrings.Add(faction1);
        factionStrings.Add(faction2);
        factionNameText.text = factionStrings[0];
        
        // Same as above but for abilities
        abilityStrings.Add(ability);
        abilityStrings.Add(ability1);
        abilityStrings.Add(ability2);
        abilityNameText.text = abilityStrings[0];

        stabilityStrings.Add(stability);
        stabilityStrings.Add(stability1);
        stabilityStrings.Add(stability2);
        stabilityNameText.text = stabilityStrings[0];

        //and again for classes
        klassStrings.Add(klass);
        klassStrings.Add(klass1);
        klassStrings.Add(klass2);
        klassNameText.text = klassStrings[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void ChangeMyFactionLeft( int _factionNumber)
    {
        _factionNumber--;
        if (_factionNumber <= -1)
            _factionNumber = 2;
        
        factionNameText.text = factionStrings[_factionNumber];

    }
    public void ChangeMyFactionRight(string _factionName, int _factionNumber)
    {
        _factionNumber++;
        if (_factionNumber >= 3)
            _factionNumber = 0;
        factionNameText.text = factionStrings[_factionNumber];
    }

    #region Ability Changer
    public void ChangeMyAbilityRight(int _abilityNumber)
    {
        _abilityNumber++;
        if (_abilityNumber >= 3)
            _abilityNumber = 0;
        abilityNameText.text = abilityStrings[_abilityNumber];
    }
    public void ChangeMyAbilityLeft(int _abilityNumber)
    {
        _abilityNumber--;
        if (_abilityNumber <= -1)
            _abilityNumber = 3;
        abilityNameText.text = abilityStrings[_abilityNumber];
    }
    #endregion

    #region Class Switcher
    public void ChangeMyKlassLeft( int _klassNumber)
    {
        if (_klassNumber <= -1)
            _klassNumber = 2;
        _klassNumber--;

        klassNameText.text = klassStrings[_klassNumber];
        Debug.Log(_klassNumber);
    }

    public void ChangeMyKlassRight()
    {
        
        

    }

    //public void ChangeMyKlassRight( int _klassNumber)
    //would of thought something like this would of worked, But no, since the int it needs actually is in the inspector
    // i can't get past whatever the value i'm adding by on the inspector
    //{
        
    //    if (_klassNumber >= 3)
    //        _klassNumber = -1;
    //    _klassNumber++;

    //    klassNameText.text = klassStrings[_klassNumber];
    //    Debug.Log(_klassNumber);
        
    //}
    # endregion


}
