using App.Common.Abstract.Helpers;
using App.Utils.Entities;

namespace App.Utils.Extensions
{
    public static class CreatedUpdatedInfoExtension
    {
        public static void AddCreatedInfo(this ICreatedUpdatedEntity entity, string byUserId)
        {
            entity.Created = NodaTimeHelper.Now;
            entity.CreatedBy = byUserId;

            if (entity.Updated != null || entity.UpdatedBy != null)
                throw new ApplicationException($"{nameof(entity.Updated)} or {nameof(entity.UpdatedBy)} is not Null");
        }
        public static void AddUpdatedInfo(this IUpdatedEntity entity, string byUserId)
        {
            entity.Updated = NodaTimeHelper.Now;
            entity.UpdatedBy = byUserId;
        }
        public static void SetDeletedInfo(this IRemovableEntity entity, string byUserId)
        {
            if (entity.Removed != null)
                throw new ApplicationException("Deleted is already set");

            entity.Removed = NodaTimeHelper.Now;
            entity.RemovedBy = byUserId;
        }
    }
}
