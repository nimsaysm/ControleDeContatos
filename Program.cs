using ControleDeContatos.Data;
using ControleDeContatos.Helper;
using ControleDeContatos.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<BancoContext>(o => o.UseSqlServer(configuration.GetConnectionString("DataBase")));

//quando chamar a interface IHttpContextAccessor vai implementar a classe HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//qual classe a interface de sessão irá instanciar
builder.Services.AddScoped<ISessao, Sessao>();

//para envio do email de recuperação de senha
builder.Services.AddScoped<IEmail, Email>();


//ao interface ser invocada, terá a injeção de dependência
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddSession (o => 
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); //habilitar o uso de sessões

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); //rota padrão ao entrar na pagina web

app.Run();
