import { Message } from './message.model';

export interface ChatListItem {
  id: number;
  buyerId: number;
  buyerName: string;
  sellerId: number;
  sellerName: string;
  createdDate: Date;
  lastMessage?: Message;
  unreadMessagesCount: number;
}