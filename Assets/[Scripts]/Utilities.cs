using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float GetExpDistValue(float lambda)
    {
        return -Mathf.Log(1f - Random.value) / lambda;
    }
    // y=L*exp(-L*x)
    //
    // x= -ln(1-r)/L  => L= -ln(1-r)/x  =>  exp (-L*x) =1-r  => r=
         

}
