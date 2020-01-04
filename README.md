# PhysicsBone v1.0.0
Tested on Unity version 2019.3.0f1.

## What *exactly* does this component do?
This component applies basic physics simulation to character's bones.
The main intent of this component was to provide a way to have neat jointed-hair physics
*without* running performance heavy physics calculations in the background.

## Can I use it?
Yes! The component is free to use in both commercial and non-commercial projects, as long as you credit me.
Simply download the `PhysicsBone.cs` file from this repository and include it in your project.

## How do I use it?
After including the script in your project, add the `PhysicsBone` component to a bone that you wish to apply
physics to.
That's it, the component should work as soon as you hit the play button!

In addition to that, you can tweak the component to your liking with these properties exposed in the inspector:

| Property               | Function      |
| -------------         |:-------------:|
| Collision radius      | Specifies the radius in which each bone will test for collision with the environment. |
| Collision accuracy    | Specifies how accurately the bone will position itself after a collision happened. A high value is suggested. |
| Stiffness             | Specifies how stiff/heavy the bone is. Low stiffness value results in very light bones. |
| Gravity               | Specifies how fast the hair will fall. |
| Force faloff          | Specifies how the force is distributed from the root towards its children in descending order. |

------------------------------------------------
    Copyright (c) 2020 Abcight

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
