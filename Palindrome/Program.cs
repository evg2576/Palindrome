using Palindrome;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the number in which you want to find palindromes: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(input) && Int64.TryParse(input, out _))
            {
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
            else throw new ArgumentException("Еhe entered text is not a number!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}