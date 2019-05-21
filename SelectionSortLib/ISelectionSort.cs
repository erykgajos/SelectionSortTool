using System.Collections.Generic;

namespace SelectionSortLib
{
    public interface ISelectionSort
    {
        List<int> GetSortedArray();
        double Sort();
    }
}