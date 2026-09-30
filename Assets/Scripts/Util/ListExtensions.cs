using System.Collections.Generic;

public static class ListExtensions
{
    public static T Random<T>(this List<T> elements)
    {
        return elements[UnityEngine.Random.Range(0, elements.Count)];
    }

    public static List<T> Shuffle<T>(this List<T> elements)
    {
        for (var i = 0; i < elements.Count - 1; i++)
        {
            var j = UnityEngine.Random.Range(i + 1, elements.Count);
            (elements[i], elements[j]) = (elements[j], elements[i]);
        }
        return elements;
    }
}