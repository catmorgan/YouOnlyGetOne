using UnityEngine;
using UnityEditor;

public class ZwickTech : MonoBehaviour
{


    [MenuItem("Editor Helpers/Make Quad %&q")]

    public static void makeQuad()
    {
        Transform camPos;
        camPos = Camera.main.transform;

        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        //quad.transform.position = new Vector3(0, 0, 0);
        quad.transform.position = new Vector3(camPos.position.x, camPos.position.y - 10, camPos.position.z);
        quad.transform.eulerAngles = new Vector3(90, 0, 0);
	    quad.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        quad.GetComponent<Renderer>().receiveShadows = false;
        DestroyImmediate(quad.GetComponent<MeshCollider>());
    }
	
	[MenuItem("Editor Helpers/Make Quad as Child")]
	
	public static void makeQuad_asChild()
	{
		
		Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
		
		foreach (Transform transform in transforms)
		{
			GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			
			quad.transform.parent = transform;
			quad.transform.localPosition = Vector3.zero;
			quad.transform.localScale = Vector3.one;
			quad.transform.localRotation = Quaternion.identity;
			
			quad.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			quad.GetComponent<Renderer>().receiveShadows = false;
			DestroyImmediate(quad.GetComponent<MeshCollider>());
		}
	}

}
