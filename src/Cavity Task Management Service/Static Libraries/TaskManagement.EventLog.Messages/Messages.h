 --------------
 HEADER SECTION
 --------------
 http://msdn.microsoft.com/library/dd996906
 ----------
 Categories
 ----------
//
//  Values are 32 bit values laid out as follows:
//
//   3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1
//   1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0
//  +---+-+-+-----------------------+-------------------------------+
//  |Sev|C|R|     Facility          |               Code            |
//  +---+-+-+-----------------------+-------------------------------+
//
//  where
//
//      Sev - is the severity code
//
//          00 - Success
//          01 - Informational
//          10 - Warning
//          11 - Error
//
//      C - is the Customer code flag
//
//      R - is a reserved bit
//
//      Facility - is the facility code
//
//      Code - is the facility's status code
//
//
// Define the facility codes
//
#define FACILITY_SYSTEM                  0x0
#define FACILITY_STUBS                   0x3
#define FACILITY_RUNTIME                 0x2
#define FACILITY_IO_ERROR_CODE           0x4


//
// Define the severity codes
//
#define STATUS_SEVERITY_WARNING          0x2
#define STATUS_SEVERITY_SUCCESS          0x0
#define STATUS_SEVERITY_INFORMATIONAL    0x1
#define STATUS_SEVERITY_ERROR            0x3


//
// MessageId: CATEGORY_GENERAL
//
// MessageText:
//
// General
//
#define CATEGORY_GENERAL                 ((WORD)0x00000001L)

//
// MessageId: CATEGORY_WINDOWS_SERVICE
//
// MessageText:
//
// Windows Service
//
#define CATEGORY_WINDOWS_SERVICE         ((WORD)0x00000002L)

 --------
 Messages
 --------
//
// MessageId: MESSAGE_SERVICE_START_SUCCESS
//
// MessageText:
//
// The task management service started successfully.
//
#define MESSAGE_SERVICE_START_SUCCESS    ((DWORD)0x000003E9L)

//
// MessageId: MESSAGE_SERVICE_STOP_SUCCESS
//
// MessageText:
//
// The task management service stopped successfully.
//
#define MESSAGE_SERVICE_STOP_SUCCESS     ((DWORD)0x000003EAL)

//
// MessageId: MESSAGE_SERVICE_SHUTDOWN_SUCCESS
//
// MessageText:
//
// The task management service shutdown successfully.
//
#define MESSAGE_SERVICE_SHUTDOWN_SUCCESS ((DWORD)0x000003EBL)

//
// MessageId: MESSAGE_SERVICE_PAUSE_SUCCESS
//
// MessageText:
//
// The task management service paused successfully.
//
#define MESSAGE_SERVICE_PAUSE_SUCCESS    ((DWORD)0x000003ECL)

//
// MessageId: MESSAGE_SERVICE_CONTINUE_SUCCESS
//
// MessageText:
//
// The task management service continued successfully.
//
#define MESSAGE_SERVICE_CONTINUE_SUCCESS ((DWORD)0x000003EDL)

//
// MessageId: MESSAGE_SERVICE_CUSTOM_SUCCESS
//
// MessageText:
//
// The task management service successfully handled the %1 custom command.
//
#define MESSAGE_SERVICE_CUSTOM_SUCCESS   ((DWORD)0x000003EEL)

//
// MessageId: MESSAGE_SERVICE_POWER_SUCCESS
//
// MessageText:
//
// The task management service successfully handled the %1 power event.
//
#define MESSAGE_SERVICE_POWER_SUCCESS    ((DWORD)0x000003EFL)

//
// MessageId: MESSAGE_SERVICE_SESSION_SUCCESS
//
// MessageText:
//
// The task management service successfully handled the %1 session change.
//
#define MESSAGE_SERVICE_SESSION_SUCCESS  ((DWORD)0x000003F0L)

//
// MessageId: MESSAGE_SERVICE_START_ERROR
//
// MessageText:
//
// The task management service failed to start.
//
#define MESSAGE_SERVICE_START_ERROR      ((DWORD)0xC00003F3L)

//
// MessageId: MESSAGE_SERVICE_STOP_ERROR
//
// MessageText:
//
// The task management service failed to stop.
//
#define MESSAGE_SERVICE_STOP_ERROR       ((DWORD)0xC00003F4L)

//
// MessageId: MESSAGE_SERVICE_SHUTDOWN_ERROR
//
// MessageText:
//
// The task management service failed to shutdown.
//
#define MESSAGE_SERVICE_SHUTDOWN_ERROR   ((DWORD)0xC00003F5L)

//
// MessageId: MESSAGE_SERVICE_PAUSE_ERROR
//
// MessageText:
//
// The task management service failed to pause.
//
#define MESSAGE_SERVICE_PAUSE_ERROR      ((DWORD)0xC00003F6L)

//
// MessageId: MESSAGE_SERVICE_CONTINUE_ERROR
//
// MessageText:
//
// The task management service failed to continue.
//
#define MESSAGE_SERVICE_CONTINUE_ERROR   ((DWORD)0xC00003F7L)

//
// MessageId: MESSAGE_SERVICE_CUSTOM_ERROR
//
// MessageText:
//
// The task management service failed to handle the %1 custom command.
//
#define MESSAGE_SERVICE_CUSTOM_ERROR     ((DWORD)0xC00003F8L)

//
// MessageId: MESSAGE_SERVICE_POWER_ERROR
//
// MessageText:
//
// The task management service failed to handle the %1 power event.
//
#define MESSAGE_SERVICE_POWER_ERROR      ((DWORD)0xC00003F9L)

//
// MessageId: MESSAGE_SERVICE_SESSION_ERROR
//
// MessageText:
//
// The task management service failed to handle the %1 session change.
//
#define MESSAGE_SERVICE_SESSION_ERROR    ((DWORD)0xC00003FAL)

//
// MessageId: MESSAGE_LOGGING_NOT_CONFIGURED
//
// MessageText:
//
// Logging has not been configured.
//
#define MESSAGE_LOGGING_NOT_CONFIGURED   ((DWORD)0x8000044CL)

