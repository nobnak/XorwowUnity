using UnityEngine;
using System.Collections;
using Xorwow;
using Xorwow.Extension;

public class Second : MonoBehaviour {
	public const string CS_WIDTH = "_Width";
	public const string CS_HEIGHT = "_Height";

	public int size = 64;
	public Material mat;

	private XorwowService _xorwow;
	private RenderTexture _outputTex;

	void Start () {
		_outputTex = new RenderTexture(size, size, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
		_outputTex.filterMode = FilterMode.Point;
		_outputTex.Create();
		_xorwow = new XorwowService(size * size);

		GetComponent<Renderer>().sharedMaterial.mainTexture = _outputTex;
	}
	void OnDestroy() {
		if (_outputTex != null) {
			Destroy(_outputTex);
		}
		if (_xorwow != null) {
			_xorwow.Dispose();
			_xorwow = null;
		}
	}
	
	void Update () {
		Graphics.ClearRandomWriteTargets();
		mat.SetInt(CS_WIDTH, size);
		mat.SetInt(CS_HEIGHT, size);
		mat.SetXorwowStateBuf(_xorwow);
		Graphics.SetRandomWriteTarget(1, _xorwow.XorwowStateBuf);
		_outputTex.DiscardContents();
		Graphics.Blit(null, _outputTex, mat);
		Graphics.ClearRandomWriteTargets();
	}
}
