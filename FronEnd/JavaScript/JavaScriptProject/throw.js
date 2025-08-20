function isPositive(a) {
    if (a > 0) {
        // If 'a' is positive, return "YES"
        return "YES";
    } else if (a === 0) {
        // If 'a' is zero, throw an Error with message "Zero Error"
        throw new Error("Zero Error");
    } else {
        // If 'a' is negative, throw an Error with message "Negative Error"
        throw new Error("Negative Error");
    }
}
  

// console.log(isPositive(3)); // Output: "YES"
// console.log(isPositive(0)); // Throws "Zero Error"
console.log(isPositive(-3)); // Throws "Negative Error"