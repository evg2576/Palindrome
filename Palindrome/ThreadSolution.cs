using System.Diagnostics;

namespace Palindrome;
internal class ThreadSolution
{
    public const int MIN_DIGITS_COUNT_IN_PALINDROME = 2;

    readonly Stopwatch _stopwatch;
    readonly MathHelper _mathHelper;
    readonly Action<string?> _output;

    public ThreadSolution(Action<string?> output)
    {
        _mathHelper = new MathHelper();
        _stopwatch = new Stopwatch();
        _output = output;
    }

    public void Run(string text)
    {
        try
        {
            long input = Convert.ToInt64(text);
            var digitArray = input
            .ToString()
            .Select(digit => long.Parse(digit.ToString()))
            .ToArray();

            _stopwatch.Start();
            Thread thread = new Thread(() =>
            {
                for (int i = MIN_DIGITS_COUNT_IN_PALINDROME; i <= digitArray.Length; i++)
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
                _stopwatch.Stop();
                _output($"Time for Thread: {_stopwatch.ElapsedMilliseconds}");
            });

            thread.Start();
        }
        catch (Exception ex)
        {
            _output(ex.Message);
        }
    }
}
