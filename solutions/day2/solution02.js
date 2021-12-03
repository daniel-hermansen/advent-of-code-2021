// Part 1
// Calculate the horizontal position and depth you would have after following the planned course. 
// What do you get if you multiply your final horizontal position by your final depth?

const fs = require("fs");

const input = fs
  .readFileSync("input.txt", { encoding: "utf-8" })
  .split("\n")
  .filter((x) => Boolean(x))
  .map((x) => {
    const [direction, n] = x.split(" ");
    return {
      direction,
      x: parseInt(n),
    };
});

let submarine = {
    horizontal: 0,
    depth: 0,
};

for(const element of input) {
    switch(element.direction) {
        case "forward":
            submarine.horizontal += element.x;
            break;
        case "up":
            submarine.depth -= element.x;
            break;
        case "down":
            submarine.depth += element.x;
            break;
    }
}

console.log(submarine.horizontal * submarine.depth)

// Part 2
// In addition to horizontal position and depth, you'll also need to track a third value, aim, which also starts at 0. 
// The commands also mean something entirely different than you first thought:

    // down X increases your aim by X units.
    // up X decreases your aim by X units.
    // forward X does two things:
        // It increases your horizontal position by X units.
        // It increases your depth by your aim multiplied by X.

        // Using this new interpretation of the commands, calculate the horizontal position 
        // and depth you would have after following the planned course. 
        // What do you get if you multiply your final horizontal position by your final depth?

submarine = {
    horizontal: 0,
    depth: 0,
    aim: 0,
};

for(const element of input){
    switch(element.direction) {
        case "forward":
            submarine.horizontal += element.x;
            submarine.depth += submarine.aim * element.x;
            break;
        case "up":
            submarine.aim -= element.x;
            break;
        case "down":
            submarine.aim += element.x;
            break;
    }
}

console.log(submarine.horizontal * submarine.depth);