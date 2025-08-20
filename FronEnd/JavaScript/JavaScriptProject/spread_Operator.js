// ... allows as interval such as 
// array or string to be expanded
// into separate elements   
// (unpack the elements )


let numbers = [1,2,3,4,5];

// Use spread operator to pass array elements as arguments
//we can't use spread with the array directly
// let maximum = Math.max(numbers);
//but if we use spread operator
// it will unpack the elements of the array
// and pass them as individual arguments to the function    
let maximum = Math.max(...numbers);
let minimum = Math.min(...numbers);
console.log('Numbers:', numbers);
console.log('Minimum:', minimum);
console.log('Maximum:', maximum);


let username ="eric cabrera";
let letters = [...username].join("-");    ;
console.log('Letters:', letters);


let fruits = ['apple', 'banana', 'cherry'];
// Use spread operator to create a new array with additional elements
console.log(fruits);
//shallow copy of the array
let newFruits = [...fruits];
console.log('New Fruits:', newFruits);


let vegetables = ['carrot', 'broccoli', 'spinach'];
// Use spread operator to combine two arrays
let combined = [...fruits, ...vegetables , "eggs","tomato"];
console.log('Combined:', combined);

