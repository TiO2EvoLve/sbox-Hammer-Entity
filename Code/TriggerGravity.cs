
[Icon("radio_button_checked"),Group( "Hammer" ), Title( "trigger_gravity" )]
public class TriggerGravity : Component,Component.ITriggerListener
{
	[Property] public float Gravity { get; set; }
	[Property] public Vector3 Gravitydir { get; set; } = Vector3.Down;
	[Property,ShowIf("UnlimitedTags",false)] public TagSet Tags { get; set; } = new(){"player"};
	[Property,Group("flag")] public bool UnlimitedTags{ get; set; }
	private bool isEnter;
	private Rigidbody rig;
	
	public void OnTriggerEnter( GameObject other )
	{
		if(!other.Tags.HasAny( Tags ) && !UnlimitedTags)return;
		rig = other.Components.GetInChildrenOrSelf<Rigidbody>();
		if ( rig is not null )
		{
			isEnter = true;
		}
	}
	public void OnTriggerExit( GameObject other )
	{
		rig = other.Components.GetInChildrenOrSelf<Rigidbody>();
		if ( rig is not null )
		{
			isEnter = false;
			rig.Gravity = true;
		}
	}
	protected override void OnFixedUpdate()
	{
		if ( isEnter )
		{
			rig.ApplyForce( Gravitydir.Normal * Gravity );
		}
	}
}
