// the specific names of the fields in MessageDto will need to change once we have a set naming convention
export interface ChatMessage {
    ticketId: string;
    user: string,
    message: string,
    date: Date
}

export interface OpenTicket {
    chatRoomId: string,
    user: string,
    message: string,
    open: boolean
}
