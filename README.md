# Xorwow[1] RNG (Random Number Generator) for Unity
[Xorwow Unity Package](Packages/Xorwow.unitypackage)

## Usage
### In Compute Shader
[Sample](Assets/Examples/First)
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
Get Random Value in Compute Shader.
```hlsl
uint randomUint = XorwowRandom(stateIndex);
float randomFloat = XorwowRandomFloat(stateIndex);
```
Finally, Call Dispose Method in OnDestroy()
```c#
_xorwow.Dispose();
```

### In Fragment Shader
Include Namespaces.
[Sample](Assets/Examples/Second)
```c#
using Xorwow;
using Xorwow.Extension;
```
Instantiate Xorwow.XorwowService class.
```c#
XorwowService _xorwow = new XorwowService(size * size);
```
Set Xorwow State Buffer on Material and Make it a Random Write Target
```c#
Graphics.ClearRandomWriteTargets();
mat.SetXorwowStateBuf(_xorwow);
Graphics.SetRandomWriteTarget(1, _xorwow.XorwowStateBuf);
_outputTex.DiscardContents();
Graphics.Blit(null, _outputTex, mat);
Graphics.ClearRandomWriteTargets();
```
Include Xorwow.cginc in Fragment Shader
```c#
#include "../../Packages/Xorwow/Xorwow.cginc"
```
Get Random Value in Fragment Shader
```c#
float r = XorwowRandomFloat(i);
```
Finally, Destroy Xorwow State Buffer in OnDestroy()
```c#
_xorwow.Dispose();
```

## References
1. Marsaglia, G. (2003). Xorshift RNGs. Journal Of Statistical Software, 8(14), 1–6. Retrieved from http://www.jstatsoft.org/v08/i14/paper
