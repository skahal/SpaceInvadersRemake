using UnityEngine;
using System.Collections;
using System.Linq;

//[RequireComponent(typeof(SpriteBuilder))]
public class Pixel3DExplosion : MonoBehaviour {
	private MeshRenderer m_spriteRenderer;
	private SpriteBuilderFromMeshRenderer m_spriteBuilder;
	private GameObject m_pixels3DContainer;

	public Vector3 Scale = new Vector3 (0.021f, 0.021f, 0.021f);
	public GameObject Pixel3DPrefab;
	public float TimeToLife = 3f;
	public float AutoExplodeDelay = -1f;
	public float ExplosionForce = 200f;
	public float ExplosionRadius = 10f;
	public Vector3 ExplosionRelativePosition = Vector3.zero;
	public bool VoxelateOnStart = false;
	public int ColliderEveryVoxel = 2;

	[Range(0, 1)]
	public float ZScaleByColorLuminance = .1f;
	[Range(0, 1)]
	public float MinAlphaToVoxel = 0.1f;

	public bool KeepContainerParent = true;


	// Use this for initialization
	void Start () {

		if (VoxelateOnStart) {
			Voxelate ();
		}

		if (AutoExplodeDelay > -1) {
			StartCoroutine (StartAutoExplode ());
		}
	}

	public TextAsset AtlasFile;

	public void Voxelate() {
		if (m_pixels3DContainer == null) {
			m_spriteRenderer = GetComponent<MeshRenderer> ();
			m_spriteBuilder = GetComponent<SpriteBuilderFromMeshRenderer> ();
			m_spriteBuilder.Build ();

			var pixels = m_spriteBuilder.ToPixels ();
			m_spriteRenderer.enabled = false;

			var localScale = transform.localScale;
			var tempScale = new Vector3 (Scale.x * localScale.x, Scale.y * localScale.y, Scale.z * localScale.z);
			var rect = m_spriteBuilder.Texture;

			var halfWidth = (rect.width / 2) * tempScale.x;
			var halfHeight = (rect.height / 2) * tempScale.y;
			m_pixels3DContainer = new GameObject ("Pixels3DContainer");

			if (KeepContainerParent) {
				m_pixels3DContainer.transform.parent = transform;
			}

			foreach (var p in pixels) {
				if (p.Color.a >= MinAlphaToVoxel) {
					var pixel3D = Instantiate (Pixel3DPrefab, new Vector3 (
						transform.position.x + p.X * tempScale.x - halfWidth, 
						transform.position.y + p.Y * tempScale.y - halfHeight,
						0)
						, Quaternion.identity) as GameObject;

					var luminance = 0.2126f * p.Color.r + 0.7152f * p.Color.g + 0.0722f * p.Color.b;
					pixel3D.transform.localScale = new Vector3(tempScale.x, tempScale.y, tempScale.z + luminance * ZScaleByColorLuminance);
					var cubeRenderer = pixel3D.GetComponent<MeshRenderer> ();
					cubeRenderer.material.color = p.Color;
					pixel3D.transform.parent = m_pixels3DContainer.transform;
				}
			}
		}
	}

	public void Explode()
	{
		Voxelate ();
		var index = 0;

		foreach (Transform child in m_pixels3DContainer.transform) {
			
			if (index % ColliderEveryVoxel == 0) {
				child.GetComponent<BoxCollider> ().enabled = true;
			}

			var rb = child.GetComponent<Rigidbody> ();
			rb.isKinematic = false;
			rb.AddExplosionForce (ExplosionForce, transform.position + ExplosionRelativePosition, ExplosionRadius);

			index++;
		}

		StartCoroutine (StartTimeToLife ());
	}

	IEnumerator StartTimeToLife() {
		yield return new WaitForSeconds (TimeToLife);
		SendMessage ("OnPixel3DExplosionEnded");

		GameObject.Destroy (m_pixels3DContainer);
		m_pixels3DContainer = null;
	}

	IEnumerator StartAutoExplode() {
		yield return new WaitForSeconds (AutoExplodeDelay);

		Explode ();

	}
}
