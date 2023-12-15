using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Random = Unity.Mathematics.Random;

namespace Xorwow {

	public class XorwowService : System.IDisposable {
		public const string CS_XORWOW_STATE_BUF = "_XorwowStateBuf";

		public ComputeBuffer XorwowStateBuf { get; private set; }

		protected Random rand;

		public XorwowService(int count, uint seed = 31) {
			rand = new Random(seed);
			Init(count);
		}

		public void Init(int count) {
			ReleaseBuf();
			var states = new XorwowState[count];
			for (var i = 0; i < count; i++)
				states[i] = GenerateState();
			XorwowStateBuf = new ComputeBuffer(count, Marshal.SizeOf(states[0]));
			XorwowStateBuf.SetData(states);
		}

		#region IDisposable implementation
		public void Dispose () {
			ReleaseBuf();
		}
        #endregion

        #region methods
        public XorwowState GenerateState() {
            return new XorwowState() {
                x = rand.NextUInt(1, int.MaxValue),
                y = rand.NextUInt(1, int.MaxValue),
                z = rand.NextUInt(1, int.MaxValue),
                w = rand.NextUInt(1, int.MaxValue),
                v = rand.NextUInt(1, int.MaxValue),
                d = rand.NextUInt(1, int.MaxValue),
            };
        }
        private void ReleaseBuf() {
			if (XorwowStateBuf != null) {
				XorwowStateBuf.Release();
				XorwowStateBuf = null;
			}
		}
        #endregion

        [StructLayout(LayoutKind.Sequential)]
		public struct XorwowState {
			public uint x, y, z, w, v, d;
		};
	}

	namespace Extension {
		public static class XorwowExtension {
			public static void SetXorwowStateBuf(this ComputeShader compute, int kernelIndex, string name,  XorwowService xorwow) {
				compute.SetBuffer(kernelIndex, name, xorwow.XorwowStateBuf);
			}
			public static void SetXorwowStateBuf(this ComputeShader compute, int kernelIndex, XorwowService xorwow) {
				SetXorwowStateBuf(compute, kernelIndex, XorwowService.CS_XORWOW_STATE_BUF, xorwow);
			}

			public static void SetXorwowStateBuf(this Material mat, string name, XorwowService xorwow) {
				mat.SetBuffer(name, xorwow.XorwowStateBuf);
			}
			public static void SetXorwowStateBuf(this Material mat, XorwowService xorwow) {
				SetXorwowStateBuf(mat, XorwowService.CS_XORWOW_STATE_BUF, xorwow);
			}
		}
	}
}