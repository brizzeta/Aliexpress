import { Message } from './message.model';

export interface Chat {
  id: number;
  buyerId: number;
  buyerName: string;
  sellerId: number;
  sellerName: string;
  createdDate: Date;
  messages: Message[];
  lastMessage?: Message;
}