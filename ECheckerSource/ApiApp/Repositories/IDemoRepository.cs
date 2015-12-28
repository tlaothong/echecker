namespace ApiApp.Repositories
{
    /// <summary>
    /// An Demo Repository Interface
    /// </summary>
    public interface IDemoRepository
    {
        /// <summary>
        /// Pretend to get some emails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetEmail(string userId);
    }
}