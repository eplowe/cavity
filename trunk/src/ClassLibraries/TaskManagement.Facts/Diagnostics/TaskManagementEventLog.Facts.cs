namespace Cavity.Diagnostics
{
    using System.Diagnostics;
    using System.ServiceProcess;
    using Xunit;

    public sealed class TaskManagementEventLogFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TaskManagementEventLog>()
                            .DerivesFrom<EventLog>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_FailureOnContinue_ExceptionNull()
        {
            new TaskManagementEventLog().FailureOnContinue(null);
        }

        [Fact]
        public void op_FailureOnCustomCommand_ExceptionNull_int()
        {
            new TaskManagementEventLog().FailureOnCustomCommand(null, 1);
        }

        [Fact]
        public void op_FailureOnPause_ExceptionNull()
        {
            new TaskManagementEventLog().FailureOnPause(null);
        }

        [Fact]
        public void op_FailureOnPowerEvent_ExceptionNull_PowerBroadcastStatus()
        {
            new TaskManagementEventLog().FailureOnPowerEvent(null, PowerBroadcastStatus.BatteryLow);
        }

        [Fact]
        public void op_FailureOnSessionChange_ExceptionNull_SessionChangeDescription()
        {
            new TaskManagementEventLog().FailureOnSessionChange(null, new SessionChangeDescription());
        }

        [Fact]
        public void op_FailureOnShutdown_ExceptionNull()
        {
            new TaskManagementEventLog().FailureOnShutdown(null);
        }

        [Fact]
        public void op_FailureOnStart_ExceptionNull()
        {
            new TaskManagementEventLog().FailureOnStart(null);
        }

        [Fact]
        public void op_FailureOnStop_ExceptionNull()
        {
            new TaskManagementEventLog().FailureOnStop(null);
        }

        [Fact]
        public void op_SuccessOnContinue()
        {
            new TaskManagementEventLog().SuccessOnContinue();
        }

        [Fact]
        public void op_SuccessOnCustomCommand_int()
        {
            new TaskManagementEventLog().SuccessOnCustomCommand(1);
        }

        [Fact]
        public void op_SuccessOnPause()
        {
            new TaskManagementEventLog().SuccessOnPause();
        }

        [Fact]
        public void op_SuccessOnPowerEvent_PowerBroadcastStatus()
        {
            new TaskManagementEventLog().SuccessOnPowerEvent(PowerBroadcastStatus.Suspend);
        }

        [Fact]
        public void op_SuccessOnSessionChange_SessionChangeDescription()
        {
            new TaskManagementEventLog().SuccessOnSessionChange(new SessionChangeDescription());
        }

        [Fact]
        public void op_SuccessOnShutdown()
        {
            new TaskManagementEventLog().SuccessOnShutdown();
        }

        [Fact]
        public void op_SuccessOnStart()
        {
            new TaskManagementEventLog().SuccessOnStart();
        }

        [Fact]
        public void op_SuccessOnStop()
        {
            new TaskManagementEventLog().SuccessOnStop();
        }
    }
}