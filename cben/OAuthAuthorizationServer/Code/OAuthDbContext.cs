using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OAuthAuthorizationServer.Code
{
    public class OAuthDbContext : DbContext
    {

        public OAuthDbContext()
            : base("DbConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

            //Database.CreateIfNotExists();
            //Database.Initialize(true);
        }


        public DbSet<CocoBen.Authorization.User> Users { get; set; }

        public DbSet<CocoBen.Authorization.Client> Clients { get; set; }

        public DbSet<CocoBen.Authorization.Nonce> Nonces { get; set; }

        public DbSet<CocoBen.Authorization.SymmetricCryptoKey> SymmetricCryptoKeys { get; set; }

        public DbSet<CocoBen.Authorization.ClientAuthorization> ClientAuthorizations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<CocoBen.Authorization.User>()
                .ToTable("User").HasKey<int>(m => m.UserId);
            modelBuilder.Entity<CocoBen.Authorization.User>().Property(m => m.UserId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CocoBen.Authorization.User>().Property(m => m.OpenIDClaimedIdentifier)
                .HasMaxLength(150).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.User>().Property(m => m.OpenIDFriendlyIdentifier)
                .HasMaxLength(150);

            modelBuilder.Entity<CocoBen.Authorization.Client>()
                .ToTable("Client").HasKey<int>(m => m.ClientId);
            modelBuilder.Entity<CocoBen.Authorization.Client>().Property(m => m.ClientId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CocoBen.Authorization.Client>().Property(m => m.ClientIdentifier)
                .HasMaxLength(50).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.Client>().Property(m => m.ClientSecret)
                .HasMaxLength(50);

            modelBuilder.Entity<CocoBen.Authorization.Nonce>()
                .ToTable("Nonce").HasKey(m => new { m.Context, m.Code, m.Timestamp });
            modelBuilder.Entity<CocoBen.Authorization.Nonce>()
                .Property(m => m.Context).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.Nonce>()
                .Property(m => m.Code).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.Nonce>()
                .Property(m => m.Timestamp).IsRequired();

            modelBuilder.Entity<CocoBen.Authorization.SymmetricCryptoKey>()
                .ToTable("SymmetricCryptoKey").HasKey(m => new { m.Bucket, m.Handle });
            modelBuilder.Entity<CocoBen.Authorization.SymmetricCryptoKey>()
                .Property(m => m.Bucket).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.SymmetricCryptoKey>()
                .Property(m => m.Handle).IsRequired();

            modelBuilder.Entity<CocoBen.Authorization.ClientAuthorization>()
                .ToTable("ClientAuthorization").HasKey<int>(m => m.AuthorizationId);
            modelBuilder.Entity<CocoBen.Authorization.ClientAuthorization>()
                .Property(t => t.AuthorizationId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CocoBen.Authorization.ClientAuthorization>()
                .Property(m => m.CreatedOnUtc).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.ClientAuthorization>()
                .Property(m => m.ClientId).IsRequired();
            modelBuilder.Entity<CocoBen.Authorization.ClientAuthorization>()
                .Property(m => m.UserId).IsRequired();
            
            //base.OnModelCreating(modelBuilder);

        }
    }
}