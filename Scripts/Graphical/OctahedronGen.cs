using UnityEngine;

namespace Misner.Lib.Graphical
{
	public class OctahedronGen : MonoBehaviour
	{
		#region MonoBehaviour
	    void Start()
	    {
            MeshGenerator generator = new MeshGenerator();

            MeshGenerator.Point x0 = generator.AddPoint(new Vector3(+1, 0, 0), new Vector2(0, 0));
            MeshGenerator.Point x1 = generator.AddPoint(new Vector3(-1, 0, 0), new Vector2(1, 1));

            //float c = 1;
            //float s = 0;
            float c = 0.7071f;
            float s = 0.7071f;
            MeshGenerator.Point y0 = generator.AddPoint(new Vector3(0, +c, +s), new Vector2(1, 0));
            MeshGenerator.Point y1 = generator.AddPoint(new Vector3(0, -c, -s), new Vector2(0, 1));

            float h = 0.5f;
            MeshGenerator.Point z0 = generator.AddPoint(new Vector3(0, -s, +c), new Vector2(h, h));
            MeshGenerator.Point z1 = generator.AddPoint(new Vector3(0, +s, -c), new Vector2(h, h));


            generator.AddTriangle(x0, y0, z0);
            generator.AddTriangle(x1, z0, y0);//
            generator.AddTriangle(x0, z0, y1);//
            generator.AddTriangle(x1, y1, z0);
            generator.AddTriangle(x0, z1, y0);//
            generator.AddTriangle(x1, y0, z1);
            generator.AddTriangle(x0, y1, z1);
            generator.AddTriangle(x1, z1, y1);//


            this.GetComponent<MeshFilter>().mesh = generator.BuildMesh();
        }

	    void Update()
	    {
	    }
		#endregion
	}
}
