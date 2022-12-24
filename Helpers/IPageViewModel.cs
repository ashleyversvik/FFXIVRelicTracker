namespace FFXIVRelicTracker
{
    public interface IPageViewModel
    {
        string Name { get; }
        public void LoadAvailableJobs();
    }
}
