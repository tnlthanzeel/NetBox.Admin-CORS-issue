import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { BaseService } from "../../../core/services/base.service";
import { UpdateUserRoleClaimModel } from "../models/update-user-role-claims.model";
import { PermissionModel, PermissionGroup } from "../models/user-permission.model";
import { UserRoleModel } from "../models/user-role.model";

@Injectable({
    providedIn: 'root'
})
export class UserPermissionService extends BaseService {

    constructor() {
        super();
    }

    getAll(): Observable<ResponseResult<PermissionGroup[]>> {
        return this.get<ResponseResult<PermissionGroup[]>>(`security/app-permissions`);
    }

    getPermissionsByRole(roleId: string) {
        return this.get<ResponseResult<UserRoleModel>>(`security/roles/${roleId}/permission-templates`);
    }

    update(roleId: string, updateRoleClaim: UpdateUserRoleClaimModel) {
        return this.put(`security/roles/${roleId}`, updateRoleClaim);
    }
}
