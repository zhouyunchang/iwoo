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


        public DbSet<Cben.Authorization.User> Users { get; set; }

        public DbSet<Cben.Authorization.Client> Clients { get; set; }

        public DbSet<Cben.Authorization.Nonce> Nonces { get; set; }

        public DbSet<Cben.Authorization.SymmetricCryptoKey> SymmetricCryptoKeys { get; set; }

        public DbSet<Cben.Authorization.ClientAuthorization> ClientAuthorizations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<Cben.Authorization.User>()
            //    .ToTable("User").HasKey<int>(m => m.UserId);
            //modelBuilder.Entity<Cben.Authorization.User>().Property(m => m.UserId)
            //    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Cben.Authorization.User>().Property(m => m.OpenIDClaimedIdentifier)
            //    .HasMaxLength(150).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.User>().Property(m => m.OpenIDFriendlyIdentifier)
            //    .HasMaxLength(150);

            //modelBuilder.Entity<Cben.Authorization.Client>()
            //    .ToTable("Client").HasKey<int>(m => m.ClientId);
            //modelBuilder.Entity<Cben.Authorization.Client>().Property(m => m.ClientId)
            //    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Cben.Authorization.Client>().Property(m => m.ClientIdentifier)
            //    .HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.Client>().Property(m => m.ClientSecret)
            //    .HasMaxLength(50);

            //modelBuilder.Entity<Cben.Authorization.Nonce>()
            //    .ToTable("Nonce").HasKey(m => new { m.Context, m.Code, m.Timestamp });
            //modelBuilder.Entity<Cben.Authorization.Nonce>()
            //    .Property(m => m.Context).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.Nonce>()
            //    .Property(m => m.Code).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.Nonce>()
            //    .Property(m => m.Timestamp).IsRequired();

            //modelBuilder.Entity<Cben.Authorization.SymmetricCryptoKey>()
            //    .ToTable("SymmetricCryptoKey").HasKey(m => new { m.Bucket, m.Handle });
            //modelBuilder.Entity<Cben.Authorization.SymmetricCryptoKey>()
            //    .Property(m => m.Bucket).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.SymmetricCryptoKey>()
            //    .Property(m => m.Handle).IsRequired();

            //modelBuilder.Entity<Cben.Authorization.ClientAuthorization>()
            //    .ToTable("ClientAuthorization").HasKey<int>(m => m.AuthorizationId);
            //modelBuilder.Entity<Cben.Authorization.ClientAuthorization>()
            //    .Property(t => t.AuthorizationId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Cben.Authorization.ClientAuthorization>()
            //    .Property(m => m.CreatedOnUtc).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.ClientAuthorization>()
            //    .Property(m => m.ClientId).IsRequired();
            //modelBuilder.Entity<Cben.Authorization.ClientAuthorization>()
            //    .Property(m => m.UserId).IsRequired();
            
            //base.OnModelCreating(modelBuilder);

        }
    }
}