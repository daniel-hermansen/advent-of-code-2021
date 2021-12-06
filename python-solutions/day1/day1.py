import os
import sys

with open(os.path.join(sys.path[0], "input.txt")) as f:
    lines = [int(line) for line in f]

# Part 1 - the number of increasing depths
print(sum(d0 < d1 for d0, d1 in zip(lines, lines[1:])))

# Part 2 - the number of increasing depths in a 3-measurement sliding window
print(sum(d0 < d3 for d0, d3 in zip(lines, lines[3:])))