namespace BusinessLogicLayer.Finder
{
    public interface IFinder<T> where T : class
    {
        IEnumerable<T> Find(Func<T, bool> predicate);

        T FirstOrDefault(Func<T, bool> predicate);
    }
}
