import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { UpdateUserRoleClaimModel } from "../models/update-user-role-claims.model";
import { UserRoleModel } from "../models/user-role.model";

@Injectable({
    providedIn: 'root'
})
export class UserRoleService extends BaseService {

    constructor() {
        super();
    }

    create(userRoleModel: UserRoleModel): Observable<ResponseResult<UserRoleModel>> {
        return this.post<ResponseResult<UserRoleModel>>('security/roles', userRoleModel);
    }

    getAll(searchRequestModel: SearchRequestModel): Observable<ResponseResult<UserRoleModel[]>> {
        return this.get<ResponseResult<UserRoleModel[]>>(`security/roles?searchQuery=${searchRequestModel.searchTerm}`);
    }

    getById(roleId: string): Observable<ResponseResult<UserRoleModel>> {
        return this.get<ResponseResult<UserRoleModel>>(`security/roles/${roleId}/permission-templates`);
    }

    updateRole(roleId: string, updateUserRole: UpdateUserRoleClaimModel) {
        return this.put(`security/roles/${roleId}`, updateUserRole);
    }

    deleteRole(roleId: string) {
        return this.delete(`security/roles/${roleId}`);
    }
}
