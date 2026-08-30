using System;
using UnityEngine;
[Serializable]
public class WaterDrop
{
    private float SecondsTillDrop = 1;
    private float DropletVolume = 0f;
    public WaterDrop()
    {
        this.SecondsTillDrop = 1;
        this.DropletVolume = 0f;
    }
    public WaterDrop(float SecondsTillDrop, float DropletVolume)
    {
        this.SecondsTillDrop = SecondsTillDrop;
        this.DropletVolume = DropletVolume;
    }

    public void SetSecondsTillDrop(float SecondsTillDrop)
    {
        this.SecondsTillDrop = SecondsTillDrop;
    }

    public void SetDropletVolume(float DropletVolume)
    {
        this.DropletVolume = DropletVolume;
    }

    public float GetSecondsTillDrop()
    {
        return this.SecondsTillDrop;
    }

    public float GetDropletVolume()
    {
        return this.DropletVolume;
    }
}
