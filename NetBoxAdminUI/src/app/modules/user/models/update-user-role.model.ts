export class UpdateUserRoleModel {
    roleId: string;
    roleName: string;

    constructor(roleId: string, roleName: string) {
        this.roleId = roleId;
        this.roleName = roleName;
    }
}