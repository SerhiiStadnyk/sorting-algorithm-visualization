public class BubbleSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public BubbleSorting(ISortingHandable handleable) : base(handleable) { }

    private bool arrayChanged = false;

    private int stepA = 0;
    private int stepB = 0;

    public override void SortingStep()
    {
        if (stepB == 0)
            arrayChanged = false;

        if (stepA < Array.Count)
        {
            if (stepB < Array.Count - stepA - 1)
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
                return;
            }

            if (arrayChanged == false)
            {
                FinishSorting();
                return;
            }

            stepB = 0;
            stepA++;
            return;
        }
    }

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