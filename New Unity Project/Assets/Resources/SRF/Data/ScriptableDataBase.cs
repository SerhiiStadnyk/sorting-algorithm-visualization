[Path("ScriptableData")]
public abstract class ScriptableDataBase<T> : ScriptableObjectSingleton<T> where T : ScriptableDataBase<T>
{
}