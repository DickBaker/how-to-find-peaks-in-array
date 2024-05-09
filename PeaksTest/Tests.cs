#define MARCIN      // code-in Marcin's algorithm
//#define DICK      // code-in Dick's algorithm
// please just define ONE of the above to avoid confusion

using Peaks;

namespace PeaksTest;

/// <summary>
/// compare test results of multiple algorithms that purport to find peaks
/// </summary>
/// <see cref="https://marcin.com/how-to-find-peaks-in-array"/>
/// <remarks>
/// 1.  the TestCaseSource attribute before each candidate will ensure identical data-driven parameters
/// 2.  the Category = "algo" provides the "Trait" selection in Test Explorer [suggest just ONE per run]
/// 3.  or [better!] just define ONE of the conditionals at top of this document (or in .csproj)
/// </remarks>
public class Tests
{
#if MARCIN
    [TestCaseSource(typeof(PeakTestData), nameof(PeakTestData.TestCases), Category = "Marcin")]
    public int[] GetPeaks_ShouldComply(int[] inputData) =>
        FindPeaks.GetPeaks(inputData);
#endif

#if DICK
    [TestCaseSource(typeof(PeakTestData), nameof(PeakTestData.TestCases), Category = "Dick")]
    public int[] GetPeaksDick_ShouldComply(int[] inputData) =>
        FindPeaks.GetPeaksDick(inputData);
#endif
}
