using System;

namespace Igmite.Lighthouse.Entities
{
    [Flags]
    public enum RequestType
    {
        New,
        Add,
        Save,
        View,
        Edit,
        Update,
        Delete,
        Cancel,
        Close,
        Exit,
        Inactive,
        Approve,
        Delegate
    }
}