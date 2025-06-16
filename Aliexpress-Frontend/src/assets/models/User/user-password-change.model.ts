export interface UserPasswordChangeDto {
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}