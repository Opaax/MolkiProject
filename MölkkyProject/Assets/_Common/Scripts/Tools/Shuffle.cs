using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shuffle
{
    public static void ShuffleList<T>(List<T> listToShuffle)
    {
        for (int i = listToShuffle.Count - 1; i >= 0; i--)
        {
            var lCurrent = listToShuffle[i];
            int lRandomToSwitch = UnityEngine.Random.Range(0, UnityEngine.Random.Range(0, listToShuffle.Count));

            listToShuffle[i] = listToShuffle[lRandomToSwitch];
            listToShuffle[lRandomToSwitch] = lCurrent;
        }
    }
}
