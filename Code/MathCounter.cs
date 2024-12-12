[Icon("calculate"),Group( "Hammer" ), Title( "math_counter" )]
public class MathCounter : Component
{
	[Property] public Action OnHitMax { get; set; }//当满足最大值时
	[Property] public Action OnHitMin { get; set; }//当满足最小值时
	[Property] public Action OnChanged { get; set; }//当值改变时
	[Property,Group( "flag" )] public bool AutoReset { get; set; }//是否自动重置
	[Property,ShowIf("AutoReset",true), Group( "flag" )] public int ResetValue { get; set; }//如果重置，设定重置的值
	private int _value;
	private int value
	{//内部的记录值
		get => _value;
		set
		{
			if ( _value != value )
			{
				_value = value;
				OnChanged?.Invoke();
				CheckValue();
			}
		}
	}
	[Property] public int Min { get; set; }//最小值
	[Property] public int Max { get; set; }//最大值
	//直接设置Value的值
	public void SetValue( int value )
	{
		this.value = value;
		CheckValue();
	}
	//设置一个值不触发任何输出
	public void SetValueNoFire( int value )
	{
		this.value = value;
	}
	//获取当前值
	public int GetValue()
	{
		return value;
	}
	//设置最大值
	public void SetHitMax( int value )
	{
		Max = value;
	}
	//设置最小值
	public void SetHitMin( int value )
	{
		Min = value;
	}
	//增加Value的值
	public void Add( int value )
	{
		this.value += value;
		CheckValue();
	}
	//减少Value的值
	public void Subtract( int value )
	{
		this.value -= value;
		CheckValue();
	}
	//计算乘法
	public void Multiply( int value )
	{
		this.value *= value;
		CheckValue();
	}
	//计算除法
	public void Divide( int value )
	{
		if(value ==0)return;
		this.value /= value;
		CheckValue();
	}
	//检查值是否满足最大值或最小值
	private void CheckValue()
	{
		if ( value == Max )
		{
			OnHitMax?.Invoke();
			if ( AutoReset ) value = ResetValue;
		}
		if ( value == Min )
		{
			OnHitMin?.Invoke();
			if ( AutoReset ) value = ResetValue;
		}
	}
}
