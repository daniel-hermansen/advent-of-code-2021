const Input = require('./input')

const sonarScans = Input.scans.split('\n').map(Number);

const problem1 = (input) => {
    let depthIncreases = 0;
    console.log(input)
    for(let i=1; i < input.length; i++){
        if(input[i] > input[i-1]){
            depthIncreases = depthIncreases + 1;
            console.log(`${i}: ${input[i]} > ${input[i-1]}`)
        }
    }
    return depthIncreases;
}

const solution1 = problem1(sonarScans);
console.log(`Part 1: ${solution1} measurements are larger than the previous measurement`)
