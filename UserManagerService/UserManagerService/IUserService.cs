using UserModel.UserModel;

namespace UserManagerService.UserManagerService
{
    public interface IUserService<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> Get();
        TDocument Get(string Id);
        TDocument Create(TDocument document);
        void Update(string Id, TDocument document);
        void Remove(string Id);
    }
}