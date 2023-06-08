using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NearMessage.Domain.Chats;

namespace NearMessage.Persistence.EntityTypeConfigurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(com => com.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.Sender)
            .WithMany(u => u.SentChats)
            .HasForeignKey(u => u.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Receiver)
            .WithMany(u => u.ReceivedChats)
            .HasForeignKey(u => u.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
