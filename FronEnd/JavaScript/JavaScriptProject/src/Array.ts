const numbers: number[] = [1, 2, 3, 4, 5];
const names: string[] = ["Alice", "Bob", "Charlie"];


interface Claim {
  id: number;
  amount: number;
  status: "Open" | "Closed";
}

const claims: Claim[] = [
  { id: 1, amount: 500, status: "Open" },
  { id: 2, amount: 1000, status: "Closed" },
  { id: 3, amount: 1200, status: "Open" }
];


const openClaims = claims.filter(claim => claim.status === "Open");

console.log(openClaims);


// What map() does

// map() transforms each item in an array and returns 
// a new array of the same length, without changing the original array.

const claimIds = claims.map(claim => claim.id);
console.log("Claim IDs:", claimIds);

// Map to a summary string
const claimSummaries = claims.map(claim => {
  return `Claim #${claim.id} is ${claim.status} and amount is $${claim.amount}`;
});

console.log("Summaries:", claimSummaries);

//Example 3: Combine filter() and map()

const openClaimIds = claims
  .filter(claim => claim.status === "Open")
  .map(claim => claim.id);

console.log("Open Claim IDs:", openClaimIds);
