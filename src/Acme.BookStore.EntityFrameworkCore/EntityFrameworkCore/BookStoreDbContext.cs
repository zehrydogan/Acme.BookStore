using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Movies;
using Acme.BookStore.Actors;
using Acme.BookStore.Directors;
using Acme.BookStore.MovieComments;
using Acme.BookStore.BookComments;
using Acme.BookStore.UserBooks;
using Acme.BookStore.UserMovies;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Book> Books { get; set; }

    public DbSet<Movie> Movies { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }
    public DbSet<MovieComment> MovieComments { get; set; }
    public DbSet<BookComment> BookComments { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }



    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Book>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Image).IsRequired().HasColumnType("nvarchar(max)");

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();

        });

        builder.Entity<Author>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Authors",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(AuthorConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });

        builder.Entity<Movie>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Movies", BookStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Image).IsRequired().HasColumnType("nvarchar(max)");
            b.HasOne<Director>().WithMany().HasForeignKey(x => x.DirectorId).IsRequired();

        });

        builder.Entity<Actor>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Actors",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ActorConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });

        builder.Entity<Director>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Directors",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(DirectorConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });

        builder.Entity<MovieActor>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "MovieActors",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<Actor>().WithMany().HasForeignKey(x => x.ActorId).IsRequired();
            b.HasOne<Movie>().WithMany().HasForeignKey(x => x.MovieId).IsRequired();
        });

        builder.Entity<MovieComment>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "MovieComments",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<Movie>().WithMany().HasForeignKey(x => x.MovieId).IsRequired();
            b.Property(x => x.Comment)
              .IsRequired()
              .HasMaxLength(MovieCommentConsts.MaxNameLength);

        });

        builder.Entity<BookComment>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "BookComments",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<Book>().WithMany().HasForeignKey(x => x.BookId).IsRequired();
            b.Property(x => x.Comment)
              .IsRequired()
              .HasMaxLength(BookCommentConsts.MaxNameLength);

        });

        builder.Entity<UserBook>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "UserBooks",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
            b.HasOne<Book>().WithMany().HasForeignKey(x => x.BookId).IsRequired();
        });
        
        builder.Entity<UserMovie>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "UserMovies",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
            b.HasOne<Movie>().WithMany().HasForeignKey(x => x.MovieId).IsRequired();
        });
    }
}