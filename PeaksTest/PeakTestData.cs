namespace PeaksTest;

/// <summary>
/// purpose is to provide set of data-driven I/P test params and O/P expected results
/// </summary>
/// <remarks>
/// 1.  intention is to contrast Marcin's and Dick's implementions
/// 2.  each algo must return results so the TestExplorer runner can validate against ExpectedResult
/// 3.  testdata is an array of TestData objects, consisting of input array and output array
/// 4.  testdata is generated once (in static ctor) and then consumed but various candidate tests
///     -i.e. doesn't need to be the classic [expensive!] yield return state-machine implementation
/// 5.  order of entries in testdata array don't matter but are ordered by TestName for convenience
///     - i.e. to correspond to the TestExplorer grid
/// 6.  the [Category("Marcin")] or [Category("Dick")] attribute in NUnit sets the Trait filter in TestExplorer
///     - my TestExplorer didn't support >1 algorithms, hence MARCIN OR DICK conditionals in Tests.cs (don't enable both)
/// </remarks>
public static class PeakTestData // : TestCaseParameters
{
    private static readonly List<TestCaseData> testdata;
    static PeakTestData() => testdata =
        [
            new TestCaseData(new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }) { TestName = "downhill has first peak", ExpectedResult = new int[] { 10 } },
            new TestCaseData(Array.Empty<int>()) { TestName = "empty entry v1", ExpectedResult = Array.Empty<int>() },
#pragma warning disable CA1825 // Avoid zero-length array allocations       // deliberate to use individual heap empty not the shared stack Empty !
            new TestCaseData(new int[0]) { TestName = "empty entry v2", ExpectedResult = new int[0] },
#pragma warning restore CA1825 // Avoid zero-length array allocations
            new TestCaseData(new int[] { 10, 8, 7, 5, 2, 7, 7, 8, 2, 9 }) { TestName = "flat before peak", ExpectedResult = new int[] { 10, 8, 9 } },
            new TestCaseData(new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }) { TestName = "flat input has no peaks", ExpectedResult = Array.Empty<int>() },
            new TestCaseData(new int[] { 1, 2, 8, 8, 8, 3, 3, 4, 4, 4 }) { TestName = "joint peaks", ExpectedResult = new int[] { 8, 8, 8, 4, 4, 4 } },
            new TestCaseData(new int[]{10, 8, 7, 5, 2, 7, 4, 8, 2, 9}) { TestName = "Marcin original", ExpectedResult = new int[] { 10, 7, 8, 9 } },
            new TestCaseData(new int[] { 10, 8, 7, 5, 2, 7, 4, 8, 2, 9 }) { TestName = "mid peaks", ExpectedResult = new int[] { 10, 7, 8, 9 } },
            new TestCaseData(null) { TestName = "null entry shouldn't blow-up", ExpectedResult = Array.Empty<int>() },
            new TestCaseData(new int[] { 1, 2, 2, 2, 0, 3, 3, 4, 1, 0 }) { TestName = "peaks #3", ExpectedResult = new int[] { 2, 2, 2, 4 } },
            new TestCaseData(new int[] { 2, 2, 0, 3, 0, 0, 4, 3, 8, 9 }) { TestName = "peaks #4", ExpectedResult = new int[] { 2, 2, 3, 4, 9 } },
            new TestCaseData(new int[] { 2, 2, 3, 0, 0, 4, 4, 3, 8, 9 }) { TestName = "peaks #5", ExpectedResult = new int[] { 3, 4, 4, 9 } },
            new TestCaseData(new int[] { 0, 9, 1, 8, 2, 7, 3, 6, 4, 5 }) { TestName = "real peaks #1", ExpectedResult = new int[] { 9, 8, 7, 6, 5 } },
            new TestCaseData(new int[] { 10, 0, 9, 1, 8, 2, 7, 3, 6, 4 }) { TestName = "real peaks #2", ExpectedResult = new int[] { 10, 9, 8, 7, 6 } },
            new TestCaseData(new int[] { 1 }) { TestName = "single entry has no peak", ExpectedResult = Array.Empty<int>() },
            new TestCaseData(new int[] { 1, 0, 0, 0, 5, 4, 4, 4, 7 }) { TestName = "troughs", ExpectedResult = new int[] { 1, 5, 7 } },
            new TestCaseData(new int[] { 2, 1 }) { TestName = "two down entries, { 2, 1 }", ExpectedResult = new int[] { 2 } },
            new TestCaseData(new int[] { 1, 1 }) { TestName = "two same entries { 1, 1 }", ExpectedResult = Array.Empty<int>() },
            new TestCaseData(new int[] { 1, 2 }) { TestName = "two up entries, { 1, 2 }", ExpectedResult = new int[] { 2 } },
            new TestCaseData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }) { TestName = "uphill has last peak", ExpectedResult = new int[] { 10 } }
        ];
    public static IEnumerable<TestCaseData> TestCases => testdata;
}