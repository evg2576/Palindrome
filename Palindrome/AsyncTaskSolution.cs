using System.Diagnostics;

namespace Palindrome;
internal class AsyncTaskSolution
{
    public const int MIN_DIGITS_COUNT_IN_PALINDROME = 2;

    readonly Stopwatch _stopwatch;
    readonly MathHelper _mathHelper;
    readonly Action<string?> _output;

    public AsyncTaskSolution(Action<string?> output)
    {
        _mathHelper = new MathHelper();
        _stopwatch = new Stopwatch();
        _output = output;
    }

    public async Task RunAsync(string text)
    {
        try
        {
            var digitArray = text.ToArray();

            _stopwatch.Start();
            await Task.Run(() =>
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

                _output($"Time for Async Task: {_stopwatch.ElapsedMilliseconds}");
            });
        }
        catch (Exception ex)
        {
            _output(ex.Message);
        }
    }
}