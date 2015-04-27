# Xorwow[1] RNG (Random Number Generator) for Unity
[Xorwow Unity Package](Packages/Xorwow.unitypackage)

## Usage
Include Namespaces.
```c#
using Xorwow;
using Xorwow.Extension;
```
Instantiate Xorwow.XorwowService class.
```c#
XorwowService _xorwow = new XorwowService(size * size);
```
Set Xorwow State Buffer on Compute Shader
```c#
compute.SetXorwowStateBuf(0, _xorwow);
```
Include Xorwow.cginc in Computer Shader
```hlsl
#include "Xorwow/Xorwow.cginc"
```
Get Random Value.
```hlsl
uint randomUint = XorwowRandom(stateIndex);
float randomFloat = XorwowRandomFloat(stateIndex);
```

## References
1. Marsaglia, G. (2003). Xorshift RNGs. Journal Of Statistical Software, 8(14), 1–6. Retrieved from http://www.jstatsoft.org/v08/i14/paper
