using LibraryManagementSoftwareRepository.DbConfigure;
using LibraryManagementSoftwareRepository.IRepository;
using LibraryManagementSoftwareRepository.Repository;
using LibraryManagementSoftwareServices.IServices;
using LibraryManagementSoftwareServices.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LibraryManagementSoftwareModels.Entities;
using LibraryManagementSoftwareServices.Additional;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var con = builder.Configuration.GetConnectionString("LibraryManagementSoftware");

builder.Services.AddDbContext<LibraryManagementDbContext>(x => x.UseSqlServer(con));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<LibraryManagementDbContext>()
	.AddDefaultTokenProviders();


*/asdfjlka*/
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true; 
	options.Password.RequireUppercase = true;
	options.User.RequireUniqueEmail = true;
});

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

builder.Services.AddAutoMapper(typeof(MappinProfile));

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
