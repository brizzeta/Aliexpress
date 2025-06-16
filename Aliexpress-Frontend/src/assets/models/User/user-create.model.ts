import { UserRole } from "../../enums/user-role.enum";

export interface UserCreateDto {
    name: string;
    email: string;
    password: string;
    phone?: string;
    role: UserRole;
    address: string;
    profileImageUrl?: string;
}