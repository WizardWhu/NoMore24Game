using System;
using UnityEngine;
[Serializable]
public class WaterDrop
{
    private int SecondsTillDrop = 1;
    private float DropletVolume = 0f;
    public WaterDrop()
    {
        this.SecondsTillDrop = 1;
        this.DropletVolume = 0f;
    }
    public WaterDrop(int SecondsTillDrop, float DropletVolume)
    {
        this.SecondsTillDrop = SecondsTillDrop;
        this.DropletVolume = DropletVolume;
    }

    public void SetSecondsTillDrop(int SecondsTillDrop)
    {
        this.SecondsTillDrop = SecondsTillDrop;
    }

    public void SetDropletVolume(float DropletVolume)
    {
        this.DropletVolume = DropletVolume;
    }
}
