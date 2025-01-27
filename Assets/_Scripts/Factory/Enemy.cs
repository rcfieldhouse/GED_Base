using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract string Name { get; }

    public abstract GameObject Create(GameObject prefab);
}
public class Crab : Enemy
{
    public override string Name => "crab";

        public override GameObject Create(GameObject prefab)
        {
        GameObject enemy = Instantiate (prefab);
        Debug.Log("Crab gang");
        return enemy;
        }
}


public class Monster : Enemy
{
    public override string Name => "monster";

    public override GameObject Create(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab);
        Debug.Log("monster gang");
        return enemy;
    }
}
