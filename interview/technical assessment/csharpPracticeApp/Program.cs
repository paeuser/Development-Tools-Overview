
//PrintNumbers();
//int result = ComputeFactorial(5);
//Console.WriteLine("Factorial of 5 is " + result);

//PrintGrowingTriangle(4);
//PrintUpsideDownSymmetricalTriangle(5);

//result = CountDigits(10);
//Console.WriteLine("# of digits in 10 is:" + result);

//PrintFibonacciUpTo(20);


//task 1 
//object testInput;
//testInput = "Hello, World!";     
//testInput = new int[] { 1, 2 };  
//testInput = new { Name = "A" };
//testInput = 1041;
//Console.WriteLine($"Input: {testInput}");


//testInput = new Solution().solution((int)testInput);

//Console.WriteLine($"\nOuput: {testInput}");

//int[] tests = { 1, 2, 3, 9, 15, 20, 32, 1041, 529, 2147483647 };

//foreach (int n in tests)
//{
//    Console.WriteLine($"N = {n}, Gap = {new Solution().solution(n)}");
//}


// task2 
//int[] testInput = [3, 8, 9, 7, 6];
//int[] result = Solution.solution(testInput, 3);
//Console.WriteLine("3");
//Console.WriteLine("\nInput:  " + string.Join(", ", testInput));
//Console.WriteLine("Output: " + string.Join(", ", result));

//static void PrintNumbers()
//{
//    for (int i = 0; i < 5; i++)
//    {
//        Console.WriteLine(i);
//    }
//}


////task 3
////int result = new Solution().solution(10, 85, 30);
////Console.WriteLine($"To get from: {10} to {85} the frog needs to jump {result} times.");


//int result = new Solution().solution([5,5,1,9,9,4] );
//Console.WriteLine($"the are {result} unique numbers");


//task 5

//using System.Diagnostics.Metrics;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//int result = new Solution().solution(6, 11, 2);
//Console.WriteLine($"The number of integers divisible by 2 between 6 and 11 is: {result}");


// The factorial of a positive integer n (written as n!) is the product 
// of all positive integers from 1 up to n.
//
// For example:
// 1! = 1
// 2! = 1 × 2 = 2
// 3! = 1 × 2 × 3 = 6
// 4! = 1 × 2 × 3 × 4 = 24
// 5! = 1 × 2 × 3 × 4 × 5 = 120
//static int ComputeFactorial(int n)
//{
//    int factorial = 1;
//    for (int i = 1; i <= n; i++)
//    {
//        factorial *= i;  // multiply factorial by i
//    }
//    return factorial;
//}

//static void PrintGrowingTriangle(int n)
//{
//    // Outer loop: iterate through rows from 1 to n
//    for (int i = 1; i <= n; i++)
//    {
//        // Inner loop: print i asterisks separated by spaces
//        for (int j = 0; j < i; j++)
//        {
//            Console.Write("* ");
//        }
//        // Move to the next line after each row
//        Console.WriteLine();
//    }
//}

//static void PrintUpsideDownSymmetricalTriangle(int n)
//{
//    // Outer loop: rows from n down to 1
//    for (int i = n; i >= 1; i--)
//    {
//        // First inner loop: print indentation spaces (2 spaces per indentation)
//        for (int j = 0; j < n - i; j++)
//        {
//            Console.Write("  "); // two spaces for indentation
//        }

//        // Second inner loop: print 2*i - 1 asterisks separated by spaces
//        for (int j = 0; j < 2 * i - 1; j++)
//        {
//            Console.Write("* ");
//        }

//        // Move to next line after each row
//        Console.WriteLine();
//    }
//}

//static int CountDigits(int n)
//{
//    // Handle the special case when n is 0
//    if (n == 0) return 1;

//    int count = 0;

//    // While n is greater than 0, keep dividing by 10
//    while (n > 0)
//    {
//        n = n / 10;  // Integer division drops the last digit
//        count++;     // Increase the count for each digit
//    }

//    return count;
//}


// 🔢 What Is the Fibonacci Sequence?
// The Fibonacci sequence is a series of numbers where:
//
// - The first number is 0
// - The second number is 1
// - Each next number is the sum of the two numbers before it
//
// This is how we build the sequence:
//
// Step | Previous Two Numbers | Sum | New Number
// -----|----------------------|-----|------------
// 1    | (Start)              |  -  | 0
// 2    | (Start)              |  -  | 1
// 3    | 0 + 1                |  1  | 1
// 4    | 1 + 1                |  2  | 2
// 5    | 1 + 2                |  3  | 3
// 6    | 2 + 3                |  5  | 5
// 7    | 3 + 5                |  8  | 8
// 8    | 5 + 8                | 13  | 13
//
// So the sequence goes: 0, 1, 1, 2, 3, 5, 8, 13...
// Let's now write a function to print all Fibonacci numbers not exceeding a given number n.

//static void PrintFibonacciUpTo(int n)
//{
//    int a = 0;  // First Fibonacci number
//    int b = 1;  // Second Fibonacci number

//    while (a <= n)
//    {
//        Console.Write(a + " "); // Print the current number in the sequence
//        int c = a + b;          // Compute the next number
//        a = b;                  // Move b to a
//        b = c;                  // Move c to b
//    }
//}

//task default
//class Solution
//{
//    public  int solution(int N)
//    {
//        Console.WriteLine("This is a placeholder solution.");
//        return N; 
//    }
//}

//train task 1 
//class Solution
//{
//    public  int solution(int N)
//    {
//        string binary = Convert.ToString(N, 2);
//        int maxGap = 0; 
//        int currentGap = 0;
//        bool counting = false;
//        foreach (char bit in binary)
//        { 
//            if (bit == '1')
//            {
//                if (counting)
//                {
//                    maxGap = Math.Max(maxGap, currentGap);
//                }
//                currentGap = 0; 
//                counting = true; 
//            }
//            else if (counting)
//            {
//                currentGap++; 
//            }
//        }

//        return maxGap;
//    }
//}

// task 2 
// class Solution
//{
//    // Fixes:
//    // 1. CS0161: Ensure all code paths return a value.
//    // 2. IDE1006: Renamed method to start with an uppercase character.
//    // 3. CA1822: Marked the method as static since it does not access instance data.
//    // 4. IDE0060: Removed unused parameter 'K'.

//    public static int[] solution(int[] A, int K)
//    {
//        if (A.Length == 0)
//            return A;

//        K = K % A.Length;
//        if (K == 0)
//            return A;

//        int[] result = new int[A.Length];
//        // Copy the last K elements of the array A into the beginning of the result array.
//        // For example, if A = [3, 8, 9, 7, 6] and K = 3,
//        // then A.Length - K = 2, so we copy [7, 6] starting at index 0 of result.
//        Array.Copy(A, A.Length - K, result, 0, K);

//        // Copy the first (Length - K) elements of array A into result starting at index K.
//        // Using the same example: copy [3, 8] into result starting at index 3.
//        Array.Copy(A, 0, result, K, A.Length - K);

//        return result;

//    }
//}

// task 2  using for loop 
//class Solution
//{
//    public static int[] solution(int[] A, int K)
//    {
//        if (A.Length == 0)
//            return A;

//        K = K % A.Length;
//        if (K == 0)
//            return A;

//        int[] result = new int[A.Length];
//        for (int i = 0; i < A.Length; i++)
//        {
//            // If the left number is smaller than the right number, the result of % is just the left number.
//            int newIndex = (i + K) % A.Length;
//            result[newIndex] = A[i];
//        }

//        return result;
//    }
//}


////task 3 
//class Solution
//{
//    public int solution(int X, int Y, int D)
//    {
//        return (Y - X + D - 1) / D;
//    }
////}


//teask 4
//class Solution
//{
//    public int solution(int[] A)
//    {
//        HashSet<int> distinctValues = new HashSet<int>();

//        foreach (int value in A)
//        {
//            distinctValues.Add(value);
//        }

//        return distinctValues.Count;
//    }
//}


//task5 
// Brute Force(Too Slow for Large Ranges):
// This works, but is too slow for large values (A and B can be up to 2,000,000,000).
//class Solution
//{
//    public int solution(int A, int B, int K)
//    {
//        int count = 0;
//        for (int i = A; i <= B; i++)
//        {
//            if (i % K == 0) count++;

//        }
//        return count;
//    }
//}


// Task 5
//✅ Updated C# Code with Beginner Explanation

// Task 5: Count numbers divisible by K in the range [A..B]
// Example input: A = 6, B = 11, K = 2
// Expected output: 3 → because 6, 8, and 10 are divisible by 2

//class Solution
//{
//    public int solution(int A, int B, int K)
//    {
//        // Step 1: Count how many numbers from 1 up to B are divisible by K
//        int countUpToB = B / K;

//        // Step 2: Count how many numbers from 1 up to A - 1 are divisible by K
//        // (we subtract 1 from A because we want to start counting from A itself)
//        int countBeforeA = (A - 1) / K;

//        // Step 3: Subtract to get how many numbers are divisible by K in [A..B]
//        return countUpToB - countBeforeA;
//    }
//}

//🧠 Beginner Summary

//Imagine all numbers that can be divided evenly by K:

//    From 1 to B, the total number of divisible values is B / K

//    From 1 to A - 1, the total is (A - 1) / K

//    So, to find how many are in the range [A..B], subtract the second from the first:

//    (B / K) - ((A - 1) / K)

//This avoids using loops and gives the answer instantly, even for large numbers.

//Would you like a version wrapped in a console app for testing input/output as well?




//demo
//int result = new Solution().solution(new int[] { 1, 3, 6, 4, 1, 2 });

//class Solution
//{
//    public int solution(int[] A)
//    {
//        HashSet<int> seen = new HashSet<int>();

//        foreach (int num in A)
//        {
//            if (num > 0)
//                seen.Add(num);
//        }

//        for (int i = 1; i <= A.Length + 1; i++)
//        {
//            if (!seen.Contains(i))
//                return i;
//        }

//        return 1; // fallback, should never reach here
//    }
//}




/************************************************************************************************************/

//EXAM//

/************************************************************************************************************/


//Task 1


//int[] A = { 1, 3, 6, 4, 1, 2 };  // You can change this input
//Solution s = new Solution();
//int result = s.solution(A);
//Console.WriteLine(result);  // Expected


//class Solution
//{
//    public int solution(int[] A)
//    {
//        HashSet<int> found = new HashSet<int>();
//        foreach (int num in A)
//            if (num > 0)
//                found.Add(num);

//        for (int i = 1; i <= A.Length + 1; i++)
//            if (!found.Contains(i))
//                return i;

//        return 1;
//    }
//}


int [] A = { -3, 1, 2, -2, 5, 6 };
Solution s = new Solution();
int result =s.solution(A);
Console.WriteLine(result);  

class Solution
{
    public int solution(int[] A)
    {
      Array.Sort(A);
      int n = A.Length;
      int maxCount = A[n-1] * A[n -2] * A[n - 3];
      int minCount = A[0] * A[1] * A[n - 1];
      int result= Math.Max(maxCount, minCount);
      return result;

    }
}
