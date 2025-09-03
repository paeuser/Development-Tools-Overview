interface Claim {
  id: number;
  amount: number;
  status: "Open" | "Closed";
}

// Simulate an async operation to fetch claims
// Simulated service (with delay):
function getClaims(): Promise<Claim[]> {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve([
        { id: 1, amount: 1000, status: "Open" },
        { id: 2, amount: 500, status: "Closed" },
        { id: 3, amount: 1500, status: "Open" },
      ]);
    }, 1000); // 1 second delay
  });
}

// Async function to filter and log open claims
async function logOpenClaims() {
  const claims = await getClaims(); // Wait for the "fetch"
  const openClaims = claims.filter((claim) => claim.status === "Open");
  console.log("Open claims:", openClaims);
}

logOpenClaims();

