using System.Diagnostics;

namespace Palindrome;
internal class ParallelSolution
{
    public const int MIN_DIGITS_COUNT_IN_PALINDROME = 2;

    readonly Stopwatch _stopwatch;
    readonly MathHelper _mathHelper;
    readonly Action<string?> _output;

    public ParallelSolution(Action<string?> output)
    {
        _stopwatch = new Stopwatch();
        _mathHelper = new MathHelper();
        _output = output;
    }

    public void Run(string text)
    {
        try
        {
            var digitArray = text.ToCharArray().Select(c => c.ToString()).ToArray();

            _stopwatch.Start();
            Parallel.For(MIN_DIGITS_COUNT_IN_PALINDROME, digitArray.Length + 1, 
                (i) => FindPalindromes(digitArray.Select(x => Convert.ToInt64(x)).ToArray(), i));
            _stopwatch.Stop();

            _output($"Time for Parallel: {_stopwatch.ElapsedMilliseconds}");
        }
        catch (Exception ex)
        {
            _output(ex.Message);
        }
    }

    void FindPalindromes(long[] digitArray, int i)
    {
        for (int j = 0; j + i <= digitArray.Length; j++)
        {
            var res = Convert.ToInt64(digitArray
                .Skip(j)
                .Take(i)
                .Select(x => x.ToString())
                .Aggregate((x, y) => x + y));

            if (_mathHelper.IsPalindrome(res))
                _output(res.ToString());
        }
    }
}