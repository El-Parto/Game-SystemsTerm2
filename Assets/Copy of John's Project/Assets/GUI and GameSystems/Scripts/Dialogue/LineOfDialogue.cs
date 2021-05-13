using UnityEngine;

[System.Serializable]
public class LineOfDialogue
{
    [TextArea(2,6)]
    public string topic, response;
    //public int minIntel;
    public float minApproval = -1;

    public float changeApproval = 0f;


    public Dialogue nextDialogue;

    [System.NonSerialized]
    public int buttonID;
}
