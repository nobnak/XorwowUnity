using UnityEngine;
using System.Collections;
using Xorwow;
using Xorwow.Extension;

public class First : MonoBehaviour {
	public const int CS_KERNEL = 0;
	public const string CS_OUTPUT_TEX = "_OutputTex";
	public const string CS_OUTPUT_WIDTH = "_Width";
	public const int CS_NUMTHREAD = 8;

	public int size = 64;
	public ComputeShader compute;

	private XorwowService _xorwow;
	private RenderTexture _outputTex;
	private int _nGroups;

	void Start () {
		_xorwow = new XorwowService(size * size);
		_outputTex = new RenderTexture(size, size, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
		_outputTex.enableRandomWrite = true;
		_outputTex.filterMode = FilterMode.Point;
		_outputTex.Create();
		_nGroups = size / CS_NUMTHREAD;

		GetComponent<Renderer>().sharedMaterial.mainTexture = _outputTex;
	}
	void OnDestroy() {
		if (_xorwow != null) {
			_xorwow.Dispose();
			_xorwow = null;
		}
		if (_outputTex != null) {
			Destroy(_outputTex);
		}
	}

	void Update() {
		_outputTex.DiscardContents();

		compute.SetInt(CS_OUTPUT_WIDTH, size);
		compute.SetXorwowStateBuf(CS_KERNEL, _xorwow);
		compute.SetTexture(CS_KERNEL, CS_OUTPUT_TEX, _outputTex);
		compute.Dispatch(CS_KERNEL, _nGroups, _nGroups, 1);
	}
}
