using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static List<int> randomizedList(int start, int finish, int take)
    {
        List<int> orderedSequence = new List<int>();
        for (int i = start; i <= finish; i++) orderedSequence.Add(i);

        List<int> finalSequence = new List<int>();
        for (int i = 0; i < take; i++)
        {
            int rng = Random.Range(0, orderedSequence.Count);
            finalSequence.Add(orderedSequence[rng]);
            orderedSequence.RemoveAt(rng);
        }

        return finalSequence;
    }
}
