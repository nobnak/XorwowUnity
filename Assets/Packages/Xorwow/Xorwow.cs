using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace Xorwow {

	public class XorwowService : System.IDisposable {
		public const string CS_XORWOW_STATE_BUF = "_XorwowStateBuf";

		public ComputeBuffer XorwowStateBuf { get; private set; }

		public XorwowService(int count) {
			Init(count);
		}

		public void Init(int count) {
			ReleaseBuf();
			var states = new XorwowState[count];
			for (var i = 0; i < count; i++)
				states[i] = XorwowState.Generate();
			XorwowStateBuf = new ComputeBuffer(count, Marshal.SizeOf(states[0]));
			XorwowStateBuf.SetData(states);
		}

		#region IDisposable implementation
		public void Dispose () {
			ReleaseBuf();
		}
		#endregion

		private void ReleaseBuf() {
			if (XorwowStateBuf != null) {
				XorwowStateBuf.Release();
				XorwowStateBuf = null;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XorwowState {
			public uint x, y, z, w, v, d;

			public static XorwowState Generate() {
				return new XorwowState(){ 
					x = (uint) Random.Range(1, int.MaxValue),
					y = (uint) Random.Range(1, int.MaxValue),
					z = (uint) Random.Range(1, int.MaxValue),
					w = (uint) Random.Range(1, int.MaxValue),
					v = (uint) Random.Range(1, int.MaxValue),
					d = (uint) Random.Range(1, int.MaxValue),
				};
			}
		};
	}

	namespace Extension {
		public static class XorwowExtension {
			public static void SetXorwowStateBuf(this ComputeShader compute, int kernelIndex, string name,  XorwowService xorwow) {
				compute.SetBuffer(kernelIndex, name, xorwow.XorwowStateBuf);
			}
			public static void SetXorwowStateBuf(this ComputeShader compute, int kernelIndex, XorwowService xorwow) {
				compute.SetBuffer(kernelIndex, XorwowService.CS_XORWOW_STATE_BUF, xorwow.XorwowStateBuf);
			}
		}
	}
}