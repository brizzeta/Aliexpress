export enum OrderStatus {
    Pending = 1,          // Ожидает оплаты/подтверждения
    Confirmed = 2,        // Подтверждён продавцом
    Processing = 3,       // В обработке
    Shipped = 4,          // Отправлен
    Delivered = 5,        // Доставлен
    Cancelled = 6,        // Отменён
    Refunded = 7,         // Возврат оформлен
    Returned = 8          // Товар возвращён
}