using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpheresController : MonoBehaviour
{
	[SerializeField] private List<TargetSphere> targetSpheres;
	[SerializeField] private FlyingTarget flyingTarget;
	[SerializeField] private float speed;
	[SerializeField] private TargetSphere currentStartSphere;

	public void ChangePositions(TargetSphere targetSphere)
	{
		if (currentStartSphere == targetSphere) return;

		var otherSphere = targetSpheres.FirstOrDefault(x => x != targetSphere);

		otherSphere.ChangePosition();
		currentStartSphere = targetSphere;
		flyingTarget.currentStartSphere = currentStartSphere;

		if (targetSphere == null) return;


		flyingTarget.transform.position = targetSphere.transform.position;
		flyingTarget.SetVelocity((-targetSphere.transform.position + otherSphere.transform.position).normalized * speed);
	}

	public void SetVerticalPositions()
	{
		foreach (var sphere in targetSpheres)
		{
			sphere.SetVerticalPositions();
		}

		flyingTarget.transform.position = targetSpheres[0].transform.position;
		currentStartSphere = targetSpheres[0];
		flyingTarget.currentStartSphere = targetSpheres[0];
	}

	public void SetStartVelocity()
	{
		flyingTarget.Enabled(true);
		flyingTarget.SetVelocity((-targetSpheres[0].transform.position + targetSpheres[1].transform.position).normalized * speed);
	}

	public void Stop()
	{
		flyingTarget.Enabled(false);
		flyingTarget.Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
	}
}
