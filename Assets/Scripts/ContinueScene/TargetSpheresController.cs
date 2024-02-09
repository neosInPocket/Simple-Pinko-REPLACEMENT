using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpheresController : MonoBehaviour
{
	[SerializeField] private List<TargetSphere> targetSpheres;
	[SerializeField] private FlyingTarget flyingTarget;
	[SerializeField] private float speed;

	public void ChangePositions(TargetSphere targetSphere)
	{
		foreach (var sphere in targetSpheres)
		{
			sphere.ChangePosition();
		}

		if (targetSphere == null) return;

		var otherSphere = targetSpheres.FirstOrDefault(x => x != targetSphere);
		flyingTarget.transform.position = targetSphere.transform.position;
		flyingTarget.SetVelocity((-targetSphere.transform.position + otherSphere.transform.position).normalized * speed);
	}

	public void SetInitialPositions()
	{
		ChangePositions(targetSpheres[0]);
		flyingTarget.transform.position = targetSpheres[0].transform.position;
		flyingTarget.Enabled(true);
	}
}
