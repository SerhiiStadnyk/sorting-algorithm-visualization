public class ShakerSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public ShakerSorting(ISortingHandable handleable) : base(handleable) { }

    private bool arrayChanged = false;

    private int stepA = 0;
    private int stepB = 0;

    private bool isFlip = false;

    public override void SortingStep()
    {
        if (stepB == 0)
            arrayChanged = false;

        if (stepA < Array.Count) 
        {
            if (stepB < Array.Count - stepA - 1) 
            {
                if (isFlip)
                {
                    LeftSort();
                    return;
                }
                else
                {
                    RightSort();
                    return;
                }
            }

            if (arrayChanged == false)
            {
                FinishSorting();
                return;
            }
        }
    }

    private void RightSort()
    {
        if (Array[stepB] > Array[stepB + 1])
        {
            int tmp = Array[stepB];
            Array[stepB] = Array[stepB + 1];
            Array[stepB + 1] = tmp;

            RelocateElements(stepB, stepB + 1);
        }

        MarkElements(stepB, stepB + 1);

        stepB++;
    }
    private void LeftSort() 
    { }

    public void MarkElements(params int[] markedElements)
    {
        Handleable.MarkElements(markedElements);
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        arrayChanged = true;
        Handleable.RelocateElements(fromIndex, toIndex);
    }

    public void FinishSorting()
    {
        Handleable.FinishSorting();
    }
}