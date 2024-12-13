[Icon("code"),Group( "Hammer" ), Title( "logic_case" )]
public class LogicCase : Component
{
	[Property] private Dictionary<int, Action> OnCase = new ();
	[Property] private Action OnDefault { get; set; }
	private List<int> _remainingKeys = new ();
	private Random _random = new ();

	// 输入一个值，根据值执行与Case对应的逻辑
	public void InValue( int value )
	{
		if ( OnCase.Count == 0 ) return;
		foreach ( var item in OnCase )
		{
			if ( value == item.Key )
			{
				item.Value?.Invoke();
				return;
			}
		}
		OnDefault?.Invoke();
	}
	// 随机选择一个Case执行
	public void PickRandom()
	{
		if ( OnCase.Count == 0 ) return;
		var keys = OnCase.Keys.ToList();
		int randomIndex = _random.Next( 0, keys.Count );
		int randomKey = keys[randomIndex];
		OnCase[randomKey]?.Invoke();
	}
	// 随机选择一个Case依次执行，直到所有Case都执行过一次
	public void PickRandomShuffle()
	{
		if ( OnCase.Count == 0 ) return;
		if ( _remainingKeys.Count == 0 )
		{
			_remainingKeys.AddRange( OnCase.Keys );
		}
		int randomIndex = _random.Next( 0, _remainingKeys.Count );
		int randomKey = _remainingKeys[randomIndex];
		// 执行选中的 Action
		OnCase[randomKey]?.Invoke();
		// 从剩余的 Keys 中移除已执行的 Key
		_remainingKeys.RemoveAt( randomIndex );
	}
	// 依次执行所有Case
	public void PickCaseAll()
	{
		if ( OnCase.Count == 0 ) return;
		foreach ( var action in OnCase.Values )
		{
			action?.Invoke();
		}
	}
	// 清空所有Case
	public void Clear()
	{
		OnCase.Clear();
		_remainingKeys.Clear();
	}
}
