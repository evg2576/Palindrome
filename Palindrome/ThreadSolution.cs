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
            var digitArray = text.ToArray();
            List<Thread> threads = new List<Thread>();

            for (int i = MIN_DIGITS_COUNT_IN_PALINDROME; i <= digitArray.Length; i++)
            {
                int k = i;
                threads.Add(new Thread(() =>
                {
                    for (int j = 0; j + k <= digitArray.Length; j++)
                    {
                        var res = Convert.ToInt64(digitArray
                            .Skip(j)
                            .Take(k)
                            .Select(x => x.ToString())
                            .Aggregate((x, y) => x + y));

                        if (_mathHelper.IsPalindrome(res))
                            _output(res.ToString());
                    }
                }));
            }

            _stopwatch.Start();
            foreach (var thread in threads)
                thread.Start();

            foreach (var thread in threads)
                thread.Join();

            _stopwatch.Stop();
            _output($"Time for Thread: {_stopwatch.ElapsedMilliseconds}");
        }
        catch (Exception ex)
        {
            _output(ex.Message);
        }
    }
}
