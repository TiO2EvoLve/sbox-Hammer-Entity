
[Icon("radio_button_checked"),Group( "Hammer" ), Title( "." )]
public class TriggerGravity : Component, Component.ITriggerListener
{
	[Property] public float Gravity { get; set; } = 2;
	[Property] public Vector3 Gravitydir { get; set; } = Vector3.Down;
	[Property, ShowIf("UnlimitedTags", false)] public TagSet Tags { get; set; } = new() { "solid" };
	[Property, Group("flag")] public bool UnlimitedTags { get; set; }

	private HashSet<Rigidbody> Rig = new();  // 存储所有进入触发器的Rigidbody

	public void OnTriggerEnter(GameObject other)
	{
		if (!other.Tags.HasAny(Tags) && !UnlimitedTags) return;

		var rig = other.Components.GetInChildrenOrSelf<Rigidbody>();
		if (rig is not null)
		{
			rig.Gravity = false;
			rig.Velocity = Vector3.Zero;
			Rig.Add(rig);  // 添加到集合中
		}
	}
	public void OnTriggerExit(GameObject other)
	{
		var rig = other.Components.GetInChildrenOrSelf<Rigidbody>();
		if (rig is not null)
		{
			rig.Gravity = true;
			Rig.Remove(rig);  // 从集合中移除
		}
	}

	protected override void OnFixedUpdate()
	{
		// 对集合中的每个 Rigidbody 施加低重力
		foreach (var rig in Rig)
		{
			rig.ApplyForce(Gravitydir.Normal * Gravity * 1000);
		}
	}
}
