using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
  public bool RandomDirection;
  public Vector3 Direction;
  public float Force;

	// Use this for initialization
	void Start () {
    if(RandomDirection)
      Direction = Random.insideUnitSphere;
    Direction.Normalize();
    this.GetComponent<Rigidbody>().AddTorque(Direction * Force);
 }
}
