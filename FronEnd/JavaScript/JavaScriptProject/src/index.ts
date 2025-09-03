// src/index.ts

function greet(name: string): string {
  return `Hello, ${name}!`;
}

interface Claim {
  		id: number;
  		amount: number;
  		status: "Open" | "Closed";  }  // Union type for strict status values

// Define a function 'filterOpenClaims' that takes an array of Claim objects and returns an array of Claim objects
const filterOpenClaims = (claims: Claim[]): Claim[] => {
  // Filter the input array to include only claims where the status is "Open"
  return claims.filter(claim => claim.status === "Open");
};

 let claims: Claim[] = [
  { id: 1, amount: 1000, status: "Open" },
  { id: 2, amount: 500, status: "Closed" },
  { id: 3, amount: 750, status: "Open" },

];

 console.log(filterOpenClaims(claims)); 




//console.log(greet("Eric"));
