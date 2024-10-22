using LibraryManagementSoftwareRepository.DbConfigure;
using LibraryManagementSoftwareRepository.IRepository;
using LibraryManagementSoftwareRepository.Repository;
using LibraryManagementSoftwareServices.IServices;
using LibraryManagementSoftwareServices.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var con = builder.Configuration.GetConnectionString("LibraryManagementSoftware");
builder.Services.AddDbContext<LibraryManagementDbContext>(x => x.UseSqlServer(con));

builder.Services.AddScoped<IBookService,     BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILoanService,     LoanService>();
builder.Services.AddScoped<IStaffService,    StaffService>();
builder.Services.AddScoped<IStudentService,  StudentService>();

builder.Services.AddScoped<IBookRepository ,   BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILoanRepository,    LoanRepository>();
builder.Services.AddScoped<IStaffRepository,   StaffRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();


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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
