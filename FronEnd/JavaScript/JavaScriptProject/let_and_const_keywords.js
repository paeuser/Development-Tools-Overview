// Import the built-in 'readline' module to read user input from the terminal
const readline = require('readline');  

// Create an interface to read from stdin (keyboard) and write to stdout (terminal)
const rl = readline.createInterface({
    input: process.stdin,    // Standard input (keyboard)
    output: process.stdout   // Standard output (console)
});

// Ask the user to enter a radius, and handle the input with a callback function
rl.question('Enter radius: ', function(input) {

    // Define a constant for the value of PI using the built-in Math object
    const PI = Math.PI;

    // Convert the input (which is a string) to a floating-point number
    const r = parseFloat(input);

    // Calculate the area of the circle using the formula: PI * r^2
    const area = PI * r * r;

    // Calculate the perimeter (circumference) using the formula: 2 * PI * r
    const perimeter = 2 * PI * r;

    // Print the area to the console
    console.log(area);

    // Print the perimeter to the console
    console.log(perimeter);

    // Close the input interface to finish the program
    rl.close();
});
