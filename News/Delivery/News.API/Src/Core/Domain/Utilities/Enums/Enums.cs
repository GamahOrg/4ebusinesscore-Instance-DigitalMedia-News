namespace Domain.Enums
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }
    public enum ActionNameEnum
    {
        Get,
        Post,
        Put,
        Delete,
    }

    public enum NewsStatus
    {
        Ordinary,
        Important
    }
}