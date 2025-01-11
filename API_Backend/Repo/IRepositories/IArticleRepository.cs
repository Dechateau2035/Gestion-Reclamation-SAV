using API_Backend.Models;

namespace API_Backend.Repo.IRepositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(int Id);
        Task<Article> AddArticle(Article article);
        Task<Article> UpdateArticle(int id, Article article);
        Task<Article> DeleteArticle(int id);
        Task<IEnumerable<Article>> SearchArticle(string character);
    }
}
