const Input = require('./input')

const sonarScans = Input.scans.split('\n').map(Number);

// As the submarine drops below the surface of the ocean, it automatically performs a sonar sweep of the nearby sea floor. On a small screen, the sonar sweep report (your puzzle input) appears: each line is a measurement of the sea floor depth as the sweep looks further and further away from the submarine.

// For example, suppose you had the following report:

// 199
// 200
// 208
// 210
// 200
// 207
// 240
// 269
// 260
// 263
// This report indicates that, scanning outward from the submarine, the sonar sweep found depths of 199, 200, 208, 210, and so on.

// The first order of business is to figure out how quickly the depth increases, just so you know what you're dealing with - you never know if the keys will get carried into deeper water by an ocean current or a fish or something.

// To do this, count the number of times a depth measurement increases from the previous measurement.

const problem1 = (input) => {
    let depthIncreases = 0;
    for(let i=1; i < input.length; i++){
        if(input[i] > input[i-1]){
            depthIncreases = depthIncreases + 1;
        }
    }
    return depthIncreases;
}

const solution1 = problem1(sonarScans);
console.log(`Part 1: ${solution1} measurements are larger than the previous measurement`)

// Your goal now is to count the number of times the sum of measurements in this sliding window increases from the previous sum. 
// So, compare A with B, then compare B with C, then C with D, and so on. 
// Stop when there aren't enough measurements left to create a new three-measurement sum.

// This can be solved by comparing the measurement entering and the measurement leaving, ie: ([i] and [i-3])

const problem2 = (input) => {
    let depthIncreases = 0;
    for(let i=3; i < input.length; i++){
        if(input[i] > input[i-3]){
            depthIncreases = depthIncreases + 1;
        }
    }
    return depthIncreases;
}

const solution2 = problem2(sonarScans);
console.log(`Part 2: ${solution2} measurements are larger than the previous measurement`)