using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Repository_Interfaces;

public interface INotificationRepository
{
    List<Notification> GetNotificationsByUserId(int userId);
    void Create(Notification notification);
}