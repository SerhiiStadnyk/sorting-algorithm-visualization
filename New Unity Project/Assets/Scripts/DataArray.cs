using System.Collections.Generic;

public class DataArray: ScriptableDataBase<DataArray>
{
    public List<int> Array { get; private set; }

    public void RelocateElements(int fromIndex, int toIndex) { }

    public void SetupElements(List<int> array) 
    {
        Array = array;
    }
}