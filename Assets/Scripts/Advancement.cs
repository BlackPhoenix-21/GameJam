using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Advancement/Advancement")]
public class Advancement : ScriptableObject
{
    public int AdvanceIDEdit;
    public string NameEdit;
    public string DescriptionEdit;
    public bool AchievedEdit;
    public Sprite ImgEdit;

    [HideInInspector]
    public int AdvanceID;
    [HideInInspector]
    public string Name, Description;
    //[HideInInspector]
    public bool Achieved;
    [HideInInspector]
    public Sprite Img;

    public void BuildNew()
    {
        AdvanceID = AdvanceIDEdit;
        Name = NameEdit;
        Description = DescriptionEdit;
        Achieved = AchievedEdit;
        Img = ImgEdit;
    }
}
