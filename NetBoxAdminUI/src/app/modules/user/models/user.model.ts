import { UserClaimModel } from "./user-role.model";

export class UserModel {
    id: string
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
    timeZone: string;
    role: string;
    displayName: string;
    roles = new Array<string>();
    permissions = new Array<string>();
    claims = new Array<UserClaimModel>();
    nicNumber: string;
    mobileNumber: string;
}