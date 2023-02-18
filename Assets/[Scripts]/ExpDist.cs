using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDist : MonoBehaviour
{
    public float Lambda = 0.5f;
    public static int NrSamples = 500;
    public float[] ExpDistSamples = new float[NrSamples];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NrSamples; i++)
        {
            ExpDistSamples[i] = Utilities.GetExpDistValue(Lambda);
            print(ExpDistSamples[i]);
        }

    }

}
