using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using API_Backend.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Backend.Controllers
{
    [Route("api/articles/")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;
        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetArticles()
        {
            try
            {
                return Ok(await articleRepository.GetArticles());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            try
            {
                var result = await articleRepository.GetArticle(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Article>> AddArticle([FromForm] Article article)
        {
            try
            {
                if (article == null)
                    return BadRequest();
                var addArticle = await articleRepository.AddArticle(article);

                return CreatedAtAction(nameof(GetArticle), new { id = addArticle.ArticleId }, addArticle);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'ajout du nouvel article : {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Article>>> SearchArticle([FromQuery] String character)
        {
            try
            {
                var result = await articleRepository.SearchArticle(character);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            try
            {
                var article = await articleRepository.GetArticle(id);
                if (article == null)
                    return NotFound($"Article with Id:{id} not found");
                return Ok(await articleRepository.DeleteArticle(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la supression de données : {ex.Message}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Article>> UpdateArticle(int id, Article article)
        {
            try
            {
                if (id == null)
                    return BadRequest("Article ID mismatch");

                var articleToUpdate = await articleRepository.GetArticle(id);
                if (articleToUpdate == null)
                    return NotFound($"Article with Id:{id} not found");

                await articleRepository.UpdateArticle(id, article);

                return Ok(articleToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise à jour de données : {ex.Message}");
            }
        }
    }
}
