#ifndef XORWOW_INCLUDE
#define XORWOW_INCLUDE

struct XorwowState {
	uint x, y, z, w, v, d;
};



RWStructuredBuffer<XorwowState> _XorwowStateBuf;



uint XorwowRandom(inout XorwowState s) {
	uint t;
	t = s.x ^ (s.x >> 2);
	s.x = s.y; s.y = s.z; s.z = s.w; s.w = s.v; s.v = (s.v ^ (s.v << 4)) ^ (t ^ (t << 1));
	return (s.d += 362437) + s.v;
}
uint XorwowRandom(uint stateIndex) {
	return XorwowRandom(_XorwowStateBuf[stateIndex]);
}
float XorwowRandomFloat(uint stateIndex) {
	return XorwowRandom(stateIndex) / 4294967296.0;
}

#endif