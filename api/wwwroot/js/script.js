window.onload = async function() 
{
    await loadHomePage();
}

const loadedJsFiles = []

async function loadHomePage() 
{
    let main = document.getElementById("main");
    let data = await getPageData("/api/home-page");
    if(data === undefined)
    {
        main.innerHTML = "Erro ao carregar Home"
        return;
    }
    main.innerHTML = data.page;
}

async function loadAboutPage()
{
    let main = document.getElementById("main");
    let data = await getPageData("/api/about-page");
    if(data === undefined)
    {
        main.innerHTML = "Erro ao carregar About"
        return;
    }
    main.innerHTML = data.page;
}

async function loadListinhaPage()
{
    let main = document.getElementById("main");
    let data = await getPageData("/api/listinha-page");
    if(data === undefined)
    {
        main.innerHTML = "Erro ao carregar About"
        return;
    }
    if(data.scriptSrc !== undefined)
    {
        if(!loadedJsFiles.includes(data.scriptSrc))
        {
            let script = document.createElement("script");
            script.src = data.scriptSrc;
            main.appendChild(script);
            loadedJsFiles.push(data.scriptSrc);
        }
    }
    main.innerHTML = data.page;
}

async function getPageData(pageDataResource)
{
    try 
    {
        const response = await fetch(pageDataResource);
        if(!response.ok) {
            throw new Error(`Erro na solicitação: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } 
    catch(error) 
    {
        console.error(error.message);
        return undefined;
    }
}