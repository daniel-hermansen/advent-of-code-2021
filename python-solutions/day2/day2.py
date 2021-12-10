import os
import sys

with open(os.path.join(sys.path[0], "input.txt")) as f:
    input = [line.split() for line in f]
    input = [(direction, int(delta)) for direction, delta in input]

x, y, y1 = 0, 0, 0
for direction, change in input:
    if direction == 'forward':
        x += change
        y1 += y * change
    elif direction == 'up':
        y -= change
    elif direction == 'down':
        y += change

print(x * y)    # Part 1
print(x * y1)   # Part 2