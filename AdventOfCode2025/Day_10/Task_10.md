# --- Day 10: Factory ---

Just across the hall, you find a large factory. Fortunately, the Elves here have plenty of time to decorate. Unfortunately, it's because the factory machines are all offline, and none of the Elves can figure out the initialization procedure.

The Elves do have the manual for the machines, but the section detailing the initialization procedure was eaten by a Shiba Inu. All that remains of the manual are some indicator light diagrams, button wiring schematics, and joltage requirements for each machine.

For example:
```
[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
```
The manual describes one machine per line. Each line contains a single indicator light diagram in [square brackets], one or more button wiring schematics in (parentheses), and joltage requirements in {curly braces}.

To start a machine, its indicator lights must match those shown in the diagram, where . means off and # means on. The machine has the number of indicator lights shown, but its indicator lights are all initially off.

So, an indicator light diagram like [.##.] means that the machine has four indicator lights which are initially off and that the goal is to simultaneously configure the first light to be off, the second light to be on, the third to be on, and the fourth to be off.

You can toggle the state of indicator lights by pushing any of the listed buttons. Each button lists which indicator lights it toggles, where 0 means the first light, 1 means the second light, and so on. When you push a button, each listed indicator light either turns on (if it was off) or turns off (if it was on). You have to push each button an integer number of times; there's no such thing as "0.5 presses" (nor can you push a button a negative number of times).

So, a button wiring schematic like (0,3,4) means that each time you push that button, the first, fourth, and fifth indicator lights would all toggle between on and off. If the indicator lights were [#.....], pushing the button would change them to be [...##.] instead.

Because none of the machines are running, the joltage requirements are irrelevant and can be safely ignored.

You can push each button as many times as you like. However, to save on time, you will need to determine the fewest total presses required to correctly configure all indicator lights for all machines in your list.

There are a few ways to correctly configure the first machine:
```
[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
```
You could press the first three buttons once each, a total of 3 button presses.
You could press (1,3) once, (2,3) once, and (0,1) twice, a total of 4 button presses.
You could press all of the buttons except (1,3) once each, a total of 5 button presses.
However, the fewest button presses required is 2. One way to do this is by pressing the last two buttons ((0,2) and (0,1)) once each.

The second machine can be configured with as few as 3 button presses:
```
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
```
One way to achieve this is by pressing the last three buttons ((0,4), (0,1,2), and (1,2,3,4)) once each.

The third machine has a total of six indicator lights that need to be configured correctly:
```
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
```
The fewest presses required to correctly configure it is 2; one way to do this is by pressing buttons (0,3,4) and (0,1,2,4,5) once each.

So, the fewest button presses required to correctly configure the indicator lights on all of the machines is 2 + 3 + 2 = 7.

Analyze each machine's indicator light diagram and button wiring schematics. What is the fewest button presses required to correctly configure the indicator lights on all of the machines?

## --- Part Two ---

All of the machines are starting to come online! Now, it's time to worry about the joltage requirements.

Each machine needs to be configured to exactly the specified joltage levels to function properly. Below the buttons on each machine is a big lever that you can use to switch the buttons from configuring the indicator lights to increasing the joltage levels. (Ignore the indicator light diagrams.)

The machines each have a set of numeric counters tracking its joltage levels, one counter per joltage requirement. The counters are all initially set to zero.

So, joltage requirements like {3,5,4,7} mean that the machine has four counters which are initially 0 and that the goal is to simultaneously configure the first counter to be 3, the second counter to be 5, the third to be 4, and the fourth to be 7.

The button wiring schematics are still relevant: in this new joltage configuration mode, each button now indicates which counters it affects, where 0 means the first counter, 1 means the second counter, and so on. When you push a button, each listed counter is increased by 1.

So, a button wiring schematic like (1,3) means that each time you push that button, the second and fourth counters would each increase by 1. If the current joltage levels were {0,1,2,3}, pushing the button would change them to be {0,2,2,4}.

You can push each button as many times as you like. However, your finger is getting sore from all the button pushing, and so you will need to determine the fewest total presses required to correctly configure each machine's joltage level counters to match the specified joltage requirements.

Consider again the example from before:
```
[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
```
Configuring the first machine's counters requires a minimum of 10 button presses. One way to do this is by pressing (3) once, (1,3) three times, (2,3) three times, (0,2) once, and (0,1) twice.

Configuring the second machine's counters requires a minimum of 12 button presses. One way to do this is by pressing (0,2,3,4) twice, (2,3) five times, and (0,1,2) five times.

Configuring the third machine's counters requires a minimum of 11 button presses. One way to do this is by pressing (0,1,2,3,4) five times, (0,1,2,4,5) five times, and (1,2) once.

So, the fewest button presses required to correctly configure the joltage level counters on all of the machines is 10 + 12 + 11 = 33.

Analyze each machine's joltage requirements and button wiring schematics. What is the fewest button presses required to correctly configure the joltage level counters on all of the machines?

---

## Solution Approach: Using SCIP for Integer Linear Programming

### Problem Classification

Part Two of this problem is a classic **Integer Linear Programming (ILP)** problem. Here's why:

- **Decision Variables:** How many times to press each button (must be non-negative integers)
- **Constraints:** Each counter must reach exactly its target joltage value
- **Objective Function:** Minimize the total number of button presses
- **Linear Relationships:** Each button press adds 1 to specific counters (linear effect)

### Why Use SCIP?

**SCIP (Solving Constraint Integer Programs)** is an open-source solver specifically designed for mixed integer programming problems. For this puzzle, brute-force approaches would be computationally infeasible:

- If you have 6 buttons and need to reach values up to 100, there are potentially millions of combinations to try
- SCIP uses sophisticated algorithms (branch-and-bound, cutting planes, presolving) to find optimal solutions efficiently
- It's part of Google OR-Tools library, making it accessible in C#, Python, and Java

### Mathematical Formulation

For a machine with `n` buttons and `m` counters:

**Variables:**
- Let `x[i]` = number of times button `i` is pressed (where i = 0 to n-1)

**Constraints:**
- For each counter `j`, the sum of presses from buttons that affect it must equal the target:
  ```
  Σ(x[i] for all buttons i that affect counter j) = target[j]
  ```

**Objective:**
- Minimize: `Σ(x[i])` - the total number of button presses

**Example:** First machine `{3,5,4,7}` with buttons `(3) (1,3) (2) (2,3) (0,2) (0,1)`:

```
Counter 0: x[5] = 3                    (only button (0,1) affects counter 0)
Counter 1: x[1] + x[5] = 5             (buttons (1,3) and (0,1) affect counter 1)
Counter 2: x[2] + x[3] + x[4] = 4      (buttons (2), (2,3), (0,2) affect counter 2)
Counter 3: x[0] + x[1] + x[3] = 7      (buttons (3), (1,3), (2,3) affect counter 3)

Minimize: x[0] + x[1] + x[2] + x[3] + x[4] + x[5]
```

SCIP solves this system and finds that the minimum is **10 presses**.

### Implementation Details

The solution uses Google OR-Tools with the SCIP solver:

1. **Create Integer Variables:** One for each button, ranging from 0 to 10,000
2. **Add Constraints:** For each counter, create an equation ensuring the sum of relevant button presses equals the target
3. **Set Objective:** Minimize the sum of all button press variables
4. **Solve:** SCIP finds the optimal solution (or determines if none exists)

### Why This Works

- **Guaranteed Optimality:** SCIP proves the solution is minimal (not just a good guess)
- **Handles Complexity:** Works even when counters are affected by multiple overlapping buttons
- **Efficient:** Solves in milliseconds what would take hours with brute force
- **Robust:** Returns -1 if the problem has no solution (constraints are impossible to satisfy)

### Alternative Solvers

Other suitable solvers for this problem:
- **CBC** (COIN-OR Branch and Cut) - Another open-source ILP solver
- **CP-SAT** (Google's Constraint Programming solver) - Often faster for discrete optimization
- **Gurobi/CPLEX** - Commercial solvers with superior performance for large-scale problems

For this Advent of Code problem, SCIP is an excellent choice: free, open-source, and performant enough for the input sizes involved.

