import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResponseResult } from "../../../core/models/response-result.model";
import { SearchRequestModel } from "../../../core/models/search-request.model";
import { BaseService } from "../../../core/services/base.service";
import { ClientTypeModel } from "../models/client-type.model";

@Injectable({
    providedIn: 'root'
})
export class ClientTypeService extends BaseService {

    constructor() {
        super();
    }

    create(clientTypeModel: ClientTypeModel): Observable<ResponseResult<ClientTypeModel>> {
        return this.post<ResponseResult<ClientTypeModel>>('settings/client-types', clientTypeModel);
    }

    getAll(searchModel: SearchRequestModel): Observable<ResponseResult<ClientTypeModel[]>> {
        return this.get<ResponseResult<ClientTypeModel[]>>(`settings/client-types?pageSize=${searchModel.pageSize}&pageNumber=${searchModel.pageNumber}&searchQuery=${searchModel.searchTerm}`);
    }

    getById(id: string): Observable<ResponseResult<ClientTypeModel>> {
        return this.get<ResponseResult<ClientTypeModel>>(`settings/client-types/${id}`);
    }

    update(clientTypeModel: ClientTypeModel) {
        return this.put(`settings/client-types/${clientTypeModel.id}`, clientTypeModel)
    }

    deleteClientType(id: string): Observable<object> {
        return this.delete(`settings/client-types/${id}`);
    }
}
