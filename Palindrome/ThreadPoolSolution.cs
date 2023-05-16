using System.Diagnostics;

namespace Palindrome;
internal class ThreadPoolSolution
{
    public const int MIN_DIGITS_COUNT_IN_PALINDROME = 2;

    readonly Stopwatch _stopwatch;
    readonly MathHelper _mathHelper;
    readonly Action<string?> _output;

    public ThreadPoolSolution(Action<string?> output)
    {
        _stopwatch = new Stopwatch();
        _mathHelper = new MathHelper();
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
            ThreadPool.QueueUserWorkItem((_) =>
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
                _output($"Time for ThreadPool: {_stopwatch.ElapsedMilliseconds}");
            });
        }
        catch (Exception ex)
        {
            _output(ex.Message);
        }
    }
}
