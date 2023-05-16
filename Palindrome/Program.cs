using Palindrome;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the number in which you want to find palindromes: ");
        string input = Console.ReadLine() ?? string.Empty;

        ParallelSolution parallelSolution = new ParallelSolution(Console.WriteLine);
        parallelSolution.Run(input);

        AsyncTaskSolution asyncTaskSolution = new AsyncTaskSolution(Console.WriteLine);
        await asyncTaskSolution.RunAsync(input);

        ThreadSolution threadSolution = new ThreadSolution(Console.WriteLine);
        threadSolution.Run(input);

        ThreadPoolSolution threadPoolSolution = new ThreadPoolSolution(Console.WriteLine);
        threadPoolSolution.Run(input);

        Console.ReadKey();
    }
}