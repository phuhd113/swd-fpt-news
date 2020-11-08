using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class NewsFPTContext : DbContext
    {
        public NewsFPTContext()
        {
        }

        public NewsFPTContext(DbContextOptions<NewsFPTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsTag> NewsTag { get; set; }
        public virtual DbSet<Subcribe> Subcribe { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserComment> UserComment { get; set; }
        public virtual DbSet<UserTag> UserTag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-MSN9OT3;Initial Catalog=NewsFPT;Persist Security Info=True;User ID=sa;Password=123456789;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelId).HasColumnName("channelID");

                entity.Property(e => e.ChannelName)
                    .HasColumnName("channelName")
                    .HasMaxLength(50);

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.GroupName)
                    .HasColumnName("groupName")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("newsID");

                entity.Property(e => e.ChannelId).HasColumnName("channelID");

                entity.Property(e => e.DayOfPost)
                    .HasColumnName("dayOfPost")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LinkImage)
                    .HasColumnName("linkImage")
                    .HasMaxLength(50);

                entity.Property(e => e.NewsContent)
                    .HasColumnName("newsContent")
                    .HasMaxLength(500);

                entity.Property(e => e.NewsTitle)
                    .HasColumnName("newsTitle")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("FK_News_Channel");
            });

            modelBuilder.Entity<NewsTag>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.NewsId });

                entity.Property(e => e.TagId).HasColumnName("tagID");

                entity.Property(e => e.NewsId).HasColumnName("newsID");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsTag)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsTag_News");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.NewsTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsTag_Tag");
            });

            modelBuilder.Entity<Subcribe>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ChannelId).HasColumnName("channelID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Channel)
                    .WithMany()
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("FK_Subcribe_Channel");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Subcribe_User");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("tagID");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.TagName)
                    .HasColumnName("tagName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.GroupId).HasColumnName("groupID");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_User_Group");
            });

            modelBuilder.Entity<UserComment>(entity =>
            {
                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(50);

                entity.Property(e => e.MasterCommentId).HasColumnName("masterCommentID");

                entity.Property(e => e.NewsId).HasColumnName("newsID");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.UserComment)
                    .HasForeignKey(d => d.NewsId)
                    .HasConstraintName("FK_UserComment_News");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserComment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserComment_User");
            });

            modelBuilder.Entity<UserTag>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TagId });

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.UserTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTag_Tag");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTag)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTag_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
