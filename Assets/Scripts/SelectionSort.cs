using UnityEngine;
using Random = System.Random;

public class SelectionSort : MonoBehaviour
{
    [SerializeField] private int lengthArray;
    [SerializeField] private int randomMin;
    [SerializeField] private int randomMax;
    private int[] _array;

    private int[] SetArray(int length)
    {
        int[] array = new int[length];
        Random rnd = new Random();
        for (int i = 0; i < array.Length; i++)
            array[i] = rnd.Next(randomMin, randomMax + 1);
        return array;
    }

    private void WriteArray(int[] array)
    {            
        Debug.Log("================================");
        foreach (var item in array)
            Debug.Log(item);
        Debug.Log("================================");
    }

    private int IndexOfMin(int[] array, int n)
    {
        int result = n;
        for (var i = n; i < array.Length; ++i)
        {
            if (array[i] < array[result]) 
                result = i;
        }
        return result;
    }

    private void Swap(ref int x, ref int y) => (x, y) = (y, x);
    
    private int[] SelectionSortMethod(int[] array, int currentIndex = 0)
    {
        if (currentIndex == array.Length)
            return array;

        var index = IndexOfMin(array, currentIndex);
        if (index != currentIndex) 
            Swap(ref array[index], ref array[currentIndex]);

        return SelectionSortMethod(array, currentIndex + 1);
    }

    void Start()
    {
        _array = SetArray(lengthArray);
        WriteArray(_array);
        SelectionSortMethod(_array);
        WriteArray(_array);
    }
}