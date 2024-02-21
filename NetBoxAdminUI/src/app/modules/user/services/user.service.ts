import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { UserModel } from "../models/user.model";

@Injectable({
    providedIn: 'root'
})
export class UserService extends BaseService {

    constructor() {
        super();
    }

    create(userModel: UserModel): Observable<ResponseResult<UserModel>> {
        return this.post<ResponseResult<UserModel>>('security/users', userModel);
    }

    getAll(searchModel: SearchRequestModel): Observable<ResponseResult<UserModel[]>> {
        return this.get<ResponseResult<UserModel[]>>(`security/users?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    getById(id: string): Observable<ResponseResult<UserModel>> {
        return this.get<ResponseResult<UserModel>>(`security/users/${id}`);
    }

    update(userModel: UserModel) {
        return this.put(`security/users/${userModel.id}`, userModel)
    }

    deleteUser(id: string): Observable<object> {
        return this.delete(`security/users/${id}`);
    }
}
