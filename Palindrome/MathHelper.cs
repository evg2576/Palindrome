namespace Palindrome;
internal class MathHelper
{
    public bool IsPalindrome(long x)
    {
        if (x < 0 || (x % 10 == 0 && x != 0))
            return false;

        long revertedNumber = 0;
        while (x > revertedNumber)
        {
            revertedNumber = revertedNumber * 10 + x % 10;
            x /= 10;
        }

        return x == revertedNumber || x == revertedNumber / 10;
    }
}
