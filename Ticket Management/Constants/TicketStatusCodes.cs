namespace Ticket_Management.Constants
{
    public static class TicketStatusCodes
    {
        public const int OPEN = 1;
        public const int CLOSE = 2;
        public const int WORK_IN_PROGRESS = 3;
        public const int HOLD = 4;
        public const int REJECT = 5;

        public static string MapToStatus(int code)
        {
            return code switch
            {
                OPEN => TicketStatus.OPEN,
                CLOSE => TicketStatus.CLOSE,
                WORK_IN_PROGRESS => TicketStatus.WORK_IN_PROGRESS,
                HOLD => TicketStatus.HOLD,
                REJECT => TicketStatus.REJECT,
                _ => null
            };
        }
    }
}

