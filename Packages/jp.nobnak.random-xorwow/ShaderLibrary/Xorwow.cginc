#ifndef XORWOW_INCLUDE
#define XORWOW_INCLUDE

struct XorwowState {
	uint x, y, z, w, v, d;
};



RWStructuredBuffer<XorwowState> _XorwowStateBuf;



uint Xorwow_NextUInt(inout XorwowState s) {
	uint t;
	t = s.x ^ (s.x >> 2);
	s.x = s.y; s.y = s.z; s.z = s.w; s.w = s.v; s.v = (s.v ^ (s.v << 4)) ^ (t ^ (t << 1));
	return (s.d += 362437) + s.v;
}
uint Xorwow_NextUInt(uint stateIndex) {
	return Xorwow_NextUInt(_XorwowStateBuf[stateIndex]);
}
float Xorwow_NextFloat(uint stateIndex) {
	return Xorwow_NextUInt(stateIndex) / 4294967296.0;
}

#endif