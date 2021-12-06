import os
import sys

with open(os.path.join(sys.path[0], "input.txt")) as f:
    data = [int(x, 2) for x in f]
    bits = max(x.bit_length() for x in data)

# Part 1
gamma = 0
for i in range(bits):
    gamma_bit = sum((x >> i) & 1 for x in data) > len(data) / 2
    gamma |= gamma_bit << i
epsilon = 2 ** bits - 1 ^ gamma
print(gamma * epsilon)

# Part 2
def part2Filter(bitField, criteria) -> int:
    for i in range(bits -1, 0, -1):
        requiredBit = sum((x >> i) & 1 for x in bitField) >= len(bitField) / 2
        requiredBit = requiredBit if criteria else not requiredBit
        bitField = [x for x in bitField if (x >> i) & 1 == requiredBit]
        if len(bitField) == 1:
            break
    return bitField[0]

o2_rating = part2Filter(data,1)
co2_rating = part2Filter(data,0)

print(o2_rating * co2_rating)