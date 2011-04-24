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
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnContinue(null);
                }
            }
        }

        [Fact]
        public void op_FailureOnCustomCommand_ExceptionNull_int()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    new TaskManagementEventLog().FailureOnCustomCommand(null, 1);
                }
            }
        }

        [Fact]
        public void op_FailureOnPause_ExceptionNull()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnPause(null);
                }
            }
        }

        [Fact]
        public void op_FailureOnPowerEvent_ExceptionNull_PowerBroadcastStatus()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnPowerEvent(null, PowerBroadcastStatus.BatteryLow);
                }
            }
        }

        [Fact]
        public void op_FailureOnSessionChange_ExceptionNull_SessionChangeDescription()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnSessionChange(null, new SessionChangeDescription());
                }
            }
        }

        [Fact]
        public void op_FailureOnShutdown_ExceptionNull()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnShutdown(null);
                }
            }
        }

        [Fact]
        public void op_FailureOnStart_ExceptionNull()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnStart(null);
                }
            }
        }

        [Fact]
        public void op_FailureOnStop_ExceptionNull()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnStop(null);
                }
            }
        }

        [Fact]
        public void op_SuccessOnContinue()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnContinue();
                }
            }
        }

        [Fact]
        public void op_SuccessOnCustomCommand_int()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnCustomCommand(1);
                }
            }
        }

        [Fact]
        public void op_SuccessOnPause()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnPause();
                }
            }
        }

        [Fact]
        public void op_SuccessOnPowerEvent_PowerBroadcastStatus()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnPowerEvent(PowerBroadcastStatus.Suspend);
                }
            }
        }

        [Fact]
        public void op_SuccessOnSessionChange_SessionChangeDescription()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnSessionChange(new SessionChangeDescription());
                }
            }
        }

        [Fact]
        public void op_SuccessOnShutdown()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnShutdown();
                }
            }
        }

        [Fact]
        public void op_SuccessOnStart()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnStart();
                }
            }
        }

        [Fact]
        public void op_SuccessOnStop()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.SuccessOnStop();
                }
            }
        }
    }
}