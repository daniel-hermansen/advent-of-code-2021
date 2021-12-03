const Input = require('./input')

const sonarScans = Input.scans.split('\n').map(Number);

const problem1 = (input) => {
    let depthIncreases = 0;
    for(let i=1; i < input.length; i++){
        console.log(i);
        console.log(input)
        if(input[i] > input[i-1]){
            depthIncreases = depthIncreases + 1;
        }
    }
    return depthIncreases;
}

const solution1 = problem1(sonarScans);
console.log(`Part 1: ${solution1} measurements are larger than the previous measurement`)
