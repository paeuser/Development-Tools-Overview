function reverseString(s) {
    try {
        // Attempt to split the string into an array of characters
        // then reverse the array and join it back into a string
        let reversed = s.split('').reverse().join('');
        
        // Print the reversed string if no error occurs
        console.log(reversed);
    } catch (e) {
        // If an error occurs (e.g., s is not a string), print the error message
        console.log(e.message);
        
        // Print the original input string when an error happens
        console.log(s);
    }
}


let result = reverseString('hello');

