namespace Peaks;

/// <summary>
/// method to find peaks in a series of integer data
/// </summary>
/// <remarks>
/// 1. spec says "A peak is any element which value is higher than values of its neighbors"
/// 2. but GetPeaks uses greater-than-or-equal relop instead, hence false-positives [e.g. {7, 7, 8} is misreported as 3 peaks!]
/// 3. GetPeaks(null), GetPeaks(new int[0]) and GetPeaks(new int[1]) throw IndexOutOfRangeException [i.e. needs entry guard clause]
/// </remarks>
public static class FindPeaks
{
    /// <summary>
    ///  Dr Marcin Jamro published "HOW TO FIND PEAKS IN ARRAY?" at
    ///  https://marcin.com/how-to-find-peaks-in-array
    /// </summary>
    /// <param name="a">input integer array</param>
    /// <returns>integer array of qualifying peaks [or empty array if none]</returns>
    /// <example>GetPeaks([10, 8, 7, 5, 2, 7, 4, 8, 2, 9]) results in [10, 7, 8, 9]</example>
    /// <remarks>
    /// 1. this code throws exception on certain inputs
    /// 2. uses GE-relop so may give false positive results
    /// 3. this NUnit test project [with data-driven tests] highlights various flaws
    /// 4. my GetPeaksDick alternative code is also provided here FYI
    /// </remarks>
    public static int[] GetPeaks(int[] a)
    {
        List<int> peaks = [];       // edited from "List peaks = [];"

        // Check whether the first element is a peak.
        if (a[0] >= a[1])
        {
            peaks.Add(a[0]);
        }

        // Check whether any element
        // (except the first and the last) is a peak.
        for (var i = 1; i < a.Length - 1; i++)
        {
            if (a[i - 1] <= a[i] && a[i] >= a[i + 1])
            {
                peaks.Add(a[i]);
            }
        }

        // Check whether the last element is a peak
        if (a[^1] >= a[^2])
        {
            peaks.Add(a[^1]);
        }

        return [.. peaks];
    }

    public static int[] GetPeaksDick(int[] a)
    {
        if (a == null || a.Length < 2) { return []; }
        List<int> peaks = [];
        for (var i = 0; i < a.Length; i++)
        {
            var current = a[i];
            if (LeftAlt(i) < current && current > RightAlt(i))     // relops strictly lt, gt
            {
                peaks.Add(current);
            }
        }
        return peaks.Count < a.Length       // all values identical ?
            ? [.. peaks]                    // no, return array of peaks
            : [];                           // yes, ignore (return empty array)

        int LeftAlt(int index)
        {
            for (var indexL = index - 1; indexL >= 0; indexL--)
            {
                if (a[indexL] != a[index])
                {
                    return a[indexL];
                }
            }
            return int.MinValue;
        }

        int RightAlt(int index)
        {
            for (var indexR = index + 1; indexR < a.Length; indexR++)
            {
                if (a[index] != a[indexR])
                {
                    return a[indexR];
                }
            }
            return int.MinValue;
        }
    }
}