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
        public void op_FailureOnContinue()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnContinue();
                }
            }
        }

        [Fact]
        public void op_FailureOnCustomCommand_int()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    new TaskManagementEventLog().FailureOnCustomCommand(1);
                }
            }
        }

        [Fact]
        public void op_FailureOnPause()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnPause();
                }
            }
        }

        [Fact]
        public void op_FailureOnPowerEvent_PowerBroadcastStatus()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnPowerEvent(PowerBroadcastStatus.BatteryLow);
                }
            }
        }

        [Fact]
        public void op_FailureOnSessionChange_SessionChangeDescription()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnSessionChange(new SessionChangeDescription());
                }
            }
        }

        [Fact]
        public void op_FailureOnShutdown()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnShutdown();
                }
            }
        }

        [Fact]
        public void op_FailureOnStart()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnStart();
                }
            }
        }

        [Fact]
        public void op_FailureOnStop()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.FailureOnStop();
                }
            }
        }

        [Fact]
        public void op_LoggingNotConfigured()
        {
            using (var obj = new TaskManagementEventLog())
            {
                if (obj.SourceExists())
                {
                    obj.LoggingNotConfigured();
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