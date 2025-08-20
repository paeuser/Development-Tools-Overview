function vowelsAndConsonants(s) {
    const vowels = ['a', 'e', 'i', 'o', 'u'];
    let consonants = [];

    // First, print vowels
    for (let char of s) {
        if (vowels.includes(char)) {
            console.log(char);
        } else {
            consonants.push(char);
        }
    }

    // Then, print consonants
    for (let char of consonants) {
        console.log(char);
    }
}

vowelsAndConsonants('taeur');


