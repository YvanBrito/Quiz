using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour {

    public string description;
    public Sprite outdoor;
    public string name;

    public void setName(string n)
    {
        this.name = n;

        TextAsset bindata = Resources.Load("Descriptions/" + this.name) as TextAsset;
        this.description = bindata.text;

        outdoor = Resources.Load<Sprite>(@"Images/outdoor/" + this.name);
    }
}
