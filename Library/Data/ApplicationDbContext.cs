using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;

namespace Library.Data
{
	public class ApplicationDBContext: DbContext
	{
		public ApplicationDBContext(
			DbContextOptions<ApplicationDBContext> options)
			: base(options)
		{
			Seed();
		}

		private void Seed()
		{
			if (Library.IsNullOrEmpty())
			{
				ImmutableList<LibraryModel> libraries =
				[
					new () {Genre = "Halacha"},
					new () {Genre = "Torah"},
					new () {Genre = "Talmud"},
					new () {Genre = "Musar"},
					new () {Genre = "Hashkafa"}
				];
				Library.AddRange(libraries);
				SaveChanges();
			}
		}

		public DbSet<LibraryModel> Library { get; set; }
		public DbSet<ShelfModel> Shelves { get; set; }
		public DbSet<SetModel> Sets { get; set; }
		public DbSet<BookModel> books { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LibraryModel>()
				.HasMany(library => library.Shelves)
				.WithOne(shelf => shelf.Library)
				.HasForeignKey(shelf => shelf.LibraryId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ShelfModel>()
				.HasMany(shelf => shelf.Sets)
				.WithOne(set => set.Shelf)
				.HasForeignKey(set => set.ShelfId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<SetModel>()
				.HasMany(set => set.Books)
				.WithOne(book => book.Set)
				.HasForeignKey(book => book.SetId)
				.OnDelete(DeleteBehavior.Cascade);
				
		}

	}
}
