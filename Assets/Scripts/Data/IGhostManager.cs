namespace View.Managers
{
    public interface IGhostManager
    {
        GhostNodeView RootGhostNodeView { get; }
        bool IsStarted { get; }
        void Execute();
    }
}