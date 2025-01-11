using API_Backend.Context;
using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API_Backend.Repo.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext appDbContext;
        public ArticleRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Article> AddArticle(Article article)
        {
            var result = await appDbContext.Articles.AddAsync(article);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Article> DeleteArticle(int id)
        {
            var result = await appDbContext.Articles
                .FirstOrDefaultAsync(a => a.ArticleId == id);
            if (result != null)
            {
                appDbContext.Articles.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Article> GetArticle(int Id)
        {
            return await appDbContext.Articles
                .FirstOrDefaultAsync(a => a.ArticleId == Id);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await appDbContext.Articles.ToListAsync();
        }

        public async Task<IEnumerable<Article>> SearchArticle(string character)
        {
            IQueryable<Article> query = appDbContext.Articles;
            if (!string.IsNullOrEmpty(character))
                query = query.Where(a => a.Nom.Contains(character));
            return await query.ToListAsync();
        }

        public async Task<Article> UpdateArticle(int id, Article article)
        {
            var existingArticle = await appDbContext.Articles.FindAsync(id);
            if (existingArticle != null)
            {
                existingArticle.Nom = article.Nom ?? existingArticle.Nom;
                existingArticle.Description = article.Description ?? existingArticle.Description;
                existingArticle.SousGarantie = article.SousGarantie ?? existingArticle.SousGarantie;
                existingArticle.DateAchat = article.DateAchat ?? existingArticle.DateAchat;
                existingArticle.Prix = article.Prix ?? existingArticle.Prix;

                await appDbContext.SaveChangesAsync();
                return article;
            }
            return null;
        }
    }
}
