import { UserRole } from "../../enums/user-role.enum";

export interface UserDto {
    id: number;
    name: string;
    email: string;
    phone?: string;
    role: UserRole;
    address: string;
    registrationDate: string;
    profileImageUrl?: string;
    rating: number;
    isActive: boolean;
}