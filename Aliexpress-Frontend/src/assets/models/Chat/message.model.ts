export interface Message {
    id: number;
    chatId: number;
    senderId: number;
    senderName: string;
    message: string;
    createdDate: Date;
  }