
// Iterative Approach (using a loop)
function factorial1(n)
{
    let result =1;
    for (let i=1; i<=n; i++)
        {
            result *= i;
        }
    return result;    
}




//✅ 2. Recursive Approach
//This method uses the mathematical definition of factorial:

function factorial2(n) {
    if (n === 0) {
        return 1;
    }
    return n * factorial2(n - 1);
}

console.log(factorial2(3)); // Output: 6
