// Currying: This function takes one argument (a) and returns another function that takes a 
// second argument (b).
// The returned function multiplies a and b. This allows you to create specialized 
// functions like double or triple.
function multiply(a) {
  return function(b) {
    return a * b;
  };
}

// Here, multiply(2) returns a function that multiplies any input by 2.
// We assign this function to 'double', so double(5) calculates 2 * 5.
const double = multiply(2);
console.log(double(5)); // 10

// You can also use it for other values
const triple = multiply(3);
console.log(triple(5)); // 15
