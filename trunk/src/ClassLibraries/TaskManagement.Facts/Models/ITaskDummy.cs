namespace Cavity.Models
{
    using System;
    using Cavity.Data;

    public sealed class ITaskDummy : ITask
    {
        DataCollection ITask.Execute(DataCollection configuration)
        {
            throw new NotSupportedException();
        }
    }
}