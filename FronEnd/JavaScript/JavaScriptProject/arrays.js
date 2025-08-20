function getSecondLargest(nums) {
    // Remove duplicates by converting to a Set, then back to array
    let uniqueNums = [...new Set(nums)];
    
    // Sort the array in descending order
    uniqueNums.sort((a, b) => b - a);
    
    // Return the second largest number (index 1)
    return uniqueNums[1];
}


function modifyArray(nums) {
    // Use map to create a new array where
    // even numbers are doubled and odd numbers are tripled
    return nums.map(num => (num % 2 === 0 ? num * 2 : num * 3));
}



let result = getSecondLargest([2, 3, 6, 6, 5]);
console.log(result); // Output: 5   

console.log(modifyArray([1, 2, 3, 4, 5])); // Output: [3, 4, 9, 8, 15]
