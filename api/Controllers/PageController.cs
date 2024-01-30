using api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api")]
public class PageController : ControllerBase
{
    public PageController()
    {
        
    }

    [HttpGet]
    [Route("/")]
    public async Task<ContentResult> Index()
    {
        //string index = await GetIndex("wwwroot/index.html");
        string layoutsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layouts");
        string indexPath = Path.Combine(layoutsDir, "index.html");
        Console.WriteLine(indexPath);
        if(System.IO.File.Exists(indexPath))
        {
            string index = await System.IO.File.ReadAllTextAsync(indexPath);
            return new ContentResult
            {
                Content = index,
                ContentType = "text/html",
                StatusCode = 200
            };
        }
        return new ContentResult
        {
            Content = @"<h1>Page Not Found</h1>",
            ContentType = "text/html",
            StatusCode = 404
        };
    }

    [HttpGet]
    [Route("home-page")]
    public async Task<PageDataDto> GetHomePage()
    {
        string layoutsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layouts");
        string pagePath = Path.Combine(layoutsDir, "home.layout.html");
        return await GetPageContent(pagePath);
    }

    [HttpGet]
    [Route("about-page")]
    public async Task<PageDataDto> GetAboutPage()
    {
        string layoutsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layouts");
        string pagePath = Path.Combine(layoutsDir, "about.layout.html");
        return await GetPageContent(pagePath);
    }

    [HttpGet]
    [Route("listinha-page")]
    public async Task<PageDataDto> GetListinhaPage()
    {
        string layoutsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layouts");
        string pagePath = Path.Combine(layoutsDir, "listinha.layout.html");
        return await GetPageContent(pagePath, "js/listinha.js");
    }

    async Task<PageDataDto> GetPageContent(string pagePath, string? scriptSrc = null)
    {
        string content = await System.IO.File.ReadAllTextAsync(pagePath);
        return new PageDataDto
        {
            Page = content,
            ScriptSrc = scriptSrc
        };
    }
}