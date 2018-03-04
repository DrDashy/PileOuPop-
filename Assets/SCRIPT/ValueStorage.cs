using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueStorage : MonoBehaviour {

    private static ValueStorage instance;

    public int respawn = 0;

    private ValueStorage() { }

    public static ValueStorage Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ValueStorage();
            }
            return instance;
        }
    }


    //Mettre vos valeurs par défaut ici
    public void ResetValues()
    {

    }

    //Mettre ici les valeur des variable qui seront reset à la mort
    public void ResetOnDeath()
    {

    }
}
