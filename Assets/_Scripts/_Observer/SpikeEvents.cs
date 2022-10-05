using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Events
public abstract class SpikeEvents
{
    public abstract Color SpikeEditorColor();
    public abstract Color ChangeToRed();
}


public class YellowMat : SpikeEvents
{
    public override Color SpikeEditorColor()
    {
        return Color.yellow;
    }
    public override Color ChangeToRed()
    {

        return Color.red;
    }
}


public class GreenMat : SpikeEvents
{
    public override Color SpikeEditorColor()
    {
        return Color.green;
    }
    public override Color ChangeToRed() { 
    
        return Color.red;
    }
}


