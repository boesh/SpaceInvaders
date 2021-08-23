using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{

	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();

				if (instance == null)
				{
					GameObject g = new GameObject("Controller");
					instance = g.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	private void Awake()
	{
		//DontDestroyOnLoad (gameObject);
		if (instance == null)
		{
			instance = this as T;
		}
		else
		{
			if (instance != this)
			{
				Destroy(gameObject);
			}
		}
	}
}