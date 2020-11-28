namespace ACALab5
{
    public static class StringSorter
    {
        public static void BubbleSort(string[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            for (var j = i; j < arr.Length; j++)
                if (arr[j].CompareTo(arr[i]) < 0)
                {
                    var t = arr[i];
                    arr[i] = arr[j];
                    arr[j] = t;
                }
        }

        public static void QuickSort(string[] arr) => QuickSort(arr, 0, arr.Length - 1);

        private static void QuickSort(string[] arr, int start, int end)
        {
            if (start >= end)
                return;
            var marker = PartialSort(arr, start, end);
            QuickSort(arr, start, marker - 1);
            QuickSort(arr, marker + 1, end);
        }

        private static int PartialSort(string[] arr, int start, int end) 
        {
            var marker = start;
            for (var i = start; i <= end; i++) 
                if (arr[i].CompareTo(arr[end]) <= 0)
                {
                    var temp = arr[marker];
                    arr[marker] = arr[i];
                    arr[i] = temp;
                    marker += 1;
                }
            return marker - 1;
        }
    }
}