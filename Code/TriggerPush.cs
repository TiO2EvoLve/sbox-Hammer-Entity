
[Icon("upgrade"),Group( "Hammer" ), Title( "trigger_push" )]
public class TriggerPush : Component,Component.ITriggerListener
{
	[Property] public Vector3 PushDirection { get; set; }
	[Property] public float PushForce { get; set; }
	[Property,ShowIf("UnlimitedTags",false)] public TagSet Tags { get; set; } = new(){"player"};
	[Property,Group("flag")] public bool UnlimitedTags{ get; set; }
	private BoxCollider Collider => Components.Get<BoxCollider>();
	
	public void OnTriggerEnter( GameObject other )
	{
		if(!other.Tags.HasAny( Tags ) && !UnlimitedTags)return;
		Rigidbody rig = other.Components.GetInChildrenOrSelf<Rigidbody>();
		if ( rig is not null )
		{
			rig.ApplyForce( PushDirection.Normal * PushForce );
		}
	}
	protected override void DrawGizmos()
	{
		Gizmo.Draw.Color = Color.Green;
		Gizmo.Draw.LineThickness = 2;
		Gizmo.Draw.Arrow( Collider.Center , WorldPosition +  PushDirection.Normal * 100);
	}
}
